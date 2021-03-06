﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Sprite backg1, backg2, backg3, backg4, backg5, backg6, backg7, backg8, backg9;
        private Sprite cursor;
        int[] maps;

        public Vector2 prevRSPos;
        public Vector2 currentRSPos;

        Player hero;
        Colliding playerColliding;
        Enemy turtle;
        MagicSpell magicBolt;
        float spellTimer;

        private SoundEffect magicStartSFX;

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
            playerColliding = new Colliding();

            //0 is current map
            maps = new int[9];
            maps[0] = 5;

            magicBolt = new MagicSpell(27, 27, 4, Color.White);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backg1 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg2 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg3 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg4 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg5 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg6 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg7 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg8 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);
            backg9 = new Sprite(Content.Load<Texture2D>("grass"), Vector2.Zero, Color.White);

            cursor = new Sprite(Content.Load<Texture2D>("cursor"), Vector2.Zero, Color.White);

            hero = new Player(Content.Load<Texture2D>("character"), 50, 75, Vector2.One * 200, Color.White);

            turtle = new Enemy(Content.Load<Texture2D>("turtle"), 48, 48, Vector2.One * 100, Color.White);

            magicStartSFX = Content.Load<SoundEffect>("magicStart");
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

                //Move cursor to mouse cursor position
                cursor.position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

                //
                turtle.Update(gameTime, maps[0]);

                //
                if (magicBolt.activeSpellsCount > 0)
                {
                    magicBolt.Update(gameTime, hero);
                    spellTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }

                //Update player character
                hero.Update(gameTime, graphics.GraphicsDevice, playerColliding);

                if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.X))
                {
                    if (spellTimer <= 2000)
                    {
                        spellTimer = 0;
                        magicBolt.Create(Content, hero);
                        magicStartSFX.Play();
                    }

                }

                #region BORDER CHECKS

                playerColliding.left = false;
                playerColliding.up = false;
                playerColliding.right = false;
                playerColliding.down = false;

                //RIGHT BORDER CHECK
                if (hero.position.X > (GraphicsDevice.Viewport.Width - 25))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
                    {
                        if (maps[0] != 3 && maps[0] != 6 && maps[0] != 9)
                        {
                            maps[0]++;
                            hero.position = new Vector2(-25, hero.position.Y);
                        }
                        else playerColliding.right = true;
                    }
                }
                //LEFT BORDER CHECK
                if (hero.position.X < -25)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
                    {
                        if (maps[0] != 1 && maps[0] != 4 && maps[0] != 7)
                        {
                            maps[0]--;
                            hero.position = new Vector2(GraphicsDevice.Viewport.Width - 25, hero.position.Y);
                        }
                        else playerColliding.left = true;
                    }
                }
                //BOTTOM BORDER CHECK
                if (hero.position.Y > (GraphicsDevice.Viewport.Height - 37))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
                    {
                        if (maps[0] != 7 && maps[0] != 8 && maps[0] != 9)
                        {
                            maps[0] = maps[0] + 3;
                            hero.position = new Vector2(hero.position.X, -37);
                        }
                        else playerColliding.down = true;
                    }
                }
                //TOP EDGE CHECK
                if (hero.position.Y < -37)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
                    {
                        if (maps[0] != 1 && maps[0] != 2 && maps[0] != 3)
                        {
                            maps[0] = maps[0] - 3;
                            hero.position = new Vector2(hero.position.X, GraphicsDevice.Viewport.Height - 37);
                        }
                        else playerColliding.up = true;
                    }
                }

                #endregion BORDER CHECKS

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //Draw backgrounds
            DrawMaps();

            if (magicBolt.activeSpellsCount > 0)
                magicBolt.Draw(spriteBatch, magicBolt.position, Color.White);

            turtle.Draw(spriteBatch, Color.White);

            //Draw player character
            hero.Draw(spriteBatch, Color.White);

            cursor.Draw(spriteBatch, cursor.position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawMaps()
        {
            if (maps[0] == 1)
                backg1.Draw(spriteBatch, backg1.position, Color.White);
            if (maps[0] == 2)
                backg2.Draw(spriteBatch, backg2.position, Color.Coral);
            if (maps[0] == 3)
                backg3.Draw(spriteBatch, backg3.position, Color.White);
            if (maps[0] == 4)
                backg4.Draw(spriteBatch, backg4.position, Color.AliceBlue);
            if (maps[0] == 5)
                backg2.Draw(spriteBatch, backg5.position, Color.White);
            if (maps[0] == 6)
                backg6.Draw(spriteBatch, backg6.position, Color.Firebrick);
            if (maps[0] == 7)
                backg7.Draw(spriteBatch, backg7.position, Color.White);
            if (maps[0] == 8)
                backg8.Draw(spriteBatch, backg8.position, Color.DarkOliveGreen);
            if (maps[0] == 9)
                backg9.Draw(spriteBatch, backg9.position, Color.White);
        }
    }
}
