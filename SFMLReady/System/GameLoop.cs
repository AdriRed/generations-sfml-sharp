﻿using System;
using System.Threading;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLReady.System
{
    public abstract class GameLoop
    {
        public const int TargetFPS = 60;
        public const float TimeUntilUpdate = 1f / TargetFPS;

        public RenderWindow Window
        {
            get;
            protected set;
        }

        public GameTime GameTime
        {
            get;
            protected set;
        }

        public Color ClearColor
        {
            get;
            protected set;
        }

        protected GameLoop(uint width, uint height, string title, Color clearColor)
        {
            ClearColor = clearColor;
            Window = new RenderWindow(new VideoMode(width, height), title);
            GameTime = new GameTime();
            Window.Closed += Window_Closed;
        }


        protected void Window_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }

        public void Run()
        {
            //double timeBeforeUpdate = 0d;
            //float prevTimeElapsed = 0f, deltaTime = 0f, timeElapsed = 0f;
                
            Clock clock;

            LoadContent();
            Initialize();

            clock = new Clock();

            while (Window.IsOpen)
            {
                
                Window.DispatchEvents();

                //float timeElapsed, deltaTime, prevTimeElapsed = 0, timeBeforeUpdate = 0;

                //timeElapsed = clock.ElapsedTime.AsSeconds();
                //deltaTime = timeElapsed - prevTimeElapsed;
                //prevTimeElapsed = timeElapsed;

                //timeBeforeUpdate += deltaTime;

                //if (timeBeforeUpdate >= TimeUntilUpdate)
                //{
                //    GameTime.Update(TimeUntilUpdate, clock.ElapsedTime.AsSeconds());
                //    Update(GameTime);

                //    timeBeforeUpdate = 0;

                //    Window.Clear(ClearColor);
                //    Draw(GameTime);
                //    Window.Display();
                //}
                float elapsedTime = clock.ElapsedTime.AsMilliseconds();
                GameTime.Update(TimeUntilUpdate, clock.ElapsedTime.AsSeconds());
                Update(GameTime);
                elapsedTime = clock.ElapsedTime.AsMilliseconds() - elapsedTime;

                if (TimeUntilUpdate * 1000 < elapsedTime)
                    Thread.Sleep((int)(elapsedTime - TimeUntilUpdate * 1000));

                Window.Clear(ClearColor);
                Draw(GameTime);
                Window.Display();
            }
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);


    }
}
