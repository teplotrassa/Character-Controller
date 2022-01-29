using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Simple_Game
{
    class Animated_Sprite : IDisposable
    {
        private Texture2D _texture;

        private readonly List<Animation> _animations;

        private Animation _activeAnimation;

        private int _frameIndex;

        private double _time;

        public Animated_Sprite(string contentPrefix)
        {
            _animations = GetAnimationsFromXml($"{contentPrefix}_animations.xml");
        }

        public void LoadContent(ContentManager content, string contentPrefix)
        {
            _texture = content.Load<Texture2D>($"{contentPrefix}_tileset");
        }

        public void PlayAnimation(string name)
        {
            if(_activeAnimation == null || name != _activeAnimation.Name)
            {
                _activeAnimation = _animations.FirstOrDefault(anim => anim.Name == name);
                _frameIndex = 0;
                _time = 0.0f;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (_activeAnimation.Name == null)
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

        private static List<Animation> GetAnimationsFromXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Animation>));

            using var stream = File.OpenRead($"Content/{fileName}");
            var animations = serializer.Deserialize(stream) as List<Animation>;

            return animations;
        }

        public void Dispose()
        {
            _texture.Dispose();
        }
    }
}
