using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{

    /// <summary>
    /// A sprite that becomes transparent if the camera is on it.
    /// </summary>
    public class WallSprite : Sprite
    {
        float strengthY;
        public WallSprite(string filename, bool keepInCache = false, bool addCollider = true, uint CollisionLayers = 0xFFFFFFFF, uint CoupleWithLayers = 0xFFFFFFFF)
        : base(filename, keepInCache, addCollider, CollisionLayers, CoupleWithLayers)
        {
            strengthY = 1f / height;
        }

        public override void Render(GLContext glContext, int RenderInt)
        {
            Vector2 relPos = CamManager.GetPosition(RenderInt);
            relPos -= TransformPoint(0, 0);
            alpha = Mathf.Clamp(Mathf.Abs(relPos.y*strengthY),0,1);
            alpha = Mathf.Max(alpha, Mathf.Clamp(Mathf.Abs(relPos.x * .01f), 0, 1));
            base.Render(glContext, RenderInt);
        }
    }
}
