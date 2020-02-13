using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    class ScoreBoard
    {
        private int numberOfPlayers;

        public int player1Score;
        public int player2Score;

        private int highScoreTest = 445543;

        private Texture2D player1Image;
        private Texture2D player2Image;
        private SpriteFont font;

        public ScoreBoard(ContentManager content, int _numberOfPlayers)
        {
            font = content.Load<SpriteFont>("scoreBoardFont");
            numberOfPlayers = _numberOfPlayers;

            player1Image = content.Load<Texture2D>("pacman1-1");
            player2Image = content.Load<Texture2D>("pacman2-1");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "HIGH SCORE: " + highScoreTest.ToString(), new Vector2(50, 0), Color.Black);

            spriteBatch.Draw(player1Image, new Vector2(800, 0));
            spriteBatch.DrawString(font, player1Score.ToString(), new Vector2(840, 0), Color.Black);

            if (numberOfPlayers > 1)
            {
                spriteBatch.Draw(player2Image, new Vector2(600, 0));
                spriteBatch.DrawString(font, player2Score.ToString(), new Vector2(640, 0), Color.Black);
            }
        }
    }
}
