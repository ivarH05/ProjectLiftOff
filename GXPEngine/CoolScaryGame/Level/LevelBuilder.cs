﻿using System;
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
        public LevelBuilder(string LevelTMX, GameObject levelHolder, GameObject objectHolder, float roomWidth, float roomHeight, float rescaleMapX = 1, float rescaleMapY = 1)
        {
            Pivot build = new Pivot();
            Pivot objects = new Pivot();
            TiledLoader loader = new TiledLoader(LevelTMX, build, false);
            loader.LoadTileLayers();

            //assuming the Level's tmx has 10x10 pixel tiles. if this isnt the case. FIX THAT
            Vector2 positionScale = .1f * new Vector2(roomWidth * rescaleMapX, roomHeight * rescaleMapY);
            foreach (AnimationSprite obj in build.GetChildren(false))
            {
                int roomName = obj.currentFrame;
                Room r = new Room("Rooms/Roomset1/Room" + roomName + ".tmx", ((int)obj.rotation/90)*90, roomHeight);
                levelHolder.AddChild(r);
                r.position = obj.position*positionScale;
                r.SetScaleXY(rescaleMapX, rescaleMapY);
            }

            loader.rootObject = objects;
            loader.LoadObjectGroups();
            List<Vector2> HiderPositions = new List<Vector2>();
            List<Vector2> SeekerPositions = new List<Vector2>();
            foreach (AnimationSprite obj in objects.GetChildren(false))
            {
                Vector2 pos = obj.position * positionScale;
                switch (obj.currentFrame)
                {
                    default: Console.WriteLine("Whoops! you have to put the CD in your computer"); break;
                    case 0:
                        GameObject toAdd = new Portable(pos.x, pos.y);
                        objectHolder.AddChild(toAdd);
                        break;
                    case 1:
                        SeekerPositions.Add(pos);
                        break;
                    case 2:
                        HiderPositions.Add(pos);
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
            Seeker seeker = new Seeker(SeekerPositions[(int)Utils.Random(0, SeekerPositions.Count)]);

            objectHolder.AddChild(hider);
            objectHolder.AddChild(seeker);

            build.LateDestroy();
        }
    }
}