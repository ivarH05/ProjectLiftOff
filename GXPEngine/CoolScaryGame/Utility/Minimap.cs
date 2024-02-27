using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class Minimap : GameObject
    {

        static Pivot _tiledMinimap;
        public static Pivot tiledMinimap
        {
            set { _tiledMinimap = value; }
        }

        //stored as the index of the room (x), and its rotation (0,  90, 180, 270) (y)
        static Vector2i[,] rooms;
        static Vector2i lowest = new Vector2i(int.MaxValue, int.MaxValue);
        static Vector2i highest = new Vector2i(int.MinValue, int.MinValue);
        static Vector2i mapDimensions;
        static Vector2 roomSize;

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
        public static void BuildMinimaps(int roomWidth, int roomHeight)
        {
            roomSize = new Vector2(roomWidth, roomHeight);
            foreach (AnimationSprite obj in _tiledMinimap.GetChildren(false))
            {
                Vector2 pos = obj.position * .1f;
                lowest.x = (int)Mathf.Min(lowest.x, pos.x);
                lowest.y = (int)Mathf.Min(lowest.y, pos.y);
                highest.x = (int)Mathf.Max(highest.x, pos.x);
                highest.y = (int)Mathf.Max(highest.y, pos.y);
            }
            Console.WriteLine(lowest + " - " + highest);
            mapDimensions = new Vector2i(highest.x - lowest.x + 1, highest.y - lowest.y + 1);
            rooms = new Vector2i[mapDimensions.x, mapDimensions.y];
            foreach (AnimationSprite obj in _tiledMinimap.GetChildren(false))
            {
                Vector2i pos = GetRoomIndexFromTiledminimapPos(obj.position);
                rooms[pos.x, pos.y] = new Vector2i(obj.currentFrame, (int)obj.rotation);
            }

            for (int y = 0; y < mapDimensions.y; y++)
            {
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    Vector2i pos = rooms[x, y];
                    Console.Write("["+pos.x +","+pos.y/90+"]");
                }
                Console.WriteLine();
            }
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
        }
        public void markPosition(Vector2 position, uint color)
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
                    if (x == markedPosition.x && y == markedPosition.y)
                    { }
                    else minimapRenderers[x, y].color = 0xFFFFFF; 
                }
        }
    }
}
