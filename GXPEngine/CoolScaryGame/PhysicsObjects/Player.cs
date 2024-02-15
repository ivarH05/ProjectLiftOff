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

        float animationSpeed = 5;
        float timer;
        public Player(Vector2 Position, string SpritePath) : base(50, 50, Position, true)
        {
            renderer = new AnimationSprite(SpritePath, 3, 3, -1, false, false);
            AddChild(renderer);
            renderer.width = (int)(width / scaleX);
            renderer.height = (int)(height / scaleY) * 2;
            renderer.y = -(int)(height / scaleY);
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
