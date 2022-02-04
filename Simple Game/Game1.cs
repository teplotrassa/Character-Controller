using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simple_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private Camera _camera;

        private Location _location;

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

            _camera = new Camera
            {
                Zoom = 3.0f
            };

            _player = new Player(
                contentPrefix: "Player/player",
                position: new Vector2(0, 0),
                maxSpeed: 250f);

            _location = new("test", "Maps");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player.LoadContent(Content);
            _location.LoadContent(Content);
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

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.GetTransformation(GraphicsDevice));

            _location.DrawLayer(gameTime, _spriteBatch, "background");
            _location.DrawLayer(gameTime, _spriteBatch, "building");
            _player.Draw(gameTime, _spriteBatch);
            _location.DrawLayer(gameTime, _spriteBatch, "top");
            _location.DrawLayer(gameTime, _spriteBatch, "alwaysTop");

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
