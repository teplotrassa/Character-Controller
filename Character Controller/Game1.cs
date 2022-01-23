using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using TiledCS;

namespace Character_Controller
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private Camera _camera;

        private TiledMap _map;
        private TiledTileset _tileset;
        private Texture2D _tilesetImage;
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
                contentPrefix : "Player/player",
                position : new Vector2(0, 0),
                maxSpeed : 450f);

            _location = new("test", "Maps");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player.LoadContent(Content);
            _location.LoadContent(Content);

            _map = new TiledMap("Content/Maps/test_map.tmx");
            _tileset = new TiledTileset("Content/Maps/test_tileset.tsx");
            _tilesetImage = Content.Load<Texture2D>("Maps/" + _tileset.Image.source.Replace(".png",""));
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
            
            _location.Draw(gameTime, _spriteBatch);
            _player.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
