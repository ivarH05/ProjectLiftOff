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
            RenderLayer = 0;
            renderer = new Sprite("triangle.png", false, false);
            AddChild(renderer);
            renderer.width = (int)(width/scaleX);
            renderer.height = (int)(height/scaleY);
        }

        void Update()
        {
            //move using wasd
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);

            //update all physics
            PhysicsUpdate();

            //move the camera towards the player
            CamManager.LerpToPoint(0, position + ActualVelocity * 0.5f, Time.deltaTime * 5);
        }
    }
}
