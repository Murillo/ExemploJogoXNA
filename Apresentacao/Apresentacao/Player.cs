using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apresentacao
{
    class Player
    {
        public Game Game { get; private set; }
        public Vector2 Position { get { return position; } }
        public Rectangle Retangulo { get; private set; }
        public Texture2D Image { get; set; }
        public float Speed { get; set; }
        Vector2 position;

        public Player(Game game)
        {
            Game = game;
            Speed = 100f;
        }

        public void LoadContent()
        {
            Image = Game.Content.Load<Texture2D>("nave");
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveLeft(GameTime gameTime)
        {
            position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveRight(GameTime gameTime)
        {
            position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void AlterPoistion(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spbatch)
        {
            spbatch.Draw(Image, position, Color.White);
        }
    }
}
