using GXPEngine.Core;
using GXPEngine;
using System;
using GXPEngine.CoolScaryGame.Particles;
using CoolScaryGame.Particles;

namespace CoolScaryGame
{
    public class Scene : GameObject
    {
        private Camera viewLeft;
        private Camera viewRight;

        public Scene()
        {
            viewLeft = new Camera(0, 0, 960, 1080, 0, false);
            viewRight = new Camera(960, 0, 960, 1080, 1, false);
            AddChild(viewLeft);
            AddChild(viewRight);
            AddChild(new Sprite("Checkers.png", false, false));
            AddChild(new Hider(new Vector2(-100, 0)));
            AddChild(new Seeker(new Vector2(100, 0)));

            ParticleData dat = new ParticleData()
            {
                sprite = "circle.png",
                SpawnPosition = new Vector2(100, 0),
                burst = 100,
                EmissionStep = 0.1f,
                EmissionTime = 9999,
                Scale = 0.25f, ScaleRandomness = 0.5f,
                R = 1, G = 0, B = 0, A = 1
            };

            AddChild(new ParticleEmitter(dat));
        }

        void Update()
        {
        }

        public Camera[] GetCameras()
        {
            return new Camera[] { viewLeft, viewRight };
        }
    }
}
