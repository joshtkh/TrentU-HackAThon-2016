using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EnemyCS;
using BulletCS;
using SpritesCS;
using TowersCS;
using LevelCS;
using PlayerCS;
using WaveCS;
using WaveManagerCS;
using MenuBarCS;
using ButtonCS;

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
        // create a wave object
        WaveManager waveManager;
        // create a player object
        Player player;
        // create a toolbar
        Toolbar toolbar;
        // create a button
        Button treeButton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // added content:
            // this sets the background size
            graphics.PreferredBackBufferWidth = level.Width * 32;   // mul by multiple of 32 to map array space value to screen space
            graphics.PreferredBackBufferHeight = 32 + level.Height * 32;
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
            // Textures:
            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            Texture2D towerTexture = Content.Load<Texture2D>("arrowtower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
            Texture2D topBar = Content.Load<Texture2D>("toolbar");
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            // The "Normal" texture for the tower button.
            Texture2D btnNormal = Content.Load<Texture2D>("GUI\\arrow-button");
            // The "MouseOver" texture for the tower button.
            Texture2D btnHover = Content.Load<Texture2D>("GUI\\arrow-hover");
            // The "Pressed" texture for the tower button.
            Texture2D btnPressed = Content.Load<Texture2D>("GUI\\arrow-pressed");

            level.AddTexture(grass); // add grass first!
            level.AddTexture(path);  // add paths afterwards so grass tiles get replaces with paths.

            // players!
            player = new Player(level, towerTexture, bulletTexture);
            // Initializing the waves!
            waveManager = new WaveManager(player, level, 24, enemyTexture);
            // toolbar
            toolbar = new Toolbar(topBar, font, new Vector2(0, level.Height * 32));
            // Initialize the button.
            treeButton = new Button(btnNormal, btnHover, btnPressed, new Vector2(0, level.Height * 32));
            // create an event for button click
            treeButton.Clicked += new EventHandler(treeButton_Clicked);
        }
        // 
        private void treeButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Arrow Tower";
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
            // wave of enemies!
            waveManager.Update(gameTime);
            player.Update(gameTime, waveManager.Enemies);
            //Update the arrow button.
            treeButton.Update(gameTime);

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
            // start
            spriteBatch.Begin();

            // draw the level
            level.Draw(spriteBatch);
            // draw an enemy
            waveManager.Draw(spriteBatch);
            // draw a tower
            player.Draw(spriteBatch);
            // draw the toolbar
            toolbar.Draw(spriteBatch, player);
            // and then our buttons.
            treeButton.Draw(spriteBatch);
            // end
            spriteBatch.End();

            // not sure what this is, but it was initiazlied like this so dont touch it.
            base.Draw(gameTime);
        }
    }
}
