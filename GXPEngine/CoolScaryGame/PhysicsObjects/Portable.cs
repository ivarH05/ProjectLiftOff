using CoolScaryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Portable : RigidBody
    {
        public Portable(float x, float y) : base(64 ,32, new Core.Vector2(0, -100))
        {
            CollisionLayers = 0b11;
            renderer = new Sprite("square.png", false, false);
            renderer.y = -32;
            proxy.AddChild(renderer);
            //renderer.width = (int)(width / scaleX);
            //renderer.height = (int)(height / scaleY) * 2;
            SetXY(x, y);
        }

        void Update()
        {
            PhysicsUpdate();
        }
    }
}
