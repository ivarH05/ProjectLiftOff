using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

namespace CoolScaryGame
{
    public class FloorCollider : InvisibleObject
    {
        public FloorCollider(TiledObject obj) : base(obj, true, 0b_1_0000) { }
    }
}
