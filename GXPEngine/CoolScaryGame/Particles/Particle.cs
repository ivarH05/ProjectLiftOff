using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.CoolScaryGame.Particles;

namespace CoolScaryGame.Particles
{
    internal class Particle : AnimationSprite
    {
        internal Vector2 Velocity;
        internal float Friction = 0.1f;

        ParticleData data;

        internal float TimeAlive = 0;
        private float lifeTime = 1;
        public Particle(ParticleData dat) : base(dat.sprite, dat.cols, dat.rows, -1, false, false)//base(dat.sprite, false, false)///
        {
            RenderLayer = dat.RenderLayer;
            depth = dat.Depth;
            data = dat;

            SetColor(dat.R, dat.G, dat.B);
            alpha = dat.A;

            scale = Randomize(dat.Scale, dat.ScaleRandomness);
            CenterOrigin();
            rotation = Utils.Random(0, 360);

            lifeTime = Randomize(dat.LifeTime, dat.LifetimeRandomness);

            Friction = Randomize(dat.Friction, dat.FrictionRandomness);

            if (data.TrackObject != null)
                position = data.TrackObject.TransformPoint(0, 0) + data.SpawnPosition;
            else
                position = data.SpawnPosition;
            position += Vector2.RandomVector(dat.SpawnRadius);

            Velocity = dat.ForceDirection + Vector2.RandomVector(dat.ForceRandomness) * Utils.Random(-dat.ForceRandomness, dat.ForceRandomness);

            SetFrame(Utils.Random(0, frameCount));
        }

        void Update()
        {
            position += Velocity * Time.deltaMillis;
            AddFriction(Friction);
            scale *= data.ScaleOverLifetime;

            TimeAlive += Time.deltaTime;
            if (TimeAlive > lifeTime)
                LateDestroy();
        }

        float Randomize(float value, float factor)
        {
            return value * Utils.Random(1 - factor, 1 + factor);
        }
        Vector2 Randomize(Vector2 value, float factor)
        {
            return value * Utils.Random(1 - factor, 1 + factor);
        }

        /// <summary>
        /// add force to the velocity
        /// </summary>
        /// <param name="Force"></param>
        public void AddForce(Vector2 Force)
        {
            Velocity += Force;
        }

        internal void AddFriction(float amount)
        {
            Velocity = Velocity.Lerp(new Vector2(), amount);
        }
    }
}
