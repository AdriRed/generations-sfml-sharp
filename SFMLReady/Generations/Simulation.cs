using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFMLReady.System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Generations.Items;
using Generations.Interfaces;
using Generations.DefaultClasses;

namespace Generations
{
    class Simulation : GameLoop
    {
        private List<Entity> Entities;
        Random rd;

        public Simulation(uint width, uint height, string title, Color clearColor) : base(width, height, title, clearColor)
        {
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Entity item in Entities)
            {
                item.Draw(Window);
            }
            DebugUtility.DrawPerformanceData(this);
        }

        public override void Initialize()
        {
            float x, y;
            for (int i = 0; i < 5; i++)
            {
                x = (float)rd.NextDouble() * Window.Size.X;
                y = (float)rd.NextDouble() * Window.Size.Y;

                Entities.Add(new Apple(new Vector2f(x, y)));
            }
            x = (float)rd.NextDouble() * Window.Size.X;
            y = (float)rd.NextDouble() * Window.Size.Y;
            AnimalData data = new AnimalData(20, 10, 4, 0);

            Entities.Add(new Rabbit(new Vector2f(x, y), new Vector2f(0, 0), data, Entities));
        }

        public override void LoadContent()
        {
            Entities = new List<Entity>();
            rd = new Random();
            DebugUtility.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                if (Entities[i].CanDispose)
                {
                    Entities.RemoveAt(i);
                    i--;
                }
            }

            foreach (Entity item in Entities)
            {
                item.Update(GameTime.DeltaTime);
            }
        }
    }
}
