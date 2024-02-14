using GXPEngine.Core;
using GXPEngine;
using System;

namespace CoolScaryGame
{
    internal class Seeker : RigidBody
    {
        float speed = 5;
        public Seeker(Vector2 Position) : base("square.png", Position, true)
        {
            RenderLayer = 1;
        }

        void Update()
        {
            //move using the arrow keys
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);

            //update all physics
            PhysicsUpdate();

            //move the camera towards the player
            CamManager.LerpToPoint(1, position + ActualVelocity * 0.5f, Time.deltaTime * 5);
        }
    }
}
