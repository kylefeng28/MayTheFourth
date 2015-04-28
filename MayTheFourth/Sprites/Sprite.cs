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
        public float rotation = 0f;

        public Vector2 pos = Vector2.Zero;
        public Vector2 vel = Vector2.Zero;
        public Vector2 acc = Vector2.Zero;

        public float vel_max = 1f;
        public float acc_max = 1f;

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

            Vector2 origin = GetOrigin(texture);
            spriteBatch.Draw(texture, drawBox, null, Color.White, rotation, origin, SpriteEffects.None, 0f);

            base.Draw(gameTime);
        }


        public void MoveWithKeyboard(KeyboardState kb, KeyboardState kb_old) {
            /*
            if (kb.IsKeyDown(Keys.D)) {
                MoveRight();
            }
            if (kb.IsKeyDown(Keys.A)) {
                MoveLeft();
            }
             */
            if (kb.IsKeyDown(Keys.W)) Forward(10);
            if (kb.IsKeyDown(Keys.S)) Forward(-10);
            if (kb.IsKeyDown(Keys.D)) Turn(10);
            if (kb.IsKeyDown(Keys.A)) Turn(-10);
        }

        public void MoveWithGamePad(GamePadState pad, GamePadState pad_old) {
            /*
             if (pad.ThumbSticks.Left.X > 0)
                MoveRight(1);
             */
        }

        public void Forward(int dir = 1) {
            if (vel.Length() < vel_max) {
                acc.X = (float) (dir * acc_max * Math.Cos(rotation));
                acc.Y = (float) (dir * acc_max * Math.Sin(rotation));
            }
            else {
                acc.X = 0;
                acc.Y = 0;
            }
        }

        public void Turn(float ang = 1) {
            rotation += (float) (ang / 180 * Math.PI);
        }

        public void Friction() {
            int dir = Math.Sign(vel.X);
            
            if (Math.Abs(vel.X) > 0) {
                // acc.X = -dir * 0.5f;
                vel *= 0.9f;
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

        public Vector2 GetOrigin(Texture2D texture) {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}
