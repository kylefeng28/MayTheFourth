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
    public abstract class Spaceship : Sprite {
        public BulletManager bullets;

        public Spaceship(Game1 game) : base(game) {
            bullets = new BulletManager(game, this);
        }

        public override void Update(GameTime gameTime) {
            Friction();

            bullets.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            bullets.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override void MoveWithKeyboard(KeyboardState kb, KeyboardState kb_old, GameTime gameTime) {
            base.MoveWithKeyboard(kb, kb_old, gameTime);

            if (kb.IsKeyDown(Keys.Space)) {
                bullets.Shoot(gameTime);
            }
        }

        public override void MoveWithGamePad(GamePadState pad, GamePadState pad_old, GameTime gameTime) {
            base.MoveWithGamePad(pad, pad_old, gameTime);

            if (pad.IsButtonDown(Buttons.A)) {
                bullets.Shoot(gameTime);
                game.io.Rumble(pad, 1f, 1f, 20);
            }
        }

    }

    public class MillenniumFalcon : Spaceship {

        public MillenniumFalcon(Game1 game) : base(game) {
            scale = 1 / 1f;
            vel_max = 100f;
            acc_max = 10f;
        }

        protected override void LoadContent() {
            ContentManager Content = Game.Content;
            texture = Content.Load<Texture2D>("Images/Millennium Falcon");
            bullets.bulletTexture = Content.Load<Texture2D>("Images/Bullet"); // TEST
            bullets.bulletColor = Color.Green;

            base.LoadContent();
        }

    }
}