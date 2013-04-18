using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Apresentacao
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Player nave;
        List<Fireball> listImagemFire = new List<Fireball>();
        Rectangle posRight, posLeft, shot;
        int totalDisparos;
        float btnFireDelay;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 480;
            graphics.ApplyChanges();

            TargetElapsedTime = TimeSpan.FromTicks(333333);

            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }


        protected override void Initialize()
        {
            btnFireDelay = 1f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");

            /* Retangulos */
            posLeft = new Rectangle(0, Window.ClientBounds.Width / 2, Window.ClientBounds.Width / 2, Window.ClientBounds.Height);
            posRight = new Rectangle(Window.ClientBounds.Width / 2, 0,  Window.ClientBounds.Width, Window.ClientBounds.Height);
            shot = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height / 2);

            /* Nave */
            nave = new Player(this);
            nave.LoadContent();
            nave.AlterPoistion(new Vector2((Window.ClientBounds.Width / 2) - (nave.Image.Width / 2), Window.ClientBounds.Height - nave.Image.Height - 20));


        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            btnFireDelay -= elapsed;
         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            TouchCollection touchInput = TouchPanel.GetState();
            foreach (TouchLocation touch in touchInput)
            {
                if (posRight.Contains((int)touch.Position.X, (int)touch.Position.Y))
                    nave.MoveRight(gameTime);
                if (posLeft.Contains((int)touch.Position.X, (int)touch.Position.Y))
                    nave.MoveLeft(gameTime);

                if (btnFireDelay <= 0)
                {
                    if (shot.Contains((int)touch.Position.X, (int)touch.Position.Y))
                    {
                        /* Fire */
                        Fireball fire = new Fireball(this, new Vector2(nave.Position.X + (nave.Image.Width / 2) - 10, nave.Position.Y), 160f);
                        fire.LoadContent();
                        listImagemFire.Add(fire);

                        totalDisparos += 1;
                        btnFireDelay = 1f;
                    }    
                }
                
            }

            for (int i = 0; i < listImagemFire.Count; i++)
            {
                listImagemFire[i].Update(gameTime);
                if (listImagemFire[i].Position.X <= 0)
                    listImagemFire.RemoveAt(i);    
            }
           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Fireball fire in listImagemFire)
                fire.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Disparos: " + totalDisparos, Vector2.Zero, Color.White);
            nave.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
