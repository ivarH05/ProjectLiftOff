using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    public class AnimationData
    {
        public int StartFrame;
        public int FrameCount;

        public AnimationData(int startFrame, int frameCount)
        {
            StartFrame = startFrame;
            FrameCount = frameCount;
        }
    }
}
