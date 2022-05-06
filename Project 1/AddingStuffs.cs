using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class AddingStuffs
    {

        public static (int, List<Items>) AddingStuff(List<Items> unsavedItems = null, int replacer = -1)
        {
            // Settings required variables
            List<Items> addItems = unsavedItems;
            List<string> yesNo = new List<string>() { "Yes", "No" };
            List<string> incOrExp = new List<string>
            {
                "This will be expense",
                "This will be income"
            };
            int priceChanges = 0;

            // Variables used in loop
            bool leave;
            string input;
            int itemPrice, year, month, day, expInc;
            DateTime itemDate;

            // Loop for making new items
            do
            {
                // starting with a clear console, then ask income or expense
                if (replacer != -1)
                {
                    expInc = Menus.ChoicePicker(incOrExp, "YOU ARE EDITING AN ITEM\n\nAre you making an income or an expense?");
                }
                else
                {
                    expInc = Menus.ChoicePicker(incOrExp, "Are you adding an income or an expense?");
                }
                Console.Clear();

                // Asking and retrieving item name
                Console.Write("Please add a name: ");
                string itemName = Console.ReadLine();

                // Getting a price. This has to be an int, so we make a check, otherwise ask again.
                leave = false;
                do
                {
                    Console.Write("Please add a value: ");
                    input = Console.ReadLine();
                    if (!double.TryParse(input, out double doublePrice))
                    {
                        Colouriser.ColourMaker("This is not a valid number\n", true);
                    }
                    itemPrice = Convert.ToInt32(doublePrice * 100);
                    if (expInc == 1 && itemPrice >= 0) { itemPrice = -itemPrice; leave = true; }
                    else if (expInc == 2 && itemPrice < 0) { Colouriser.ColourMaker("You are adding an income, not an expense\n", true); }
                    else leave = true;
                }
                while (leave == false);

                // Getting a date. Prompt if made today or not. If no, go to line 135
                int i = Menus.ChoicePicker(yesNo, "Was this transaction today?");
                if (i == 1)
                {
                    itemDate = DateTime.Now;
                }
                else
                {
                    // Clearing console from the prompt and recreating the "previous" menu for visibility reasons
                    Console.Clear();
                    Console.WriteLine($"Please add a name: {itemName}\nPlease add a price: {itemPrice}");

                    // Get the year
                    leave = false;
                    do
                    {
                        Console.Write("Please input a year in 'yyyy' format: ");
                        input = Console.ReadLine();
                        if (!int.TryParse(input, out year))
                        {
                            Colouriser.ColourMaker("This is not a valid number\n", true);
                        }
                        if (year > 9999 || year < 1000)
                        {
                            Colouriser.ColourMaker("Please use the 'yyyy' format\n", true);
                        }
                        else { leave = true; }
                    }
                    while (leave == false);

                    // Get the month
                    leave = false;
                    do
                    {
                        Console.Write("Please input a month in 'MM' format: ");
                        input = Console.ReadLine();
                        if (!int.TryParse(input, out month))
                        {
                            Colouriser.ColourMaker("This is not a valid number\n", true);
                        }
                        if (month > 12 || month < 1)
                        {
                            Colouriser.ColourMaker("Please use the 'MM' format\n", true);
                        }
                        else { leave = true; }
                    }
                    while (leave == false);

                    // Get the day
                    leave = false;
                    do
                    {
                        Console.Write("Please input a day in 'dd' format: ");
                        input = Console.ReadLine();
                        if (!int.TryParse(input, out day))
                        {
                            Colouriser.ColourMaker("This is not a valid number\n", true);
                        }
                        if (day > 31 || day < 1)
                        {
                            Colouriser.ColourMaker("Please use the 'dd' format\n", true);
                        }
                        else { leave = true; }
                    }
                    while (leave == false);

                    // Make inputs a datetime
                    itemDate = new DateTime(year, month, day);
                }

                // Check for if adding or editing if i== -1, it means we did NOT take an input to replace
                if (replacer == -1)
                {
                    // Adding the newly created item to the list
                    i = Menus.ChoicePicker(yesNo, $"You are adding {itemName}, priced at {itemPrice.ToString().Insert(itemPrice.ToString().Length - 2, ",")}, transaction made {itemDate}. \n\nIs this correct?\n");
                    if (i == 1)
                    {
                        addItems.Add(new Items(itemName, itemPrice, itemDate));
                        priceChanges += itemPrice;
                    }
                    else
                    {
                        Colouriser.ColourMaker("The item was not added.", true);
                    }
                    // Asking if one likes to make another item, or not
                    i = Menus.ChoicePicker(yesNo, "Would you like to create another item?");
                    if (i == 2) { break; }
                }
                else
                {
                    // EDITING newly created item with old one, prompt
                    i = Menus.ChoicePicker(yesNo, $"You are replacing {unsavedItems[replacer].Title}, priced at {unsavedItems[replacer].Amount}, transaction made {unsavedItems[replacer].DateString}" +
                        $"\nFor the following:\n{itemName}, priced at {itemPrice.ToString().Insert(itemPrice.ToString().Length - 2, ",")}, transaction made {itemDate}. \n\nIs this correct?\n");
                    if (i == 1)
                    {
                        int tempChange = unsavedItems[replacer].Amount;
                        addItems.RemoveAt(replacer);
                        addItems.Add(new Items(itemName, itemPrice, itemDate));
                        priceChanges += itemPrice -= tempChange;
                    }
                    else
                    {
                        Colouriser.ColourMaker("The item was not added.", true);
                    }
                    break;
                }
            }
            while (true);

            // Returning the created list, and job done
            return (priceChanges, addItems);
        }
    }
}
