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
    class House : Entity
    {
        private Shape Shape;

        private static float Size = 15, Outline = 1;
        private static Color OutlineColor = Color.Black, FillColor = Color.Magenta;

        public House(Vector2f position) : base(position)
        {
            Shape = new CircleShape(Size, 5);
            Shape.FillColor = FillColor;
            Shape.OutlineColor = OutlineColor;
            Shape.OutlineThickness = Outline;
            Shape.Position = Position;
        }

        public override void Draw(RenderWindow Window)
        {
            Window.Draw(Shape);
        }
    }
}
