using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseWindows
{
    public class Level
    {
        int[,] map = new int[,]
        {
            {0, 0, 1, 0, 0, 0, 0, 0 },
            {0, 0, 1, 1, 0, 0, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 0, 0, 1, 0, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 1, 1, 0, 0, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0 },
            {0, 0, 1, 1, 1, 1, 1, 1 },
        };

        // get/set for width and height of our map
        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }

        private List<Texture2D> tileTextures = new List<Texture2D>();

        // method to add textures to the list
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        // Draw method
        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++) // loop for x axis
            {
                for (int y = 0; y < Height; y++) // loop for y axis
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];
                    batch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }
            }
        }
    }
}
