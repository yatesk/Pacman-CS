using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Pacman_CS
{
    class Level
    {
        public void LoadLevel()
        {

        }

        public List<char[]> LoadTextFile(List<char[]> level)
        {
            String line;
            FileStream fsSource = new FileStream("level1.txt", FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fsSource))
            {
                while ((line = sr.ReadLine()) != null)
                    level.Add(line.ToCharArray());
            }
            return level;
        }
    }
}
