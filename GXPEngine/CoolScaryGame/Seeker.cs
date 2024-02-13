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
            renderer = new Sprite("square.png", false, false);
            AddChild(renderer);
            renderer.width = (int)(width / scaleX);
            renderer.height = (int)(height / scaleY);
        }

        void Update()
        {
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);

            PhysicsUpdate();
            CamManager.LerpToPoint(1, position + Velocity * 0.2f, Time.deltaTime * 5);
        }
    }
}
