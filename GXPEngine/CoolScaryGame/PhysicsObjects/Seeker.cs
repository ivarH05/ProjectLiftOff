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
            base(Position, new AnimationData("Animations/SeekerIdleAnim.png", 5, 2), new AnimationData("Animations/SeekerMovementAnim.png", 4, 3))
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
