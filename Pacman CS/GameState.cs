using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman_CS
{
    class GameState : State
    {
        private Player player;
        public Level level;
        public List<Ghost> ghosts = new List<Ghost>();

        private int playerSpeed = 2;

        KeyboardState keyboardState;

        int score;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {
            level = new Level(content);
            player = new Player(content, new Vector2(level.playerStartingLocation.X, level.playerStartingLocation.Y));


            foreach (var position in level.ghostStartingLocations)
            {
                ghosts.Add(new Ghost(content, position));
            }

            score = 0;

            LoadContent();
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                player.nextDirection = Player.Directions.Left;

            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player.nextDirection = Player.Directions.Right;

            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                player.nextDirection = Player.Directions.Up;

            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                player.nextDirection = Player.Directions.Down;

            }

            float startingVelocityX = player.velocity.X;
            float startingVelocityY = player.velocity.Y;
            Vector2 startingPosition = player.position;

            player.Update();

            System.Diagnostics.Debug.WriteLine(score);

            
            // Check new position collision
            // If there is a collision, use old velocity and position
            if (level.CheckCollision(new Rectangle((int)player.position.X, (int)player.position.Y, player.size, player.size)) == null)
            {
                player.nextDirection = Player.Directions.None;
            }
            else
            {
                player.velocity.X = startingVelocityX;
                player.velocity.Y = startingVelocityY;
                player.position = startingPosition;

                player.position += player.velocity;

                Tile collisionTile = level.CheckCollision(new Rectangle((int)player.position.X, (int)player.position.Y, player.size, player.size));

                if (collisionTile != null)
                {
                    // right
                    if (player.velocity.X > 0)
                    {
                        player.position.X = collisionTile.position.X - player.size;

                        if (player.nextDirection == Player.Directions.Right)
                        {
                            player.nextDirection = Player.Directions.None;
                        }

                    }
                    // left
                    else if (player.velocity.X < 0)
                    {
                        player.position.X = collisionTile.position.X + collisionTile.width;

                        if (player.nextDirection == Player.Directions.Left)
                        {
                            player.nextDirection = Player.Directions.None;
                        }
                    }
                    // down
                    else if (player.velocity.Y > 0)
                    {
                        player.position.Y = collisionTile.position.Y - player.size;

                        if (player.nextDirection == Player.Directions.Down)
                        {
                            player.nextDirection = Player.Directions.None;
                        }
                    }
                    // up
                    else if (player.velocity.Y < 0)
                    {
                        player.position.Y = collisionTile.position.Y + collisionTile.width;

                        if (player.nextDirection == Player.Directions.Up)
                        {
                            player.nextDirection = Player.Directions.None;
                        }
                    }
                }
            }

            // move?
            foreach (var ghost in ghosts)
            {
                ghost.Update();
                System.Diagnostics.Debug.WriteLine("FDd");
            }

            if (level.CheckPelletCollision(new Rectangle((int)player.position.X, (int)player.position.Y, player.size, player.size)))
            {
                score += 1;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (var ghost in ghosts)
            {
                ghost.Draw(spriteBatch);
            }
        }
    }
}