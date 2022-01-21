using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Character_Controller
{
    class Player : Character
    {
        private bool _isMoving = false;

        public Player(Vector2 position, float maxSpeed) : base(position, maxSpeed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Vector2 inputVector = InputManager.GetDirectionalInputVector();

            if (inputVector.X != 0f || inputVector.Y != 0f)
            {
                _sprite.PlayAnimation("Walking");
                _isMoving = true;
                _speed = _maxSpeed;

                inputVector.Normalize();
                _position += inputVector * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _sprite.PlayAnimation("Idle");
                _isMoving = false;
                _speed = 0f;
            }
        }

        public override void LoadContent(ContentManager сontent, string spriteName)
        {
            _sprite.LoadContent(сontent.Load<Texture2D>(spriteName));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            _sprite.Draw(gameTime, spriteBatch, _position, SpriteEffects.None);
        }

        protected override Dictionary<string, Animation> CreateAnimations()
        {
            return new Dictionary<string, Animation>
            {
                ["Idle"] = new Animation(1, true, 2, 16, 16, 0),
                ["Walking"] = new Animation(1, true, 2, 16, 16, 1) 
            };
        }
    }
}
