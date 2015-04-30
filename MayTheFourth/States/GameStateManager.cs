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

// https://gist.github.com/ohrodr/2411774
// https://xnahell.wordpress.com/

namespace MayTheFourth.States {
    public class GameStateManager : GameComponent {
        public GameStateManager(Game game) : base(game) {
        }

        // private Stack<GameState> gameStates = new Stack<GameState>();
        public GameState state; // TEST
    }
}
