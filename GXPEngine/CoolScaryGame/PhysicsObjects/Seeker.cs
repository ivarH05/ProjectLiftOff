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
        private static Seeker Singleton;
        public Seeker(Vector2 Position) :
            base(Position, new AnimationData("SeekerSpriteMap.png", 3, 3), new AnimationData("SeekerMovementMap.png", 3, 4))
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
            PlayerUpdates(1);
        }

        public static Vector2 GetPosition()
        {
            if (Singleton == null)
                return new Vector2();
            return Singleton.position;
        }
    }
}
