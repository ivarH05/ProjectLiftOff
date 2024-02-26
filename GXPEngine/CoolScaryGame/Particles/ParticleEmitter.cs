using GXPEngine;
using GXPEngine.CoolScaryGame.Particles;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace CoolScaryGame.Particles
{
    internal class ParticleEmitter : GameObject
    {
        ParticleData data;

        float timer;
        float timeAlive;
        public ParticleEmitter(ParticleData d)
        {
            data = d;
            //RenderLayer = d.RenderLayer;
            //position = d.SpawnPosition;

            for (int i = 0; i < d.burst; i++)
            {
                AddChild(new Particle(data));
            }
        }

        void Update()
        {
            timeAlive += Time.deltaTime;

            if(timeAlive >= data.EmissionTime || data.EmissionStep == 0)
            {
                if (timeAlive > data.EmissionTime + data.LifeTime * (1 + data.LifetimeRandomness))
                    LateDestroy();
                return;
            }

            timer += Time.deltaTime;
            while(timer > data.EmissionStep)
            {
                timer -= data.EmissionStep;
                AddChild(new Particle(data));
            }
        }
    }
}
