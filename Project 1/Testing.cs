using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class Testing
    {
        static int testing(string datePart)
        {
            /// Code scrapped because: Asking year, month, day separately meant people could
            /// put in, for example, February 30, and break the code.
            /// This is a no bueno
            // variables used in code
            bool leave = false;
            string datePartShort;
            int result;

            // Renaming variables based on input
            if (datePart == "day") datePartShort = "dd";
            else if (datePart == "month") datePartShort = "MM";
            else datePartShort = "yyyy";

            // Creating the question and check if it works out
            do
            {
                Console.Write($"Please input a {datePart} in {datePartShort} format: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result))
                {
                    Colouriser.ColourMaker("This is not a valid number\n", true);
                }
                if (result > 31 || result < 1)
                {
                    Colouriser.ColourMaker($"Please use the {datePartShort} format\n", true);
                }
                else { leave = true; }
            }
            while (leave == false);
            return result;
        }
    }
}
