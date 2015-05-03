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
using MayTheFourth.States;

namespace MayTheFourth {
    public struct Physics {
        // Kinematics
        public Vector2 pos; // = Vector2.Zero;
        public Vector2 vel; // = Vector2.Zero;
        public Vector2 acc; // = Vector2.Zero;

        // Circular motion and flight dynamics
        public float yaw_pos; // = 0f;
        public float yaw_vel; // = 0f;
        public float yaw_acc; // = 0f;

        public void Euler(float dt = 1f) {
            vel += acc * dt;
            pos += vel * dt;

            yaw_vel += yaw_acc * dt;
            yaw_pos += yaw_vel * dt;
        }

        public void Verlet(float dt = 1f) {
            Vector2 vel_old = vel;
            vel += acc * dt;
            pos += (vel + vel_old) * 0.5f * dt;

            float yaw_vel_old = yaw_vel;
            yaw_vel += yaw_acc * dt;
            yaw_pos += (yaw_vel + yaw_vel_old) * 0.5f * dt;
        }

        public void ResetAcceleration() {
            acc = Vector2.Zero;
            yaw_acc = 0f;
        }
    }
}
