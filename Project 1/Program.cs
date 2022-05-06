using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Project_1
{

    internal class Program
    {
        public static void Save(string path, int currCash, List<Items> addedItems = null, bool remove = false)
        {
            int posSetter;

            // Will delete items, or create examples if nothing exists yet
            if (!File.Exists(path) || remove == true)
            {
                List<Items> examples = new List<Items>() {
                    new Items("Vests", -15000, new DateTime(2021, 08, 30)),
                    new Items("Horse", -375000, new DateTime(2022, 04, 25)),
                    new Items("Car", -4000000, new DateTime(2021, 08, 26)),
                    new Items("Computer", -2150000, new DateTime(2020, 12, 15)),
                    new Items("Shirts", -25000, new DateTime(2018, 05, 15)),
                    new Items("Basketball", 150000, new DateTime(2022, 01, 31)),
                    new Items("Found", 375000, new DateTime(2022, 04, 25)),
                    new Items("Robbed", 4000000, new DateTime(2022, 04, 27))
                };
                if (addedItems == null) { addedItems = examples; }

                string[] listCreator = new string[(addedItems.Count) * 3 + 1];
                posSetter = 1;
                listCreator[0] = currCash.ToString();

                for (int ik = 0; ik < addedItems.Count; ik++)
                {
                    listCreator[posSetter] = addedItems[ik].Title;
                    posSetter++;
                    listCreator[posSetter] = addedItems[ik].Amount.ToString();
                    posSetter++;
                    listCreator[posSetter] = addedItems[ik].DateString;
                    posSetter++;
                }
                File.WriteAllLines(path, listCreator);
            }

            // Save newly added items if the file does exist
            else if (addedItems != null && remove == false)
            {
                string[] entireListArr = File.ReadAllLines(path);
                posSetter = (entireListArr.Length);
                int newSize = (addedItems.Count * 3 + entireListArr.Length);
                Array.Resize(ref entireListArr, newSize);

                for (int ik = 0; ik < addedItems.Count; ik++)
                {
                    entireListArr[posSetter] = addedItems[ik].Title;
                    posSetter++;
                    entireListArr[posSetter] = addedItems[ik].Amount.ToString();
                    posSetter++;
                    entireListArr[posSetter] = addedItems[ik].DateString;
                    posSetter++;
                }
                entireListArr[0] = currCash.ToString();
                File.WriteAllLines(path, entireListArr);
            }
        }

        static void Main(string[] args)
        {
            int i = Menus.ChoicePicker(new List<string>() { "Account 1","Account 2","Account 3"}, "Please choose which account to open");
            string path = "";
            // Base variables used
            if (i == 1) { path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "List.txt"); }
            else if (i == 2) { path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lists_2.txt"); }
            else { path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lists_3.txt"); }

            // Inputs the examples if they do not yet exist
            // Delete temporary files just in case they somehow exist
            if (!File.Exists(path)) { Save(path, 500000); }

            // Variables used in code below
            List<Items> addedItems = new List<Items>();
            Cash currentCash = new Cash(path);
            int currCash = currentCash.NewCash;
            int priceChanges;


        MainMenu:
            (i, currCash) = Menus.ShowMenu(currCash);

            // 1-10 = Show Items. 11-20 = Adding items. 21-30 = Editing Items
            // -1 for Save and quit, -2 for deleting the list

            // Save and quit
            if (i == -1)
            {
                Save(path, currCash, addedItems);
                Environment.Exit(0);
            }

            // List confirmation for (and then) deletion
            else if (i == -2)
            {
                Console.Clear();
                Colouriser.ColourMaker("You are about to delete the entire list. \nWRITE \"YES\" IF YOU ARE YOU CERTAIN!\n", true);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                string certain = Console.ReadLine();
                if (certain == "YES")
                {
                    File.Delete(path);
                    Colouriser.ColourMaker("File wiped. Closing the application.", true);
                    Environment.Exit(520);
                }
                else
                {
                    Colouriser.ColourMaker("Files have not been deleted.\n", false, 14);
                    Console.Write("Returning to main menu.");
                    for (int j = 0; j < 3; j++)
                    {
                        Thread.Sleep(1000);
                        Console.Write(".");
                    }
                }
                goto MainMenu;
            }

            // Menu for showing the items, sorted according to submenu choice
            else if (i < 10)
            {
                Console.Clear();
                Console.WriteLine("Showing the list below.");
                if (addedItems.Count != 0) { Items.ShowItems(i, path, addedItems); }
                else Items.ShowItems(i, path);
            }

            // Submenu inside addingstuff to have expenses and incomes alternately be added more easily
            // instead of having to go back to menu each time
            else if (i < 20)
            {
                (priceChanges, addedItems) = AddingStuffs.AddingStuff(addedItems);
                currCash += priceChanges;
            }

            // Editing and removing specific items
            // Setting addeditems back to new list because EditRemoved automatically saves
            else if (i < 30)
            {
                // Edit is 21, remove is 22
                currCash = Edit_and_Remove.EditRemove(path, i, currCash, addedItems);
                addedItems = new List<Items>();
            }
            goto MainMenu;
        }
    }
}
