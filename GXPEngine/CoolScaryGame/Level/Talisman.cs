using CoolScaryGame;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.CoolScaryGame.Level
{
    public class Talisman : AnimationSprite
    {
        float timer = 0;
        bool grabbed = false;
        float progress = 0;
        float MaxProgress = 10;

        ParticleData Particles;
        public Talisman(float x, float y) : base("Animations/TalismanAnimations.png", 5, 6, 30, false, true, 0b10, 0b10)
        {
            SetXY(x, y);
            depthSort = true;
            SetCycle(0, 10);
            Particles = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = this,
                SpawnPosition = new Vector2(64, 64),
                ForceRandomness = 0.7f,
                burst = 0,
                LifeTime = 1,
                LifetimeRandomness = 0,
                Friction = 0.05f,
                EmissionStep = 9999999,
                EmissionTime = 999999,
                Scale = 0.2f,
                ScaleRandomness = 0.25f,
                ScaleOverLifetime = 0.999f,
                RenderLayer = -1,
                R = 0.75f,
                G = 1f,
                B = 0.5f,
            };
            SceneManager.AddParticles(Particles);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if(timer > 0.1f)
            {
                timer -= 0.1f;
                NextFrame();
                if(currentFrame == 29)
                {
                    LateDestroy();
                }
            }
        }

        public void AddProgress(float amount)
        {
            if (grabbed)
                return;

            progress += amount;
            float val = progress / MaxProgress;
            Particles.EmissionStep = (1.05f - Mathf.Clamp01(val)) / 2;
            Particles.R = 1 - val / 2;
            Particles.G = 0.5f + val / 2;
            SetColor(Particles.R, Particles.G, Particles.B);
            Console.WriteLine(Particles.EmissionStep);
            if(progress > MaxProgress)
            {
                SetCycle(10, 20);
                SoundManager.PlaySound(new Sound("Sound/Task.mp3"));
                PlayerManager.AddTalisman();
                grabbed = true;
            }
            Console.WriteLine(progress);
        }

        public void ResetProgress()
        {
            Particles.EmissionStep = 9999999;
            progress = 0;
        }

        protected override void OnDestroy()
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                SpawnPosition = TransformPoint(64, 64),
                burst = 50,
                LifeTime = 2,
                ForceRandomness = 0.65f,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.3f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = .75f,
                G = 1f,
                B = .75f,
            };

            SceneManager.AddParticles(dat);
        }
    }
}
