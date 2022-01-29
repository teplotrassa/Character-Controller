﻿using System;
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
    abstract class Character : IDisposable
    {
        protected string _contentPrefix;

        protected Animated_Sprite _sprite;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        protected Vector2 _position;

        protected float _maxSpeed;

        public Character(string contentPrefix, Vector2 position, float maxSpeed)
        {
            _position = position;
            _maxSpeed = maxSpeed;
            _contentPrefix = contentPrefix;
            _sprite = new Animated_Sprite(_contentPrefix);
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public void Dispose()
        {
            _sprite.Dispose();
        }
    }
}
