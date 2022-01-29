using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using TiledCS;

namespace Character_Controller
{
    public class Location
    {
        private readonly string _name;

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
            string[] layerNames = { "background", "building", "top", "alwaysTop" };

            for (int j = 0; j < _map.Layers.Length; j++)
            {
                string layername = layerNames[j];
                int layerID = _map.Layers.FirstOrDefault(x => x.name == layername).id - 1;

                for (int i = 0; i < _map.Layers[layerID].data.Length; i++)
                {
                    int gid = _map.Layers[layerID].data[i] - 1;

                    if (gid >= 0)
                    {
                        int mapCol = i % _map.Layers[layerID].height;
                        int mapRow = i / _map.Layers[layerID].width;

                        int x = mapCol * _map.TileWidth;
                        int y = mapRow * _map.TileHeight;

                        int tilesetColumn = gid % _tilesets[0].Columns;
                        int tilesetRow = gid / _tilesets[0].Columns;

                        Rectangle tilesetRect = new(_tilesets[0].TileWidth * tilesetColumn, _tilesets[0].TileHeight * tilesetRow, _tilesets[0].TileWidth, _tilesets[0].TileHeight);

                        SpriteEffects spriteEffects = SpriteEffects.None;
                        float rotation = 0.0f;

                        if ((_map.Layers[layerID].dataRotationFlags[i] & 0b100) != 0)
                        {
                            spriteEffects |= SpriteEffects.FlipHorizontally;
                        }
                        if ((_map.Layers[layerID].dataRotationFlags[i] & 0b001) != 0)
                        {
                            rotation = MathHelper.PiOver2;
                        }
                        if ((_map.Layers[layerID].dataRotationFlags[i] & 0b010) != 0)
                        {
                            spriteEffects |= SpriteEffects.FlipVertically;
                        }

                        spriteBatch.Draw(_tilesetImages[0], new Vector2(x, y), tilesetRect, Color.White, rotation, new Vector2(16, 16), 1.0f, spriteEffects, 0);
                    }
                }
            }
        }

        public void DrawLayer(GameTime gameTime, SpriteBatch spriteBatch, string layername)
        {
            if (_map == null) return;
            if (_map.Layers.FirstOrDefault(x => x.name == layername) == null) return;

            int layerID = _map.Layers.FirstOrDefault(x => x.name == layername).id - 1;

            for (int i = 0; i < _map.Layers[layerID].data.Length; i++)
            {
                int gid = _map.Layers[layerID].data[i] - 1;

                if (gid >= 0)
                {
                    int mapCol = i % _map.Layers[layerID].height;
                    int mapRow = i / _map.Layers[layerID].width;

                    int x = mapCol * _map.TileWidth;
                    int y = mapRow * _map.TileHeight;

                    int tilesetColumn = gid % _tilesets[0].Columns;
                    int tilesetRow = gid / _tilesets[0].Columns;

                    Rectangle tilesetRect = new(_tilesets[0].TileWidth * tilesetColumn, _tilesets[0].TileHeight * tilesetRow, _tilesets[0].TileWidth, _tilesets[0].TileHeight);

                    SpriteEffects spriteEffects = SpriteEffects.None;
                    float rotation = 0.0f;

                    if ((_map.Layers[layerID].dataRotationFlags[i] & 0b100) != 0)
                    {
                        spriteEffects |= SpriteEffects.FlipHorizontally;
                    }
                    if ((_map.Layers[layerID].dataRotationFlags[i] & 0b001) != 0)
                    {
                        rotation = MathHelper.PiOver2;
                    }
                    if ((_map.Layers[layerID].dataRotationFlags[i] & 0b010) != 0)
                    {
                        spriteEffects |= SpriteEffects.FlipVertically;
                    }

                    spriteBatch.Draw(_tilesetImages[0], new Vector2(x, y), tilesetRect, Color.White, rotation, new Vector2(16, 16), 1.0f, spriteEffects, 0);
                }
            }
        }
    }
}
