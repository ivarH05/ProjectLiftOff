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
    /// A sprite that becomes transparent if the camera is on it.
    /// </summary>
    public class WallSprite : AnimationSprite
    {
        float strengthY;
        public WallSprite(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows, -1, true, false)
        { }
        public WallSprite(string filename, bool keepInCache = false, bool addCollider = true, uint CollisionLayers = 0xFFFFFFFF, uint CoupleWithLayers = 0xFFFFFFFF)
        : base(filename, 1, 1, -1, keepInCache, addCollider, CollisionLayers, CoupleWithLayers)
        {
            strengthY = 1f / height;
            depthSort = true;
        }

        public override void Render(GLContext glContext, int RenderInt)
        {
            depthSort = true;

            depth = TransformPoint(0, 0).y * -.0001f;

            Vector2 relPos = CamManager.GetPosition(RenderInt);
            relPos -= TransformPoint(0, 0);
            alpha = Mathf.Abs(relPos.y*strengthY);
            alpha = Mathf.Max(alpha, (Mathf.Abs(relPos.x * .01f)));
            alpha = Mathf.Clamp01(alpha-.5f);
            base.Render(glContext, RenderInt);
        }
    }
}
