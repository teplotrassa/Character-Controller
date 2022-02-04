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

            if (inputVector.X != 0f || inputVector.Y != 0f) //-V3024
            {
                _isMoving = true;
                UpdateDirection(inputVector);

                inputVector.Normalize();
                _position += inputVector * _maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _isMoving = false;
            }

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            if (_isMoving)
                _sprite.PlayAnimation("Walking_" + _facingDirection.ToString());
            else
                _sprite.PlayAnimation("Idle_" + _facingDirection.ToString());
        }

        private void UpdateDirection(Vector2 inputVector)
        {
            if (inputVector.X != 0f) //-V3024
                _facingDirection = inputVector.X == 1f ? FacingDirection.Right : FacingDirection.Left; //-V3024
            else if (inputVector.Y != 0f) //-V3024
                _facingDirection = inputVector.Y == 1f ? FacingDirection.Down : FacingDirection.Up; //-V3024
            else
                _facingDirection = FacingDirection.Right;
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
