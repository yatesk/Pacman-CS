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

        public List<Pellet> pellets = new List<Pellet>();

        public Vector2 player1StartingLocation; 
        public Vector2 player2StartingLocation;
        public List<Vector2> ghostStartingLocations = new List<Vector2>();

        public ContentManager content;

        public Texture2D pelletTexture;
        public Texture2D powerPelletTexture;

        public Level(ContentManager _content)
        {
            content = _content;

            player1StartingLocation = Vector2.Zero;
            player2StartingLocation = Vector2.Zero;

            LoadContent();
            LoadLevel();
        }

        public void Update()
        {
        }

        public Tile CheckCollision(Rectangle playerCoords)
        {
            Rectangle playerBoundingBox = playerCoords;

            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].type == Tile.TileType.Wall)
                { 
                    Rectangle tileBoundingBox = new Rectangle((int)tiles[i].position.X, (int)tiles[i].position.Y, tiles[i].width, tiles[i].height);

                    if (playerBoundingBox.Intersects(tileBoundingBox))
                    {
                        return tiles[i];
                    }
                }
            }
            return null;
        }

        public bool CheckPelletCollision(Rectangle playerCoords)
        {
            Rectangle playerBoundingBox = playerCoords;

            foreach (var pellet in pellets)
            {
                Rectangle tileBoundingBox = new Rectangle((int)pellet.position.X, (int)pellet.position.Y, pellet.width, pellet.height);

                    if (playerBoundingBox.Intersects(tileBoundingBox))
                    {
                        pellets.Remove(pellet);
                        return true; 
                    }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                spriteBatch.Draw(tileTextures[tile.type], tile.position);
            }

            foreach (var pellet in pellets)
            {
                if (pellet.type == Pellet.PelletType.Pellet)
                {
                    spriteBatch.Draw(pelletTexture, pellet.position);
                }
                else if (pellet.type == Pellet.PelletType.PowerPellet)
                {
                    spriteBatch.Draw(powerPelletTexture, pellet.position);
                }
            }         
        }

        public void LoadContent()
        {
            tileTextures.Add(Tile.TileType.Wall, content.Load<Texture2D>("wall1"));
            tileTextures.Add(Tile.TileType.Open, content.Load<Texture2D>("open"));

            powerPelletTexture = content.Load<Texture2D>("powerPellet");
            pelletTexture = content.Load<Texture2D>("pellet");
        }

        public void LoadLevel()
        {
            int topMargin = 32;
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
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Wall));
                    else if (level[i][j] == '0')
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));
                    else if (level[i][j] == 'A')
                    {
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));
                        player1StartingLocation = new Vector2(32 * j, 32 * i + topMargin);
                    }
                    else if (level[i][j] == 'B')
                    {
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));
                        player2StartingLocation = new Vector2(32 * j, 32 * i + topMargin);
                    }
                    else if (level[i][j] == 'P')
                    {
                        // middle - size / 2
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));
                        pellets.Add(new Pellet(new Vector2(32 * j + 8, 32 * i + 8 + topMargin), 16, 16, Pellet.PelletType.PowerPellet));
                    }
                    else if (level[i][j] == 'G')
                    {
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));
                        ghostStartingLocations.Add(new Vector2(32 * j, 32 * i + topMargin));
                    }
                    else
                    {
                        tiles.Add(new Tile(new Vector2(32 * j, 32 * i + topMargin), 32, 32, Tile.TileType.Open));

                        pellets.Add(new Pellet(new Vector2(32 * j + 12, 32 * i + 12 + topMargin), 8, 8, Pellet.PelletType.Pellet));
                    }
                }
        }
    }
}