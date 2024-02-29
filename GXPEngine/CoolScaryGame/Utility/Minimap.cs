using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.CoolScaryGame.Level;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class Minimap : GameObject
    {
        //stored as the index of the room (rooms[].x), and its rotation (0,  90, 180, 270) (rooms[].y)
        static Vector2i[,] rooms;
        static Vector2i lowest = new Vector2i(int.MaxValue, int.MaxValue);
        static Vector2i highest = new Vector2i(int.MinValue, int.MinValue);
        static Vector2 roomSize;
        static Vector2i mapDimensions;

        public static MinimapSpriteData[] talismans; //cant be fucked to do this properly
        public static Vector2i roomsDimensions {get { return mapDimensions; } }

        AnimationSprite[,] minimapRenderers;
        private int _width;
        private int _height;

        private Vector2i markedPosition;
        public float width
        {
            get { return _width*scaleX; }
        }
        public float height
        {
            get { return _height*scaleY; }
        }
        public static void BuildMinimap(GameObject tiledMinimap, int roomWidth, int roomHeight)
        {
            roomSize = new Vector2(roomWidth, roomHeight);
            foreach (AnimationSprite obj in tiledMinimap.GetChildren(false))
            {
                Vector2 pos = obj.position * .1f;
                lowest.x = (int)Mathf.Min(lowest.x, pos.x);
                lowest.y = (int)Mathf.Min(lowest.y, pos.y);
                highest.x = (int)Mathf.Max(highest.x, pos.x);
                highest.y = (int)Mathf.Max(highest.y, pos.y);
            }
            //Console.WriteLine(lowest + " - " + highest);

            mapDimensions = new Vector2i(highest.x - lowest.x + 1, highest.y - lowest.y + 1);
            rooms = new Vector2i[mapDimensions.x, mapDimensions.y];
            foreach (AnimationSprite obj in tiledMinimap.GetChildren(false))
            {
                Vector2i pos = GetRoomIndexFromTiledminimapPos(obj.position);
                rooms[pos.x, pos.y] = new Vector2i(obj.currentFrame, (int)obj.rotation);
            }

            /*for (int y = 0; y < mapDimensions.y; y++)
            {
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    Vector2i pos = rooms[x, y];
                    Console.Write("["+pos.x +","+pos.y/90+"]");
                }
                Console.WriteLine();
            }*/
        }
        static Vector2i GetRoomIndexFromTiledminimapPos(Vector2 tiledMinimapPos)
        {
            Vector2 pos = tiledMinimapPos * .1f;
            return new Vector2i((int)pos.x - lowest.x, (int)pos.y - lowest.y);
        }

        static Vector2i GetRoomIndexFromGlobalPos(Vector2 position)
        {
            Vector2 TiledScale = (position / roomSize)*10;
            return GetRoomIndexFromTiledminimapPos(TiledScale);
        }

        public static Vector2 GetGlobalPosFromRoomIndex(Vector2i index)
        {
            index += lowest;
            return new Vector2(roomSize.x * index.x, roomSize.y * index.y) + .5f*roomSize;
        }

        public Minimap()
        {
            minimapRenderers = new AnimationSprite[mapDimensions.x, mapDimensions.y];
            //map tiles are hardcoded as 10x10 elsewhere anyways. why not make it worse
            _width = mapDimensions.x * 10;
            _height = mapDimensions.y * 10;
            for (int y = 0; y < mapDimensions.y; y++)
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    AnimationSprite obj = new AnimationSprite("Rooms/Textures/roomIcons.png", 4, 4, 16, true, false);
                    obj.CenterOrigin();
                    obj.currentFrame = rooms[x, y].x;
                    obj.rotation = rooms[x, y].y;
                    obj.position = new Vector2(obj.width * x + 5, obj.height * y + 5);
                    obj.position += new Vector2();
                    AddChild(obj);
                    minimapRenderers[x, y] = obj;
                }
            foreach(MinimapSpriteData data in talismans)
            {
                MinimapSprite slop = new MinimapSprite(data.realObject, data.color);
                slop.position = data.pos;
                slop.depth = -1.1f;
                AddChild(slop);
                Console.WriteLine(data.pos);
            }
        }
        public void MarkPosition(Vector2 position, uint color)
        {
           markedPosition = GetRoomIndexFromGlobalPos(position);
            if (markedPosition.x < 0 || markedPosition.y < 0 || markedPosition.x >= mapDimensions.x || markedPosition.y >= mapDimensions.y)
                return;
            minimapRenderers[markedPosition.x, markedPosition.y].color = color;
        }
        void Update()
        {
            for (int y = 0; y < mapDimensions.y; y++)
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    if (!(x == markedPosition.x && y == markedPosition.y)) 
                        minimapRenderers[x, y].color = 0xFFFFFF; 
                }
        }

        public static Vector2i GetRoom(Vector2i position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= mapDimensions.x || position.y >= mapDimensions.y)
                return new Vector2i();
            return rooms[position.x, position.y];
        }
        public static Vector2i GetRoom(int x, int y)
        {
            return GetRoom(new Vector2i(x, y));
        }
    }
}
