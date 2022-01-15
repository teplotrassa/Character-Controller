using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Character_Controller
{
    class Player
    {
        private Animated_Sprite sprite;

        private Vector2 playerPosition;
        private Vector2 oldInputVector = new Vector2(0f, 0f);

        private float maxPlayerSpeed = 500f;
        private float playerSpeed = 0f;

        private bool isMoving = false;
        private bool wasMoving = false;

        private Game1 game;

        public Player(Game1 game, Vector2 position)
        {
            this.game = game;

            playerPosition = position;

            sprite = new Animated_Sprite(
                new Animation[] 
                { 
                    new Animation(0.5, true, 2, 16, 16, 0),
                    new Animation(0.5, true, 2, 16, 16, 1)
                });
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
                sprite.PlayAnimation(1);
                isMoving = true;

                if (inputVector.X == 2f)
                    inputVector.X = oldInputVector.X;
                if (inputVector.Y == 2f)
                    inputVector.Y = oldInputVector.Y;

                if ((oldInputVector.X != 0f && inputVector.X != oldInputVector.X && inputVector.Y == 0f) ||
                    (oldInputVector.Y != 0f && inputVector.Y != oldInputVector.Y && inputVector.X == 0f))
                    playerSpeed = 0;

                oldInputVector = inputVector;

                inputVector.Normalize();

                if ((wasMoving || playerSpeed == 0f) && playerSpeed < maxPlayerSpeed)
                {
                    playerSpeed += 125;
                    if (playerSpeed > maxPlayerSpeed)
                        playerSpeed = maxPlayerSpeed;
                }

                playerPosition += inputVector * playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                wasMoving = true;
            }
            else
            {
                sprite.PlayAnimation(0);
                isMoving = false;
                wasMoving = false;
                playerSpeed = 0f;
            }
        }

        public void LoadContent()
        {
            sprite.LoadContent(game.Content.Load<Texture2D>("Player/Idle"));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {         
            sprite.Draw(gameTime, spriteBatch, playerPosition, SpriteEffects.None);
        }
    }
}
