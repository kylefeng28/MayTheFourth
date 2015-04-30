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
            ResetAcceleration();
            MoveWithKeyboard(game.io.kb, game.io.kb_old);
            MoveWithGamePad(game.io.pad1, game.io.pad1_old);
            Friction();

            bullets.Update(gameTime);

            // TEST START
            if (game.io.kb.IsKeyDown(Keys.Space)) {
                bullets.Shoot();
            }
            if (game.io.kb.IsKeyDown(Keys.D1)) bullets.thread = BulletThread.Straight;
            if (game.io.kb.IsKeyDown(Keys.D2)) bullets.thread = BulletThread.EnergyBurst;
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            bullets.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}