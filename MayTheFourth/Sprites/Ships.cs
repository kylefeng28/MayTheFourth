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
            bullets.bulletTexture = Content.Load<Texture2D>("Images/Laser");
            bullets.shootSound = Content.Load<SoundEffect>("Sounds/Falcon-Guns");
            bullets.bulletColor = Color.Green;

            base.LoadContent();
        }

    }

    public class XWing : Spaceship {

        public XWing(Game1 game) : base(game) {
            scale = 1 / 1f;
            vel_max = 100f;
            acc_max = 10f;
        }

        protected override void LoadContent() {
            ContentManager Content = Game.Content;
            texture = Content.Load<Texture2D>("Images/XWing");
            bullets.bulletTexture = Content.Load<Texture2D>("Images/Laser");
            bullets.shootSound = Content.Load<SoundEffect>("Sounds/XWing-Laser");
            bullets.bulletColor = Color.Red;

            base.LoadContent();
        }

    }


}