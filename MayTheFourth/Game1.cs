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
        public MillenniumFalcon millenniumFalcon;
        public XWing xwing;

        // Player and AI
        public Player player1;
        public Player player2;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
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
            titleScreen.Initialize();

            millenniumFalcon = new MillenniumFalcon(this);
            xwing = new XWing(this);

            player1 = new Player(this, millenniumFalcon, PlayerIndex.One);
            player1.Initialize();

            player2 = new Player(this, xwing, PlayerIndex.Two);
            player2.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Fonts.LoadContent(Content);
            stateManager.ChangeState(GameState.Title);

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
            if (io.pad1.Buttons.Back == ButtonState.Pressed || io.kb.IsKeyDown(Keys.F10))
                this.Exit();

            switch (stateManager.state) {
            case GameState.Title: {
                    titleScreen.Update(gameTime);
                    camera.pos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                    break;
                }
            case GameState.Playing: {
                    camera.Update(gameTime);
                    camera.Follow(player1.ship, player2.ship);
                    player1.Update(gameTime);
                    player2.Update(gameTime);
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
                    titleScreen.Draw(gameTime);
                    break;
                }
            case GameState.Playing: {
                    player1.Draw(gameTime);
                    player2.Draw(gameTime);
                    break;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
