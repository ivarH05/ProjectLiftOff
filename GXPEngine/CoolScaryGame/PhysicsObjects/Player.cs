using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CoolScaryGame
{
    public class Player : RigidBody
    {
        internal float speed = 5;
        public Vector2 ActualVelocity;

        internal float animationSpeed = 15;

        float stunTimer;

        float timer;
        int State = 0;

        internal AnimationData idleAnim;
        internal AnimationData walkAnim;
        public Player(Vector2 Position, AnimationData idleAnim, AnimationData walkAnim) : base(64, 64, Position, true)
        {
            this.idleAnim = idleAnim;
            this.walkAnim = walkAnim;
            renderer = new FOVAnimationSprite(idleAnim, -1, 300, true);
            SetAnimation(idleAnim);
        }
        internal void PlayerUpdates(int playerIndex)
        {
            Vector2 LastPos = TransformPoint(0, 0);

            //move the camera towards the player
            CamManager.LerpToPoint(playerIndex, TransformPoint(0, 0) + ActualVelocity * 20, Time.deltaTime * 5);

            //update all physics
            if (stunTimer > 0)
                stunTimer -= Time.deltaTime;
            else
                PhysicsUpdate();

            //switch animation frames if necessary
            AnimationUpdate();

            ActualVelocity = (TransformPoint(0, 0) - LastPos);
        }

        public void Stun(float time)
        {
            stunTimer = time;
            Velocity = new Vector2();
        }

        /// <summary>
        /// update the player animations
        /// </summary>
        internal void AnimationUpdate()
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;
            if(Velocity.Magnitude > 200)
                rend.Mirror(Velocity.x < 0, false);
            animationSpeed = Velocity.Magnitude / 100 + 5;

            timer += Time.deltaTime;
            if(timer > 1/animationSpeed)
            {
                timer -= 1/animationSpeed;
                if (State != 0 && Velocity.Magnitude < 100)
                {
                    State = 0;
                    SetAnimation(idleAnim);
                }
                if (State != 1 && Velocity.Magnitude > 100)
                {
                    State = 1;
                    SetAnimation(walkAnim);
                }
                rend.NextFrame();
            }
        }
        private void SetAnimation(AnimationData dat)
        {
            if(renderer != null)
                renderer.LateDestroy();

            renderer = new FOVAnimationSprite(dat, -1, 300, true);
            proxy.AddChild(renderer);
            renderer.width = 128;
            renderer.height = 128;
            renderer.y = -96;
            renderer.x = -32;
        }
    }
}
