using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman_CS
{
    class GameState : State
    {
        private Player player1;
        private Player player2;
        public Level level;
        public List<Ghost> ghosts = new List<Ghost>();
        public ScoreBoard scoreBoard;

        KeyboardState keyboardState;

        public GameState(Game1 game, ContentManager content, int numberOfPlayers) : base(game, content)
        {
            level = new Level(content);
            player1 = new Player(content, new Vector2(level.player1StartingLocation.X, level.player1StartingLocation.Y), "pacman1");

            if (numberOfPlayers == 2)
            {
                player2 = new Player(content, new Vector2(level.player2StartingLocation.X, level.player2StartingLocation.Y), "pacman2");
            }


            foreach (var position in level.ghostStartingLocations)
            {
                ghosts.Add(new Ghost(content, position));
            }

            scoreBoard = new ScoreBoard(content, 1);

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
                player1.nextDirection = Player.Directions.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player1.nextDirection = Player.Directions.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                player1.nextDirection = Player.Directions.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                player1.nextDirection = Player.Directions.Down;
            }

            float startingVelocityX = player1.velocity.X;
            float startingVelocityY = player1.velocity.Y;
            Vector2 startingPosition = player1.position;

            player1.Update();
            player2.Update();
            
            // Check new position collision
            // If there is a collision, use old velocity and position
            if (level.CheckCollision(new Rectangle((int)player1.position.X, (int)player1.position.Y, player1.size, player1.size)) == null)
            {
                player1.nextDirection = Player.Directions.None;
            }
            else
            {
                player1.velocity.X = startingVelocityX;
                player1.velocity.Y = startingVelocityY;
                player1.position = startingPosition;

                player1.position += player1.velocity;

                Tile collisionTile = level.CheckCollision(new Rectangle((int)player1.position.X, (int)player1.position.Y, player1.size, player1.size));

                if (collisionTile != null)
                {
                    // right
                    if (player1.velocity.X > 0)
                    {
                        player1.position.X = collisionTile.position.X - player1.size;

                        if (player1.nextDirection == Player.Directions.Right)
                        {
                            player1.nextDirection = Player.Directions.None;
                        }

                    }
                    // left
                    else if (player1.velocity.X < 0)
                    {
                        player1.position.X = collisionTile.position.X + collisionTile.width;

                        if (player1.nextDirection == Player.Directions.Left)
                        {
                            player1.nextDirection = Player.Directions.None;
                        }
                    }
                    // down
                    else if (player1.velocity.Y > 0)
                    {
                        player1.position.Y = collisionTile.position.Y - player1.size;

                        if (player1.nextDirection == Player.Directions.Down)
                        {
                            player1.nextDirection = Player.Directions.None;
                        }
                    }
                    // up
                    else if (player1.velocity.Y < 0)
                    {
                        player1.position.Y = collisionTile.position.Y + collisionTile.width;

                        if (player1.nextDirection == Player.Directions.Up)
                        {
                            player1.nextDirection = Player.Directions.None;
                        }
                    }
                }
            }

            // move?
            foreach (var ghost in ghosts)
            {
                ghost.Update();
                
            }

            if (level.CheckPelletCollision(new Rectangle((int)player1.position.X, (int)player1.position.Y, player1.size, player1.size)))
            {
                scoreBoard.player1Score += 1;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
            player1.Draw(spriteBatch);

            if (player2 != null)
                player2.Draw(spriteBatch);

            foreach (var ghost in ghosts)
            {
                ghost.Draw(spriteBatch);
            }

            scoreBoard.Draw(spriteBatch);
        }
    }
}