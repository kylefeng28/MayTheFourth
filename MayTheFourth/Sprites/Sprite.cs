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
    public abstract class Sprite : DrawableGameComponent {
        public Game1 game;

        // Graphics
        public Texture2D texture;
        public Rectangle drawBox;
        public Color color = Color.White;
        public float scale = 1f;
        public float rotation = 0f;

        public Physics physics = new Physics();

        public float vel_max; // = 1f;
        public float acc_max; // = 1f;

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
            physics.Verlet((float) gameTime.ElapsedGameTime.Milliseconds / 16f);
            rotation = physics.yaw_pos;

            drawBox = CreateRectangle(texture, physics.pos, scale);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = game.spriteBatch;

            Vector2 origin = GetOrigin(texture);
            spriteBatch.Draw(texture, drawBox, null, color, rotation, origin, SpriteEffects.None, 0f);

            base.Draw(gameTime);
        }

        public void Forward(int dir = 1) {
            if (physics.vel.Length() < vel_max) {
                physics.acc.X = (float) (Math.Sign(dir) * acc_max * Math.Cos(physics.yaw_pos));
                physics.acc.Y = (float) (Math.Sign(dir) * acc_max * Math.Sin(physics.yaw_pos));
            }
            else {
                physics.acc.X = 0;
                physics.acc.Y = 0;
            }
        }

        public void TurnYaw(float ang = 1) {
            physics.yaw_vel = (float) (MathHelper.ToRadians(ang));
        }

        public void Roll(float ang = 1) {
            physics.acc.X = (float) (ang * Math.Cos(physics.yaw_pos + MathHelper.PiOver2));
            physics.acc.Y = (float) (ang * Math.Sin(physics.yaw_pos + MathHelper.PiOver2));
        }

        public void Friction() {
            int dir = Math.Sign(physics.vel.X);
            
            if (Math.Abs(physics.vel.Length()) > 0) {
                physics.vel *= 0.9f;
            }

            if (Math.Abs(physics.yaw_vel) > 0) {
                physics.yaw_vel *= 0.9f;
            }
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
