using System;
using System.Collections;
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
    /// A room, built using TiledLoader. 
    /// </summary>
    public class Room : GameObject
    {
        private static Hashtable LoaderCache = new Hashtable();
        Pivot tiles = new Pivot();
        Pivot objects = new Pivot();
        SpriteContainer roomContainer;
        public Room(string TMX, float rotation, float roomHeight)
        {
            this.rotation = rotation;
            Vector2 rotated = new Vector2(0,roomHeight*.5f).Rotate(rotation);
            TiledLoader build = getLoader(TMX);
            build.rootObject = tiles;
            build.addColliders = false;
            build.autoInstance = false;
            build.LoadTileLayers();
            build.rootObject = objects;
            build.addColliders = true;
            build.autoInstance = true;
            build.LoadObjectGroups();

            int walltype = Utils.Random(0, 2);
            foreach (GameObject obj in objects.GetChildren())
            {
                if (obj is SpriteContainer)
                {
                    roomContainer = (SpriteContainer)obj;
                    roomContainer.Remove();
                    roomContainer.AddProxy();
                    roomContainer.SetOrigin(0, 0);
                    AddChild(roomContainer);
                    roomContainer.proxy.AddChild(tiles);
                    roomContainer.proxy.AddChild(objects);
                    tiles.x = -roomContainer.x + roomContainer.width * .5f;
                    objects.x = -roomContainer.x + roomContainer.width * .5f;
                    tiles.y = -roomContainer.y + roomContainer.height * .5f;
                    objects.y = -roomContainer.y + roomContainer.height * .5f;
                    roomContainer.position = -.5f*(new Vector2(roomContainer.width,roomContainer.height));
                }
                if (obj is InvisibleObject)
                {
                    if (obj is WallSprite)
                    {
                        WallSprite cj = (WallSprite)obj;
                        cj.Setup(rotation, walltype);
                        if((cj.position-.5f*new Vector2(roomHeight, roomHeight)).Rotate(rotation* 0.0174532925f).y > 0) cj.removeWallIfHorizontal();
                    }
                    else ((InvisibleObject)obj).Setup();
                }
            }
            tiles.depth = 99;
        }
        TiledLoader getLoader(string TMX)
        {
            TiledLoader res = LoaderCache[TMX] as TiledLoader;
            if(res == null)
            {
                res = new TiledLoader(TMX, null, false);
                LoaderCache[TMX] = res;
            }
            return res;
        }
    }
}
