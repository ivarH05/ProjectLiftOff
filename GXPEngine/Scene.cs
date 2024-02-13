using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Scene : GameObject
    {
        private Camera viewLeft;
        private Camera viewRight;

        public Scene()
        {
            viewLeft = new Camera(0, 0, 960, 1080, false);
            viewRight = new Camera(960, 0, 960, 1080, false);
            AddChild(viewLeft);
            AddChild(viewRight);
            AddChild(new ReferencePoint());
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
