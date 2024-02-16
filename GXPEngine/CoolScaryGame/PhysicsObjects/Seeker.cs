using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Seeker : Player
    {
        private static Seeker Singleton;
        public Seeker(Vector2 Position) : base(Position, "SeekerSpriteMap.png")
        {
            if (Singleton != null)
            {
                LateDestroy();
                return;
            }
            Singleton = this;

        }

        void Update()
        {
            //move using the arrow keys
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);
            //move the camera towards the player
            CamManager.LerpToPoint(1, TransformPoint(0, 0) + ActualVelocity * 0.5f, Time.deltaTime * 5);

            //update all physics
            PhysicsUpdate();
            //switch animation frames if necessary
            AnimationUpdate();
        }

        public static Vector2 GetPosition()
        {
            if (Singleton == null)
                return new Vector2();
            return Singleton.position;
        }
    }
}
