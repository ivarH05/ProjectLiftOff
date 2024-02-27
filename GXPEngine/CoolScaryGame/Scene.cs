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
            viewLeft.scale = 0.65f;
            viewRight.scale = 0.65f;
            AddChild(viewLeft);
            AddChild(viewRight);
            //AddChild(new Sprite("Checkers.png", false, false));
            //AddChild(new Hider(new Vector2(-100, 0)));
            //AddChild(new Seeker(new Vector2(100, 0)));
            //AddChild(new Portable(-100, 100));
            //AddChild(new Portable(0, 100));
            //AddChild(new Portable(100, 100));

            //I LOVE UNDERLIME SLOPPER

            LevelManager.BuildLevelByIndex(this, 1);
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
            UIManager.BuildMinimaps();
        }
    }
}
