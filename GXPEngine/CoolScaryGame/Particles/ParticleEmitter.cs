using GXPEngine;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame.Particles
{
    internal class ParticleEmitter : GameObject
    {
        ParticleData data;

        float timer;
        float timeAlive;
        public ParticleEmitter(ParticleData d)
        {
            RenderLayer = 0;
            data = d;
            RenderLayer = d.RenderLayer;
            position = d.SpawnPosition;

            for (int i = 0; i < d.burst; i++)
            {
                AddChild(new Particle(data));
            }
        }

        void Update()
        {
            if (data.EmissionStep == 0 || data.EmissionTime == 0)
            { 
                LateDestroy(); 
                return; 
            }

            if(timeAlive > data.EmissionTime)
            {
                if (timeAlive > data.EmissionTime + data.LifeTime * (1 + data.LifetimeRandomness))
                    LateDestroy();
                return;
            }

            timer += Time.deltaTime;
            if(timer > data.EmissionStep)
            {
                timer -= data.EmissionStep;
                AddChild(new Particle(data));
            }
        }
    }
}
