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
        private Rectangle frameSize { get; set; }
        public int frameSizeX { get; set; }
        public int frameSizeY { get; set; }
        public Vector2 position { get; set; }
        public Color color { get; set; }

        public char stance { get; set; }
        public int animationFrames { get; set; }
        public int animationInterval { get; set; }
        public float speed { get; set; }
        public float internalClock { get; set; }
        
        //Instantiate colliding class
        public Colliding colliding = new Colliding();

        public Player(Texture2D texture, int frameSizeX, int frameSizeY, Vector2 position, Color color)
        {
            this.texture = texture;
            this.frameSizeX = frameSizeX;
            this.frameSizeY = frameSizeY;
            this.frameSize = new Rectangle(0, 0, this.frameSizeX, this.frameSizeY);
            this.position = position;
            this.color = color;

            speed = 2.0f;
            animationFrames = 3;
            animationInterval = 200;
            stance = 'd';
        }

        //Update
        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice, Colliding colliding)
        {
            #region shorthands
            var mouseX = Mouse.GetState().Position.X;
            var mouseY = Mouse.GetState().Position.Y;
            var viewX = graphicsDevice.Viewport.Width;
            var viewY = graphicsDevice.Viewport.Height;
            var centeredX = position.X + (frameSizeX / 2);
            var centeredY = position.Y + (frameSizeY / 2);
            #endregion shorthands

            #region ANIMATION (DEFAULT)

            //Increment player time
            internalClock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //
            if (internalClock >= animationInterval)
            {
                if (animationFrames >= 3) animationFrames = 0;
                else animationFrames++;

                internalClock = 0;
            }

            #endregion ANIMATION (DEFAULT)
            #region STANCE (DEFAULT)

            //SET CURRENT STANCE DIRECTION
            if (stance == 'd')
                frameSize = new Rectangle(0, 0, frameSize.Width, frameSize.Height);
            if (stance == 'u')
                frameSize = new Rectangle(0, 75, frameSize.Width, frameSize.Height);
            if (stance == 'l')
                frameSize = new Rectangle(0, 150, frameSize.Width, frameSize.Height);
            if (stance == 'r')
                frameSize = new Rectangle(50, 225, frameSize.Width, frameSize.Height);

            #endregion STANCE (DEFAULT)
            #region MOVEMENT (DEFAULT)

            //Update collision bools from outside player class
            this.colliding = colliding;

            //MOVING UP
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
            {
                if (!this.colliding.up)
                {
                    //Movement
                    position = new Vector2(position.X, position.Y - (speed + (60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)));
                }
                //Animation
                frameSize = new Rectangle(50 * animationFrames, 75, frameSizeX, frameSizeY);
                stance = 'u';
            }

            //MOVING DOWN
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                if (!this.colliding.down)
                {
                    //Movement
                    position = new Vector2(position.X, position.Y + (speed + (60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)));
                }
                //Animation
                frameSize = new Rectangle(50 * animationFrames, 0, frameSizeX, frameSizeY);
                stance = 'd';
            }

            //MOVING LEFT
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                if (!this.colliding.left)
                {
                    //Movement
                    position = new Vector2(position.X - (speed + (60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)), position.Y);
                }
                //Animation
                frameSize = new Rectangle(50 * animationFrames, 150, frameSizeX, frameSizeY);
                stance = 'l';
            }

            //MOVING RIGHT
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                if (!this.colliding.right)
                {
                    //Movement
                    position = new Vector2(position.X + (speed + (60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)), position.Y);
                }
                //Animation
                frameSize = new Rectangle(50 * animationFrames, 225, frameSizeX, frameSizeY);
                stance = 'r';
            }

            #endregion MOVEMENT (DEFAULT)
            #region CONTROLS (DEFAULT)

            //Mouse is within the game bounds
            if ((mouseX < viewX && mouseX > 0) && (mouseY < viewY && mouseY > 0))
            {
                if ((mouseX < centeredX && ((mouseX - centeredY) > (mouseX - centeredX))))
                    stance = 'l';
                if (mouseY < centeredY && ((mouseX - centeredX) > (mouseY - centeredY)))
                    stance = 'u';
                if (mouseY > centeredY && ((mouseX - centeredX) < (mouseY - centeredY)))
                    stance = 'd';
                if (mouseX > centeredX && ((mouseY - centeredY) < (mouseX - centeredX)))
                    stance = 'r';
            }
            
            //
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
                stance = 'l';
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
                stance = 'u';
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
                stance = 'r';
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
                stance = 'd';

            #endregion CONTROLS (DEFAULT)
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, position, frameSize, color);
        }
    }
}
