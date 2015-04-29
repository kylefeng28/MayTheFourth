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
    public class BulletManager : DrawableGameComponent {
        protected Game1 game;
        protected Sprite sprite;

        private List<Bullet> bulletList = new List<Bullet>();
        public Texture2D bulletTexture;

        public BulletManager(Game1 game, Sprite sprite) : base(game) {
            this.game = game;
            this.sprite = sprite;
        }

        public override void Update(GameTime gameTime) {
            foreach (Bullet bullet in bulletList) {
                if (bullet.Enabled) {
                    bullet.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            foreach (Bullet bullet in bulletList) {
                if (bullet.Enabled) {
                    bullet.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        public void ShootStraight() {
            Bullet bullet = new Bullet(game);

            bullet.pos = sprite.pos;
            bullet.pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(sprite.rotation));
            bullet.pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(sprite.rotation));

            bullet.revolution = sprite.rotation;
            bullet.texture = bulletTexture;
            bullet.vel_max = 10f;

            bulletList.Add(bullet);
        }

    }
}
