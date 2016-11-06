using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EnemyCS;
using AmmoCS;
using SpritesCS;
using TowersCS;

namespace TowerDefenseWindows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Start here!!!
        // create the level object
        Level level = new Level();
        // create an enemy object
        Enemy enemy1;
        // tower object next!!
        Tower tower;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // added content:
            // this sets the background size
            graphics.PreferredBackBufferWidth = level.Width * 32;   // mul by multiple of 32 to map array space value to screen space
            graphics.PreferredBackBufferHeight = level.Height * 32;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Added content:
            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            level.AddTexture(grass); // add grass first!
            level.AddTexture(path);  // add paths afterwards so grass tiles get replaces with paths.
            // Enemys!!
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
            enemy1 = new Enemy(enemyTexture, Vector2.Zero, 100, 10, 0.5f);
            enemy1.SetWaypoints(level.Waypoints);
            // TOWERS! :D
            Texture2D towerTexture = Content.Load<Texture2D>("arrowtower");
            tower = new Tower(towerTexture, Vector2.Zero);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // enemies!
            enemy1.Update(gameTime);

            // tower target updates
            if (tower.Target == null)
            {
                List<Enemy> enemies = new List<Enemy>();
                enemies.Add(enemy1);

                tower.GetClosestEnemy(enemies);
            }
            tower.Update(gameTime);
            // end of tower target updates

            // ??????
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // draw the level
            level.Draw(spriteBatch);
            // draw an enemy
            enemy1.Draw(spriteBatch);
            // draw a tower
            //tower.Draw(spriteBatch);
            spriteBatch.End();

            // not sure what this is, but it was initiazlied like this so dont touch it.
            base.Draw(gameTime);
        }
    }
}
