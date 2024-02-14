using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class SpriteContainer : InvisibleObject
    {
        public SpriteContainer(int width, int height) : base(width, height, overrideVisible:true) {
        }

        public override void Render(GLContext glContext, int s)
        {
            if(OnScreen())
                base.Render(glContext, s);
        }
    }
}
