using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generations.DefaultClasses
{
    public class WorldTime
    {
        public const float DayLength = 20;
        public const float DeltaTime = 0.03f;

        public WorldTime()
        {
            Day = 0;
        }

        public float ActualTime { get; private set; }
        public uint Day { get; private set; }
        public bool FinishedDay
        {
            get
            {
                return ActualTime >= DayLength;
            }
        }

        public void NewDay()
        {
            ActualTime = 0;
            Day++;
        }

        public void Update(float seconds)
        {
            ActualTime += DeltaTime * seconds;
        }


    }
}
