using GXPEngine.Core;
using GXPEngine;
using System;

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
        }

        void Update()
        {
            Console.WriteLine(viewLeft.position);
        }

        public Camera[] GetCameras()
        {
            return new Camera[] { viewLeft, viewRight };
        }
    }
}
