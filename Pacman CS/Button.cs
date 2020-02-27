using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    // refactor
    class Button
    {
        private Texture2D buttonTexture;
        private SpriteFont font;
        private Vector2 position;

        private string text;
        private string textureName;
        private Color textColor;

        private Texture2D middleTexture;
        private Vector2 middleTexturePosition;

        private bool mouseOver;

        public Button(string buttonTextureName, string fontName, Vector2 _position, string _text, Color _textColor, ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>(buttonTextureName);
            font = content.Load<SpriteFont>(fontName);
            position = _position;
            text = _text;
            textColor = _textColor;
        }

        public Button(string buttonTextureName, Texture2D _middleTexture, Vector2 _position, ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>(buttonTextureName);
            position = _position;

            middleTexture = _middleTexture;
            middleTexturePosition = new Vector2(position.X + (buttonTexture.Width / 2) - (middleTexture.Width / 2), position.Y + (buttonTexture.Height / 2) - (middleTexture.Height / 2));
        }

        public Button(string _textureName, Vector2 _position, ContentManager content)
        {
            textureName = _textureName;
            buttonTexture = content.Load<Texture2D>(textureName);
            position = _position;
        }

        public string getText()
        {
            if (!string.IsNullOrEmpty(text))
                return text;
            else
                return textureName;
        }
        public bool Clicked(int _x, int _y)
        {
            if (_x >= position.X && _x <= position.X + buttonTexture.Width && _y >= position.Y && _y <= position.Y + buttonTexture.Height)
                return true;
            else
                return false;
        }

        public void Update(int _x, int _y)
        {
            if (_x >= position.X && _x <= position.X + buttonTexture.Width && _y >= position.Y && _y <= position.Y + buttonTexture.Height)
                mouseOver = true;
            else
                mouseOver = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mouseOver)
            {
                spriteBatch.Draw(buttonTexture, position, Color.Gray);
            }
            else
            {
                spriteBatch.Draw(buttonTexture, position);
            }

            if (middleTexture != null)
            { 
                
                spriteBatch.Draw(middleTexture, middleTexturePosition);
            }

            if (!string.IsNullOrEmpty(text))
            {
                float x = position.X + (buttonTexture.Width / 2) - (font.MeasureString(text).X / 2);
                float y = position.Y + (buttonTexture.Height / 2) - (font.MeasureString(text).Y / 2);

                spriteBatch.DrawString(font, text, new Vector2(x, y), textColor);
            }
        }
    }
}