using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    public class Seeker : Player
    {
        public Seeker(Vector2 Position) :
            base(Position, "Animations/SeekerAnimations.png", 4, 6, new AnimationData(12, 10), new AnimationData(0, 12))
        {

        }

        void Update()
        {
            //move using the arrow keys
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);
            PlayerUpdates(1);
        }

        public void OnCollision(GameObject Other)
        {
            if (Other is Portable)
            {
                Stun(0.5f);
                Other.LateDestroy();
            }
        }
    }
}
