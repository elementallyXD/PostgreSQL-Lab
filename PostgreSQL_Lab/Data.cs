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

        private static int GetLastNum(string table)
        {
            int sum = 0;
            string command = String.Format("Select _id from {0} ORDER BY _id DESC LIMIT 1", table);
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(command, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        //Console.Write("ID: " + reader.GetValue(0));
                        sum = reader.GetInt32(0);
                    }
                conn.Close();
            }
            return sum;
        }
        
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
                        Console.WriteLine();
                    }

                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine("Table Buyers");
                Console.ResetColor(); // сбрасываем в стандартный
                using (var cmd = new NpgsqlCommand("SELECT * FROM buyers", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.Write("C_ID: " + reader.GetValue(0));
                        Console.Write(" \tS_ID: " + reader.GetValue(1));
                        Console.WriteLine();
                    }
                conn.Close();
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
        }

        public static void InsertData(){
            string tableNames = "", values = "", columns = "", command = "";
            int c_id = 0, s_id = 0;
            
            Console.WriteLine("Example: INSERT INTO table_name(column1, column2, column3, ...) VALUES (value1, value2, value3, ...);");
                    try
                    {
                        Console.Write("INSERT INTO ");
                        tableNames = Convert.ToString(Console.ReadLine());
                        Console.Write("Columns ");
                        columns = Convert.ToString(Console.ReadLine());
                        Console.Write("VALUES ");
                        values = Convert.ToString(Console.ReadLine());
                    }
                    catch (FormatException f)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(f.Message);
                        Console.ResetColor();
                        Console.ReadKey();
                    }
            if (tableNames != "" || values != "")
            {
                command = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableNames, columns, values);
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(command, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                if (tableNames == "customer") {
                    command = String.Format("INSERT INTO buyers (c_id, s_id) VALUES ('{0}', '{1}')", c_id = GetLastNum("customer"), s_id = RandomNumber(1, GetLastNum("supplier") + 1));

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
                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine("Запись добавлена!\n");
                Console.ResetColor(); // сбрасываем в стандартный
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }

        private static Random random = new Random();
        
        public static string RandomString(int length)
        {
            const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
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

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
       
        public static void InsertRandData()
        {
            string fullname = "", city = "", contacts = "", command = "", description = "";
            bool avilable = false;
            int c_id = 0, s_id = 0;
            DateTime date;
            
                    for (int i = 0; i < 3; i++)
                    {
                        command = String.Format("INSERT INTO supplier (title) VALUES ('{0}')", fullname = RandomString(15));
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

                    for (int i = 0; i < 20; i++)
                    {
                        command = String.Format("INSERT INTO products (designation, description, avilable) VALUES ('{0}','{1}','{2}')", fullname = RandomString(10), description = RandomString(45), avilable = RandomBool(random, 50));
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


                    for (int i = 0; i < 10; i++)
                    {
                        command = String.Format("INSERT INTO customer (fullname, city, contacts, buydate) VALUES ('{0}','{1}','{2}','{3}')", fullname = RandomString(15), city = RandomString(8), contacts = RandomContacts(), date = RandomDay());
                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand(command, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                        command = String.Format("INSERT INTO buyers (c_id, s_id) VALUES ('{0}', '{1}')", c_id = GetLastNum("customer"), s_id = RandomNumber(1, GetLastNum("supplier") + 1));
                        
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
            ReturnAll();
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
                    if (tablenames != "" || columns != "" || conditions != ""){
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
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                        Console.WriteLine("Запись не изменена, повторите еще раз!\n");
                        Console.ResetColor(); // сбрасываем в стандартный
                    }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
        
        public static void DeleteData(){
            string tableNames = "", conditions = "", command = "";
            ReturnAll();
            Console.WriteLine("Example: DELETE FROM table_name WHERE condition;");
            try
            {
                Console.Write("DELETE FROM ");
                tableNames = Convert.ToString(Console.ReadLine());
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
            if (tableNames != "" || conditions != "")
            {
                command = String.Format("DELETE FROM {0} WHERE {1}", tableNames, conditions);
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
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                Console.WriteLine("Запись не изменена, повторите еще раз!\n");
                Console.ResetColor(); // сбрасываем в стандартный
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
            Console.Clear();
        }
    
        public static void GetByPk(){
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("select supplier.*, customer.* from supplier inner join buyers b on b.s_id = supplier._id inner join customer on customer._id = b.c_id", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.Write("ID_S: " + reader.GetValue(0));
                        Console.Write("\ttitle: " + reader.GetString(1));
                        Console.Write("\tID_C: " + reader.GetValue(2));
                        Console.Write("\tfullname: " + reader.GetString(3));
                        Console.WriteLine("\tcity: " + reader.GetString(4));
                        Console.WriteLine("\tcontacts: " + reader.GetString(5));
                        Console.Write("\tbuydate: " + reader.GetDate(6));
                        Console.WriteLine();
                    }
                conn.Close();
            }
            Console.WriteLine("\n\nPress any key...");
            Console.ReadKey();
        }    
    
        public static void FullTextSearchWord(){
            ReturnAll();
            string text = "", command = "";
                    Console.WriteLine("Слово не входить:");
                    try
                    {
                        Console.Write("Слово: ");
                        text = Convert.ToString(Console.ReadLine());
                    }
                    catch (FormatException f)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(f.Message);
                        Console.ResetColor();
                        Console.ReadKey();
                    }

                    command = String.Format("select products.* FROM products WHERE to_tsvector(products.description) @@ to_tsquery('!{0}')", text);
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(command, conn))
                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                            {
                                Console.Write("ID: " + reader.GetValue(0));
                                Console.Write("\tdesignation: " + reader.GetString(1));
                                Console.Write("\tdescription: " + reader.GetString(2));
                                Console.Write("\tailable: " + reader.GetBoolean(3));
                                Console.WriteLine();
                            }
                        conn.Close();
                    }
        }

        public static void FullTextSearchPhrase(){
            ReturnAll();
            string text = "", command = "";
            Console.WriteLine("Входить цiла фраза");
            try
            {
                Console.Write("Фраза: ");
                text = Convert.ToString(Console.ReadLine());
            }
            catch (FormatException f)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(f.Message);
                Console.ResetColor();
                Console.ReadKey();
            }
            text = text.Replace(" ", " <-> ");
            command = String.Format("select products.* FROM products WHERE to_tsvector(products.description) @@ to_tsquery('{0}')", text);
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(command, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Console.Write("ID: " + reader.GetValue(0));
                        Console.Write("\tdesignation: " + reader.GetString(1));
                        Console.Write("\tdescription: " + reader.GetString(2));
                        Console.Write("\tailable: " + reader.GetBoolean(3));
                        Console.WriteLine();
                    }
                conn.Close();
            }
        }
    }
}
