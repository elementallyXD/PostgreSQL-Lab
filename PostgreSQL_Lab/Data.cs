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

                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine("Table Products");
                Console.ResetColor(); // сбрасываем в стандартный
                using (var cmd = new NpgsqlCommand("SELECT * FROM products", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.Write("ID: " + reader.GetValue(0));
                        Console.Write("\tdesignation: " + reader.GetString(1));
                        Console.Write("\tdescription: " + reader.GetString(2));
                        Console.Write("\tailable: " + reader.GetBoolean(3));
                        Console.Write(" \tC_ID: " + reader.GetValue(4));
                        Console.Write(" \tS_ID: " + reader.GetValue(5));
                        Console.WriteLine();
                    }
                conn.Close();
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            //Console.Clear();
        }

        public static void InsertData(int index){
            string fullname = "", city = "", contacts = "", command = "", description = "";
            int date=0;
            bool avilable = false;

            switch (index)
            {
                case 0:
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
                    command = String.Format("INSERT INTO customer (fullname, city, contacts, buydate) VALUES ('{0}','{1}','{2}','{3}')", fullname, city, contacts, date);
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(command, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    break;
                case 1:
                    int c_id = 0, s_id = 0;
                    try
                    {
                        Console.Write("Designation: ");
                        fullname = Convert.ToString(Console.ReadLine());
                        Console.Write("Description: ");
                        description = Convert.ToString(Console.ReadLine());
                        Console.Write("avilable: ");
                        avilable = Convert.ToBoolean(Console.ReadLine());
                        Console.Write("c_id: ");
                        c_id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("s_id: ");
                        s_id = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException f)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(f.Message);
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    command = String.Format("INSERT INTO products (designation, description, avilable, c_id, s_id) VALUES ('{0}','{1}','{2}','{3}','{4}')", fullname, description, avilable, c_id, s_id);
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(command, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    break;
                case 2:
                    try
                    {
                        Console.Write("Full Name: ");
                        fullname = Convert.ToString(Console.ReadLine());
                    }
                    catch (FormatException f)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(f.Message);
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    command = String.Format("INSERT INTO supplier (title) VALUES ('{0}')", fullname);
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(command, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    break;
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
            const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
        
        private static bool RandomBool(Random r, int truePercentage = 50)
        {
            return r.NextDouble() < truePercentage / 100.0;
        }
      
        public static void InsertRandData(int index)
        {
            string fullname = "", city = "", contacts = "", command = "", description = "";
            bool avilable = false;
            DateTime date;

            switch (index)
            {
                case 0:
                    for (int i = 0; i < 10; i++)
                    {
                        command = String.Format("INSERT INTO customer (fullname, city, contacts, buydate) VALUES ('{0}','{1}','{2}','{3}')", fullname = RandomString(12), city = RandomString(8), contacts = RandomContacts(), date = RandomDay());
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
                    for (int i = 0; i < 5; i++)
                    {
                        command = String.Format("INSERT INTO products (designation, description, avilable, c_id, s_id) VALUES ('{0}','{1}','{2}','{3}','{4}')", fullname = RandomString(10), description = RandomString(30), avilable = RandomBool(random, 50), 1, 1);
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
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        command = String.Format("INSERT INTO supplier (title) VALUES ('{0}')", fullname = RandomString(12));
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

        public static void UpdateData()        
        {
            string tablenames = "", columns = "", conditions = "", command = "";
            Console.WriteLine("Example: UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE condition; ");

                    try
                    {
                        Console.Write("UPDATE ");
                        tablenames = Convert.ToString(Console.ReadLine());
                        Console.Write("SET ");
                        columns = Convert.ToString(Console.ReadLine());
                        Console.Write("WHERE ");
                        conditions = Convert.ToString(Console.ReadLine());

                    }
                    catch (FormatException f)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(f.Message);
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    command = String.Format("UPDATE {0} SET {1} WHERE {2}", tablenames, columns, conditions);
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
            Console.WriteLine("Запись изменена!\n");
            Console.ResetColor(); // сбрасываем в стандартный
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
        
        public static void DeleteData(){
            string tablenames = "", conditions = "", command = "";
            Console.WriteLine("Example: DELETE FROM table_name WHERE condition;");

            try
            {
                Console.Write("DELETE FROM ");
                tablenames = Convert.ToString(Console.ReadLine());
                Console.Write("WHERE ");
                conditions = Convert.ToString(Console.ReadLine());

            }
            catch (FormatException f)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(f.Message);
                Console.ResetColor();
                Console.ReadKey();
            }
            command = String.Format("DELETE FROM {0} WHERE {1}", tablenames, conditions);
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
            Console.WriteLine("Запись(cи) удалена!\n");
            Console.ResetColor(); // сбрасываем в стандартный
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
