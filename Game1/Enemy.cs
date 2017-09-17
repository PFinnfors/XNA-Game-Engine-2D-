using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Enemy
    {
        public Texture2D texture { get; set; }
        private Rectangle frameSize { get; set; }
        public int frameSizeX { get; set; }
        public int frameSizeY { get; set; }
        public Vector2 position { get; set; }
        public Color color { get; set; }

        public int homeMap { get; set; }
        public bool onMap { get; set; }
        public int animationFrames { get; set; }
        public int animationInterval { get; set; }
        public float speed { get; set; }
        public float internalClock { get; set; }

        //Instantiate colliding class
        public Colliding colliding = new Colliding();

        public Enemy(Texture2D texture, int frameSizeX, int frameSizeY, Vector2 position, Color color)
        {
            this.texture = texture;
            this.frameSizeX = frameSizeX;
            this.frameSizeY = frameSizeY;
            this.frameSize = new Rectangle(0, 192, this.frameSizeX, this.frameSizeY);
            this.position = position;
            this.color = color;

            homeMap = 2;
            onMap = false;
            speed = 1.0f;
            animationFrames = 2;
            animationInterval = 200;
        }

        public void Update(GameTime gameTime, int currentMap)
        {
            if (currentMap == homeMap) onMap = true;
            else onMap = false;

            if (onMap)
            {
                internalClock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                //
                if (internalClock >= animationInterval)
                {
                    if (animationFrames >= 3) animationFrames = 0;
                    else animationFrames++;

                    internalClock = 0;
                }

                position = new Vector2(position.X + (speed + (60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds)), position.Y);
                frameSize = new Rectangle(48 * animationFrames, 288, frameSizeX, frameSizeY);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (onMap)
                spriteBatch.Draw(texture, position, frameSize, color);
        }
    }
}
