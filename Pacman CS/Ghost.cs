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

        public enum Directions { Up, Down, Left, Right, None };
        public Directions nextDirection = Directions.None;

        private int ghostSpeed = 2;

        private int frameCount = 0;
        private int spriteIndex = 0;

        // Pacman can eat ghost if ghost is scared.
        public bool scared = false;

        public Vector2 origin;

        public Ghost(ContentManager _content, Vector2 _startingLocation)
        {
            image[0] = _content.Load<Texture2D>("ghost1");
            image[1] = _content.Load<Texture2D>("ghost2");


            position = _startingLocation;
            velocity = new Vector2(0, 0);

            nextDirection = Directions.None;


            origin = new Vector2(image[0].Width / 2f, image[0].Height / 2f);
        }

        public void Update()
        {

            //if (nextDirection == Directions.Left)
            //{
            //    velocity.X = -playerSpeed;
            //    velocity.Y = 0;
            //}
            //else if (nextDirection == Directions.Right)
            //{
            //    velocity.X = playerSpeed;
            //    velocity.Y = 0;
            //}
            //else if (nextDirection == Directions.Up)
            //{
            //    velocity.Y = -playerSpeed;
            //    velocity.X = 0;
            //}
            //else if (nextDirection == Directions.Down)
            //{
            //    velocity.Y = playerSpeed;
            //    velocity.X = 0;
            //}

            //position += velocity;



            // refactor animations
            frameCount += 1;

            if (frameCount % 60 < 30)
                spriteIndex = 0;
            else if (frameCount % 60 < 60)
                spriteIndex = 1;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                spriteBatch.Draw(image[spriteIndex], position);
            }
            else if (velocity.X < 0)
            {
                spriteBatch.Draw(image[spriteIndex], position + new Vector2(16, 16), null, Color.White, (float)(180 * (Math.PI / 180)), origin, 1.0f, SpriteEffects.None, 0);
            }
            else if (velocity.Y > 0)
            {
                spriteBatch.Draw(image[spriteIndex], position + new Vector2(16, 16), null, Color.White, (float)(90 * (Math.PI / 180)), origin, 1.0f, SpriteEffects.None, 0);

            }
            else if (velocity.Y < 0)
            {
                spriteBatch.Draw(image[spriteIndex], position + new Vector2(16, 16), null, Color.White, (float)(270 * (Math.PI / 180)), origin, 1.0f, SpriteEffects.None, 0);
            }
            else
                spriteBatch.Draw(image[spriteIndex], position);
        }
    }
}
