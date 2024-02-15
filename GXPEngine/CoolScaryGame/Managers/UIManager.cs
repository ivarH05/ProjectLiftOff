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
        }
    }
}
