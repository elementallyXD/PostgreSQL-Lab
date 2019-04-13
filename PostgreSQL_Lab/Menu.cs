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
        private static int index = 0, menuItemsCount = menuItems.Count;

        public Menu(){
            
            while(true){
            Console.CursorVisible = false;
                DrawMenu(menuItems);
                int selectedMenu = SelectedMenu(menuItemsCount);
                switch (selectedMenu)
                {
                    case 0:
                        Console.WriteLine("Insert");
                        break;
                    case 1:
                        Console.WriteLine("Update");
                        break;
                    case 2:
                        Console.WriteLine("Delete");
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
                    
                    
                    default:
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
        
    }
}
