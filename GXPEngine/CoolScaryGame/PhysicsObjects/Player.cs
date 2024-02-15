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
        internal float speed = 5;

        internal float animationSpeed = 5;
        float timer;
        public Player(Vector2 Position, string SpritePath) : base(64, 64, Position, true)
        {
            renderer = new AnimationSprite(SpritePath, 3, 3, -1, false, false);
            proxy.AddChild(renderer);
            renderer.width = 64;
            renderer.height = 128;
            renderer.y = -96;
        }

        /// <summary>
        /// update the player animations
        /// </summary>
        internal void AnimationUpdate()
        {
            AnimationSprite rend = (AnimationSprite)renderer;
            rend.Mirror(Velocity.x > 0, false);

            timer += Time.deltaTime;
            if(timer > 1/animationSpeed)
            {
                timer -= 1/animationSpeed;
                int currentFrame = rend.currentFrame;

                //idle frames
                if (currentFrame >= 3 && Velocity.Magnitude < 100f)
                    rend.SetFrame(0);
                //walk frames
                if ((currentFrame < 3 || currentFrame >= 6) && Velocity.Magnitude > 100f)
                    rend.SetFrame(3);

                //cycle through 3 frames, seems the easiest wayright now. might bite me back later.
                rend.SetFrame(rend.currentFrame + (rend.currentFrame % 3 == 2 ? -2 : 1));
            }
        }
    }
}
