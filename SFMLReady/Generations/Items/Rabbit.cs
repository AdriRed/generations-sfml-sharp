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
        protected override double FacingVariance
        {
            get
            {
                return Math.PI;
            }
        }

        private Shape shape;
        private static float Size = 15, Outline = 1;
        private static Color OutlineColor = Color.Black, FillColor = Color.Yellow;
        private List<Entity> Entities;

        public Rabbit(Vector2f position, Vector2f facing, AnimalData data, List<Entity> entities) : base(position, facing, data)
        {
            shape = new RectangleShape(new Vector2f(Size, Size));
            shape.FillColor = FillColor;
            shape.OutlineThickness = Outline;
            shape.OutlineColor = OutlineColor;
            shape.Origin = new Vector2f(Size / 2, Size / 2);
            Entities = entities;
        }

        public override void Draw(RenderWindow Window)
        {
            shape.Position = Position;
            //shape.Rotation = (float)(Math.Atan(Facing.Y / Facing.X) * 180f / Math.PI);
            Window.Draw(shape);
        }

        protected override void Find(List<Entity> targets)
        {
            if (Target == null)
                foreach (Entity item in targets)
                {
                    if (item is Food f && 
                        Data.ViewRange >= Vector2.Distance(this.Position, item.Position))
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
            Find(Entities);
            Act();
            Move(Data.Speed, seconds);
        }
    }
}
