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

        Texture2D backg1, backg2, backg3, backg4, backg5, backg6, backg7, backg8, backg9;
        int[] maps;

        Player hero;

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

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;

            this.Window.Title = "Pier's RPG Test";
            #endregion
        }

        protected override void Initialize()
        {
            //0 is current map
            maps = new int[9];
            maps[0] = 5;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hero = new Player();

            backg1 = Content.Load<Texture2D>("grass");
            backg2 = Content.Load<Texture2D>("grass");
            backg3 = Content.Load<Texture2D>("grass");
            backg4 = Content.Load<Texture2D>("grass");
            backg5 = Content.Load<Texture2D>("grass");
            backg6 = Content.Load<Texture2D>("grass");
            backg7 = Content.Load<Texture2D>("grass");
            backg8 = Content.Load<Texture2D>("grass");
            backg9 = Content.Load<Texture2D>("grass");

            hero.LoadContent(Content, "character");
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
                
                //Update player character
                hero.Update(gameTime, GraphicsDevice, maps);

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //Draw background texture
            if (maps[0] == 1)
                spriteBatch.Draw(backg1, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Firebrick);
            if (maps[0] == 2)
                spriteBatch.Draw(backg2, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Firebrick);
            if (maps[0] == 3)
                spriteBatch.Draw(backg3, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Firebrick);
            if (maps[0] == 4)
                spriteBatch.Draw(backg4, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.YellowGreen);
            if (maps[0] == 5)
                spriteBatch.Draw(backg5, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.White);
            if (maps[0] == 6)
                spriteBatch.Draw(backg6, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.BlueViolet);
            if (maps[0] == 7)
                spriteBatch.Draw(backg7, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Firebrick);
            if (maps[0] == 8)
                spriteBatch.Draw(backg8, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Tomato);
            if (maps[0] == 9)
                spriteBatch.Draw(backg9, destinationRectangle: new Rectangle(0, 0, 1280, 720), color: Color.Firebrick);

            //Draw player character
            spriteBatch.Draw(hero.texture,
                destinationRectangle: hero.drawArea,
                sourceRectangle: hero.frameArea,
                color: Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
