using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace Character_Controller
{
    public class Location
    {
        private readonly string  _name;

        private TiledMap _map;

        private TiledTileset[] _tilesets;

        private Texture2D[] _tilesetImages;

        public Location(string name, string contentPrefix)
        {
            _name = name;
            _map = new TiledMap($"Content/{contentPrefix}/{name}_map.tmx");
            GetTilesets(contentPrefix);
        }

        private void GetTilesets(string contentPrefix)
        {
            _tilesets = new TiledTileset[_map.Tilesets.Length];

            for (int i = 0; i < _map.Tilesets.Length; i++)
            {
                _tilesets[i] = new TiledTileset($"Content/{contentPrefix}/{_map.Tilesets[i].source}");
            }
        }

        public void LoadContent(ContentManager content)
        {
            _tilesetImages = new Texture2D[_map.Tilesets.Length];

            for (int i = 0; i < _tilesets.Length; i++)
            {
                _tilesetImages[i] = content.Load<Texture2D>("Maps/" + _tilesets[i].Image.source.Replace(".png", ""));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _map.Layers[0].data.Length; i++)
            {
                int gid = _map.Layers[0].data[i];

                if (gid == 0) { continue; }
                else
                {
                    int row = i / _map.Layers[0].width;

                    float x = (i % _map.Layers[0].width) * _map.TileWidth;
                    float y = row * _map.TileHeight;

                    Rectangle tilesetRect = new(_map.TileWidth * (gid - 1), _map.TileHeight * ((gid - 1) / (_tilesets[0].TileCount / _tilesets[0].Columns)), 32, 32);

                    spriteBatch.Draw(_tilesetImages[0], new Rectangle((int)x, (int)y, 32, 32), tilesetRect, Color.White);
                }
            }
        }
    }
}
