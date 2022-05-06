using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class Edit_and_Remove
    {
        public static int EditRemove(string path, int edOrRem, int currCash, List<Items> unsavedItems = null)
        {
            // Starting variables used throughout code
            string[] entireListArr = File.ReadAllLines(path);
            List<Items> entireList = Items.ListCreate(entireListArr);
            List<string> choices = new List<string>()
            {
                "Search through the entire list sorted by date",
                "Give input name to look through the list"
            };

            // Choicelist will be the resulting list out of which the person can choose which to delete or edit
            List<string> choiceList = new List<string>();
            string searcher = "";

            // Adding unsaved items if necessary, then sorting the list
            if (unsavedItems != null) { entireList.AddRange(unsavedItems); }
            entireList.Sort((x, y) => DateTime.Compare(y.Date, x.Date));


            // Starting actual code. Saving choice in a temp int as we'll be changing 'i'
            int i = Menus.ChoicePicker(choices, "How would you like to find your item?\n");
            int[] listNrHolder = new int[50];
            int listNrHolderCounter = 0;
            int iTemp = -1;

            // Ask what to search for if person wants to search by string
            if (i == 2)
            {
                Console.Write("\n\nPlease input the title (or name) of what you would like to delete: ");
                searcher = Console.ReadLine().ToLower();
            }

            // Make list based on choice
            foreach (Items item in entireList)
            {
                iTemp++;
                if (i == 2 && !item.Title.ToLower().Contains(searcher))
                {
                    continue;
                }
                string itemTitle;
                if (item.Title.Length > 14) { itemTitle = item.Title[..14]; }
                else { itemTitle = item.Title; }
                choiceList.Add(itemTitle.PadRight(15) + item.Amount.ToString().PadRight(15) + item.DateString);

                // Fill in an array with each result's position, in case person searches by input string instead of the entire list
                if (i == 2)
                {
                    listNrHolder[listNrHolderCounter] = iTemp;
                    listNrHolderCounter++;
                }
            }

            iTemp = i;
            i = Menus.ChoicePicker(choiceList, "") - 1;

            // Making sure there are no duplicates
            if (iTemp == 2)
            {
                // changing i into the original list's position number
                i = listNrHolder[i];

                // Finding its index in the original list using an exact comparison of the choice into the original list. This avoids accidental duplication
                i = entireList.FindIndex(a => a.Title == entireList[i].Title && a.Amount == entireList[i].Amount && a.DateString == entireList[i].DateString);
            }

            // Now we edit or remove!
            // Edit is 21, remove is 22
            if (edOrRem == 21)
            {
                int priceChanges = 0;
                (priceChanges, entireList) = AddingStuffs.AddingStuff(entireList, i);
                currCash += priceChanges;
                Program.Save(path, currCash, entireList, true);
                return currCash;
            }

            // Remove item prompt for certainty, and then actual removal
            else
            {
                Colouriser.ColourMaker($"\nWARNING: Are you sure you want to delete \"{entireList[i].Title}\"? Doing so will automatically save any changes.\nWrite Y/N\n", true);
                do
                {
                    ConsoleKeyInfo certain = Console.ReadKey();
                    if (certain.Key == ConsoleKey.Y)
                    {
                        int cashChange = Convert.ToInt32(entireList[i].Amount);
                        currCash -= cashChange;
                        entireList.RemoveAt(i);
                        Program.Save(path, currCash, entireList, true);
                        return currCash;

                    }
                    else if (certain.Key == ConsoleKey.N)
                    {
                        return currCash;
                    }
                    else { Colouriser.ColourMaker("Please press Y or N\n", true); }
                }
                while (true);
            }
        }
    }
}
