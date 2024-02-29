using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace CoolScaryGame
{
    public struct Item
    {
        public static readonly float[] ItemDurations = new float[] { -1, 5, 5, 3};

        public int ID;

        public const int empty = 0;
        public const int speedUp = 1;
        public const int showEnemy = 2;
        public const int invincibility = 3;
    }
}
