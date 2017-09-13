using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Sprite
    {
        private readonly Texture2D texture;
        public Vector2 position;
        public Color color;

        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }

    public class MagicSpell
    {
        private readonly Texture2D texture;
        public Vector2 position;
        public Color color;
        public float internalClock { get; set; }
        public int animationFrames { get; set; }
        public int animationBreak { get; set; }

        public MagicSpell(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }

    public class Colliding
    {
        public bool left { get; set; }
        public bool up { get; set; }
        public bool right { get; set; }
        public bool down { get; set; }

        public Colliding()
        {
            this.left = false;
            this.up = false;
            this.right = false;
            this.down = false;
        }
    }
}
