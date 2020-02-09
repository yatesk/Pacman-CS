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
            //player = content.Load<Texture2D>("pacman32");
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                player.velocity.X = -playerSpeed;
                player.velocity.Y = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player.velocity.X = playerSpeed;
                player.velocity.Y = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                player.velocity.X = 0;
                player.velocity.Y = -playerSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                player.velocity.X = 0;
                player.velocity.Y = playerSpeed;
            }

            player.Update();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            player.Draw(spriteBatch);

            //spriteBatch.Draw(player, new Vector2(50, 50));
        }
    }
}
