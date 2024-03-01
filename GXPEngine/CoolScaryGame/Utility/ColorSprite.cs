using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace CoolScaryGame
{
    public class ColorSprite : Sprite
    {
        public ColorSprite(float width, float height, uint color) : base("UI/whitePixel.png", true, false)
        {
            this.color = color;
            this.scaleX = width;
            this.scaleY = height;
        }
    }
}