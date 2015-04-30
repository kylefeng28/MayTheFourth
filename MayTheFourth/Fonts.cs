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

namespace MayTheFourth {
    public static class Fonts {
        public static SpriteFont Debug;

        public static void LoadContent(ContentManager Content) {
            Debug = Content.Load<SpriteFont>("Fonts/Debug");
        }
    }
}
