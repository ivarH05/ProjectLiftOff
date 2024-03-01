using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.CoolScaryGame.Level;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class MinimapSprite : ColorSprite
    {
        GameObject realObject;
        public MinimapSprite(GameObject realObject, uint color) : base(3,3, color)
        {
            this.realObject = realObject;
            //CenterOrigin();
        }
        void Update()
        {
            if (realObject == null || !realObject.InHierarchy())
                alpha *= 1-Mathf.Min(Time.deltaTime,.025f);

            if (alpha > .03f)
                return;
            Console.WriteLine("destroyed minimapsprite");
            LateDestroy();
        }
    }
    public class MinimapSpriteData
    {
        public GameObject realObject;
        public uint color;
        public Vector2 pos;
        public MinimapSpriteData(GameObject realObject, uint color, Vector2 pos)
        {
            this.realObject = realObject;
            this.color = color;
            this.pos = pos;
        }
    }
}
