using System;
using System.Threading;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFMLReady.System;
using System.Collections.Generic;
using Generations.DefaultClasses;

namespace Generations
{
    public static class DebugSimulationData
    {
        public const string FontPath = "./fonts/Roboto-Regular.ttf";
        public const uint FontSize = 14;
        public static Color FontColor = new Color(10, 10, 10, 200);
        public static Font Font;

        public static void LoadContent()
        {
            Font = new Font(FontPath);
        }

        public static void DrawData(Simulation simulation)
        {
            if (Font != null)
            {
                float ActualY = 4, DeltaY = 20, ActualX = 8, DeltaX = 0;
                Dictionary<string, string> information = new Dictionary<string, string>();

                information.Add("Animals", simulation.Animals.ToString());
                information.Add("Food", simulation.Food.ToString());
                information.Add("Days", simulation.WorldTime.Day.ToString());

                foreach (KeyValuePair<string, string> item in information)
                {
                    Text actualText = new Text(item.Key + ": " + item.Value, Font, FontSize);
                    actualText.Position = new Vector2f(ActualX, ActualY);
                    actualText.FillColor = FontColor;

                    simulation.Window.Draw(actualText);

                    ActualX += DeltaX;
                    ActualY += DeltaY;
                }

            }
        }
    }
}
