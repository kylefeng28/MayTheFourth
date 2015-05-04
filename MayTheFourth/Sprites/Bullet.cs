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
    public class Bullet : Sprite {
        public int EVAPORTATE_TIME = 200;
        public int evaporateTimer;
        public Color baseColor;

        public Bullet(Game1 game) : base(game) {
            evaporateTimer = EVAPORTATE_TIME;
        }

        public override void Update(GameTime gameTime) {
            if (evaporateTimer > 0) evaporateTimer--;
            if (evaporateTimer == 0) Enabled = false;

            color = baseColor * ((float) evaporateTimer / EVAPORTATE_TIME);

            base.Update(gameTime);
        }

    }
}
