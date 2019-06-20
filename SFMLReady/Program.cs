using System;
using System.Collections.Generic;
using Generations;
using Generations.DefaultClasses;
using SFML.Audio;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using System.IO;

namespace SFMLReady
{
    class Program
    {
        public static void Main(string[] args)
        {
            Simulation s = new Simulation(1300, 800, "SIMULATION", Color.White);
            s.Run();

            SimulationData data = s.AllData;

            FileSaveData(data);

        }

        private static void FileSaveData(SimulationData data)
        {
            StreamWriter sw;
            string directory = @"./data";
            Line ln;
            
            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] files = d.GetFiles("*.csv");
            string fileName;
            int maxnum = 0;

            if (files.Length != 0)
            {
                foreach (FileInfo item in files)
                {
                    int num;
                    string[] splitted = item.Name.Split(new string[] { "simulation_", ".csv" }, StringSplitOptions.RemoveEmptyEntries);
                    if (Int32.TryParse(splitted[0], out num))
                    {
                        if (maxnum < num)
                            maxnum = num;
                    }
                }

                maxnum++;
            }
            fileName = String.Format("simulation_{0}.csv", maxnum);

            sw = new StreamWriter(directory + "/" + fileName, false);

            ln = new Line("Total animals," + data.TotalAnimals.ToString());
            sw.WriteLine(ln.LineInfo);
            foreach (GenerationData generation in data.Generation)
            {
                ln = new Line(generation.DataToDictionary(), false);
                sw.WriteLine(ln.LineInfo);
                foreach (AnimalData item in generation.Animals)
                {
                    ln = new Line(item.DataToDictionary(), false);
                    sw.WriteLine(ln.LineInfo);
                }
            }

            sw.Close();
            
        }

        class Line
        {
            static int number;
            public string LineInfo
            {
                get; private set;
            }
            static Line()
            {
                number = 0;
            }

            public Line(string data)
            {
                number++;
                LineInfo = "";// number.ToString();
                LineInfo += data;
            }

            public Line(string[] data)
            {
                number++;
                for (int i = 0; i < data.Length; i++)
                {
                    LineInfo += data[i] + ",";
                }
            }

            public Line(Dictionary<string, string> data, bool newLine)
            {
                number++;
                LineInfo = "";// number.ToString();
                foreach (KeyValuePair<string, string> item in data)
                {
                    Line l = new Line(item.Key + "," + item.Value + ",");
                    if (newLine)
                        LineInfo += l.LineInfo + "\n";
                }
            }
        }
    } 
}