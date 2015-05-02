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
    public class MillenniumFalcon : Sprite {
        public BulletManager bullets;
        public float roll = 0f;

        public MillenniumFalcon(Game1 game) : base(game) {
            scale = 1 / 1f;
            vel_max = 100f;
            acc_max = 10f;

            bullets = new BulletManager(game, this);
        }

        public override void Initialize() {
            bullets.Initialize();
            base.Initialize();
        }

        protected override void LoadContent() {
            ContentManager Content = Game.Content;
            texture = Content.Load<Texture2D>("Images/Millennium Falcon");
            bullets.bulletTexture = Content.Load<Texture2D>("Images/Bullet"); // TEST

            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            physics.ResetAcceleration();
            MoveWithKeyboard(game.io.kb, game.io.kb_old);
            MoveWithGamePad(game.io.pad1, game.io.pad1_old);
            Friction();

            bullets.Update(gameTime);

            // TEST
            if (game.io.kb.IsKeyDown(Keys.Space)) {
                bullets.Shoot(gameTime);
            }

            if (game.io.kb.IsKeyDown(Keys.D1)) bullets.thread = BulletThread.Linear;
            if (game.io.kb.IsKeyDown(Keys.D2)) bullets.thread = BulletThread.EnergyBurst;
            if (game.io.kb.IsKeyDown(Keys.D3)) bullets.thread = BulletThread.Butterfly;
            if (game.io.kb.IsKeyDown(Keys.D4)) bullets.thread = BulletThread.DoubleEllipse;
            if (game.io.kb.IsKeyDown(Keys.D5)) bullets.thread = BulletThread.Spiral;
            if (game.io.kb.IsKeyDown(Keys.D6)) bullets.thread = BulletThread.Sakura;
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            bullets.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}