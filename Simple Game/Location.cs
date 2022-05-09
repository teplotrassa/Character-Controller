using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace Simple_Game
{
    public class Location
    {
        private readonly string _name;

        private TiledMap _map;

        private TiledTileset[] _tilesets;

        private Dictionary<int, int> _tilesetIndexByGid;

        private List<Rectangle> _collisionData;

        private Texture2D[] _tilesetImages;

        public int TileWidth 
        { 
            get { return _map.TileWidth; }
        }

        public int TileHeight 
        {
            get { return _map.TileHeight; } 
        }

        public Location(string name, string contentPrefix)
        {
            _name = name;
            _map = new TiledMap($"Content/{contentPrefix}/{name}_map.tmx");

            _tilesetIndexByGid = new Dictionary<int, int>();
            _collisionData = new List<Rectangle>();

            GetTilesets(contentPrefix);
            CreateTilesetIndexByGidTable();
            CreateCollisionData("building");
        }

        private void GetTilesets(string contentPrefix)
        {
            _tilesets = new TiledTileset[_map.Tilesets.Length];

            for (int i = 0; i < _map.Tilesets.Length; i++)
            {
                _tilesets[i] = new TiledTileset($"Content/{contentPrefix}/{_map.Tilesets[i].source}");
            }
        }

        private int GetTilesetIndexByGid(int gid)
        {
            return _tilesetIndexByGid[gid];
        }

        private void CreateTilesetIndexByGidTable()
        {
            for (int i = 0; i < _map.Tilesets.Length; i++)
            {
                int startingGid = _map.Tilesets[i].firstgid;
                int tilesetLength = _tilesets[i].TileCount;

                for (int j = 0; j < tilesetLength; j++)
                {
                    _tilesetIndexByGid.Add(startingGid + j, i);
                }
            }
        }

        private void CreateCollisionData(string collisionLayerName)
        {
            TiledLayer collisionLayer = Array.Find(_map.Layers, x => x.name == collisionLayerName);

            if (collisionLayer == null)
                return;

            for (int i = 0; i < collisionLayer.data.Length; i++)
            {
                if(collisionLayer.data[i] > 0)
                {
                    int mapCol = i % collisionLayer.height;
                    int mapRow = i / collisionLayer.width;

                    int x = mapCol * _map.TileWidth;
                    int y = mapRow * _map.TileHeight;

                    _collisionData.Add(new Rectangle(x, y, _map.TileWidth, _map.TileHeight));
                }
            }
        }

        public List<Rectangle> GetCollisionData()
        {
            return _collisionData;
        }

        public void LoadContent(ContentManager content)
        {
            _tilesetImages = new Texture2D[_tilesets.Length];

            for (int i = 0; i < _tilesets.Length; i++)
            {
                _tilesetImages[i] = content.Load<Texture2D>("Maps/" + _tilesets[i].Image.source.Replace(".png", ""));
            }
        }

        public void DrawLayer(GameTime gameTime, SpriteBatch spriteBatch, string layername)
        {
            if (_map == null) return;

            TiledLayer currentLayer = Array.Find(_map.Layers, x => x.name == layername);
            if (currentLayer == null) return;

            for (int i = 0; i < currentLayer.data.Length; i++)
            {
                int gid = currentLayer.data[i];

                if (gid > 0)
                {
                    int tilesetIndex = GetTilesetIndexByGid(gid);
                    TiledTileset currentTileset = _tilesets[tilesetIndex];

                    gid -= 1;

                    int mapCol = i % currentLayer.height;
                    int mapRow = i / currentLayer.width;

                    int x = mapCol * _map.TileWidth;
                    int y = mapRow * _map.TileHeight;

                    int tilesetColumn = gid % currentTileset.Columns;
                    int tilesetRow = gid / currentTileset.Columns;

                    Rectangle tilesetRect = new(currentTileset.TileWidth * tilesetColumn, currentTileset.TileHeight * tilesetRow, currentTileset.TileWidth, currentTileset.TileHeight);

                    SpriteEffects spriteEffects = SpriteEffects.None;
                    float rotation = 0.0f;

                    if ((currentLayer.dataRotationFlags[i] & 0b100) != 0)
                    {
                        spriteEffects |= SpriteEffects.FlipHorizontally;
                    }
                    if ((currentLayer.dataRotationFlags[i] & 0b001) != 0)
                    {
                        //TODO: Rotate befgre flipping
                        //rotation = MathHelper.PiOver2;
                    }
                    if ((currentLayer.dataRotationFlags[i] & 0b010) != 0)
                    {
                        spriteEffects |= SpriteEffects.FlipVertically;
                    }

                    spriteBatch.Draw(_tilesetImages[tilesetIndex], new Vector2(x, y), tilesetRect, Color.White, rotation, Vector2.Zero, 1.0f, spriteEffects, 0);
                }
            }
        }
    }
}
