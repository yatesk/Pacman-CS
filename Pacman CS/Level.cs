using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    class Level
    {
        public List<Tile> tiles = new List<Tile>();
        private Dictionary<Tile.TileType, Texture2D> tileTextures = new Dictionary<Tile.TileType, Texture2D>();

        public Vector2 playerStartingLocation;

        public ContentManager content;

        public Level(ContentManager _content)
        {
            content = _content;

            playerStartingLocation = Vector2.Zero;

            LoadContent();

            LoadLevel();
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                spriteBatch.Draw(tileTextures[tile.type], tile.position);
            }
        }

        public void LoadContent()
        {
            tileTextures.Add(Tile.TileType.Wall, content.Load<Texture2D>("wall1"));
            tileTextures.Add(Tile.TileType.Open, content.Load<Texture2D>("open"));
        }

        public void LoadLevel()
        {
            String line;
            List<char[]> level = new List<char[]>();
            FileStream fsSource = new FileStream("level1.txt", FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fsSource))
            {
                while ((line = sr.ReadLine()) != null)
                    level.Add(line.ToCharArray());
            }

            for (int i = 0; i < level.Count; i++)
                for (int j = 0; j < level[i].Length; j++)
                {
                    if (level[i][j] == '*')
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i), 32, 32, Tile.TileType.Wall));
                    else if(level[i][j] == 'S')
                    {
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i), 32, 32, Tile.TileType.Open));
                        playerStartingLocation = new Vector2(32 * j, 32 * i);
                    }
                    else
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i), 32, 32, Tile.TileType.Open));
                }
        }
    }
}
