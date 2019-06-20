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
        private int AppleQuant = 10, RabbitQuant = 5;

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
            float x, y, speed, viewRange, actRange, replication;
            for (int i = 0; i < AppleQuant; i++)
            {
                x = (float)rd.NextDouble() * Window.Size.X;
                y = (float)rd.NextDouble() * Window.Size.Y;

                Entities.Add(new Apple(new Vector2f(x, y)));
            }
            AnimalData data;

            for (int i = 0; i < RabbitQuant; i++)
            {
                x = (float)rd.NextDouble() * Window.Size.X;
                y = (float)rd.NextDouble() * Window.Size.Y;
                speed = DefaultsData.SpeedBase + (float)rd.NextDouble() * DefaultsData.SpeedVariation;
                viewRange = DefaultsData.ViewRangeBase + (float)rd.NextDouble() * DefaultsData.ViewRangeVariation;
                actRange = DefaultsData.ActBase + (float)rd.NextDouble() * DefaultsData.ActVariation;
                replication = DefaultsData.ReplicationBase + (float)rd.NextDouble() * DefaultsData.ReplicationVariation;


                data = new AnimalData(viewRange, actRange, speed, replication);
                Entities.Add(new Rabbit(new Vector2f(x, y), new Vector2f(0, 0), data, rd, Entities));
            }
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
    struct DefaultsData {
        public static float
            SpeedBase = 10,
            SpeedVariation = 4,

            ViewRangeBase = 80,
            ViewRangeVariation = 20,
            
            ActBase = 10,
            ActVariation = 0,
            
            ReplicationBase = 0,
            ReplicationVariation = 0;
            
    }
}
