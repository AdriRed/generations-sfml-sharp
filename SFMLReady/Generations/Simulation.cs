using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFMLReady.System;
using SFMLReady.Libraries;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Generations.Items;
using Generations.Interfaces;
using Generations.DefaultClasses;

namespace Generations
{
    public class Simulation : GameLoop
    {
        private static float Padding = 30f;
        private List<Entity> Entities;
        public List<GenerationData> SimulationData;
        Random rd;
        private int AppleQuant = 25, RabbitQuant = 100, MinApples = 10;
        public WorldTime WorldTime { get; private set; }

        public SimulationData AllData
        {
            get
            {
                return new SimulationData(SimulationData);
            }
        }

        public int Animals
        {
            get
            {
                int count = 0;
                foreach (Entity item in Entities)
                {
                    if (item is Animal) count++;
                }
                return count;
            }
        }

        public int Food
        {
            get
            {
                int count = 0;
                foreach (Entity item in Entities)
                {
                    if (item is Food) count++;
                }
                return count;
            }
        }

        public Simulation(uint width, uint height, string title, Color clearColor) : base(width, height, title, clearColor)
        {
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Entity item in Entities)
            {
                item.Draw(Window);
            }
        }

        public override void Initialize()
        {
            
            //Set house
            Entities.Add(new House(new Vector2f(Window.Size.X / 2, Window.Size.Y / 2)));
            DebugSimulationData.LoadContent();

            SetFood();
            SetAnimals();
            SimulationData = new List<GenerationData>();
        }

        private void SetAnimals()
        {
            for (int i = 0; i < RabbitQuant; i++)
            {
                Animal a = NewRabbit();
                Entities.Add(a);
            }
        }

        private Rabbit NewRabbit()
        {
            float speed, viewRange, actRange, mutation;
            GeneticData data;

            speed = DefaultsData.SpeedBase + (float)(rd.NextDouble() - 0.5) * DefaultsData.SpeedVariation;
            viewRange = DefaultsData.ViewRangeBase + (float)(rd.NextDouble() - 0.5) * DefaultsData.ViewRangeVariation;
            actRange = DefaultsData.ActBase + (float)(rd.NextDouble() - 0.5) * DefaultsData.ActVariation;
            mutation = DefaultsData.MutationBase + (float)(rd.NextDouble() - 0.5) * DefaultsData.MutationVariation;

            data = new GeneticData(viewRange, actRange, speed, mutation);

            return NewRabbit(data);
            
        }

        private Rabbit NewRabbit(GeneticData data)
        {
            Vector2f position, facing;

            position = Vector2.Random(rd, Padding, Window.Size.X - Padding, Padding, Window.Size.Y - Padding);
            facing = Vector2.Random(rd, -1, 1, -1, 1);

            return new Rabbit(position, facing, data, rd, Entities, (Vector2f)Window.Size, WorldTime);
        }
        private void SetFood()
        {
            Vector2f position;

            for (int i = 0; i < AppleQuant; i++)
            {
                position = Vector2.Random(rd, Padding, Window.Size.X - Padding, Padding, Window.Size.Y - Padding);

                Entities.Add(new Apple(position));
            }
        }

        public override void LoadContent()
        {
            WorldTime = new WorldTime();
            Entities = new List<Entity>();
            rd = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            WorldTime.Update(GameTime.DeltaTime);
            CheckDeads();
            foreach (Entity item in Entities)
            {
                item.Update(GameTime.DeltaTime);
            }
            if (WorldTime.FinishedDay)
            {
                NewDay();
            }
            
        }

        private void NewDay()
        {
            SaveData();
            int maximum = Entities.Count;
            GeneticData newData;
            for (int i = 0; i < maximum; i++)
            {
                if (Entities[i] is Animal newAnimal)
                {
                    for (int r = 0; r < newAnimal.FoodQuantity; r++)
                    {
                        newData = newAnimal.GeneticData.Copy();
                        if (rd.NextDouble() <= newData.MutationRate)
                        {
                            newData.Mutate(rd);
                        }

                        Entities.Add(NewRabbit(newData));
                    }
                    newAnimal.FoodQuantity = 0;
                }
            }
            WorldTime.NewDay();
            if (AppleQuant-- < MinApples)
                AppleQuant++;
            SetFood();
        }

        private void SaveData()
        {
            List<AnimalData> animalData = new List<AnimalData>();
            GenerationData thisGeneration;
            int food = 0;
            foreach (Entity item in Entities)
            {
                if (item is Food)
                {
                    food++;
                }
                else if (item is Animal a)
                {
                    animalData.Add(a.Data);
                    food += a.FoodQuantity;
                }
            }
            thisGeneration = new GenerationData(animalData, food, (int)WorldTime.Day);
            SimulationData.Add(thisGeneration);
        }

        private void CheckDeads()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                if (Entities[i].CanDispose)
                {
                    Entities.Remove(Entities[i]);
                    i--;
                }
            }
        }
    }
    struct DefaultsData {
        public static float
            SpeedBase = 5,
            SpeedVariation = 4,

            ViewRangeBase = 100,
            ViewRangeVariation = 50,
            
            ActBase = 5,
            ActVariation = 3,
            
            MutationBase = 10f,
            MutationVariation = 5f;
            
    }

    public struct GenerationData
    {
        public int Id;
        public int AvailableFood;
        public List<AnimalData> Animals;
        public int SurvivedAnimals;
        public int TotalAnimals;

        public GenerationData(List<AnimalData> animals, int availableFood, int id)
        {
            AvailableFood = availableFood;
            SurvivedAnimals = 0;
            TotalAnimals = 0;
            Animals = animals;
            Id = id;

            foreach (AnimalData item in Animals)
            {
                TotalAnimals++;
                if (item.FoodQuantity > 0)
                {
                    SurvivedAnimals++;
                }
            }
        }

        public Dictionary<string, string> DataToDictionary()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("Id", Id.ToString());
            data.Add("Survived Animals", SurvivedAnimals.ToString());
            data.Add("Total Animals", TotalAnimals.ToString());
            data.Add("Available Food", AvailableFood.ToString());

            return data;
        }
    }

    public struct SimulationData
    {
        public List<GenerationData> Generation;
        public int TotalAnimals;
        public SimulationData(List<GenerationData> generation)
        {
            Generation = generation;
            TotalAnimals = Animal.CurrentAnimal + 1;
        }
    }
}
