using System;
using Generations;
using SFML.Audio;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace SFMLReady
{
    class Program
    {
        public static void Main(string[] args)
        {
            Simulation s = new Simulation(800, 800, "SIMULATION", Color.White);
            s.Run();
        }
    } 
}