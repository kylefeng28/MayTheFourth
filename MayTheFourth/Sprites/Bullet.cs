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
        public float revolution = 0f;

        public Bullet(Game1 game) : base(game) {
            ang_acc = 1;
        }

        public override void Update(GameTime gameTime) {
            vel.X = (float) (vel_max * Math.Cos(revolution));
            vel.Y = (float) (vel_max * Math.Sin(revolution));
            base.Update(gameTime);
        }

    }
}
