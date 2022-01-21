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
        private Texture2D _texture;

        private readonly Dictionary<string, Animation> _animations;

        private Animation _activeAnimation;
        private string _activeAnimationName;

        private int _frameIndex;

        private double _time;

        public Animated_Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
        }

        public void LoadContent(Texture2D texture)
        {
            _texture = texture;
        }

        public void PlayAnimation(string name)
        {
            if (name == _activeAnimationName)
                return;

            _activeAnimationName = name;
            _activeAnimation = _animations[name];
            _frameIndex = 0;
            _time = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (_activeAnimation == null)
                return;

            _time += gameTime.ElapsedGameTime.TotalSeconds;
            while(_time > _activeAnimation.FrameTime)
            {
                _time -= _activeAnimation.FrameTime;

                if (_activeAnimation.IsLooping)
                {
                    _frameIndex = (_frameIndex + 1) % _activeAnimation.FrameCount;
                }
                else
                {
                    _frameIndex = Math.Min(_frameIndex + 1, _activeAnimation.FrameCount - 1);
                }
            }

            int sourceX = (_frameIndex * _activeAnimation.FrameWidth) % (_activeAnimation.FrameCount * _activeAnimation.FrameWidth);
            int sourceY = _activeAnimation.VerticalOffset * _activeAnimation.FrameHeight;
            Rectangle source = new(sourceX, sourceY, _activeAnimation.FrameWidth, _activeAnimation.FrameHeight);

            spriteBatch.Draw(_texture, position, source, Color.White, 0.0f, _activeAnimation.GetOrigin(), 1.0f, spriteEffects, 0.0f);
        }
    }
}
