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
// https://gist.github.com/ohrodr/2411608

namespace MayTheFourth.States {
    public class GameStateManager : GameComponent {
        public Game1 game;

        public GameStateManager(Game1 game) : base(game) {
            this.game = game;
        }

        private Stack<GameState> gameStates = new Stack<GameState>();
        public GameState state { get { return gameStates.Peek(); } }

        public void Push(GameState newState) {
            gameStates.Push(newState);

            switch (newState) {
            case GameState.Title: {
                    MediaPlayer.Play(game.titleScreen.mainTheme);
                    break;
                }
            case GameState.Playing: {
                    break;
                }
            case GameState.GameOver: {
                    break;
                }
            }
        }

        public GameState Pop() {
            if (gameStates.Count > 0) {
                GameState oldState = gameStates.Pop();

                switch (oldState) {
                case GameState.Title: {
                        MediaPlayer.Stop();
                        break;
                    }
                case GameState.Playing: {
                        break;
                    }
                case GameState.GameOver: {
                        break;
                    }
                }

                return oldState;
            }
            else return GameState.None;
        }

        public void ChangeState(GameState newState) {
            Pop();
            Push(newState);
        }

    }
}
