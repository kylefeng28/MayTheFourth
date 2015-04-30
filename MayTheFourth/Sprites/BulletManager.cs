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
    public class BulletThread {
        protected Game1 game;
        protected Sprite sprite;
        protected BulletManager bulletManager;

        public float ang_pos;
        public Vector2 pos = Vector2.Zero;
        public Vector2 vel = Vector2.Zero;
        public Texture2D texture;

        public BulletThread(Game1 game, Sprite sprite, BulletManager bulletManager) {
            this.game = game;
            this.sprite = sprite;
            this.bulletManager = bulletManager;
        }

        public void Shoot() {
            bulletManager.Add(ang_pos, pos, vel, texture);
        }
    }

    public class BulletThreadStraight : BulletThread {
        public BulletThreadStraight(Game1 game, Sprite sprite, BulletManager bulletManager) : base(game, sprite, bulletManager) {
            ang_pos = sprite.ang_pos;

            pos = sprite.pos;
            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));

            float vel_max = 10f;
            vel.X = (float) (vel_max * Math.Cos(ang_pos));
            vel.Y = (float) (vel_max * Math.Sin(ang_pos));

            texture = bulletManager.bulletTexture;
        }
    }

        public class  BulletThreadEnergyBurst : BulletThread {
            public BulletThreadEnergyBurst(Game1 game, Sprite sprite, BulletManager bulletManager) : base(game, sprite, bulletManager) {
            for (int ang = 0; ang <= 360; ang += 10) {
                ang_pos = MathHelper.ToRadians(ang);

                pos = sprite.pos;
                pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));

                float vel_max = 10f;
                vel = new Vector2();
                vel.X = (float) (vel_max * Math.Cos(ang_pos));
                vel.Y = (float) (vel_max * Math.Sin(ang_pos));

                texture = bulletManager.bulletTexture;
            }
        }
    }

    public class BulletManager : DrawableGameComponent {
        protected Game1 game;
        protected Sprite sprite;

        public BulletThread thread;

        private List<Bullet> bulletList = new List<Bullet>();
        public Texture2D bulletTexture;

        public BulletManager(Game1 game, Sprite sprite) : base(game) {
            this.game = game;
            this.sprite = sprite;
            this.thread = new BulletThread(game, sprite, this);
        }

        public override void Update(GameTime gameTime) {
            foreach (Bullet bullet in bulletList.Reverse<Bullet>()) {
                if (bullet.Enabled) {
                    bullet.Update(gameTime);
                }
                else {
                    bulletList.Remove(bullet);
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

        public void Shoot() {
            thread.Shoot();
        }

        private void Add(float ang_pos, Vector2 pos, Vector2 vel, Texture2D texture) {
            Bullet bullet = new Bullet(game);

            bullet.ang_pos = ang_pos;
            bullet.pos = pos;
            bullet.vel = vel;
            bullet.texture = texture;

            bulletList.Add(bullet);
        }

    }
}
