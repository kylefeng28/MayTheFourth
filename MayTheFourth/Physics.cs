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

        // Circular motion
        public float ang_pos; // = 0f;
        public float ang_vel; // = 0f;
        public float ang_acc; // = 0f;

        public void Euler(float dt = 1f) {
            vel += acc * dt;
            pos += vel * dt;

            ang_vel += ang_acc * dt;
            ang_pos += ang_vel * dt;
        }

        public void Verlet(float dt = 1f) {
            Vector2 vel_old = vel;
            vel += acc * dt;
            pos += (vel + vel_old) * 0.5f * dt;

            float ang_vel_old = ang_vel;
            ang_vel += ang_acc * dt;
            ang_pos += (ang_vel + ang_vel_old) * 0.5f * dt;
        }

        public void ResetAcceleration() {
            acc = Vector2.Zero;
            ang_acc = 0f;
        }
    }
}
