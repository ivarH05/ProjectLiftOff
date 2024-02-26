using CoolScaryGame;
using CoolScaryGame.Particles;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Portable : RigidBody
    {
        public Portable(float x, float y) : base(64 ,32, new Core.Vector2(0, -100))
        {
            CollisionLayers = 0b11;
            renderer = new Sprite("square.png", false, false);
            renderer.y = -renderer.height / 2;
            proxy.AddChild(renderer);
            SetXY(x, y);
        }

        void Update()
        {
            PhysicsUpdate();
        }

        protected override void OnDestroy()
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                SpawnPosition = renderer.TransformPoint(renderer.width / 2, renderer.height / 2),
                ForceDirection = Velocity,
                burst = 30,
                LifeTime = 1,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 1f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 0.4f,
                G = .4f,
                B = 1f,
            };

            SceneManager.AddParticles(dat);
        }
    }
}
