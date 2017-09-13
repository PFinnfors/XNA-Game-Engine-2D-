using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class MagicSpell
    {
        private Texture2D texture;
        public int activeSpellsCount { get; set; }
        public Vector2 position;
        public Color color;
        private Rectangle frameArea { get; set; }
        private int frameAreaX { get; set; }
        private int frameAreaY { get; set; }
        public float internalClock { get; set; }
        private int animationFrames { get; set; }
        private int currentAnimationFrame { get; set; }
        private int animationInterval { get; set; }

        public MagicSpell(int frameAreaX, int frameAreaY, int animationFrames, Color color)
        {
            activeSpellsCount = 0;
            this.frameAreaX = frameAreaX;
            this.frameAreaY = frameAreaY;
            this.frameArea = new Rectangle(0, 0, frameAreaX, frameAreaY);
            this.animationFrames = animationFrames - 1;
            this.currentAnimationFrame = animationFrames - 1;
            this.animationInterval = 50;
            this.color = color;
        }

        public void Create(ContentManager content, Player caster)
        {
            activeSpellsCount++;
            texture = content.Load<Texture2D>("magicTest");

            //Spell location
            if (caster.stance == 'l')
                position = new Vector2(caster.position.X - frameAreaX, caster.position.Y + frameAreaY + (frameAreaY / 4));
            if (caster.stance == 'u')
                position = new Vector2(caster.position.X + (caster.frameSizeX / 2) - (frameAreaX / 2), caster.position.Y);
            if (caster.stance == 'r')
                position = new Vector2(caster.position.X + caster.frameSizeX, caster.position.Y + frameAreaY + (frameAreaY / 4));
            if (caster.stance == 'd')
                position = new Vector2(caster.position.X + (caster.frameSizeX / 2) - (frameAreaX / 2), caster.position.Y + caster.frameSizeY - (frameAreaY / 2));

        }

        public void Destroy(ContentManager content)
        {

            activeSpellsCount--;
        }

        public void Update(GameTime gameTime, Player caster)
        {
            //Spell location
            if (caster.stance == 'l')
                position = new Vector2(caster.position.X - frameAreaX, caster.position.Y + frameAreaY + (frameAreaY / 4));
            if (caster.stance == 'u')
                position = new Vector2(caster.position.X + (caster.frameSizeX / 2) - (frameAreaX / 2), caster.position.Y);
            if (caster.stance == 'r')
                position = new Vector2(caster.position.X + caster.frameSizeX, caster.position.Y + frameAreaY + (frameAreaY / 4));
            if (caster.stance == 'd')
                position = new Vector2(caster.position.X + (caster.frameSizeX / 2) - (frameAreaX / 2), caster.position.Y + caster.frameSizeY - (frameAreaY / 2));

            //Animation stuff
            internalClock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //
            if (internalClock >= animationInterval)
            {
                if (currentAnimationFrame >= animationFrames) currentAnimationFrame = 0;
                else currentAnimationFrame++;

                internalClock = 0;
            }

            frameArea = new Rectangle(frameAreaX * currentAnimationFrame, 0, frameAreaX, frameAreaY);
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, frameArea, color);
        }
    }
}
