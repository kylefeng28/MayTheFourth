﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MayTheFourth.Sprites;

namespace MayTheFourth {
    public class Camera {
        protected Game1 game;
        public Vector2 pos = Vector2.Zero;
        public float zoom = 1f;
        public float rotation = 0f;
        public Rectangle bounds;

        public Camera(Game1 game) {
            this.game = game;
            bounds = game.GraphicsDevice.Viewport.Bounds;
        }

        public Matrix TransformMatrix() {
            return
                Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(zoom)
                * Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));
        }

        public void Follow(Sprite sprite) {
            this.pos = sprite.pos;
        }

    }
}