using Microsoft.Xna.Framework;

namespace Pacman_CS
{
    class Pellet
    {
        public PelletType type;

        public int width;
        public int height;
        public Vector2 position;

        public enum PelletType { Pellet, PowerPellet };

        public Pellet(Vector2 position, int width, int height, PelletType type)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.type = type;
        }
    }
}
