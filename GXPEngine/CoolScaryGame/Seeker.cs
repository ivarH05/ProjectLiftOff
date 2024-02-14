using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Seeker : RigidBody
    {
        float speed = 5;
        Sprite renderer;

        public Seeker(Vector2 Position) : base(50,50, Position, true) {
            RenderLayer = 1;
            renderer = new Sprite("square.png", false, false);
            AddChild(renderer);
            renderer.width = (int)(width / scaleX);
            renderer.height = (int)(height / scaleY);
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
