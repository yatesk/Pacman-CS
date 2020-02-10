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
        public int size = 32;

        public string nextDirection;
        private int playerSpeed = 2;

        public Player(ContentManager _content, Vector2 _startingLocation)
        {
            image = _content.Load<Texture2D>("pacman32");
            position = _startingLocation;
            velocity = new Vector2(0, 0);

            nextDirection = "none";
        }

        public void Update()
        {

            if (nextDirection == "left")
            {
                velocity.X = -playerSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == "right")
            {
                velocity.X = playerSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == "up")
            {
                velocity.Y = -playerSpeed;
                velocity.X = 0;
            }
            else if (nextDirection == "down")
            {
                velocity.Y = playerSpeed;
                velocity.X = 0;
            }

            position += velocity;

            //animation?
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position);
        }
    }
}
