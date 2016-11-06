using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveCS;
using LevelCS;
using EnemyCS;
using PlayerCS;

namespace WaveManagerCS
{
    public class WaveManager
    {
        private int numberOfWaves; // How many waves the game will have
        private float timeSinceLastWave; // How long since the last wave ended
        private Queue<Wave> waves = new Queue<Wave>(); // A queue of all our waves
        private Texture2D enemyTexture; // The texture used to draw the enemies
        private bool waveFinished = false; // Is the current wave over?
        private Level level; // A reference to our level class
        // get/sets
        public Wave CurrentWave // Get the wave at the front of the queue
        {
            get { return waves.Peek(); }
        }
        public List<Enemy> Enemies // Get a list of the current enemeies
        {
            get { return CurrentWave.Enemies; }
        }
        public int Round // Returns the wave number
        {
            get { return CurrentWave.RoundNumber + 1; }
        }
        // class constructor
        public WaveManager(Player player, Level level, int numberOfWaves, Texture2D enemyTexture)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemyTexture = enemyTexture;

            this.level = level;

            for (int i = 0; i < numberOfWaves; i++)
            {
                // these two lines alter the num of enemies each wave has
                // so right now: every 6 waves, 6 more enemies spawn.
                // expand upon this for bosses/special levels.
                int initialNumerOfEnemies = 6;
                int numberModifier = (i / 6) + 1;

                Wave wave = new Wave(i, initialNumerOfEnemies * numberModifier, level, player, enemyTexture);

                waves.Enqueue(wave);
            }

            StartNextWave();
        }
        // method that will start the next wave
        private void StartNextWave()
        {
            if (waves.Count > 0) // If there are still waves left
            {
                waves.Peek().Start(); // Start the next one

                timeSinceLastWave = 0; // Reset timer
                waveFinished = false;
            }
        }
        // update function
        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime);

            if (CurrentWave.RoundOver) // update
            {
                waveFinished = true;
            }
            if (waveFinished)
            {
                timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds; // Start the timer
            }
            if (timeSinceLastWave > 30.0f) // if 30 seconds has passed
            {
                waves.Dequeue();    // remove finished wave
                StartNextWave();    // start the next wave
            }
        }
        // draw function
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
