using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

namespace CoolScaryGame
{

    /// <summary>
    /// A sprite that is actually a wall
    /// </summary>
    public class WallSprite : InvisibleObject
    {
        float strengthY;
        AnimationSprite renderer;
        bool rendererVertical = false;
        public WallSprite(TiledObject obj) : base(obj, true, 0b1, 0, true)
        { }
        public WallSprite(int width, int height, bool overrideVisible = false)
        : base(width, height, true, 0b1, 0, overrideVisible)
        {
            depthSort = true;
        }
        public override void Setup()
        {
            AddProxy();
            rendererVertical = width < height;
            renderer = new AnimationSprite("Rooms/textures/roomtiles.png", 4, 2, 8, true, false, 0, 0);
            proxy.AddChild(renderer);
            renderer.SetFrame(Utils.Random(0,4));
            if(!rendererVertical)
                renderer.SetOrigin(renderer.width*.5f, renderer.height);
            else renderer.SetOrigin(renderer.width * .5f, renderer.height - height * .5f);

            depthSort = true;
        }
        public override void Render(GLContext glContext, int RenderInt)
        {
            SetDepthByY(RenderInt);

            Vector2 relPos = CamManager.GetPosition(RenderInt);
            relPos -= TransformPoint(0, 0);
            //alpha = Mathf.Abs(relPos.y*strengthY);
            //alpha = Mathf.Max(alpha, (Mathf.Abs(relPos.x * .01f)));
            //alpha = Mathf.Clamp01(alpha-.5f);
            base.Render(glContext, RenderInt);
        }
    }
}
