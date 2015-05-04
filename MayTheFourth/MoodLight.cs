using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MayTheFourth {
    public class MoodLight {
        private bool redIncrease = true;
        private bool greenIncrease = true;
        private bool blueIncrease = true;

        private byte redIntensity = 0;
        private byte greenIntensity = 0;
        private byte blueIntensity = 0;

        public Color color {
            get {
                return NextColor();
            }
            set {
                redIntensity = value.R;
                greenIntensity = value.G;
                blueIntensity = value.B;

                redIncrease = true;
                greenIncrease = true;
                blueIncrease = true;
            }
        }

        public MoodLight(byte redIntensity, byte greenIntensity, byte blueIntensity) {
            color = new Color(redIntensity, greenIntensity, blueIntensity);
        }

        public MoodLight(Color initColor) {
            color = initColor;
        }

        public Color NextColor() {
            if (redIncrease) { redIntensity++; } else { redIntensity--; }
            if (greenIncrease) { greenIntensity++; } else { greenIntensity--; }
            if (blueIncrease) { blueIntensity++; } else { blueIntensity--; }

            if (redIntensity == 255) { redIncrease = false; }
            else if (redIntensity == 0) { redIncrease = true; }

            if (greenIntensity == 255) { greenIncrease = false; }
            else if (greenIntensity == 0) { greenIncrease = true; }

            if (blueIntensity == 255) { blueIncrease = false; }
            else if (blueIntensity == 0) { blueIncrease = true; }

            return new Color(redIntensity, greenIntensity, blueIntensity);
        }
    }
}
