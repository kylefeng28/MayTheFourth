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

namespace MayTheFourth.Sprites {
    public class Player : DrawableGameComponent {
        public Game1 game;
        public Spaceship ship;
        public PlayerIndex playerIndex;

        public Player(Game1 game, Spaceship ship, PlayerIndex playerIndex) : base(game) {
            this.game = game;
            this.ship = ship;
            this.playerIndex = playerIndex;
        }

        public override void Initialize() {
            ship.Initialize();
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            ship.physics.ResetAcceleration();

            ship.MoveWithKeyboard(game.io.kb, game.io.kb_old, gameTime);

            switch (playerIndex) {
            case PlayerIndex.One: {
                ship.MoveWithGamePad(game.io.pad1, game.io.pad1_old, gameTime);
                    break;
                }
            case PlayerIndex.Two: {
                ship.MoveWithGamePad(game.io.pad2, game.io.pad2_old, gameTime);
                    break;
                }
            }

            ship.Update(gameTime);

            if (game.io.kb.IsKeyDown(Keys.D1)) ship.bullets.thread = BulletThread.Linear;
            if (game.io.kb.IsKeyDown(Keys.D2)) ship.bullets.thread = BulletThread.EnergyBurst;
            if (game.io.kb.IsKeyDown(Keys.D3)) ship.bullets.thread = BulletThread.Butterfly;
            if (game.io.kb.IsKeyDown(Keys.D4)) ship.bullets.thread = BulletThread.DoubleEllipse;
            if (game.io.kb.IsKeyDown(Keys.D5)) ship.bullets.thread = BulletThread.Spiral;
            if (game.io.kb.IsKeyDown(Keys.D6)) ship.bullets.thread = BulletThread.Sakura;
        }

        public override void Draw(GameTime gameTime) {
            ship.Draw(gameTime);
            
            base.Draw(gameTime);
        }

    }
}
