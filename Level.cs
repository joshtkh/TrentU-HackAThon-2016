using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelCS
{
    public class Level
    {
        // creates the map matrix
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
        // WAYPOINTS FOR PATHWAY
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        // param-less constructor for level, values hard coded in FOR NOW.
        public Level()
        {
            waypoints.Enqueue(new Vector2(2, 0) * 32);
            waypoints.Enqueue(new Vector2(2, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 7) * 32);
            waypoints.Enqueue(new Vector2(7, 7) * 32);
        }
        // get/set for our queue
        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }

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

        // returns the index of the requested cell
        public int GetIndex(int cellX, int cellY)
        {
            // It needs to be Width - 1 and Height - 1.
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return map[cellY, cellX];
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
                    batch.Draw(texture, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                }
            }
        }
    }
}
