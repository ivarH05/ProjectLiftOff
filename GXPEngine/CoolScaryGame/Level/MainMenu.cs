using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    public class MainMenu : AnimationSprite
    {
        public MainMenu() : base("UI/MainMenu.png", 4, 4, 15)
        {
            scale = scale * 2;
            SetCycle(0, 15, 100);
        }

        void Update()
        {
            if (Input.AnyKeyDown())
            {
                SceneManager.LoadScene(false);
                LateDestroy();
            }
            Animate(Time.deltaMillis);
        }
    }
}
