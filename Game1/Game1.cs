using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D myChar;
        Vector2 charVector;
        Rectangle destRect, sourceRect;
        float elapsed;
        float delay;
        int frames;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            #region extra settings
            //Cap at 30 fps
            //this.IsFixedTimeStep = true;
            //this.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 33);

            //VSync off
            //this.graphics.SynchronizeWithVerticalRetrace = false;
            #endregion
        }

        protected override void Initialize()
        {
            charVector = new Vector2(200, 200);
            destRect = new Rectangle(x: 100, y: 100, width: 50, height: 75);
            delay = 200f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            myChar = this.Content.Load<Texture2D>("charactervector");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsed >= delay)
                {
                    if (frames >= 3) frames = 0;
                    else frames++;

                    elapsed = 0;
                }

                sourceRect = new Rectangle(0, 0, 50, 75);

                #region MOVEMENT
                ////MOVE DOWN
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    charVector.Y += (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    sourceRect = new Rectangle(50 * frames, 0, 50, 75);
                }
                ////MOVE LEFT
                //if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //    textureX -= (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                ////MOVE UP
                //if (Keyboard.GetState().IsKeyDown(Keys.Up))
                //    textureY -= (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                ////MOVE RIGHT
                //if (Keyboard.GetState().IsKeyDown(Keys.Right))
                //    textureX += (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);

                ////OUTSIDE RIGHT
                //if (textureX == this.GraphicsDevice.Viewport.Width + texture.Width && Keyboard.GetState().IsKeyDown(Keys.Right))
                //    textureX = -texture.Width;
                ////OUTSIDE LEFT
                //if (textureX == -texture.Width && Keyboard.GetState().IsKeyDown(Keys.Left))
                //    textureX = this.GraphicsDevice.Viewport.Width + texture.Width;

                ////OUTSIDE BOTTOM
                //if (textureY == this.GraphicsDevice.Viewport.Height + texture.Height && Keyboard.GetState().IsKeyDown(Keys.Down))
                //    textureY = -texture.Height;
                ////OUTSIDE TOP
                //if (textureY == -texture.Height && Keyboard.GetState().IsKeyDown(Keys.Up))
                //    textureY = this.GraphicsDevice.Viewport.Height + texture.Height;
                #endregion MOVEMENT

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(myChar,
                destinationRectangle: destRect,
                sourceRectangle: sourceRect);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
