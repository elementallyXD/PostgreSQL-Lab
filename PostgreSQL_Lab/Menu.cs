using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL_Lab
{
    class Menu
    {
        private static readonly List<string> menuItems = new List<string>() {
                "Insert",
                "Update",
                "Delete",
                "Search by attributes",
                "Full text search",
                "Get all",
                "Get by PK",
                "Exit"
        };

        private static readonly List<string> insertMenuItems = new List<string>() { 
                "Random",
                "Console",
                "Back"
        };

        private static readonly List<string> fullSearchItems = new List<string>() {
                "Word",
                "Phrase",
                "Back"
        };

        private static readonly List<string> tableMenuItems = new List<string>() {
                "Customer",
                "Product",
                "Supplier",
                "Buyers",
                "Back"
        };
        private static int index = 0;

        public Menu(){
            while(true){
            Console.CursorVisible = false;
                DrawMenu(menuItems);
                int selectedMenu = SelectedMenu(menuItems.Count);
                switch (selectedMenu)
                {
                    case 0:
                        InsertMenu();
                        break;
                    case 1:
                        Data.UpdateData();
                        break;
                    case 2:
                        Data.DeleteData();
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        SearchMenu();
                        break;
                    case 5:
                        Data.ReturnAll();
                        break;
                    case 6:
                        Data.GetByPk();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void SearchMenu(){
            bool existMenu = true;
            index = 0;
            while (existMenu)
            {
                Console.CursorVisible = false;
                DrawMenu(fullSearchItems);
                int selectedMenu = SelectedMenu(fullSearchItems.Count);
                switch (selectedMenu)
                {
                    case 0:
                        Data.FullTextSearchWord();
                        break;
                    case 1:      
                        Data.FullTextSearchPhrase();
                        break;
                    case 2:
                        Console.WriteLine("Back");
                        existMenu = false;
                        break;
                }
            }
        }

        private static void DrawMenu(List<string> items)
        {
            Console.WriteLine("\n\n\t\tMenu:");
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }
        }

        private static int SelectedMenu(int count){
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow && index != count - 1)
                index++;
            else if (ckey.Key == ConsoleKey.UpArrow && index > 0)
                index--;
            else if (ckey.Key == ConsoleKey.Enter){
                Console.Clear();
                Console.CursorVisible = true;
                return index;
            }

            Console.Clear();
            return -1;
        }
        
        private void InsertMenu(){
            bool existMenu = true;
            index = 0;
            while (existMenu)
            {
                Console.CursorVisible = false;
                DrawMenu(insertMenuItems);
                int selectedMenu = SelectedMenu(insertMenuItems.Count);
                switch (selectedMenu)
                {
                    case 0:
                        Console.WriteLine("Random");
                        Data.InsertRandData();
                        break;
                    case 1:
                        Console.WriteLine("Console");
                        Data.InsertData();
                        break;
                    case 2:
                        Console.WriteLine("Back");
                        existMenu = false;
                        break;
                }
            }
        }
    }
}
