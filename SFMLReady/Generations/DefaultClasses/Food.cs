using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Generations.DefaultClasses
{
    abstract class Food : Entity
    {
        public Food(Vector2f position) : base(position)
        {

        }

    }
}
