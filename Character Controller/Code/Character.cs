using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Character_Controller
{
    abstract class Character
    {
        protected Animated_Sprite _sprite;

        protected Vector2 _position;

        protected float _maxSpeed;
        protected float _speed;

        public Character(Vector2 position, float maxSpeed)
        {
            _position = position;
            _maxSpeed = maxSpeed;
            _sprite = new Animated_Sprite(CreateAnimations());
        }

        public abstract void LoadContent(ContentManager content, string spriteName);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        protected abstract Dictionary<string, Animation> CreateAnimations();
    }
}
