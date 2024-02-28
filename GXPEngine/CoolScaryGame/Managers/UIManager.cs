using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public static class UIManager
    {
        private static List<GameObject> UI = new List<GameObject>();
        private static HealthBar[] HiderHealthBars;
        private static Minimap[] Minimaps;
        public static void AddHiderHealthbar()
        {
            HealthBar inquestion = new HealthBar("UI/healthOverlay.png", new Vector2(27, 13), new Vector2i(142, 25), 0, 100, 100);
            HealthBar inquestion2 = new HealthBar("UI/healthOverlay.png", new Vector2(27, 13), new Vector2i(142, 25), 0, 100, 100);
            HiderHealthBars = new HealthBar[] { inquestion, inquestion2 };
            inquestion.RenderLayer = 0;
            inquestion2.RenderLayer = 1;
            inquestion.y = Game.main.height / 2;
            inquestion2.y = Game.main.height / 2;
            CamManager.AddUI(inquestion, 0);
            CamManager.AddUI(inquestion2, 1);
            UI.Add(inquestion);
            UI.Add(inquestion2);
        }
        public static void BuildMinimaps()
        {
            Minimaps = new Minimap[] { new Minimap(), new Minimap() };
            for(int i = 0; i < Minimaps.Length; i++)
            {
                Minimap map = Minimaps[i];
                map.RenderLayer = i;
                map.SetScaleXY(300/map.width, 300/map.width);
                //offset by .01 to fix floating point errors
                map.y = -Game.main.height / 2 + .01f;
                map.x = (i == 0 ? Game.main.width/-4 : Game.main.width/4 - map.width) + .01f;
                map.depth = -98;
                CamManager.AddUI(map, i);
                UI.Add(map);
            }
        }
        public static void MarkMinimap(Vector2 position, int index, uint color)
        {
            Minimaps[index].markPosition(position, color);
        }
        public static void UpdateHiderHealth(float health)
        {
            foreach(HealthBar bar in  HiderHealthBars)
                bar.Health = health;
        }
        public static void WipeUI()
        {
            foreach (GameObject go in UI)
            {
                go.LateDestroy();
            }
            UI.Clear();
            HiderHealthBars = null;
            Minimaps = null;
        }
    }
}
