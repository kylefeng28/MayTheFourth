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

namespace MayTheFourth.States {
    public class Screen : DrawableGameComponent {
        protected Game1 game;

        public Texture2D background;
        public Rectangle backgroundRect;

        public Screen(Game1 game) : base(game) {
            this.game = game;
        }

        public override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            backgroundRect = ViewportRectangle(game.GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(background, backgroundRect, Color.White);
        }

        public Rectangle ViewportRectangle(Viewport viewport) {
            return new Rectangle(0, 0, viewport.Width, viewport.Height);
        }

        public Rectangle CreateRectangle(Texture2D texture, Vector2 pos, float scale = 1f) {
            int x = (int) pos.X;
            int y = (int) pos.Y;
            int width = (int) (texture.Width * scale);
            int height = (int) (texture.Height * scale);
            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;
        }
    }

    public class TitleScreen : Screen {
        public Song mainTheme;

        public TitleScreen(Game1 game) : base(game) {
        }

        protected override void LoadContent() {
            ContentManager Content = game.Content;

            // mainTheme = Content.Load<Song>(""); // TODO
            background = Content.Load<Texture2D>("pixel"); // TODO
            
            base.LoadContent();
        }
    }
}
