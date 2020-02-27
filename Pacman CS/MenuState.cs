using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pacman_CS
{
    class MenuState : State
    {
        private List<Button> buttons;

        MouseState previousMouseState;
        MouseState currentMouseState;

        private SpriteFont titleFont;

        private string gameName;

        public MenuState(Game1 game, ContentManager content) : base(game, content)
        {
            gameName = "Pacman";
        }

        public override void LoadContent()
        {
            titleFont = content.Load<SpriteFont>("titleFont");

            buttons = new List<Button>
            {
                new Button("button250-100", "buttonFont", new Vector2((Game1.screenWidth / 2) - 125, (Game1.screenHeight / 2) - 100), "New Game", Color.Black, content),
                new Button("button250-100", "buttonFont", new Vector2((Game1.screenWidth / 2) - 125, (Game1.screenHeight / 2)), "Options", Color.Black, content),
                new Button("button250-100", "buttonFont", new Vector2((Game1.screenWidth / 2) - 125, (Game1.screenHeight / 2) + 100), "High Scores", Color.Black, content),
                new Button("button250-100", "buttonFont", new Vector2((Game1.screenWidth / 2) - 125, (Game1.screenHeight / 2) + 200), "Quit", Color.Black, content)
            };
        }

        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                foreach (var button in buttons)
                {
                    if (button.Clicked(currentMouseState.X, currentMouseState.Y))
                    {
                        switch (button.getText())
                        {
                            case "New Game":
                                buttons.Remove(button);
                                buttons.Add(new Button("1player", new Vector2((Game1.screenWidth / 2) - 125, (Game1.screenHeight / 2) - 100), content));
                                buttons.Add(new Button("2player", new Vector2((Game1.screenWidth / 2), (Game1.screenHeight / 2) - 100), content));
                                return;

                            case "Options":
                                System.Diagnostics.Debug.WriteLine("Options Button Clicked");
                                break;

                            case "High Scores":
                                System.Diagnostics.Debug.WriteLine("High Scores Button Clicked");
                                break;

                            case "Quit":
                                game.Exit();
                                break;

                            case "1player":
                                game.ChangeState(new GameState(game, content, 1));
                                break;

                            case "2player":
                                game.ChangeState(new GameState(game, content, 2));
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (var button in buttons)
                {
                    button.Update(currentMouseState.X, currentMouseState.Y);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float x = Game1.screenWidth / 2 - (titleFont.MeasureString(gameName).X / 2);

            spriteBatch.DrawString(titleFont, gameName, new Vector2(x, 100), Color.Black);

            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
