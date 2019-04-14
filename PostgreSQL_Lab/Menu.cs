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
        private static List<string> menuItems = new List<string>() {
                "Insert",
                "Update",
                "Delete",
                "Search",
                "Full text search",
                "Get all",
                "Get by PK",
                "Exit"
        };

        private static List<string> insertMenuItems = new List<string>() { 
                "Random",
                "Insert by me",
                "Back"
        };

        private static List<string> tableMenuItems = new List<string>() {
                "Customer",
                "Product",
                "Supplier",
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
                        Console.WriteLine("Insert");
                        tableMenu();
                        break;
                    case 1:
                        Data.UpdateData();
                        break;
                    case 2:
                        Data.DeleteData();
                        break;
                    case 3:
                        Console.WriteLine("Search");
                        break;
                    case 4:
                        Console.WriteLine("Full text search");
                        break;
                    case 5:
                        Console.WriteLine("Get All");
                        Data.ReturnAll();
                        break;
                    case 6:
                        Console.WriteLine("Get by PK");
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void DrawMenu(List<string> items)
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

        private int SelectedMenu(int count){
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
        
        private void InsertMenu(int index){
            bool existMenu = true;
            while (existMenu)
            {
                Console.CursorVisible = false;
                DrawMenu(insertMenuItems);
                int selectedMenu = SelectedMenu(insertMenuItems.Count);
                switch (selectedMenu)
                {
                    case 0:
                        Console.WriteLine("Random");
                        Data.InsertRandData(index);
                        break;
                    case 1:
                        Console.WriteLine("Insert by me");
                        Data.InsertData(index);
                        break;
                    case 2:
                        Console.WriteLine("Back");
                        existMenu = false;
                        break;
                }
            }
        }



        private void tableMenu()
        {
            bool existMenu = true;
            while (existMenu)
            {
                Console.CursorVisible = false;
                DrawMenu(tableMenuItems);
                int selectedMenu = SelectedMenu(tableMenuItems.Count);
                switch (selectedMenu)
                {
                    case 0:
                        InsertMenu(index);
                        break;
                    case 1:
                        InsertMenu(index);
                        break;
                    case 2:
                        InsertMenu(index);
                        break;
                    case 3:
                        Console.WriteLine("Back");
                        existMenu = false;
                        break;
                }
            }
        }

    }
}
