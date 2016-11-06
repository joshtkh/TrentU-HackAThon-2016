using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerCS;
using LevelCS;
using EnemyCS;

namespace WaveCS
{
    public class Wave
    {
        private int numOfEnemies;   // num of enemies to spawn
        private int waveNumber;     // # wave you are on
        private float spawnTimer = 0;   // when an enemy should spawn
        private int enemiesSpawned = 0; // how many enemies have spawned
        private bool enemyAtEnd;    // Has an enemy reached the end of the path?
        private bool spawningEnemies;   // a check to see if enemies are spawning
        private Player player;
        private Level level;    // reference of the level
        private Texture2D enemyTexture; // texture reference for creating enemies
        private List<Enemy> enemies = new List<Enemy>();    // list of enemies

        public bool RoundOver
        {
            get { return enemies.Count == 0 && enemiesSpawned == numOfEnemies; }
        }
        public int RoundNumber
        {
            get { return waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public Wave(int waveNumber, int numOfEnemies, Level level, Player player, Texture2D enemyTexture)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;

            this.player = player;
            this.level = level;
            this.enemyTexture = enemyTexture;
        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(enemyTexture, level.Waypoints.Peek(), 50, 1, 0.5f);
            enemy.SetWaypoints(level.Waypoints);
            enemies.Add(enemy);
            spawnTimer = 0;
            // example alter to waves:
            //if (waveNumber == 5)
            //{
            //    float speed = 2.0f;


            //    enemy = new Enemy(enemyTexture,
            //        level.Waypoints.Peek(), 50, 1, speed);
            //    enemy.SetWaypoints(level.Waypoints);
            //}

            enemiesSpawned++;
        }

        public void Start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false;    // stop spawning when we reach max

            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (spawnTimer > 2) // every 2 seconds, spawn another enemy
                    AddEnemy(); 
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);

                // if an enemy "dies" we check if it died by a turret or by reaching the end
                if (enemy.IsDead)
                {
                    // if enemy still has health, but is tagged as "dead"
                    // than it is at the end of the path.
                    if (enemy.CurrentHealth > 0)
                    {
                        enemyAtEnd = true;
                        player.Lives -= 1;
                    }
                    else
                    {
                        // otherwise the enemy died to a tower, so money should be awarded
                        player.Money += enemy.BountyGiven;
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw all enemies
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
    }
}