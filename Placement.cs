using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static WebApp1.Tower1;

namespace WebApp1
{
    public class Player
    {
        private int money = 50;
        private int lives = 30;

        private Texture2D towerTexture;

        private List<Tower> towers = new List<Tower>();

        private MouseState mouseState; 
        private MouseState oldState; 

        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        private Level level;

        public int Money
        {
            get { return money; }
        }
        public int Lives
        {
            get { return lives; }
        }

        public Player(Level level, Texture2D towerTexture)
        {
            this.level = level;
            this.towerTexture = towerTexture;
        }

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 &&
                cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towers)
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 32);
            cellY = (int)(mouseState.Y / 32); 

            tileX = cellX * 32; 
            tileY = cellY * 32; 

            if (mouseState.LeftButton == ButtonState.Released
                && oldState.LeftButton == ButtonState.Pressed)
            {
                if (IsCellClear())
                {
                    Tower tower = new Tower(towerTexture, new Vector2(tileX, tileY));
                    towers.Add(tower);
                }
            }

            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}