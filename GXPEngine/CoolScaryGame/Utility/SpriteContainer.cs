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
    /// A container for sprites, that doesnt even bother calling Render for its children if it itself isnt visible.
    /// Exclusively for optimisation.
    /// </summary>
    public class SpriteContainer : InvisibleObject
    {
        public SpriteContainer(TiledObject obj) : base(obj, false, 0, overrideVisible:false) { }
        public SpriteContainer(int width, int height) : base(width, height, overrideVisible:false) {
        }

        public override void Render(GLContext glContext, int s)
        {
            if(OnScreen())
                base.Render(glContext, s);
        }
    }
}
