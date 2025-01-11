using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class LoadPath
    {
        public static void LoadPathFromFile(CatmullRomPath path, string file)
        {
            //Read in file from string
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                path.AddPoint(InputParser.ParseVector2(line));
            }
        }
    }
}
