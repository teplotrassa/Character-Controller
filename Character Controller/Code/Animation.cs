using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Character_Controller
{
    public class Animation
    {
        public string Name { get; set; }

        public float FrameTime { get; set; }

        public bool IsLooping { get; set; }

        public int FrameCount { get; set; }

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public int VerticalOffset { get; set; }

        public Animation() { }

        public Animation(string name, float frameTime, bool isLooping, int frameCount, int frameWidth, int frameHeight, int verticalOffset)
        {
            Name = name;
            FrameTime = frameTime;
            IsLooping = isLooping;
            FrameCount = frameCount;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            VerticalOffset = verticalOffset;
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(FrameWidth / 2, FrameHeight);
        }
    }
}
