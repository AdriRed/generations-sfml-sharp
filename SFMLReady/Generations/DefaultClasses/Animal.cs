using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFMLReady.Libraries;

namespace Generations.DefaultClasses
{
    abstract class Animal : MovingEntity
    {
        public static int CurrentAnimal;
        protected virtual double FacingVariance
        {
            get
            {
                return 2;
            }
        }
        public char Id { get; private set; }
        
        protected float Energy;
        protected WorldTime Time;
        protected Random rd;
        protected Entity Target;
        public int FoodQuantity;
        protected Vector2f WorldLimits;

        static Animal()
        {
            CurrentAnimal = 0;
        }

        public Animal(Vector2f position, Vector2f facing, GeneticData data, Random rd, Vector2f worldLimits, WorldTime time) : base(position, facing)
        {
            GeneticData = data;
            Target = null;
            this.rd = rd;
            WorldLimits = worldLimits;
            Time = time;
            Id = (char) ('A' + CurrentAnimal);

            CurrentAnimal++;
        }

        public GeneticData GeneticData { get; private set; }

        public AnimalData Data
        {
            get
            {
                return new AnimalData(GeneticData, Id, FoodQuantity);
            }
        }

        protected virtual void Find(List<Entity> targets)
        {
            foreach (Entity item in targets)
            {
                if (GeneticData.ViewRange < Vector2.Distance(this.Position, item.Position))
                {
                    Target = item;
                }
            }
        }

        public virtual void Act()
        {
            
            if (Target != null)
            {
                if (Vector2.Distance(this.Position, Target.Position) <= GeneticData.ActRange)
                {
                    Interactuate();
                }
                else
                {
                    LookAt(Target);
                }
            }
            else
            {
                RandomFacing();
            }
            
        }

        protected virtual void RandomFacing()
        {
            double varianceX = (rd.NextDouble() * FacingVariance) - FacingVariance / 2;
            double varianceY = (rd.NextDouble() * FacingVariance) - FacingVariance / 2;

            Facing += new Vector2f((float)varianceX, (float)varianceY);
            Facing = Vector2.Normalize(Facing);
        }

        protected abstract void Interactuate();

        protected virtual void Eat()
        {
            FoodQuantity++;
            Target.canDispose = true;
            Target = null;
        }
    }

    public struct AnimalData
    {
        public GeneticData Genes;
        public int FoodQuantity;
        public char Id;

        public AnimalData(GeneticData genes, char id, int foodQuantity)
        {
            Genes = genes;
            FoodQuantity = foodQuantity;
            Id = id;
        }

        public Dictionary<string, string> DataToDictionary()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("Id", Id.ToString());
            data.Add("Food Quantity", FoodQuantity.ToString());
            foreach (KeyValuePair<string, string> item in Genes.DataToDictionary())
            {
                data.Add(item.Key, item.Value);
            }


            return data;
        }
    }

    public struct GeneticData
    {
        public float ViewRange;
        public float Speed;
        public float MutationRate;
        public float ActRange;

        public GeneticData(float viewRange, float actRange, float speed, float mutationRate)
        {
            ViewRange = viewRange;
            Speed = speed;
            MutationRate = mutationRate;
            ActRange = actRange;
        }

        public GeneticData Copy()
        {
            return new GeneticData(ViewRange, ActRange, Speed, MutationRate);
        }

        public void Mutate(Random rd)
        {
            ViewRange += (float)(rd.NextDouble() - 0.5);
            Speed += (float)(rd.NextDouble() - 0.5);
            MutationRate += (float)(rd.NextDouble() - 0.5);
            ActRange += (float)(rd.NextDouble() - 0.5);
        }

        public Dictionary<string, string> DataToDictionary()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("View Range", ViewRange.ToString());
            data.Add("Speed", Speed.ToString());
            data.Add("Act Range", ActRange.ToString());
            data.Add("Mutation Rate", MutationRate.ToString());

            return data;
        }
    }
}
