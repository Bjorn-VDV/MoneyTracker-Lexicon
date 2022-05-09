using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Project_1
{
    class Items
    {
        public Items(string title, int amount, DateTime date)
        {
            this.Title = title;
            this.Amount = amount;
            this.DateString = date.ToString("dd/MM/yyyy");
            this.Date = date;
        }
        public string Title { get; set; }
        public int Amount { get; set; }
        public string DateString { get; set; }
        public DateTime Date { get; set; }

        public static List<Items> ListCreate(string[] theList)
        {
            string[] entireListArr = theList;
            List<Items> entireList = new List<Items>();
            for (int nr = 1; nr < entireListArr.Length - 1; nr++)
            {
                string title = entireListArr[nr];
                nr++;
                int amount = Convert.ToInt32(entireListArr[nr]);
                nr++;
                DateTime date = DateTime.ParseExact(entireListArr[nr], "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                entireList.Add(new Items(title, amount, date));
            }
            return entireList;
        }

        public static void ShowItems(int input, string path, List<Items> addedItems = null)
        {
            // Creating all the lists
            List<Items> outputList = new List<Items>();
            string[] entireListArr = File.ReadAllLines(path);
            List<Items> entireList = ListCreate(entireListArr);
            List<string> options = new List<string>()
            {
                "By item name",
                "By price, Ascending",
                "By price, Descending",
                "By date, Recent first",
                "By date, Recent last"
            };
            int totalListSize = (addedItems == null) ? entireList.Count : (addedItems.Count + entireList.Count);

            // Outputlist will be filled with the desired list.
            // Input: 1 == Expense, 2 == income, 3 == both
            if (input == 1 || input == 3)
            {
                if (addedItems != null)
                {
                    foreach (Items item in addedItems) { if (item.Amount < 0) outputList.Add(item); }
                }
                foreach (Items item in entireList) { if (item.Amount < 0) outputList.Add(item); }

            }
            if (input == 2 || input == 3)
            {
                if (addedItems != null)
                {
                    foreach (Items item in addedItems) { if (item.Amount >= 0) outputList.Add(item); }
                }
                foreach (Items item in entireList) { if (item.Amount >= 0) outputList.Add(item); }
            }

            // Sorting list based on choice of the person
            int i = Menus.ChoicePicker(options, "How would you like to sort your list?");
            switch (i)
            {
                case 1:
                    outputList.Sort((x, y) => string.Compare(x.Title, y.Title));
                    break;
                case 2:
                    outputList.Sort((x, y) => x.Amount.CompareTo(y.Amount));
                    break;
                case 3:
                    outputList.Sort((x, y) => y.Amount.CompareTo(x.Amount));
                    break;
                case 4:
                    outputList.Sort((x, y) => DateTime.Compare(y.Date, x.Date));
                    break;
                case 5:
                    outputList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                    break;

            };
            Console.Clear();

            // Outputting the list. If the title of the item is too big, we limit its length for readability reasons.
            foreach (Items item in outputList)
            {
                string itemTitle;
                if (item.Title.Length > 14) { itemTitle = item.Title[..14]; }
                else { itemTitle = item.Title; }
                string itemAmountString = (item.Amount / 100).ToString("n2");
                Console.WriteLine(itemTitle.PadRight(15) + string.Format("{0,11}",itemAmountString).PadRight(15) + item.DateString);
            }
            Colouriser.ColourMaker("Press enter to go back to the main menu.", false, 14);
            Console.ReadKey();
        }

    }
}
