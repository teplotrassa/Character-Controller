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
        private Vector2 _oldInputVector = new Vector2(0f, 0f);

        private float _maxPlayerSpeed = 500f;
        private float _playerSpeed = 0f;

        private bool _isMoving = false;
        private bool _wasMoving = false;

        public Player(Vector2 position)
        {
            _playerPosition = position;

            _sprite = new Animated_Sprite(CreateAnimations());
        }

        public void Update(GameTime gameTime, KeyboardState kstate)
        {
            var inputVector = new Vector2(0f, 0f);

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.Right))
                inputVector.X = kstate.IsKeyDown(Keys.Left) ? (kstate.IsKeyDown(Keys.Right) ? 2f : -1f) : kstate.IsKeyDown(Keys.Right) ? 1f : 0f;

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.Down))
                inputVector.Y = kstate.IsKeyDown(Keys.Up) ? (kstate.IsKeyDown(Keys.Down) ? 2f : -1f) : kstate.IsKeyDown(Keys.Down) ? 1f : 0f;

            if (inputVector.X != 0f || inputVector.Y != 0f)
            {
                _sprite.PlayAnimation("Walking");
                _isMoving = true;

                if (inputVector.X == 2f)
                    inputVector.X = _oldInputVector.X;
                if (inputVector.Y == 2f)
                    inputVector.Y = _oldInputVector.Y;

                if ((_oldInputVector.X != 0f && inputVector.X != _oldInputVector.X && inputVector.Y == 0f) ||
                    (_oldInputVector.Y != 0f && inputVector.Y != _oldInputVector.Y && inputVector.X == 0f))
                    _playerSpeed = 0;

                _oldInputVector = inputVector;

                inputVector.Normalize();

                if ((_wasMoving || _playerSpeed == 0f) && _playerSpeed < _maxPlayerSpeed)
                {
                    _playerSpeed += 125;
                    if (_playerSpeed > _maxPlayerSpeed)
                        _playerSpeed = _maxPlayerSpeed;
                }

                _playerPosition += inputVector * _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _wasMoving = true;
            }
            else
            {
                _sprite.PlayAnimation("Idle");
                _isMoving = false;
                _wasMoving = false;
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
