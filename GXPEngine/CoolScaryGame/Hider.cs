using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Hider : RigidBody
    {
        float speed = 5;
        Sprite renderer;
        public Hider(Vector2 position) : base(50,50, position, true) 
        {
            renderer = new Sprite("triangle.png", false, false);
            AddChild(renderer);
            renderer.width = (int)(width/scaleX);
            renderer.height = (int)(height/scaleY);
        }

        void Update()
        {
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);

            PhysicsUpdate();
            CamManager.LerpToPoint(0, position + Velocity * 0.2f, Time.deltaTime * 5);
        }
    }
}
