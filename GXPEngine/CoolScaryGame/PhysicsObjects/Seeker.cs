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
        float freeze = 0;
        public Seeker(Vector2 Position) :
            base(Position, 1, "Animations/SeekerAnimations.png", 4, 9, new AnimationData(12, 10), new AnimationData(0, 12))
        {
            PlayerManager.SetSeeker(this);
            ((FOVAnimationSprite)renderer).SetVisibility(340);
            speed = 3;
            playerColor = 0xFFA060;
        }

        void Update()
        {
            freeze -= Time.deltaTime;

            PlayerUpdates(1);
            //move using the arrow keys
            if (freeze < 0 && stunTimer < 0)
                AddForce(Input.ArrowVector() * Time.deltaMillis * (speed+speedBoost));

            if (Input.GetKeyDown(Key.RIGHT_CTRL) && stunTimer < 0)
                Attack();

            if (Input.GetKeyDown(Key.RIGHT_SHIFT))
                Exterminate();

            if(Input.GetKeyDown(Key.RIGHT_CTRL))
                useItem();
        }

        /// <summary>
        /// Attack nearby boxes
        /// </summary>
        void Attack()
        {
            GameObject[] objs = GetObjectsInFront();
            foreach (GameObject obj in objs)
            {
                if (obj is Portable)
                {
                    obj.LateDestroy();
                }
            }
            Stun(0.5f, false);
        }

        /// <summary>
        /// start extermination the ghost
        /// </summary>
        void Exterminate()
        {
            float dist = Vector2.Distance(position, PlayerManager.GetPosition(0));
            if (dist < 300)
            {
                PlayerManager.SlowPlayer(0, (300 / dist));
                freeze = 0.2f;
                PlayerManager.DamagePlayer(0, 1);
            }
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
