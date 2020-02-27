using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman_CS
{
    class GameState : State
    {
        private Player player1;
        private Player player2;
        private int numberOfPlayers;
        public Level level;
        public List<Ghost> ghosts = new List<Ghost>();
        public ScoreBoard scoreBoard;

        KeyboardState keyboardState;
        SoundEffect pacmanIntro;

        //SoundEffect pacmanChomp;

        public GameState(Game1 game, ContentManager content, int _numberOfPlayers) : base(game, content)
        {
            numberOfPlayers = _numberOfPlayers;
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

            scoreBoard = new ScoreBoard(content, numberOfPlayers);

            LoadContent();
        }

        public override void LoadContent()
        {
            pacmanIntro = content.Load<SoundEffect>(@"Sounds\pacman_beginning");

            // .1f is the volume (0-1)
            pacmanIntro.Play(.1f, 0, 0);

            //pacmanChomp = content.Load<SoundEffect>(@"Sounds\pacman_chomp");
            //var instance = pacmanChomp.CreateInstance();
            //instance.IsLooped = true;
            //instance.Volume = .1f;
            //instance.Play();
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            // Player 1 Input
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

            // Player 2 Input
            if (keyboardState.IsKeyDown(Keys.A))
            {
                player2.nextDirection = Player.Directions.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                player2.nextDirection = Player.Directions.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                player2.nextDirection = Player.Directions.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                player2.nextDirection = Player.Directions.Down;
            }

            // Player Movement
            playerMovement(player1);

            if (numberOfPlayers == 2)
                playerMovement(player2);

            // move?
            foreach (var ghost in ghosts)
            {
                ghostMovement(ghost);
            }

            if (level.CheckPelletCollision(new Rectangle((int)player1.position.X, (int)player1.position.Y, player1.size, player1.size)))
                scoreBoard.player1Score += 1;

            if (numberOfPlayers == 2)
            {
                if (level.CheckPelletCollision(new Rectangle((int)player2.position.X, (int)player2.position.Y, player2.size, player2.size)))
                    scoreBoard.player2Score += 1;
            }
        }


        public void ghostMovement(Ghost ghost)
        {
            float startingVelocityX = ghost.velocity.X;
            float startingVelocityY = ghost.velocity.Y;
            Vector2 startingPosition = ghost.position;

            ghost.Update();

            // Check new position collision
            // If there is a collision, use old velocity and position
            if (level.CheckCollision(new Rectangle((int)ghost.position.X, (int)ghost.position.Y, ghost.size, ghost.size)) == null)
            {
                ghost.nextDirection = Ghost.Directions.None;
            }
            else
            {
                ghost.velocity.X = startingVelocityX;
                ghost.velocity.Y = startingVelocityY;
                ghost.position = startingPosition;

                ghost.position += ghost.velocity;

                Tile collisionTile = level.CheckCollision(new Rectangle((int)ghost.position.X, (int)ghost.position.Y, ghost.size, ghost.size));

                if (collisionTile != null)
                {
                    // right
                    if (ghost.velocity.X > 0)
                    {
                        ghost.position.X = collisionTile.position.X - ghost.size;

                        if (ghost.nextDirection == Ghost.Directions.Right)
                        {
                            ghost.nextDirection = Ghost.Directions.None;
                        }

                    }
                    // left
                    else if (ghost.velocity.X < 0)
                    {
                        ghost.position.X = collisionTile.position.X + collisionTile.width;

                        if (ghost.nextDirection == Ghost.Directions.Left)
                        {
                            ghost.nextDirection = Ghost.Directions.None;
                        }
                    }
                    // down
                    else if (ghost.velocity.Y > 0)
                    {
                        ghost.position.Y = collisionTile.position.Y - ghost.size;

                        if (ghost.nextDirection == Ghost.Directions.Down)
                        {
                            ghost.nextDirection = Ghost.Directions.None;
                        }
                    }
                    // up
                    else if (ghost.velocity.Y < 0)
                    {
                        ghost.position.Y = collisionTile.position.Y + collisionTile.width;

                        if (ghost.nextDirection == Ghost.Directions.Up)
                        {
                            ghost.nextDirection = Ghost.Directions.None;
                        }
                    }
                }
            }
        }

        // temp refactor?
        public void playerMovement(Player player)
        {
            float startingVelocityX = player.velocity.X;
            float startingVelocityY = player.velocity.Y;
            Vector2 startingPosition = player.position;

            player.Update();

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