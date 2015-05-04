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
        public Game1 game;
        public Sprite sprite;

        public BulletThread thread;
        private List<Bullet> bulletList = new List<Bullet>();

        public MoodLight moodLight;

        // Bullet properties
        public Texture2D bulletTexture;
        public Color bulletColor;
        public float bullet_vel_max = 10f;
        public int numBullets = 36;

        // Shooting properties
        public int shootDelay = 5;
        public int shootBurst = 5;
        public int shootBurstTimer = 0;
        public int shootTimer = 0;

        public SoundEffect shootSound;

        public BulletManager(Game1 game, Sprite sprite) : base(game) {
            this.game = game;
            this.sprite = sprite;
            this.thread = BulletThread.Linear;
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

            shootTimer--;

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

        public void Shoot(GameTime gameTime) {
            if (shootTimer <= 0) {
                shootBurstTimer--;
                if (shootBurstTimer <= 0) {
                    shootBurstTimer = shootBurst;
                    shootTimer = shootDelay;
                }

                float ang_pos = 0f;
                Vector2 pos = Vector2.Zero;
                Vector2 vel = Vector2.Zero;

                shootSound.Play();

                switch (thread) {
                case BulletThread.Linear: {
                        ang_pos = sprite.physics.yaw_pos;

                        pos = sprite.physics.pos;
                        pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                        pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));

                        vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                        vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                        Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                        break;
                    }
                case BulletThread.EnergyBurst: {
                        for (int ang = 0; ang <= 360; ang += 360 / numBullets) {
                            ang_pos = sprite.physics.yaw_pos + MathHelper.ToRadians(ang);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));

                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                        }
                        break;
                    }
                case BulletThread.Butterfly: {
                        for (int ang = 0; ang <= 360; ang += 360 / numBullets) {
                            ang_pos = sprite.physics.yaw_pos + MathHelper.ToRadians(ang);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));
                            vel.X = (float) (bullet_vel_max * Math.Sin(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));
                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (-bullet_vel_max * Math.Cos(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                        }
                        break;
                    }
                case BulletThread.DoubleEllipse: {
                        for (int ang = 0; ang <= 360; ang += 360 / numBullets) {
                            ang_pos = sprite.physics.yaw_pos + MathHelper.ToRadians(ang);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));
                            vel.X = (float) (-bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos));
                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (-bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                        }
                        break;
                    }
                case BulletThread.Spiral: {
                        for (int ang = 0; ang <= 360; ang += 360 / numBullets) {
                            ang_pos = sprite.physics.yaw_pos + MathHelper.ToRadians(ang);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos - gameTime.TotalGameTime.Milliseconds / 100f));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos - gameTime.TotalGameTime.Milliseconds / 100f));

                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, bulletColor);

                        }
                        break;
                    }
                case BulletThread.Sakura: {
                        if (moodLight == null) {
                            moodLight = new MoodLight(Color.Green); // Green makes cool firework patterns!
                        }

                        for (int ang = 0; ang <= 360; ang += 360 / 5) {
                            ang_pos = sprite.physics.yaw_pos + MathHelper.ToRadians(ang);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos - gameTime.TotalGameTime.Milliseconds / 100f));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos - gameTime.TotalGameTime.Milliseconds / 100f));

                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, moodLight.color);

                            pos = sprite.physics.pos;
                            pos.X += (float) (sprite.texture.Width / 2 * Math.Cos(ang_pos + gameTime.TotalGameTime.Milliseconds / 100f));
                            pos.Y += (float) (sprite.texture.Width / 2 * Math.Sin(ang_pos + gameTime.TotalGameTime.Milliseconds / 100f));

                            vel.X = (float) (bullet_vel_max * Math.Cos(ang_pos));
                            vel.Y = (float) (bullet_vel_max * Math.Sin(ang_pos));

                            Add(ang_pos, pos, vel, bulletTexture, moodLight.color);
                        }
                        break;
                    }
                }


            }
        }

        public void Add(float ang_pos, Vector2 pos, Vector2 vel, Texture2D texture, Color color) {
            Bullet bullet = new Bullet(game);

            bullet.physics.yaw_pos = ang_pos;
            bullet.physics.pos = pos;
            bullet.physics.vel = vel;
            bullet.texture = texture;
            bullet.baseColor = color;

            bulletList.Add(bullet);
        }

    }
}
