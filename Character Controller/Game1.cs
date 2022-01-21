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
            _camera.Zoom = 3.0f;

            _player = new Player(
                new Vector2(0, 0),
                maxSpeed : 450f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player.LoadContent(Content, "Player/Idle");

            _map = new TiledMap("Content/Map.tmx");
            _tileset = new TiledTileset("Content/map_tileset.tsx");
            _tilesetImage = Content.Load<Texture2D>(_tileset.Image.source.Replace(".png",""));
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

            for (int i = 0; i < _map.Layers[0].data.Length; i++)
            {
                int gid = _map.Layers[0].data[i];

                if (gid == 0) { continue; }
                else
                {
                    int row = i / _map.Layers[0].width;

                    float x = (i % _map.Layers[0].width) * _map.TileWidth;
                    float y = row * _map.TileHeight;

                    Rectangle tilesetRect = new(_map.TileWidth * (gid-1), _map.TileHeight * ((gid-1) / (_tileset.TileCount / _tileset.Columns))  , 32, 32);

                    _spriteBatch.Draw(_tilesetImage, new Rectangle((int)x, (int)y, 32, 32), tilesetRect, Color.White);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
