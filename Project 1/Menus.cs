using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class Menus
    {
        public static int ChoicePicker(List<string> options, string title)
        {
            int curItem = 0;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine(title);
                for (int i = 0; i < options.Count; i++)
                {
                    if (curItem == i)
                    {
                        Console.Write(">> (");
                        Colouriser.ColourMaker(Convert.ToString(i + 1), false, 13);
                        Console.WriteLine($") {options[i]}");
                    }

                    else
                    {
                        Console.Write("(");
                        Colouriser.ColourMaker(Convert.ToString(i + 1), false, 13);
                        Console.WriteLine($") {options[i]}");
                    }
                }

                // What the arrow keys do
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Select your choice with the arrow keys.");
                Console.ResetColor();
                key = Console.ReadKey(true);

                if (key.Key.ToString() == "DownArrow")
                {
                    curItem = (curItem >= (options.Count - 1)) ? 0 : (curItem + 1);
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem = (curItem == 0) ? options.Count - 1 : (curItem - 1);
                }
            }

            // press enter = break loop and go to current choice item thing
            while (key.KeyChar != 13);
            return (curItem + 1);
        }

        public static (int, int) ShowMenu(int currCash)
        {
            // Creating menu and submenu lists. Also taking in current money to show at the top.        
            List<string> optionsMain = new List<string>
            {
                "Show items (All/Expense(s)/Income(s))",
                "Add new expense/Income",
                "Edit item (edit, remove)",
                "Save and quit",
                "Delete entire list"
            };
            List<string> optionsOne = new List<string>
            {
                "Show only expenses",
                "Show only incomes",
                "Show all"
            };
            List<string> optionsThree = new List<string>
            {
                "Edit an item",
                "Remove an item"
            };

            // Choice picker using list up there. Return the value chosen.
            // We are also inserting a comma into the cash by converting it to a string.
            // ==> I started with floats and doubles, but learned they are not accurate. Instead I multiply
            //     every cash income by 100, and then add a comma afterwards. This keeps values after the comma accurate!
            int i = ChoicePicker(optionsMain, $"Welcome to TrackMoney!\nYou currently have {currCash.ToString().Insert(currCash.ToString().Length - 2, ",")} SEK on your account.\nPick an option:\n");

            // Depending on choice, go to submenu below
            switch (i)
            {
                case 1:
                    i = ChoicePicker(optionsOne, "Welcome to TrackMoney!\nPlease pick an option:\n"); break;
                case 2:
                    i = 10;
                    break;
                case 3:
                    i = (ChoicePicker(optionsThree, "Welcome to TrackMoney!\nPlease pick an option:\n") + 20); break;
                case 4:
                    i = -1;
                    break;
                case 5:
                    i = -2;
                    break;
            }
            return (i, currCash);
        }
    }
}
