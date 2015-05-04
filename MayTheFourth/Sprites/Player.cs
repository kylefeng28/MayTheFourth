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

            MoveWithKeyboard(game.io.kb, game.io.kb_old, gameTime);

            switch (playerIndex) {
            case PlayerIndex.One: {
                MoveWithGamePad(game.io.pad1, game.io.pad1_old, gameTime);
                    break;
                }
            case PlayerIndex.Two: {
                MoveWithGamePad(game.io.pad2, game.io.pad2_old, gameTime);
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

        public virtual void MoveWithKeyboard(KeyboardState kb, KeyboardState kb_old, GameTime gameTime) {
            if (playerIndex == PlayerIndex.One) {
                if (kb.IsKeyDown(Keys.W)) ship.Forward(10);
                if (kb.IsKeyDown(Keys.S)) ship.Forward(-10);
                if (kb.IsKeyDown(Keys.D)) ship.Roll(5);
                if (kb.IsKeyDown(Keys.A)) ship.Roll(-5);
                if (kb.IsKeyDown(Keys.E)) ship.TurnYaw(5);
                if (kb.IsKeyDown(Keys.Q)) ship.TurnYaw(-5);
                if (kb.IsKeyDown(Keys.Space)) ship.bullets.Shoot(gameTime);
            }
            else if (playerIndex == PlayerIndex.Two) {
                if (kb.IsKeyDown(Keys.NumPad8)) ship.Forward(10);
                if (kb.IsKeyDown(Keys.NumPad2)) ship.Forward(-10);
                if (kb.IsKeyDown(Keys.NumPad6)) ship.Roll(5);
                if (kb.IsKeyDown(Keys.NumPad4)) ship.Roll(-5);
                if (kb.IsKeyDown(Keys.NumPad9)) ship.TurnYaw(5);
                if (kb.IsKeyDown(Keys.NumPad7)) ship.TurnYaw(-5);
                if (kb.IsKeyDown(Keys.NumPad0)) ship.bullets.Shoot(gameTime);
            }
        }

        public virtual void MoveWithGamePad(GamePadState pad, GamePadState pad_old, GameTime gameTime) {
            if (Math.Abs(pad.ThumbSticks.Left.X) > 0)
                ship.TurnYaw(pad.ThumbSticks.Left.X * 10);
            if (Math.Abs(pad.ThumbSticks.Right.X) > 0)
                ship.Roll(pad.ThumbSticks.Right.X * 10);
            if (pad.Triggers.Right > 0)
                ship.Forward();

            if (pad.IsButtonDown(Buttons.A)) {
                ship.bullets.Shoot(gameTime);
                game.io.Rumble(pad, 1f, 1f, 20);
            }
        }

    }
}
