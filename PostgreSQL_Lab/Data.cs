using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PostgreSQL_Lab
{
    class Data
    {
        private readonly static string connString = "Host=127.0.0.1;Username=postgres;Password=cda;Database=MyData";
        
        public static void ReturnAll(){
            Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            Console.WriteLine("Table Customers");
            Console.ResetColor(); // сбрасываем в стандартный
            
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT _id, fullname, city, contacts, buydate FROM customer", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()){ 
                        Console.Write("ID: " + reader.GetValue(0));
                        Console.Write("\tfullname: " + reader.GetString(1));
                        Console.Write("\tcity: " + reader.GetString(2));
                        Console.Write("\tcontacts: " + reader.GetString(3));
                        Console.Write("\tbuydate: " + reader.GetDate(4));
                        Console.WriteLine();
                    }

                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine("Table Supplier");
                Console.ResetColor(); // сбрасываем в стандартный
                using (var cmd = new NpgsqlCommand("SELECT _id, title FROM supplier", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.Write("ID: " + reader.GetValue(0));
                        Console.Write("\ttitle: " + reader.GetString(1));
                        Console.WriteLine();
                    }
                conn.Close();
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void InsertData(int index){
            string fullname = "", city = "", contacts = "";
            int date=0;
            try
            {
                Console.Write("Full Name: ");
                 fullname = Convert.ToString(Console.ReadLine());
                Console.Write("City: ");
                city = Convert.ToString(Console.ReadLine());
                Console.Write("Contacts: ");
                contacts = Convert.ToString(Console.ReadLine());
                Console.Write("Date: ");
                date = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(f.Message);
                Console.ResetColor();
                Console.ReadKey();
            }
            string command = String.Format("INSERT INTO customer (fullname, city, contacts, buydate) VALUES ('{0}','{1}','{2}','{3}')", fullname, city, contacts, date);
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(command, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            
            Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            Console.WriteLine("Запись добавлена!\n");
            Console.ResetColor(); // сбрасываем в стандартный
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        public static string RandomNumbers(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomContacts(){
            return "+" + RandomNumbers(12);
        }

        public static void InsertRandData(int index)
        {
            string fullname = "", city = "", contacts = "";
            DateTime date;

            switch (index)
            {
                case 0:
                    for (int i = 0; i < 10; i++)
                    {
                        string command = String.Format("INSERT INTO customer (fullname, city, contacts, buydate) VALUES ('{0}','{1}','{2}','{3}')", fullname = RandomString(12), city = RandomString(8), contacts = RandomContacts(), date = RandomDay());
                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand(command, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }
                    break;
                case 1:
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        string command = String.Format("INSERT INTO supplier (title) VALUES ('{0}')", fullname = RandomString(12));
                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand(command, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }
                    break;
            }
            

            Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            Console.WriteLine("Записи добавлены!\n");
            Console.ResetColor(); // сбрасываем в стандартный
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
