using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{
    /// <summary>
    /// Builds a level using TiledLoader. 
    /// </summary>
    public class LevelBuilder
    {
        public LevelBuilder(string LevelTMX, Pivot levelHolder, int roomWidth, int roomHeight)
        {
            Pivot build = new Pivot();
            TiledLoader loader = new TiledLoader(LevelTMX, build, false);
            loader.LoadTileLayers();

            //assuming the Level's tmx has 10x10 pixel tiles. if this isnt the case. FIX THAT
            Vector2 positionScale = .1f * new Vector2(roomWidth, roomHeight);
            foreach (AnimationSprite obj in build.GetChildren(false))
            {
                int roomName = obj.currentFrame;
                Console.WriteLine(((int)obj.rotation / 90) * 90);
                Room r = new Room("Rooms/testroom" + roomName + ".tmx", ((int)obj.rotation/90)*90);
                levelHolder.AddChild(r);
                r.position = obj.position*positionScale;
            }

            build.LateDestroy();
        }
    }
}
