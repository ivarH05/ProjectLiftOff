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
            SceneManager.SetScene(this);
            viewLeft = new Camera(0, 0, 960, 1080, 0, false);
            viewRight = new Camera(960, 0, 960, 1080, 1, false);
            AddChild(viewLeft);
            AddChild(viewRight);
            AddChild(new Sprite("Checkers.png", false, false));
            AddChild(new Hider(new Vector2(-100, 0)));
            AddChild(new Seeker(new Vector2(100, 0)));
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
            //I LOVE UNDERLIME SLOPPER

            Pivot levelContainer = new Pivot();
            AddChild(levelContainer);
            new LevelBuilder("Rooms/testMap.tmx", levelContainer, 160, 160);
            levelContainer.SetScaleXY(4, 4);
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
