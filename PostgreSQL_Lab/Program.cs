using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PostgreSQL_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/t/tЛабараторная робота №1");
             
            string connString = "Host=127.0.0.1;Username=postgres;Password=cda;Database=MyData";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                Console.WriteLine("Связь с базой установлена!\n");
                Console.ResetColor(); // сбрасываем в стандартный
               

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT Name FROM customer", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine("\t " + reader.GetString(0));

                conn.Close();
            }

            Console.WriteLine("\nНажмите любую кнопку, чтобы выйти...");
            Console.ReadLine();
        }
    }
}
