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

namespace MayTheFourth {
    public class IOManager : GameComponent {
        public GamePadState pad1, pad1_old;
        public GamePadState pad2, pad2_old;
        public KeyboardState kb, kb_old;
        public MouseState mouse, mouse_old;

        public IOManager(Game1 game) : base(game) {
        }

        public override void Update(GameTime gameTime) {
            pad1_old = pad1;
            pad2_old = pad2;
            kb_old = kb;
            mouse_old = mouse;

            pad1 = GamePad.GetState(PlayerIndex.One);
            pad2 = GamePad.GetState(PlayerIndex.Two);
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();
        }

        public bool IsGamePadButtonTapped(Buttons button) {
            return pad1.IsButtonDown(button) && pad1_old.IsButtonUp(button);
        }

        public bool IsKeyTapped(Keys key) {
            return kb.IsKeyDown(key) && kb_old.IsKeyUp(key);
        }

    }
}
