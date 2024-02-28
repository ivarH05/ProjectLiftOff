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
        public float Speed = 1;

        public AnimationData(int startFrame, int frameCount, float speed = 1)
        {
            StartFrame = startFrame;
            FrameCount = frameCount;
            Speed = speed;
        }
    }
}
