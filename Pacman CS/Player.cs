﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Pacman_CS
{
    public class Player
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D[] image = new Texture2D[4];
        public int size = 32;

        public enum Directions { Up, Down, Left, Right, None };
        public Directions nextDirection = Directions.None;
        //public string nextDirection;
        private int playerSpeed = 2;

        private int frameCount = 0;
        private int spriteIndex = 0;

        public Vector2 origin;

        public Player(ContentManager _content, Vector2 _startingLocation, string pacmanNumber)
        {
            image[0] = _content.Load<Texture2D>(pacmanNumber + "-1");
            image[1] = _content.Load<Texture2D>(pacmanNumber + "-2");
            image[2] = _content.Load<Texture2D>(pacmanNumber + "-3");
            image[3] = _content.Load<Texture2D>(pacmanNumber + "-2");

            position = _startingLocation;
            velocity = new Vector2(0, 0);

            nextDirection = Directions.None;

            this.origin = new Vector2(image[0].Width / 2f, image[0].Height / 2f);
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

            // refactor animations
            frameCount += 1;

            if (frameCount % 40 < 10)
                spriteIndex = 0;
            else if (frameCount % 40 < 20)
                spriteIndex = 1;
            else if (frameCount % 40 < 30)
                spriteIndex = 2;
            else
                spriteIndex = 3;
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
                spriteBatch.Draw(image[spriteIndex], position + new Vector2(16, 16), null, Color.White, (float)(90 * (Math.PI/180)), origin, 1.0f, SpriteEffects.None, 0);

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
