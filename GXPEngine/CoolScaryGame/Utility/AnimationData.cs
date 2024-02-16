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
        public int rows;
        public int columns;

        public AnimationData(string path, int rows, int columns)
        {
            this.path = path;
            this.rows = rows;
            this.columns = columns;
        }
    }
}
