﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolScaryGame;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    /// <summary>
    /// Animation sprite, except it becomes gradually transparent over visibilityRadius.
    /// </summary>
    public class FOVAnimationSprite : AnimationSprite
    {
        float invVisibilityRadius;
        int currentRenderInt;
        public FOVAnimationSprite(string filename, int cols, int rows, int frames = -1, float visibilityRadius = 300, bool keepInCache = false, bool addCollider = true, uint CollisionLayers = 0xFFFFFFFF, uint CoupleWithLayers = 0xFFFFFFFF)
        : base(filename, cols, rows, frames, keepInCache, addCollider, CollisionLayers, CoupleWithLayers)
        {
            invVisibilityRadius = 1 / visibilityRadius;
        }

        public void SetVisibility(float visibility)
        {
            invVisibilityRadius = 1.0f / visibility;
        }

        public override void Render(GLContext glContext, int RenderInt)
        {
            currentRenderInt = RenderInt;
            base.Render(glContext, RenderInt);
        }
        protected override void RenderSelf(GLContext glContext)
        {
            Vector2 relPos = CamManager.GetPosition(currentRenderInt);
            relPos -= TransformPoint(0, 0);
            relPos *= invVisibilityRadius;
            float trueAlpha = alpha;
            alpha *= Mathf.Max(0,1f - relPos.MagnitudeSquared);
            base.RenderSelf(glContext);
            alpha = trueAlpha;
        }
    }
}
