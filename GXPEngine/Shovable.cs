using CoolScaryGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Shovable : RigidBody
    {
        public Shovable() : base(64 ,64, new Core.Vector2(0, -100))
        {
            renderer = new Sprite("circle.png", false, false);
            AddChild(renderer);
            renderer.width = (int)(width / scaleX);
            renderer.height = (int)(height / scaleY);
        }

        void Update()
        {
            PhysicsUpdate();
        }
    }
}
