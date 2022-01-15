using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Character_Controller
{
    class Animated_Sprite
    {
        public Texture2D Texture
        {
            get { return _texture; }
        }
        private Texture2D _texture;

        private Animation[] _animations;

        private int _animationIndex;

        public Animated_Sprite(Animation[] animations)
        {
            _animations = animations;
        }

        public int FrameIndex
        {
            get
            {
                return _frameIndex;
            }
        }
        private int _frameIndex;

        private double _time;

        public void LoadContent(Texture2D texture)
        {
            _texture = texture;
        }

        public Vector2 GetOrigin(int index)
        {
            return new Vector2(_animations[index].FrameWidth / 2, _animations[index].FrameHeight);
        }

        public void PlayAnimation(int index)
        {
            if (index == _animationIndex)
                return;

            _animationIndex = index;
            _frameIndex = 0;
            _time = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (_animationIndex == null)
                return;

            _time += gameTime.ElapsedGameTime.TotalSeconds;
            while(_time > _animations[_animationIndex].FrameTime)
            {
                _time -= _animations[_animationIndex].FrameTime;

                if (_animations[_animationIndex].IsLooping)
                {
                    _frameIndex = (_frameIndex + 1) % _animations[_animationIndex].FrameCount;
                }
                else
                {
                    _frameIndex = Math.Min(_frameIndex + 1, _animations[_animationIndex].FrameCount - 1);
                }
            }

            int sourceX = (_frameIndex * _animations[_animationIndex].FrameWidth) % (_animations[_animationIndex].FrameCount * _animations[_animationIndex].FrameWidth);
            int sourceY = _animations[_animationIndex].VerticalOffset * _animations[_animationIndex].FrameHeight;
            Rectangle source = new Rectangle(sourceX, sourceY, _animations[_animationIndex].FrameWidth, _animations[_animationIndex].FrameHeight);

            spriteBatch.Draw(Texture, position, source, Color.White, 0.0f, GetOrigin(_animationIndex), 10.0f, spriteEffects, 0.0f);
        }
    }
}
