using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Player : RigidBody
    {
        float speed = 5;

        public Player(Vector2 Position, string SpritePath) : base(50, 50, Position, true)
        {
            renderer = new AnimationSprite(SpritePath, 6, 2);
            AddChild(renderer);
            renderer.width = (int)(width / scaleX);
            renderer.height = (int)(height / scaleY);
        }

        void Update()
        {
            if(renderer is AnimationSprite Anim)
            {
                    //im writing this right now,
            }
        }
    }
}
