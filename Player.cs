using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowersCS;
using LevelCS;
using EnemyCS;
using TreeTowerCS;

namespace PlayerCS
{
    public class Player
    {
        // basic player properties
        private int money = 50;
        private int lives = 30;
        // list of the players towers
        private List<Tower> towers = new List<Tower>();
        // tower texture
        private Texture2D towerTexture;
        // create a bullet texture
        private Texture2D bulletTexture;
        // create a level object reference
        private Level level;
        // type of tower to add
        private string newTowerType;
        // mouse states for player
        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame
        // get/sets
        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }
        public string NewTowerType
        {
            set { newTowerType = value; }
        }
        // constructor for the class
        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.level = level;
            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
        }
        // --Update method for the player--
        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            // I know this shit (next 4 lines) looks weird and totally redundant,
            // but it accounts for integer rounding when casting
            // floats to ints. (basically it doesnt mess up our position).
            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space.
            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space

            // checks to see if the user clicks
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                // if the cell is clear, adds a tower to the cell
                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }

            // loop through player towers and update them
            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }
            // Set the oldState so it becomes the state of the previous frame.
            oldState = mouseState; 
        }
        // End of --Update method for the player--

        // checks to see if a cell is clear
        private bool IsCellClear()
        {
            // Make sure tower is within limits
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.Width && cellY < level.Height;
            bool spaceClear = true;
            // loop through all towers to make sure there is no tower currently there.
            foreach (Tower tower in towers)
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;  // if a tower is there, dont place it
            }
            // check to see if the cell is a path cell
            bool onPath = (level.GetIndex(cellX, cellY) != 1);
            // if all checks are true, return true
            return inBounds && spaceClear && onPath;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }

        public void AddTower()
        {
            Tower towerToAdd = null;
            // switch/case for tower selection
            switch(newTowerType)
            {
                case "Arrow Tower":
                    towerToAdd = new TreeTower(towerTexture, bulletTexture, new Vector2(tileX, tileY));
                    break;
            }
            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= money)
            {
                towers.Add(towerToAdd);
                money -= towerToAdd.Cost;

                // Reset the newTowerType field.
                newTowerType = string.Empty;
            }
        }
    }
}
