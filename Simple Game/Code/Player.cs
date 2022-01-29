using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simple_Game
{
    class Player : Character
    {
        private bool _isMoving;

        public Player(string contentPrefix, Vector2 position, float maxSpeed) 
            : base(contentPrefix, position, maxSpeed) { }

        public override void Update(GameTime gameTime)
        {
            Vector2 inputVector = InputManager.GetDirectionalInputVector();

            if (inputVector.X != 0f || inputVector.Y != 0f)
            {
                _sprite.PlayAnimation("Walking");
                _isMoving = true;

                inputVector.Normalize();
                _position += inputVector * _maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _sprite.PlayAnimation("Idle");
                _isMoving = false;
            }
        }

        public override void LoadContent(ContentManager сontent)
        {
            _sprite.LoadContent(сontent, _contentPrefix);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            _sprite.Draw(gameTime, spriteBatch, _position, SpriteEffects.None);
        }
    }
}
