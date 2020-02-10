using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman_CS
{
    class GameState : State
    {
        private Player player;
        public Level level;

        private int playerSpeed = 2;

        KeyboardState keyboardState;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {
            level = new Level(content);
            player = new Player(content, new Vector2(level.playerStartingLocation.X, level.playerStartingLocation.Y));

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
                player.nextDirection = "left";

            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player.nextDirection = "right";

            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                player.nextDirection = "up";

            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                player.nextDirection = "down";

            }

            float startingVelocityX = player.velocity.X;
            float startingVelocityY = player.velocity.Y;
            Vector2 startingPosition = player.position;

            player.Update();

            System.Diagnostics.Debug.WriteLine(player.nextDirection);

            Tile collisionTile = level.CheckCollision(new Rectangle((int)player.position.X, (int)player.position.Y, player.size, player.size));

            if (collisionTile == null)
            {
                player.nextDirection = "none";
            }
            else
            {
                player.velocity.X = startingVelocityX;
                player.velocity.Y = startingVelocityY;
                player.position = startingPosition;

                player.position += player.velocity;

                Tile collisionTile2 = level.CheckCollision(new Rectangle((int)player.position.X, (int)player.position.Y, player.size, player.size));

                if (collisionTile2 != null)
                {
                    // right
                    if (player.velocity.X > 0)
                    {
                        player.position.X = collisionTile2.position.X - player.size;

                        if (player.nextDirection == "right")
                        {
                            player.nextDirection = "none";
                        }

                    }
                    // left
                    else if (player.velocity.X < 0)
                    {
                        player.position.X = collisionTile2.position.X + collisionTile2.width;

                        if (player.nextDirection == "left")
                        {
                            player.nextDirection = "none";
                        }
                    }
                    // down
                    else if (player.velocity.Y > 0)
                    {
                        player.position.Y = collisionTile2.position.Y - player.size;

                        if (player.nextDirection == "down")
                        {
                            player.nextDirection = "none";
                        }
                    }
                    // up
                    else if (player.velocity.Y < 0)
                    {
                        player.position.Y = collisionTile2.position.Y + collisionTile2.width;

                        if (player.nextDirection == "up")
                        {
                            player.nextDirection = "none";
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }
    }
}