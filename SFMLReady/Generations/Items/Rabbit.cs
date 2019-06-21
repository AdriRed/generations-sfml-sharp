using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generations.DefaultClasses;
using Generations.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFMLReady.Libraries;

namespace Generations.Items
{
    class Rabbit : Animal
    {
        private readonly float EnergyDefault = 100000;

        private float DeltaEnergy
        {
            get
            {
                return GeneticData.Speed * GeneticData.ViewRange;
            }
        }

        private static float Size = 7, Outline = 1;
        private static Color OutlineColor = Color.Black, FillColor = Color.Yellow, RangeColor = new Color(0, 0, 0, 0);
        protected override double FacingVariance
        {
            get
            {
                return 0.35;
            }
        }

        private Text lbl_Food;
        public bool InHouse = false;
        public Shape shape;
        private Shape rangeShape;
        private List<Entity> Entities;

        public Rabbit(Vector2f position, Vector2f facing, GeneticData data, Random rd, List<Entity> entities, Vector2f limits, WorldTime time) : base(position, facing, data, rd, limits, time)
        {
            shape = new CircleShape(Data.Genes.ActRange, 3);
            shape.FillColor = GetColorByData();
            shape.OutlineThickness = Outline;
            shape.OutlineColor = OutlineColor;
            shape.Origin = new Vector2f(Size, Size);
            Entities = entities;

            //rangeShape = new CircleShape(data.ViewRange);
            //rangeShape.OutlineColor = OutlineColor;
            //rangeShape.OutlineThickness = Outline;
            //rangeShape.FillColor = RangeColor;
            //rangeShape.Origin = new Vector2f(data.ViewRange, data.ViewRange);

            Energy = EnergyDefault;

            lbl_Food = new Text(FoodQuantity.ToString(), DebugSimulationData.Font, 10);
            lbl_Food.FillColor = DebugSimulationData.FontColor;
        }

        private Color GetColorByData()
        {
            byte
                g = (byte)((GeneticData.MutationRate - DefaultsData.MutationBase) / DefaultsData.MutationVariation * 256),
                r = (byte)( (GeneticData.Speed - DefaultsData.SpeedBase) / DefaultsData.SpeedVariation * 256 ),
                b = (byte)( (GeneticData.ViewRange - DefaultsData.ViewRangeBase) / DefaultsData.ViewRangeVariation * 256 );


            return new Color(r, g, b);
        }

        public override void Draw(RenderWindow Window)
        {
            if (!InHouse)
            {
                Window.Draw(shape);
                Window.Draw(lbl_Food);
                //Window.Draw(rangeShape);
            }
        }

        protected override void Find(List<Entity> targets)
        {
            if (FoodQuantity > 0)
            {
                Target = targets[0];
            } else
            {
                Target = null;
            }
            
            foreach (Entity item in targets)
            {
                if (item is Food f && 
                    GeneticData.ViewRange >= Vector2.Distance(this.Position, item.Position))
                {
                    if (Target != null)
                    {
                        if (Vector2.Distance(this.Position, item.Position) < Vector2.Distance(this.Position, Target.Position))
                        {
                            Target = item;
                        }
                    } else
                    {
                        Target = item;
                    }
                }
            }
        }

        public override void Update(float seconds)
        {
            if (!InHouse)
            {
                lbl_Food.DisplayedString = FoodQuantity.ToString();
                lbl_Food.Position = Position + new Vector2f(0, 10) * shape.Scale.X;
                lbl_Food.Scale = shape.Scale;
                Find(Entities);
                Act();
                Move(GeneticData.Speed, seconds);
                Energy -= DeltaEnergy * seconds;

                if (Energy <= 0 || Time.FinishedDay)
                {
                    if (FoodQuantity > 0)
                    {
                        FoodQuantity--;
                        Energy = EnergyDefault;
                    } else
                    {
                        CanDispose = true;
                    }
                    
                }

                shape.Position = Position;
                shape.Rotation = (float)(Math.Atan(Facing.Y / Facing.X) * 180f / Math.PI);
                //rangeShape.Position = Position;
            }
            
        }

        protected override void Interactuate()
        {
            if (Target is Food)
            {
                Eat();
            }
            if (Target is House)
            {
                InHouse = true;
            }
        }

        public override void Move(float speed, float seconds)
        {
            Vector2f nextPosition = Position + Facing * speed * seconds;

            if (nextPosition.X >= 0 && nextPosition.X <= WorldLimits.X &&
                nextPosition.Y >= 0 && nextPosition.Y <= WorldLimits.Y)
            {
                Position = nextPosition;
            }
        }

        public override string ToString()
        {
            return "Rabbit " + Id + " is " + (!CanDispose ? "Alive" : "Dead");
        }
    }
}
