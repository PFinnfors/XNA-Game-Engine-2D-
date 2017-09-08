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
        float elapsed;

        Texture2D spriteSheet;
        int moveSpeed, animationFrames;
        float animationDelay;
        Rectangle drawRect;
        Rectangle frameRect;
        char stance;

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
            animationDelay = 200f;
            animationFrames = 3;
            moveSpeed = 1;
            drawRect = new Rectangle(x: 100, y: 100, width: 50, height: 75);
            stance = 'd';

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteSheet = this.Content.Load<Texture2D>("charactervector");
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

                if (elapsed >= animationDelay)
                {
                    if (animationFrames >= 3) animationFrames = 0;
                    else animationFrames++;

                    elapsed = 0;
                }
                
                #region MOVEMENT
                //MOVE UP
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    //Movement
                    drawRect.Y -= moveSpeed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    //Animation
                    frameRect = new Rectangle(50 * animationFrames, 75, 50, 75);
                    stance = 'u';
                }
                //MOVE DOWN
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    //Movement
                    drawRect.Y += moveSpeed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    //Animation
                    frameRect = new Rectangle(50 * animationFrames, 0, 50, 75);
                    stance = 'd';
                }
                //MOVE LEFT
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    //Movement
                    drawRect.X -= moveSpeed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    //Animation
                    frameRect = new Rectangle(50 * animationFrames, 150, 50, 75);
                    stance = 'l';
                }
                //MOVE RIGHT
                if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    //Movement
                    drawRect.X += moveSpeed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    //Animation
                    frameRect = new Rectangle(50 * animationFrames, 225, 50, 75);
                    stance = 'r';
                }

                //Default player frame
                if (stance == 'd' && !(Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)))
                    frameRect = new Rectangle(0, 0, 50, 75);
                if (stance == 'u' && !(Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W)))
                    frameRect = new Rectangle(0, 75, 50, 75);
                if (stance == 'l' && !(Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)))
                    frameRect = new Rectangle(0, 150, 50, 75);
                if (stance == 'r' && !(Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)))
                    frameRect = new Rectangle(50, 225, 50, 75);

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

            spriteBatch.Draw(spriteSheet,
                destinationRectangle: drawRect,
                sourceRectangle: frameRect);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
