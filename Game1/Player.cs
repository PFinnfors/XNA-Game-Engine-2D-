using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1;

namespace Game1
{
    public class Player
    {
        public Texture2D texture { get; set; }
        public Rectangle frameArea { get; set; }
        public Rectangle drawArea { get; set; }
        public char stance { get; set; }
        public int animationFrames { get; set; }
        public int animationPause { get; set; }

        public int speed { get; set; }
        public bool colliding { get; set; }
        public float internalClock { get; set; }

        public Player()
        {
            speed = 2;
            animationFrames = 3;
            animationPause = 200;
            stance = 'd';
            colliding = false;

            frameArea = new Rectangle(0, 0, 50, 75);
            drawArea = new Rectangle(200, 200, 50, 75);
        }

        //Load
        public void LoadContent(ContentManager Content, string spriteSheet)
        {
            texture = Content.Load<Texture2D>(spriteSheet);
        }

        //Update
        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice, int[] _maps)
        {
            internalClock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (internalClock >= animationPause)
            {
                if (animationFrames >= 3) animationFrames = 0;
                else animationFrames++;

                internalClock = 0;
            }

            #region MOVEMENT


            #region DEFAULT MOVEMENT

            //MOVE UP
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //Movement
                drawArea = new Rectangle(drawArea.X, drawArea.Y - (speed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)),
                    drawArea.Width, drawArea.Height);
                //Animation
                frameArea = new Rectangle(50 * animationFrames, 75, 50, 75);
                stance = 'u';
            }
            ////MOVE DOWN
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //Movement
                drawArea = new Rectangle(drawArea.X, drawArea.Y + speed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds),
                    drawArea.Width, drawArea.Height);
                //Animation
                frameArea = new Rectangle(50 * animationFrames, 0, 50, 75);
                stance = 'd';
            }
            ////MOVE LEFT
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //Movement
                drawArea = new Rectangle(drawArea.X - (speed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)), drawArea.Y,
                    drawArea.Width, drawArea.Height);
                //Animation
                frameArea = new Rectangle(50 * animationFrames, 150, 50, 75);
                stance = 'l';
            }
            ////MOVE RIGHT
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //Movement
                if (colliding)
                    drawArea = new Rectangle(drawArea.X + speed + (int)(60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds), drawArea.Y, drawArea.Width, drawArea.Height);
                //Animation
                frameArea = new Rectangle(50 * animationFrames, 225, 50, 75);
                stance = 'r';
            }
            #endregion DEFAULT MOVEMENT

            #region MOVEMENT ANIMATION

            //Default player frame
            if (stance == 'd' && !(Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)))
                frameArea = new Rectangle(0, 0, 50, 75);
            if (stance == 'u' && !(Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W)))
                frameArea = new Rectangle(0, 75, 50, 75);
            if (stance == 'l' && !(Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)))
                frameArea = new Rectangle(0, 150, 50, 75);
            if (stance == 'r' && !(Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)))
                frameArea = new Rectangle(50, 225, 50, 75);
            #endregion DEFAULT MOVEMENT

            #region BORDER MOVEMENT

            //OUTSIDE RIGHT
            if (drawArea.X == graphicsDevice.Viewport.Width && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //check if current map is at the right edge
                if (_maps[0] != 3 && _maps[0] != 6 && _maps[0] != 9)
                {
                    //Set current map to the map located to the right
                    _maps[0]++;
                    //Move player to the left side
                    drawArea = new Rectangle(0, drawArea.Y, drawArea.Width, drawArea.Height);
                }
                else
                {
                    colliding = true;
                }
            }
            ////OUTSIDE LEFT
            if (drawArea.X == -drawArea.Width && Keyboard.GetState().IsKeyDown(Keys.Left))
                drawArea = new Rectangle(graphicsDevice.Viewport.Width, drawArea.Y, drawArea.Width, drawArea.Height);

            ////OUTSIDE BOTTOM
            if (drawArea.Y == graphicsDevice.Viewport.Height && Keyboard.GetState().IsKeyDown(Keys.Down))
                drawArea = new Rectangle(drawArea.X, drawArea.Y - drawArea.Height, drawArea.Width, drawArea.Height);
            ////OUTSIDE TOP
            if (drawArea.Y == -drawArea.Height && Keyboard.GetState().IsKeyDown(Keys.Up))
                drawArea = new Rectangle(drawArea.X, graphicsDevice.Viewport.Height, drawArea.Width, drawArea.Height);
            #endregion BORDER MOVEMENT

            #endregion MOVEMENT
        }

    }
}
