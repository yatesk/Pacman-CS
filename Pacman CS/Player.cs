using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Pacman_CS
{
    public class Player
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D image;
        public int size = 32;

        public enum Directions { Up, Down, Left, Right, None };
        public Directions nextDirection = Directions.None;
        //public string nextDirection;
        private int playerSpeed = 2;



        public Vector2 origin;


        public Player(ContentManager _content, Vector2 _startingLocation)
        {
            image = _content.Load<Texture2D>("pacman2");
            position = _startingLocation;
            velocity = new Vector2(0, 0);

            nextDirection = Directions.None;


            this.origin = new Vector2(image.Width / 2f, image.Height / 2f);
        }

        public void Update()
        {

            if (nextDirection == Directions.Left)
            {
                velocity.X = -playerSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == Directions.Right)
            {
                velocity.X = playerSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == Directions.Up)
            {
                velocity.Y = -playerSpeed;
                velocity.X = 0;
            }
            else if (nextDirection == Directions.Down)
            {
                velocity.Y = playerSpeed;
                velocity.X = 0;
            }

            position += velocity;

            //animation?
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                spriteBatch.Draw(image, position);
            }
            else if (velocity.X < 0)
            {
                spriteBatch.Draw(image, position + new Vector2(16, 16), null, Color.White, (float)(180 * (Math.PI / 180)), origin, 1.0f, SpriteEffects.None, 0);
            }
            else if (velocity.Y > 0)
            {
                spriteBatch.Draw(image, position + new Vector2(16, 16), null, Color.White, (float)(90 * (Math.PI/180)), origin, 1.0f, SpriteEffects.None, 0);

            }
            else if (velocity.Y < 0)
            {
                spriteBatch.Draw(image, position + new Vector2(16, 16), null, Color.White, (float)(270 * (Math.PI / 180)), origin, 1.0f, SpriteEffects.None, 0);
            }
            else
                spriteBatch.Draw(image, position);
        }
    }
}
