using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using GXPEngine.CoolScaryGame.Particles;
using CoolScaryGame.Particles;

namespace CoolScaryGame
{
    public class Player : RigidBody
    {
        internal float speed = 4;
        internal int playerIndex;
        public Vector2 ActualVelocity;

        internal float animationSpeed = 15;

        internal float stunTimer;

        float timer;
        int State = 0;

        internal AnimationData idleAnim;
        internal AnimationData walkAnim;
        internal ParticleData WalkParticles = new ParticleData();
        public Player(Vector2 Position, int playerIndex, string AnimationSprite, int rows, int columns, AnimationData idleAnim, AnimationData walkAnim) : base(48, 32, Position, true)
        {
            this.idleAnim = idleAnim;
            this.walkAnim = walkAnim;

            this.playerIndex = playerIndex;
            renderer = new FOVAnimationSprite(AnimationSprite, rows, columns, idleAnim.FrameCount, 300, false, false);
            SetupRenderer();
            SetupWalkParticles();
        }

        void SetupRenderer()
        {
            renderer.depthSort = true;

            proxy.AddChild(renderer);
            renderer.width = 128;
            renderer.height = 128;
            renderer.CenterOrigin();
            renderer.y = -48;
            renderer.x = width / 2;
            SetAnimation(idleAnim);
        }

        void SetupWalkParticles()
        {
            WalkParticles = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                SpawnPosition = new Vector2(0, 32),
                ForceRandomness = 0.75f,
                burst = 0,
                LifeTime = 2,
                Friction = 0.05f,
                EmissionStep = .05f,
                EmissionTime = 999999,
                Scale = 0.25f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                RenderLayer = playerIndex,
                R = 0.9f,
                G = 0.95f,
                B = 1f,
            };
            SceneManager.AddParticles(WalkParticles);
        }

        internal void PlayerUpdates(int playerIndex)
        {
            WalkParticles.EmissionStep = 0.5f / Mathf.Max(ActualVelocity.Magnitude, 1f);
            WalkParticles.ForceDirection = -ActualVelocity / 15;
            WalkParticles.Depth = TrueDepth() - 0.1f;
            stunTimer -= Time.deltaTime;
            Vector2 LastPos = TransformPoint(0, 0);

            //move the camera towards the player
            CamManager.LerpToPoint(playerIndex, TransformPoint(0, 0) + ActualVelocity * 15, Time.deltaTime * 6);

            PhysicsUpdate();

            //switch animation frames if necessary
            AnimationUpdate();

            ActualVelocity = (TransformPoint(0, 0) - LastPos);
        }

        public void Stun(float time, bool AddParticles = true)
        {
            stunTimer = time;
            if (!AddParticles)
                return;

            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                burst = 0,
                LifeTime = 2,
                Friction = 0.05f,
                EmissionStep = .05f,
                EmissionTime = time,
                Scale = 0.5f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 0.75f,
                G = 0.85f,
                B = 1f,
            };
            SceneManager.AddParticles(dat);
        }

        /// <summary>
        /// update the player animations
        /// </summary>
        internal void AnimationUpdate()
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;

            if(Velocity.Magnitude > 200)
                rend.Mirror(Velocity.x < 0, false);

            animationSpeed = ActualVelocity.Magnitude / 1.25f + 6;

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
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;
            rend.SetCycle(dat.StartFrame, dat.FrameCount);
        }
    }
}
