using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Hider : Player
    {
        float speed = 5;

        public Hider(Vector2 Position) : base(Position, "HiderSpriteMap.png")
        {
            //RenderLayer = 0;
        }

        void Update()
        {
            //move using wasd
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);

            //update all physics
            PhysicsUpdate();

            AnimationUpdate();

            //move the camera towards the player
            CamManager.LerpToPoint(0, position + ActualVelocity * 0.5f, Time.deltaTime * 5);
        }
    }
}
