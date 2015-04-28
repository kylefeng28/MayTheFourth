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

namespace MayTheFourth.Sprites {
    public class Sprite : DrawableGameComponent {
        private Game1 game;

        public Texture2D texture;
        public Rectangle drawBox;
        public float scale = 1f;

        public Vector2 pos = Vector2.Zero;
        public Vector2 vel = Vector2.Zero;
        public Vector2 acc = Vector2.Zero;

        public float vel_max = 1f;

        public Sprite(Game1 game) : base(game) {
            this.game = game;
        }

        public override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            MoveWithKeyboard(game.io.kb, game.io.kb_old);

            Friction();
            
            Verlet();

            drawBox = CreateRectangle(texture, pos, scale);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = game.spriteBatch;

            spriteBatch.Draw(texture, drawBox, Color.White);

            base.Draw(gameTime);
        }


        public void MoveWithKeyboard(KeyboardState kb, KeyboardState kb_old) {
            if (kb.IsKeyDown(Keys.D)) {
                MoveRight();
            }
            if (kb.IsKeyDown(Keys.A)) {
                MoveLeft();
            }
        }

        public void MoveWithGamePad(GamePadState pad, GamePadState pad_old) {
            if (pad.ThumbSticks.Left.X > 0)
                MoveRight(1);
        }

        public void MoveRight(int dir = 1) {
            // vel.X = dir * vel_max;
            if (Math.Abs(vel.X) < vel_max) {
                acc.X = dir;
            }
            else {
                acc.X = 0;
            }
        }

        public void MoveLeft(int dir = 1) {
            MoveRight(-dir);
        }

        public void Friction() {
            int dir = Math.Sign(vel.X);
            
            if (Math.Abs(vel.X) > 0) {
                // acc.X = -dir * 0.5f;
                vel.X *= 0.9f;
            }

        }

        public void Verlet(float dt = 1f) {
            vel += acc * dt;
            pos += vel * dt;
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
}
