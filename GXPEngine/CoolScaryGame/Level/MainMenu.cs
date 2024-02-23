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
        public MainMenu() : base("UI/MainMenu.png", 1, 2)
        {
            scale = scale * 2;
            SetCycle(0, 2, 100);
        }

        void Update()
        {
            if (Input.AnyKeyDown())
            {
                SceneManager.LoadScene(false);
                LateDestroy();
            }
            Animate(Time.deltaMillis / 5);
        }
    }
}
