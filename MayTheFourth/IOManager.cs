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
    public class IOManager : GameComponent {
        public GamePadState pad1, pad1_old;
        public KeyboardState kb, kb_old;
        public MouseState mouse, mouse_old;

        public IOManager(Game1 game) : base(game) {
        }

        public override void Update(GameTime gameTime) {
            pad1_old = pad1;
            kb_old = kb;
            mouse_old = mouse;

            pad1 = GamePad.GetState(PlayerIndex.One);
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();
        }
    }
}
