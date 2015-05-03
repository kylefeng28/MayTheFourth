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
using MayTheFourth.Sprites;

// http://gamedev.stackexchange.com/questions/59301/xna-2d-camera-scrolling-why-use-matrix-transform

namespace MayTheFourth {
    public class Camera {
        public Game1 game;

        public Vector2 pos = Vector2.Zero;
        public float zoom = 1f;
        public float rotation = 0f;
        public Rectangle bounds;

        public Camera(Game1 game) {
            this.game = game;
            bounds = game.GraphicsDevice.Viewport.Bounds;
        }

        public void Update(GameTime gameTime) {
            MoveWithMouse(game.io.mouse, game.io.mouse_old);
            MoveWithGamePad(game.io.pad1, game.io.pad1_old);
        }

        public Matrix TransformMatrix() {
            return
                Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(zoom)
                * Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));
        }

        public void Follow(Sprite sprite) {
            this.pos = MathHelperExtensions.SmoothStep(this.pos, sprite.physics.pos, 0.5f);
        }

        public void Follow(Sprite sprite1, Sprite sprite2) {
            Vector2 pos1 = sprite1.physics.pos;
            Vector2 pos2 = sprite2.physics.pos;
            Vector2 pos_avg = (pos1 + pos2) / 2f;
            Vector2 pos_diff = pos2 - pos1;

            this.pos = MathHelperExtensions.SmoothStep(this.pos, pos_avg, 0.5f);

            float zoomAmount;
            if (pos_diff.Length() > 0) {
                zoomAmount = 1000 / pos_diff.Length();
            }
            else zoomAmount = 1f;

            this.zoom = MathHelper.SmoothStep(this.zoom, zoomAmount, 1f);
        }

        public void Zoom(float dir) {
            if (dir > 0) zoom *= 1.1f;
            else if (dir < 0) zoom *= 0.9f;
        }

        public void MoveWithMouse(MouseState mouse, MouseState mouse_old) {
            float deltaScroll = mouse.ScrollWheelValue - mouse_old.ScrollWheelValue;
            Zoom(deltaScroll);
        }

        public void MoveWithGamePad(GamePadState pad, GamePadState pad_old) {
            if (pad.IsButtonDown(Buttons.LeftShoulder)) Zoom(-1);
            if (pad.IsButtonDown(Buttons.RightShoulder)) Zoom(+1);
        }
    }
}
