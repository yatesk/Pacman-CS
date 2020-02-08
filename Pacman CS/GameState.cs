using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    class GameState : State
    {
        private Texture2D player;
        public Level level;

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {
            level = new Level(content);

            LoadContent();
        }

        public override void LoadContent()
        {
            //player = content.Load<Texture2D>("pacman32");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            //spriteBatch.Draw(player, new Vector2(50, 50));
        }
    }
}
