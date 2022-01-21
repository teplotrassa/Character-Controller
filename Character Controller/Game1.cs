using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Character_Controller
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private Camera _camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            _camera = new Camera();

            _player = new Player(
                new Vector2(0, 0),
                maxSpeed : 450f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player.LoadContent(Content, "Player/Idle");
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.UpdateKeyboardState();

            _player.Update(gameTime);
            _camera.Position = _player.Position;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.GetTransformation(GraphicsDevice));
            _player.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
