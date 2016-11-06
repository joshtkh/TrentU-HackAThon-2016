using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp1
{
         public class Wave
        {
            private int numOfEnemies;
            private int waveNumber;

            private float spawnTimer = 0;
            private int enemiesSpawned = 0;

            private bool enemyAtEnd;
            private bool spawningEnemies;

            private Player player;
            private Level level;

            private Texture2D enemyTexture;

            private List<Enemy> enemies = new List<Enemy>();

            public bool RoundOver
            {
                get
                {
                    return enemies.Count == 0 && enemiesSpawned == numOfEnemies;
                }
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

            public Wave(int waveNumber, int numOfEnemies,
                Player player, Level level, Texture2D enemyTexture)
            {
                this.waveNumber = waveNumber;
                this.numOfEnemies = numOfEnemies;

                this.player = player;
                this.level = level;

                this.enemyTexture = enemyTexture;
            }

            private void AddEnemy()
            {
                Enemy enemy = new Enemy(enemyTexture,
                    level.Waypoints.Peek(), 50, 1, 0.5f);

                enemy.SetWaypoints(level.Waypoints);

                enemies.Add(enemy);

                spawnTimer = 0;
                enemiesSpawned++;
            }

            public void Start()
            {
                spawningEnemies = true;
            }

            public void Update(GameTime gameTime)
            {
                if (enemiesSpawned == numOfEnemies)
                    spawningEnemies = false;

                if (spawningEnemies)
                {
                    spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (spawnTimer > 2)
                        AddEnemy();
                }

                for (int i = 0; i < enemies.Count; i++)
                {
                    Enemy enemy = enemies[i];
                    enemy.Update(gameTime);

                    if (enemy.IsDead)
                    {
                        if (enemy.CurrentHealth > 0)
                        {
                            enemyAtEnd = true;
                            player.Lives -= 1;
                        }

                        else
                        {
                            player.Money += enemy.BountyGiven;
                        }

                        enemies.Remove(enemy);
                        i--;
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                foreach (Enemy enemy in enemies)
                    enemy.Draw(spriteBatch);
            }
        }
    }





