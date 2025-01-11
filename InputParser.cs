using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TowerDefense
{
    // InputParser
    // Because this class is simply a collection of helper functions,
    // it is suitable to declare as static.
    public static class InputParser
    {
        //convert string to int
        public static int ParseInt(string str)
        {
            if (!int.TryParse(str, out int x))
                throw new Exception("Couldn't parse '" + str + "' to int");
          
            return x;
        }

        //Convert comma string to int
        public static int[] ParseInts(string str)
        {
            string[] strings = str.Split(',');
            int[] ints = new int[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                ints[i] = ParseInt(strings[i]);
            }
            return ints;
        }

        // Parse string of 2x comma-separated integers from string to a Vector2
        // E.g. "1, 5" -> Vector2(1, 5)
        public static Vector2 ParseVector2(string line)
        {
            int[] ints = ParseInts(line);

            return new Vector2(ints[0], ints[1]);
        }
    }
}
