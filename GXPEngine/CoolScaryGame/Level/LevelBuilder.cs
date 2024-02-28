using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.CoolScaryGame.Level;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{
    /// <summary>
    /// Builds a level using TiledLoader. 
    /// </summary>
    public class LevelBuilder
    {

        public LevelBuilder(string LevelTMX, GameObject levelHolder, GameObject objectHolder, GameObject miniMap, float roomWidth, float roomHeight, float rescaleMapX = 1, float rescaleMapY = 1)
        {

            Pivot objects = new Pivot();
            TiledLoader loader = new TiledLoader(LevelTMX, miniMap, false);
            loader.LoadTileLayers();

            Minimap.BuildMinimap(miniMap, 640, 640);
            Room.buildDoorConnections();

            //assuming the Level's tmx has 10x10 pixel tiles. if this isnt the case, FIX THAT
            Vector2 positionScale = .1f * new Vector2(roomWidth * rescaleMapX, roomHeight * rescaleMapY);

            Vector2i dims = Minimap.roomsDimensions;
            for (int y = 0; y<dims.y; y++)
            {
                for(int x = 0; x<dims.x; x++)
                {
                    uint doors = 0;
                    doors |= (Room.getDoorConnections(Minimap.GetRoom(x - 1, y)) & 0b0100) >> 2; //room left  - door 0
                    doors |= (Room.getDoorConnections(Minimap.GetRoom(x, y + 1)) & 0b1000) >> 2; //room below - door 1
                    doors |= (Room.getDoorConnections(Minimap.GetRoom(x + 1, y)) & 0b0001) << 2; //room right - door 2
                    doors |= (Room.getDoorConnections(Minimap.GetRoom(x, y - 1)) & 0b0010) << 2; //room above - door 3

                    Vector2i room = Minimap.GetRoom(x, y);
                    Room r = new Room(Room.RoomName + room.x + ".tmx", room.y, roomHeight, doors);
                    levelHolder.AddChild(r);
                    r.position = Minimap.GetGlobalPosFromRoomIndex(new Vector2i(x, y));
                    r.SetScaleXY(rescaleMapX, rescaleMapY);

                    Console.Write(Convert.ToString(Room.getDoorConnections(room), 2).PadLeft(4, '0')+" ");
                }
                Console.WriteLine();
            }
            /*
            foreach (AnimationSprite obj in miniMap.GetChildren(false))
            {
                int roomName = obj.currentFrame;
                Room r = new Room("Rooms/Roomset1/Room" + roomName + ".tmx", ((int)obj.rotation/90)*90, roomHeight);
                levelHolder.AddChild(r);
                r.position = obj.position*positionScale;
                r.SetScaleXY(rescaleMapX, rescaleMapY);
            }*/

            loader.rootObject = objects;
            loader.LoadObjectGroups();
            List<Vector2> HiderPositions = new List<Vector2>();
            List<Vector2> SeekerPositions = new List<Vector2>();
            foreach (AnimationSprite obj in objects.GetChildren(false))
            {
                Vector2 pos = obj.position * positionScale;
                GameObject toAdd;
                switch (obj.currentFrame)
                {
                    default: Console.WriteLine("Whoops! you have to put the object in your levelbuilder"); break;
                    case 0:
                        toAdd = new Portable(pos.x, pos.y);
                        objectHolder.AddChild(toAdd);
                        break;
                    case 1:
                        SeekerPositions.Add(pos);
                        break;
                    case 2:
                        HiderPositions.Add(pos);
                        break;
                    case 3:
                        toAdd = new Talisman(pos.x, pos.y);
                        objectHolder.AddChild(toAdd);
                        break;
                }
            }
            int ra = Utils.Random(0, HiderPositions.Count);
            Console.WriteLine(ra);

            //IF THERES AN ERROR HERE
            //THAT MEANS YOU FORGOT TO PUT IN SPAWNPOINTS
            //FOR EITHER PLAYER

            //READ ABOVE ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IF YOU DONT ILL MURDER YOU. IN REAL LIFE. IM GONNA LOOK FOR YOU & KILL YOU AND IT WILL HURT. A LOT
            Hider hider = new Hider(HiderPositions[ra]);
            Seeker seeker = new Seeker(SeekerPositions[Utils.Random(0, SeekerPositions.Count)]);

            objectHolder.AddChild(hider);
            objectHolder.AddChild(seeker);
        }
    }
}
