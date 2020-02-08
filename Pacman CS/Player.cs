using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    public class Player
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D image;


        public Player(ContentManager _content, int _x, int _y)
        {
            image = _content.Load<Texture2D>("pacman32");
            position = new Vector2(_x, _y);
            velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position);
        }
    }
}
