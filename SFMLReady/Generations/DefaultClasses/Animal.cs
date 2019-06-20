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
        protected virtual double FacingVariance
        {
            get
            {
                return 0.5;
            }
        }

        protected Random rd;
        protected Entity Target;
        public int FoodQuantity;
        

        public Animal(Vector2f position, Vector2f facing, AnimalData data, Random rd) : base(position, facing)
        {
            Data = data;
            Target = null;
            this.rd = rd;
        }

        public AnimalData Data { get; private set; }

        protected virtual void Find(List<Entity> targets)
        {
            foreach (Entity item in targets)
            {
                if (Data.ViewRange < Vector2.Distance(this.Position, item.Position))
                {
                    Target = item;
                }
            }
        }

        public virtual void Act()
        {
            if (Target != null)
            {
                if (Vector2.Distance(this.Position, Target.Position) <= Data.ActRange)
                {
                    Interactuate();
                } else
                {
                    LookAt(Target);
                }
            } else
            {
                RandomFacing();
            }
        }

        protected virtual void RandomFacing()
        {
            double random = rd.NextDouble();
            double variance = (random * FacingVariance) - FacingVariance / 2;

            Facing += new Vector2f((float)variance, (float)variance);
            Facing = Vector2.Normalize(Facing);
        }

        protected virtual void Interactuate()
        {
            if (Target is Food)
            {
                Eat();
            }
        }

        protected virtual void Eat()
        {
            FoodQuantity++;
            Target.canDispose = true;
            Target = null;
        }
    }


    struct AnimalData
    {
        public float ViewRange;
        public float Speed;
        public float ReplicationRate;
        public float ActRange;

        public AnimalData(float viewRange, float actRange, float speed, float replicationRate)
        {
            ViewRange = viewRange;
            Speed = speed;
            ReplicationRate = replicationRate;
            ActRange = actRange;
        }
    }
}
