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
        AnimationData AttackAnim = new AnimationData(22, 14);
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
            if(Input.GetKeyDown(Key.RIGHT_CTRL) && stunTimer < 0)
            {
                Console.WriteLine("pressed");
                GameObject[] objs = GetObjectsInFront();
                foreach(GameObject obj in objs)
                {
                    Console.WriteLine("looping");
                    if (obj is Portable)
                    {
                        Console.WriteLine("breaking");
                        obj.LateDestroy();
                    }
                }
                Stun(0.5f, false);
            }
            UIManager.MarkMinimap(position, 1, 0xFFA060);
        }
        public GameObject[] GetObjectsInFront()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(position);

            return s.GetCollisions();
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
