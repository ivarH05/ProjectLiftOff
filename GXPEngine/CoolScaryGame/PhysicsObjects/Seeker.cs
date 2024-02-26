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
            base(Position, 1, "Animations/SeekerAnimations.png", 4, 6, new AnimationData(12, 10), new AnimationData(0, 12))
        {
            PlayerManager.SetSeeker(this);
            ((FOVAnimationSprite)renderer).SetVisibility(440);
        }

        void Update()
        {
            //move using the arrow keys
            AddForce(stunTimer > 0 ? new Vector2() : (Input.ArrowVector() * Time.deltaMillis * speed));
            PlayerUpdates(1);
        }

        public void OnCollision(GameObject Other)
        {
            if (Other is Portable)
            {
                Stun(1);
                Other.LateDestroy();
            }
        }
    }
}
