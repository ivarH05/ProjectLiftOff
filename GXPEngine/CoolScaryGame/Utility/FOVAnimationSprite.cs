using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolScaryGame;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class FOVAnimationSprite : AnimationSprite
    {
        float invVisibilityRadius;
        public FOVAnimationSprite(string filename, int cols, int rows, int frames = -1, float visibilityRadius = 500, bool keepInCache = false, bool addCollider = true, uint CollisionLayers = 0xFFFFFFFF, uint CoupleWithLayers = 0xFFFFFFFF)
        :base(filename, cols, rows, frames, keepInCache, addCollider, CollisionLayers, CoupleWithLayers)
        {
            invVisibilityRadius = 1 / visibilityRadius;
        }

        public override void Render(GLContext glContext, int RenderInt)
        {
            Vector2 relPos = CamManager.GetPosition(RenderInt);
            relPos -= TransformPoint(0, 0);
            relPos *= invVisibilityRadius;
            float trueAlpha = alpha;
            alpha *= Mathf.Max(0,1f - relPos.MagnitudeSquared);
            base.Render(glContext, RenderInt);
            alpha = trueAlpha;
        }
    }
}
