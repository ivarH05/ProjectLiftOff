using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    public class AnimationData
    {
        public string path;
        public int columns;
        public int rows;

        public AnimationData(string path, int columns, int rows)
        {
            this.path = path;
            this.columns = columns;
            this.rows = rows;
        }
    }
}
