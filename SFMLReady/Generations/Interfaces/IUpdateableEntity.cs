using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generations.Interfaces
{
    interface IUpdateableEntity
    {
        bool CanDispose { get; set; }
        void Update(float seconds);
    }
}
