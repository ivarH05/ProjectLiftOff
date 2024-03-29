using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{
    public static class UIManager
    {
        private static List<GameObject> UI = new List<GameObject>();
        private static HealthBar[] HiderHealthBars;
        private static Minimap[] Minimaps;
        private static ItemBox[] skillBoxes;
        private static EasyDraw[] Timers;

        public static void SetupTimer()
        {
            EasyDraw Timer0 = new EasyDraw(200, 100);
            Timer0.TextAlign(CenterMode.Center, CenterMode.Center);
            Timer0.SetXY(400, -500);
            Timer0.CenterOrigin();
            Timer0.depthSort = true;
            Timer0.depth = -98;
            Timer0.RenderLayer = 0;

            EasyDraw Timer1 = new EasyDraw(200, 100);
            Timer1.TextAlign(CenterMode.Center, CenterMode.Center);
            Timer1.SetXY(-430, -500);
            Timer1.CenterOrigin();
            Timer1.depthSort = true;
            Timer1.depth = -98;
            Timer1.RenderLayer = 1;


            CamManager.AddUI(Timer0, 0);
            CamManager.AddUI(Timer1, 1);
            UI.Add(Timer0);
            UI.Add(Timer1);

            Timers = new EasyDraw[]{ Timer0, Timer1};
        }

        public static void AddVisionCircles()
        {
            Sprite circle1 = new Sprite("UI/Vision.png", false, false);
            circle1.CenterOrigin();
            circle1.depthSort = true;
            circle1.depth = -90;
            circle1.RenderLayer = 1;
            circle1.scale = 1.8f;
            CamManager.AddUI(circle1, 1);
            UI.Add(circle1);


            Sprite circle0 = new Sprite("UI/Vision.png", false, false);
            circle0.CenterOrigin();
            circle0.depthSort = true;
            circle0.depth = -90;
            circle0.RenderLayer = 0;
            circle0.scale = 2.2f;
            CamManager.AddUI(circle0, 0);
            UI.Add(circle0);

        }

        public static void UpdateTimer(int talisman, float time)
        {
            Timers[0].ClearTransparent();
            Timers[1].ClearTransparent();
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            string text = minutes.ToString("00") + ":" + seconds.ToString("00");
            Timers[0].Text(talisman + " / 4   |   " + text);
            Timers[1].Text(text);
        }

        public static void WinLose(int Winner)
        {
            Sprite WinText = new Sprite("UI/YouWonText.png", false, false);
            Sprite LoseText = new Sprite("UI/YouLostText.png", false, false);
            WinText.depthSort = true;
            LoseText.depthSort = true;
            WinText.CenterOrigin();
            LoseText.CenterOrigin();
            WinText.depth = -98;
            LoseText.depth = -98;

            WinText.RenderLayer = Winner;
            LoseText.RenderLayer = 1 - Winner;

            CamManager.AddUI(WinText, Winner);
            CamManager.AddUI(LoseText, 1 - Winner);
            UI.Add(WinText);
            UI.Add(LoseText);
        }

        public static void AddHiderHealthbar()
        {
            int val = Hider.StarterHealth;
            HealthBar inquestion = new HealthBar("UI/healthOverlay.png", new Vector2(27, 13), new Vector2i(142, 15), 0, val, val);
            HealthBar inquestion2 = new HealthBar("UI/healthOverlay.png", new Vector2(27, 13), new Vector2i(142, 15), 0, val, val);
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
        public static void AddMinimaps()
        {
            Minimaps = new Minimap[] { new Minimap(), new Minimap() };
            for(int i = 0; i < Minimaps.Length; i++)
            {
                Minimap map = Minimaps[i];
                map.RenderLayer = i;
                float avg = .5f*(map.width + map.height);
                map.scale = 300/avg;
                //offset by .01 to fix floating point errors
                map.y = -Game.main.height / 2 + .01f;
                map.x = (i == 0 ? Game.main.width/-4 : Game.main.width/4 - map.width) + .01f;
                map.depth = -98;
                CamManager.AddUI(map, i);
                UI.Add(map);
            }
        }
        public static void AddSkillBoxes()
        {
            skillBoxes = new ItemBox[] { new ItemBox("UI/itemOverlay.png", "UI/items.png"), new ItemBox("UI/itemOverlay.png", "UI/items.png") };
            for(int i = 0; i < skillBoxes.Length; i++)
            {
                ItemBox box = skillBoxes[i];
                box.RenderLayer = i;
                box.scale = 2;
                box.y = Game.main.height / 2 - 128;
                box.x = (i == 0 ? Game.main.width / -4 : Game.main.width / 4 - 256) + .01f;
                box.depth = -97;
                CamManager.AddUI(box, i);
                UI.Add(box);
            }
        }
        public static void SetItems(int skill1, int skill2, int index)
        {
            skillBoxes[index].SetItems(skill1, skill2);
        }
        public static void MarkMinimap(Vector2 position, int index, uint color)
        {
            Minimaps[index].MarkPosition(position, color);
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
