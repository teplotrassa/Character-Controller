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
        private enum PlayerStates
        {
            MapMovement
        }
        private PlayerStates _state;

        private bool _isMoving;

        private Rectangle _collisionMask;
        private int _collisionMaskWidth;
        private int _collisionMaskHeight;
        
        private Game1 _gameRef;

        public Player(Game1 gameRef, string contentPrefix, Vector2 position, float maxSpeed) 
            : base(contentPrefix, position, maxSpeed) 
        {
            _gameRef = gameRef;
            _state = PlayerStates.MapMovement;

            _collisionMaskWidth = 32;
            _collisionMaskHeight = 32;
            _collisionMask = new((int)_position.X - _collisionMaskWidth / 2, (int)_position.Y - _collisionMaskHeight, _collisionMaskWidth, _collisionMaskHeight);
        }

        public override void Update(GameTime gameTime)
        {
            if(_state == PlayerStates.MapMovement)
            {
                Vector2 inputVector = InputManager.GetDirectionalInputVector();

                if (inputVector.X != 0f || inputVector.Y != 0f) //-V3024
                {
                    _isMoving = true;
                    UpdateDirection(inputVector);
                    UpdatePositon(gameTime, inputVector);
                }
                else
                {
                    _isMoving = false;
                }
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

        private void UpdatePositon(GameTime gameTime, Vector2 inputVector)
        {
            inputVector.Normalize();
            Vector2 movementVector = inputVector * _maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            List<Rectangle> colidingTiles = CheckForCollisions(movementVector);

            if (colidingTiles.Count > 0)
                movementVector *= 0;

            _position += movementVector;
            _collisionMask = UpdateCollisionMask(_position);
        }

        private List<Rectangle> CheckForCollisions(Vector2 movementVector)
        {
            Location location = _gameRef.Location;

            Vector2 newPlayerPosition = _position + movementVector;
            Vector2 playerTileCenter = new(MathF.Floor(newPlayerPosition.X /  location.TileWidth) * location.TileWidth + location.TileWidth / 2, MathF.Floor(newPlayerPosition.Y / location.TileHeight) * location.TileHeight + location.TileWidth / 2);

            int radiusX = (int)(location.TileWidth * 1.5);
            int radiusY = (int)(location.TileHeight * 1.5);

            Rectangle checkRect = new((int)playerTileCenter.X - radiusX, (int)playerTileCenter.Y - radiusY, 2 * radiusX, 2 * radiusY);

            List<Rectangle> tilesToCheck = location.GetCollisionData().FindAll(x =>
            {
                if(checkRect.Intersects(x))
                    return true;
                return false;
            });

            Rectangle newCollisionMask = UpdateCollisionMask(newPlayerPosition);

            return tilesToCheck.FindAll(x =>
            {
                if (x.Intersects(newCollisionMask))
                    return true;
                return false;
            });
        }

        private Rectangle UpdateCollisionMask(Vector2 newPosition)
        {
            return new Rectangle((int)newPosition.X - _collisionMaskWidth / 2, (int)newPosition.Y - _collisionMaskHeight, _collisionMaskWidth, _collisionMaskHeight);
        }

        public override void LoadContent(ContentManager сontent)
        {
            _sprite.LoadContent(сontent, _contentPrefix);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            _sprite.Draw(gameTime, spriteBatch, _position, SpriteEffects.None);

            Texture2D temp = new(_gameRef.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.Red });

            spriteBatch.Draw(temp, _collisionMask, Color.White);
        }
    }
}
