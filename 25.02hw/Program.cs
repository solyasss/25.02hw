using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace email_list_app
{
    class Program
    {
        private static SqlConnection _connection;
        private static string _connectionString;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            
            _connectionString = config.GetConnectionString("DefaultConnection");
            
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("  email_list_db app");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1 - connect to database");
                Console.WriteLine("2 - disconnect from database");
                Console.WriteLine("3 - show all customers");
                Console.WriteLine("4 - show all customer emails");
                Console.WriteLine("5 - show all sections");
                Console.WriteLine("6 - show all promotions");
                Console.WriteLine("7 - show all cities");
                Console.WriteLine("8 - show all countries");
                Console.WriteLine("9 - show customers by city");
                Console.WriteLine("10 - show customers by country");
                Console.WriteLine("0 - exit");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        connect_to_database();
                        break;
                    case "2":
                        disconnect_from_database();
                        break;
                    case "3":
                        show_all_customers();
                        break;
                    case "4":
                        show_all_customer_emails();
                        break;
                    case "5":
                        show_all_sections();
                        break;
                    case "6":
                        show_all_promotions();
                        break;
                    case "7":
                        show_all_cities();
                        break;
                    case "8":
                        show_all_countries();
                        break;
                    case "9":
                        show_customers_by_city();
                        break;
                    case "10":
                        show_customers_by_country();
                        break;
                    case "0":
                        disconnect_from_database();
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        private static void connect_to_database()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Already connected to the database");
                return;
            }

            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                Console.WriteLine("Successfully connected to the database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the database: " + ex.Message);
            }
        }
        
        private static void disconnect_from_database()
        {
            if (_connection == null)
            {
                Console.WriteLine("No active connection to close");
                return;
            }

            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
                Console.WriteLine("Disconnected from the database");
            }
            else
            {
                Console.WriteLine("Connection was not open");
            }
        }
        
        private static void show_all_customers()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT * FROM customers";
                IEnumerable<customer> customers = _connection.Query<customer>(sql);

                Console.WriteLine("\nAll customers:");
                foreach (var c in customers)
                {
                    Console.WriteLine($"id: {c.id}, name: {c.full_name}, birth_date: {c.birth_date.ToShortDateString()}, " +
                                      $"gender: {c.gender}, email: {c.email}, country: {c.country}, city: {c.city}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing all customers: " + ex.Message);
            }
        }

        private static void show_all_customer_emails()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT email FROM customers";
                IEnumerable<string> emails = _connection.Query<string>(sql);

                Console.WriteLine("\nAll customer emails:");
                foreach (var e in emails)
                {
                    Console.WriteLine(e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing customer emails: " + ex.Message);
            }
        }
        
        private static void show_all_sections()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT * FROM sections";
                IEnumerable<section> sections = _connection.Query<section>(sql);

                Console.WriteLine("\nAll sections:");
                foreach (var s in sections)
                {
                    Console.WriteLine($"id: {s.id}, section_name: {s.section_name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing sections: " + ex.Message);
            }
        }
        
        private static void show_all_promotions()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT * FROM promotions";
                IEnumerable<promotion> promotions = _connection.Query<promotion>(sql);

                Console.WriteLine("\nAll promotions:");
                foreach (var p in promotions)
                {
                    Console.WriteLine($"id: {p.id}, section_id: {p.section_id}, promotion_name: {p.promotion_name}, " +
                                      $"country: {p.country}, start_date: {p.start_date.ToShortDateString()}, " +
                                      $"end_date: {p.end_date.ToShortDateString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing promotions: " + ex.Message);
            }
        }
        
        private static void show_all_cities()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT DISTINCT city FROM customers";
                IEnumerable<string> cities = _connection.Query<string>(sql);

                Console.WriteLine("\nAll cities:");
                foreach (var city in cities)
                {
                    Console.WriteLine(city);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing cities: " + ex.Message);
            }
        }
        
        private static void show_all_countries()
        {
            if (!is_connected()) return;

            try
            {
                string sql = "SELECT DISTINCT country FROM customers";
                IEnumerable<string> countries = _connection.Query<string>(sql);

                Console.WriteLine("\nAll countries:");
                foreach (var country in countries)
                {
                    Console.WriteLine(country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing countries: " + ex.Message);
            }
        }
        
        private static void show_customers_by_city()
        {
            if (!is_connected()) return;

            Console.Write("Enter city name: ");
            string city = Console.ReadLine();

            try
            {
                string sql = "SELECT * FROM customers WHERE city = @City";
                var parameters = new { City = city };
                IEnumerable<customer> customers = _connection.Query<customer>(sql, parameters);

                Console.WriteLine($"\nCustomers from {city}:");
                foreach (var c in customers)
                {
                    Console.WriteLine($"id: {c.id}, name: {c.full_name}, email: {c.email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing customers by city: " + ex.Message);
            }
        }
        
        private static void show_customers_by_country()
        {
            if (!is_connected()) return;

            Console.Write("Enter country name: ");
            string country = Console.ReadLine();

            try
            {
                string sql = "SELECT * FROM customers WHERE country = @Country";
                var parameters = new { Country = country };
                IEnumerable<customer> customers = _connection.Query<customer>(sql, parameters);

                Console.WriteLine($"\nCustomers from {country}:");
                foreach (var c in customers)
                {
                    Console.WriteLine($"id: {c.id}, name: {c.full_name}, email: {c.email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing customers by country: " + ex.Message);
            }
        }
        
        private static bool is_connected()
        {
            if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Not connected to the database");
                return false;
            }
            return true;
        }
    }
}
