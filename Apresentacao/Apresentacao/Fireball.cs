using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apresentacao
{
    class Fireball
    {
        public Game Game { get; private set; }
        public Vector2 Position { get { return position; } }
        public Rectangle Retangulo { get; private set; }
        public Texture2D image { get; set; }
        public float Speed { get; set; }
        Vector2 position;

        public Fireball(Game game, Vector2 position, float updateSpeed)
        {
            Game = game;
            this.position = position;
            Speed = updateSpeed;
        }

        public void LoadContent()
        {
            image = Game.Content.Load<Texture2D>("fire");
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spbatch)
        {
            spbatch.Draw(image, position, Color.White);
        }
    }
}
