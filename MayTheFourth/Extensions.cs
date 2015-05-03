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
    public static class Extensions {
        public static Vector2 ToVector2(this Point point) {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 Unit(this Vector2 v) {
            Vector2 v_hat = v;
            v_hat.Normalize();
            return v_hat;
        }
    }

    public static class MathHelperExtensions {
        public static Vector2 SmoothStep(Vector2 v1, Vector2 v2, float amount) {
            float X =  MathHelper.SmoothStep(v1.X, v2.X, amount);
            float Y = MathHelper.SmoothStep(v1.Y, v2.Y, amount);
            return new Vector2(X, Y);
        }

        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float amount) {
            float X = MathHelper.Lerp(v1.X, v2.X, amount);
            float Y = MathHelper.Lerp(v1.Y, v2.Y, amount);
            return new Vector2(X, Y);
        }
    }
}
