using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace CoolScaryGame
{
    public static class LevelManager
    {
        static Pivot levelContainer = new Pivot();
        static Pivot objectContainer = new Pivot();
        static Pivot minimapHolder = new Pivot();

        public static void BuildLevelByIndex(GameObject parent, int levelIndex)
        {
            parent.AddChild(levelContainer);
            parent.AddChild(objectContainer);
            new LevelBuilder("Rooms/Map" + levelIndex + ".tmx", levelContainer, objectContainer, minimapHolder, 640, 640, 1, 1);
            Minimap.tiledMinimap = minimapHolder;
        }

        public static void RemoveCurrentLevel()
        {
            levelContainer.LateDestroy();
            objectContainer.LateDestroy();
            minimapHolder.LateDestroy();
            levelContainer = new Pivot();
            objectContainer = new Pivot();
            minimapHolder = new Pivot();
        }
    }
}
