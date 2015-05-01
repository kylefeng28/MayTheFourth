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
    public enum BulletThread {
        Straight,
        EnergyBurst,
        Spiral,
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
            this.thread = BulletThread.EnergyBurst;
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
            float ang_pos = 0f;
            Vector2 pos = Vector2.Zero;
            Vector2 vel = Vector2.Zero;
            Texture2D texture;

            switch (thread) {
            case BulletThread.Straight: {
                    ang_pos = sprite.physics.ang_pos;

                    pos = sprite.physics.pos;
                    pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                    pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));

                    float vel_max = 10f;
                    vel.X = (float) (vel_max * Math.Cos(ang_pos));
                    vel.Y = (float) (vel_max * Math.Sin(ang_pos));

                    texture = bulletTexture;
                    Add(ang_pos, pos, vel, texture);
                    break;
                }
            case BulletThread.EnergyBurst: {
                    const int NUM_BULLETS = 36;
                    for (int ang = 0; ang <= 360; ang += 360 / NUM_BULLETS) {
                        ang_pos = MathHelper.ToRadians(ang);

                        pos = sprite.physics.pos;
                        pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(sprite.physics.ang_pos + ang_pos));
                        pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(sprite.physics.ang_pos + ang_pos));

                        float vel_max = 10f;
                        vel = new Vector2();
                        vel.X = (float) (vel_max * Math.Cos(sprite.physics.ang_pos + ang_pos));
                        vel.Y = (float) (vel_max * Math.Sin(sprite.physics.ang_pos + ang_pos));

                        texture = bulletTexture;
                        Add(ang_pos, pos, vel, texture);
                    }
                    break;
                }
            case BulletThread.Spiral: {
                    const int NUM_BULLETS = 36;
                    for (int ang = 0; ang <= 360; ang += 360 / NUM_BULLETS) {
                        ang_pos = MathHelper.ToRadians(ang);

                        pos = sprite.physics.pos;
                        pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(sprite.physics.ang_pos + ang_pos));
                        pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(sprite.physics.ang_pos + ang_pos));

                        float vel_max = 10f;
                        vel = new Vector2();
                        vel.X = (float) (vel_max * Math.Sin(sprite.physics.ang_pos + ang_pos));
                        vel.Y = (float) (vel_max * Math.Cos(sprite.physics.ang_pos + ang_pos));

                        texture = bulletTexture;
                        Add(ang_pos, pos, vel, texture);
                    }
                break;
                }
            }
        }

        public void Add(float ang_pos, Vector2 pos, Vector2 vel, Texture2D texture) {
            Bullet bullet = new Bullet(game);

            bullet.physics.ang_pos = ang_pos;
            bullet.physics.pos = pos;
            bullet.physics.vel = vel;
            bullet.texture = texture;

            bulletList.Add(bullet);
        }

    }
}
