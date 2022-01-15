using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Character_Controller
{
    class Animation
    {
        public double FrameTime { get; }

        public bool IsLooping { get; }

        public int FrameCount { get; }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        public int VerticalOffset { get; }

        public Animation(double frameTime, bool isLooping, int frameCount, int frameWidth, int frameHeight, int verticalOffset)
        {
            FrameTime = frameTime;
            IsLooping = isLooping;
            FrameCount = frameCount;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            VerticalOffset = verticalOffset;
        }
    }
}
