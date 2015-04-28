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
    public class Annakin : Sprite {
        public Annakin(Game1 game) : base(game) {
            scale = 1 / 10f;
        }

        protected override void LoadContent() {
            ContentManager Content = Game.Content;
            texture = Content.Load<Texture2D>("Link");

            base.LoadContent();
        }
    }
}