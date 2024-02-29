using CoolScaryGame;
using CoolScaryGame.Particles;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Portable : RigidBody
    {
        public float StunableTimer;
        public AnimationData IdleAnim = new AnimationData(20, 6, 0.1f);
        public AnimationData BreakAnim = new AnimationData(0, 20, 2);
        float animspeed = 1;

        float AnimationTimer = 0;
        public Portable(float x = 0, float y = 0) : base(64 ,32, new Core.Vector2(0, -100))
        {
            CollisionLayers = 0b11;
            renderer = new AnimationSprite("Animations/BarrelAnimations.png", 5, 6, -1, false, false);
            renderer.CenterOrigin();
            renderer.y = -renderer.height / 5;
            renderer.x = width / 2;
            proxy.AddChild(renderer);
            SetXY(x, y);
            AnimationSprite a = (AnimationSprite)renderer;
            a.SetCycle(IdleAnim.StartFrame, IdleAnim.FrameCount);
            animspeed = IdleAnim.Speed;
        }

        void Update()
        {
            AnimationTimer += Time.deltaTime;
            if(AnimationTimer > 0)
            {
                AnimationTimer -= 0.1f / animspeed;
                AnimationSprite a = (AnimationSprite)renderer;
                if (a.currentFrame == 19)
                {
                    LateDestroy();
                    return;
                }
                a.NextFrame();
            }

            StunableTimer -= Time.deltaTime;
            PhysicsUpdate();
        }

        public void DestroySelf()
        {
            AnimationSprite a = (AnimationSprite)renderer;
            a.SetCycle(BreakAnim.StartFrame, BreakAnim.FrameCount);
            animspeed = BreakAnim.Speed;
            isDissabled = true;
        }

        public void Drop(Vector2 Position, Vector2 Velocity)
        {
            position = position + Velocity.Normalized * 10;
            this.Velocity = Velocity + Velocity.Normalized * 600;
            StunableTimer = 1;
            isDissabled = false;
            isKinematic = false;
        }

        protected override void OnDestroy()
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                SpawnPosition = renderer.TransformPoint(renderer.width / 2, renderer.height / 2),
                ForceDirection = Velocity / 500,
                burst = 30,
                LifeTime = 1,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.4f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 1f,
                G = .75f,
                B = 0.75f,
            };

            SceneManager.AddParticles(dat);
        }
    }
}
