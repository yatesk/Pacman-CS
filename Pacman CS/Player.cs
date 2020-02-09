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


        public Player(ContentManager _content, Vector2 _startingLocation)
        {
            image = _content.Load<Texture2D>("pacman32");
            position = _startingLocation;
            velocity = new Vector2(0, 0);
        }

        public void Update()
        {
            position += velocity;


            //animation?
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position);
        }
    }
}
