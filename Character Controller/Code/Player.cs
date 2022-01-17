using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Character_Controller
{
    class Player
    {
        private Animated_Sprite _sprite;

        private Vector2 _playerPosition;

        private float _maxPlayerSpeed = 500f;
        private float _playerSpeed = 0f;

        private bool _isMoving = false;

        public Player(Vector2 position)
        {
            _playerPosition = position;
            _sprite = new Animated_Sprite(CreateAnimations());
        }

        public void Update(GameTime gameTime, KeyboardState kstate)
        {
            bool directionChanged = false;
            var inputVector = InputManager.GetDirectionalInputVector(kstate, ref directionChanged);

            if (inputVector.X != 0f || inputVector.Y != 0f)
            {
                inputVector.Normalize();
                _sprite.PlayAnimation("Walking");
                _isMoving = true;

                if (directionChanged)
                {
                    _playerSpeed = 0;
                }
                else if (_playerSpeed < _maxPlayerSpeed)
                {
                    _playerSpeed += 125;
                    if (_playerSpeed > _maxPlayerSpeed)
                        _playerSpeed = _maxPlayerSpeed;
                }

                _playerPosition += inputVector * _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _sprite.PlayAnimation("Idle");
                _isMoving = false;
                _playerSpeed = 0f;
            }
        }

        public void LoadContent(ContentManager сontent)
        {
            _sprite.LoadContent(сontent.Load<Texture2D>("Player/Idle"));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            _sprite.Draw(gameTime, spriteBatch, _playerPosition, SpriteEffects.None);
        }

        private static Dictionary<string, Animation> CreateAnimations()
        {
            return new Dictionary<string, Animation>
            {
                ["Idle"] = new Animation(1, true, 2, 16, 16, 0),
                ["Walking"] = new Animation(1, true, 2, 16, 16, 1) 
            };
        }
    }
}
