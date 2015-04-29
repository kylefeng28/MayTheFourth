using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MayTheFourth.Sprites;
using MayTheFourth.States;

namespace MayTheFourth {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        // Graphics
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Camera camera;

        // States
        public GameStateManager stateManager;
        public IOManager io;

        // Screens
        public TitleScreen titleScreen;

        // Objects
        public MillenniumFalcon player;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800 * 2;
            graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            camera = new Camera(this);

            stateManager = new GameStateManager(this);
            io = new IOManager(this);

            titleScreen = new TitleScreen(this);
            player = new MillenniumFalcon(this);

            titleScreen.Initialize();
            player.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ChangeState(GameState.Title);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            io.Update(gameTime);

            // Allows the game to exit
            if (io.pad1.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (stateManager.state) {
            case GameState.Title: {
                    titleScreen.Update(gameTime);
                    player.Update(gameTime);
                    break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, camera.TransformMatrix());

            switch (stateManager.state) {
            case GameState.Title: {
                    camera.Follow(player);
                    titleScreen.Draw(spriteBatch, gameTime);
                    player.Draw(gameTime);
                    break;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void ChangeState(GameState newState) {
            stateManager.state = newState;

            switch (newState) {
            case GameState.Title: {
                    // MediaPlayer.Play(titleScreen.mainTheme);
                    break;
                }
            }
        }
    }
}
