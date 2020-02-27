using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pacman_CS
{
    public class Ghost
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D[] image = new Texture2D[2];
        public int size = 32;

        public enum Directions { Up, Down, Left, Right,};
        public Directions nextDirection = Directions.Up;

        private int ghostSpeed = 2;

        private int frameCount = 0;
        private int spriteIndex = 0;

        // Pacman can eat ghost if ghost is scared.
        public bool scared = false;

        static Random random = new Random();

        public Vector2 origin;

        public Ghost(ContentManager _content, Vector2 _startingLocation)
        {
            image[0] = _content.Load<Texture2D>("ghost1");
            image[1] = _content.Load<Texture2D>("ghost2");

            position = _startingLocation;
            velocity = new Vector2(0, 0);

            nextDirection = Directions.Up;

            origin = new Vector2(image[0].Width / 2f, image[0].Height / 2f);
        }

        public void Update()
        {
            if (nextDirection == Directions.Left)
            {
                velocity.X = -ghostSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == Directions.Right)
            {
                velocity.X = ghostSpeed;
                velocity.Y = 0;
            }
            else if (nextDirection == Directions.Up)
            {
                velocity.Y = -ghostSpeed;
                velocity.X = 0;
            }
            else if (nextDirection == Directions.Down)
            {
                velocity.Y = ghostSpeed;
                velocity.X = 0;
            }

            System.Diagnostics.Debug.WriteLine(nextDirection);

            position += velocity;

            // refactor animations
            frameCount += 1;

            if (frameCount % 60 < 30)
                spriteIndex = 0;
            else if (frameCount % 60 < 60)
                spriteIndex = 1;
        }

        public Directions RandomDirection()
        {
            int rand = random.Next(0, 4);

            if (rand == 0)
                return Directions.Up;
            else if (rand == 1)
                return Directions.Down;
            else if (rand == 2)
                return Directions.Left;
            else
                return Directions.Right;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image[spriteIndex], position);
        }
    }
}
