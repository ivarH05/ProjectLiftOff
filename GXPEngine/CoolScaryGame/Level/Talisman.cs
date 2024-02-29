using CoolScaryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.CoolScaryGame.Level
{
    public class Talisman : AnimationSprite
    {
        float timer = 0;
        bool grabbed = false;
        public Talisman(float x, float y) : base("Animations/TalismanAnimations.png", 5, 6, 30, false, true, 0, 0b100)
        {
            SetXY(x, y);
            depthSort = true;
            SetCycle(0, 10);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if(timer > 0.1f)
            {
                timer -= 0.1f;
                NextFrame();
                if(currentFrame == 29)
                {
                    LateDestroy();
                }
            }
        }

        public void OnCollision(GameObject Other)
        {
            if (!(Other is Hider) || grabbed)
                return;
            SetCycle(10, 20);
            PlayerManager.AddTalisman();
            grabbed = true;
        }
    }
}
