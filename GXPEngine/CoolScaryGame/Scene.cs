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
                burst = 250,
                LifeTime = 1,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.25f, ScaleRandomness = 0.5f, ScaleOverLifetime = 0.95f,
                R = 1, G = 0, B = 0, A = 0.25f,
                RenderLayer = 0
            };

            AddChild(new ParticleEmitter(dat));
            AddChild(new Portable(-100, 100));
            AddChild(new Portable(0, 100));
            AddChild(new Portable(100, 100));

            SpriteContainer slop = new SpriteContainer(200, 200);
            slop.proxy.AddChild(new Sprite("Checkers.png", false, false));
            Sprite s = new WallSprite(50,10,true);
            slop.proxy.AddChild(s);
            s.x += 180;
            s.y += 50;
            AddChild(slop);

        }

        void Update()
        {
        }

        public Camera[] GetCameras()
        {
            return new Camera[] { viewLeft, viewRight };
        }

        public void AddUI()
        {
            UIManager.AddHiderHealthbar();
        }
    }
}
