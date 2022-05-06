﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class Colouriser
    {
        public static void ColourMaker(string input, bool error, int colourNr = 0, int? backColour = null)
        {
            if (error)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)colourNr;
                if (backColour != null) Console.BackgroundColor = (ConsoleColor)backColour;
            }
            Console.Write(input);
            Console.ResetColor();
        }

    }
}
