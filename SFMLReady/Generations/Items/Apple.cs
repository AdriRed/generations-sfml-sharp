using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generations.DefaultClasses;
using Generations.Interfaces;
using SFML.System;
using SFML.Graphics;

namespace Generations.Items
{
    class Apple : Food
    {
        private static float Radius = 8, Outline = 1;
        private static Color DefaultColor = Color.Red, OutlineColor = Color.Black;

        private Shape shape;
        public Apple(Vector2f position) : base(position)
        {
            shape = new CircleShape(Radius);
            shape.FillColor = DefaultColor;
            shape.OutlineThickness = Outline;
            shape.OutlineColor = OutlineColor;
            shape.Origin = new Vector2f(Radius, Radius);
        }

        public override void Draw(RenderWindow Window)
        {
            shape.Position = this.Position;
            Window.Draw(shape);
        }
    }
}
