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

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("App");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1 - connect to database");
                Console.WriteLine("2 - disconnect from database");
                Console.WriteLine("3 - show main information");
                Console.WriteLine("4 - operations menu");
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
                        show_main_information_menu();
                        break;
                    case "4":
                        operations_menu();
                        break;
                    case "0":
                        disconnect_from_database();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        private static void show_main_information_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Main information menu:");
                Console.WriteLine("1 - show all customers");
                Console.WriteLine("2 - show all customer emails");
                Console.WriteLine("3 - show all sections");
                Console.WriteLine("4 - show all promotions");
                Console.WriteLine("5 - show all cities");
                Console.WriteLine("6 - show all countries");
                Console.WriteLine("7 - show customers by city");
                Console.WriteLine("8 - show customers by country");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        show_all_customers();
                        break;
                    case "2":
                        show_all_customer_emails();
                        break;
                    case "3":
                        show_all_sections();
                        break;
                    case "4":
                        show_all_promotions();
                        break;
                    case "5":
                        show_all_cities();
                        break;
                    case "6":
                        show_all_countries();
                        break;
                    case "7":
                        show_customers_by_city();
                        break;
                    case "8":
                        show_customers_by_country();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        private static void operations_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Operations menu:");
                Console.WriteLine("1 - add operations");
                Console.WriteLine("2 - update operations");
                Console.WriteLine("3 - delete operations");
                Console.WriteLine("4 - additional display operations");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        add_operations_menu();
                        break;
                    case "2":
                        update_operations_menu();
                        break;
                    case "3":
                        delete_operations_menu();
                        break;
                    case "4":
                        display_operations_menu();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        private static void add_operations_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Add operations:");
                Console.WriteLine("1 - add new customer");
                Console.WriteLine("2 - add new country");
                Console.WriteLine("3 - add new city");
                Console.WriteLine("4 - add new section");
                Console.WriteLine("5 - add new promotion");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        insert_new_customer();
                        break;
                    case "2":
                        insert_new_country();
                        break;
                    case "3":
                        insert_new_city();
                        break;
                    case "4":
                        insert_new_section();
                        break;
                    case "5":
                        insert_new_promotion();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        private static void update_operations_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Update operations:");
                Console.WriteLine("1 - update customer");
                Console.WriteLine("2 - update country");
                Console.WriteLine("3 - update city");
                Console.WriteLine("4 - update section");
                Console.WriteLine("5 - update promotion");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        update_customer();
                        break;
                    case "2":
                        update_country();
                        break;
                    case "3":
                        update_city();
                        break;
                    case "4":
                        update_section();
                        break;
                    case "5":
                        update_promotion();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        private static void delete_operations_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Delete operations:");
                Console.WriteLine("1 - delete customer");
                Console.WriteLine("2 - delete country");
                Console.WriteLine("3 - delete city");
                Console.WriteLine("4 - delete section");
                Console.WriteLine("5 - delete promotion");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        delete_customer();
                        break;
                    case "2":
                        delete_country();
                        break;
                    case "3":
                        delete_city();
                        break;
                    case "4":
                        delete_section();
                        break;
                    case "5":
                        delete_promotion();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        private static void display_operations_menu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Additional display operations:");
                Console.WriteLine("1 - display cities by country");
                Console.WriteLine("2 - display sections by customer");
                Console.WriteLine("3 - display promotions by section");
                Console.WriteLine("0 - back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        display_cities_by_country();
                        break;
                    case "2":
                        display_sections_by_customer();
                        break;
                    case "3":
                        display_promotions_by_section();
                        break;
                    case "0":
                        back = true;
                        break;
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
                string sql = @"SELECT p.id, p.section_id, p.promotion_name, p.country, p.start_date, p.end_date, s.section_name
                               FROM promotions p
                               INNER JOIN sections s ON p.section_id = s.id";
                var promotions = _connection.Query(sql);
                Console.WriteLine("\nAll promotions:");
                foreach (var p in promotions)
                {
                    Console.WriteLine($"id: {p.id}, promotion: {p.promotion_name}, section: {p.section_name}, " +
                                      $"country: {p.country}, start_date: {((DateTime)p.start_date).ToShortDateString()}, " +
                                      $"end_date: {((DateTime)p.end_date).ToShortDateString()}");
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
                string sql = "SELECT DISTINCT city, country FROM customers";
                var cities = _connection.Query(sql);
                Console.WriteLine("\nAll cities (with country):");
                foreach (var c in cities)
                {
                    Console.WriteLine($"City: {c.city}  |  Country: {c.country}");
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
                    Console.WriteLine($"id: {c.id}, name: {c.full_name}, email: {c.email}, country: {c.country}");
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
                    Console.WriteLine($"id: {c.id}, name: {c.full_name}, email: {c.email}, city: {c.city}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error showing customers by country: " + ex.Message);
            }
        }

        private static void insert_new_customer()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter full name: ");
                string full_name = Console.ReadLine();
                Console.Write("Enter birth date (yyyy-MM-dd): ");
                DateTime birth_date;
                while (!DateTime.TryParse(Console.ReadLine(), out birth_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                Console.Write("Enter gender: ");
                string gender = Console.ReadLine();
                Console.Write("Enter email: ");
                string email = Console.ReadLine();
                Console.Write("Enter country: ");
                string country = Console.ReadLine();
                Console.Write("Enter city: ");
                string city = Console.ReadLine();

                string sql = "INSERT INTO customers (full_name, birth_date, gender, email, country, city) " +
                             "VALUES (@full_name, @birth_date, @gender, @email, @country, @city)";
                var parameters = new { full_name, birth_date, gender, email, country, city };
                int rows = _connection.Execute(sql, parameters);
                Console.WriteLine("Inserted " + rows + " customer(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting customer: " + ex.Message);
            }
        }

        private static void insert_new_country()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter country name: ");
                string country_name = Console.ReadLine();
                string sql = "INSERT INTO countries (country_name) VALUES (@country_name)";
                int rows = _connection.Execute(sql, new { country_name });
                Console.WriteLine("Inserted " + rows + " country(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting country: " + ex.Message);
            }
        }

        private static void insert_new_city()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter city name: ");
                string city_name = Console.ReadLine();
                Console.Write("Enter country id: ");
                int country_id;
                while (!int.TryParse(Console.ReadLine(), out country_id))
                {
                    Console.Write("Invalid input. Enter a valid country id: ");
                }
                string sql = "INSERT INTO cities (city_name, country_id) VALUES (@city_name, @country_id)";
                int rows = _connection.Execute(sql, new { city_name, country_id });
                Console.WriteLine("Inserted " + rows + " city(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting city: " + ex.Message);
            }
        }

        private static void insert_new_section()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter section name: ");
                string section_name = Console.ReadLine();
                string sql = "INSERT INTO sections (section_name) VALUES (@section_name)";
                int rows = _connection.Execute(sql, new { section_name });
                Console.WriteLine("Inserted " + rows + " section(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting section: " + ex.Message);
            }
        }

        private static void insert_new_promotion()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter section id: ");
                int section_id;
                while (!int.TryParse(Console.ReadLine(), out section_id))
                {
                    Console.Write("Invalid input. Enter a valid section id: ");
                }
                Console.Write("Enter promotion name: ");
                string promotion_name = Console.ReadLine();
                Console.Write("Enter country: ");
                string country = Console.ReadLine();
                Console.Write("Enter start date (yyyy-MM-dd): ");
                DateTime start_date;
                while (!DateTime.TryParse(Console.ReadLine(), out start_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                Console.Write("Enter end date (yyyy-MM-dd): ");
                DateTime end_date;
                while (!DateTime.TryParse(Console.ReadLine(), out end_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                string sql = "INSERT INTO promotions (section_id, promotion_name, country, start_date, end_date) " +
                             "VALUES (@section_id, @promotion_name, @country, @start_date, @end_date)";
                var parameters = new { section_id, promotion_name, country, start_date, end_date };
                int rows = _connection.Execute(sql, parameters);
                Console.WriteLine("Inserted " + rows + " promotion(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting promotion: " + ex.Message);
            }
        }
        

        private static void update_customer()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter customer id to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid customer id: ");
                }
                Console.Write("Enter new full name: ");
                string full_name = Console.ReadLine();
                Console.Write("Enter new birth date (yyyy-MM-dd): ");
                DateTime birth_date;
                while (!DateTime.TryParse(Console.ReadLine(), out birth_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                Console.Write("Enter new gender: ");
                string gender = Console.ReadLine();
                Console.Write("Enter new email: ");
                string email = Console.ReadLine();
                Console.Write("Enter new country: ");
                string country = Console.ReadLine();
                Console.Write("Enter new city: ");
                string city = Console.ReadLine();

                string sql = "UPDATE customers SET full_name = @full_name, birth_date = @birth_date, gender = @gender, " +
                             "email = @email, country = @country, city = @city WHERE id = @id";
                var parameters = new { full_name, birth_date, gender, email, country, city, id };
                int rows = _connection.Execute(sql, parameters);
                Console.WriteLine("Updated " + rows + " customer(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating customer: " + ex.Message);
            }
        }

        private static void update_country()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter country id to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid country id: ");
                }
                Console.Write("Enter new country name: ");
                string country_name = Console.ReadLine();
                string sql = "UPDATE countries SET country_name = @country_name WHERE id = @id";
                int rows = _connection.Execute(sql, new { country_name, id });
                Console.WriteLine("Updated " + rows + " country(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating country: " + ex.Message);
            }
        }

        private static void update_city()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter city id to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid city id: ");
                }
                Console.Write("Enter new city name: ");
                string city_name = Console.ReadLine();
                Console.Write("Enter new country id: ");
                int country_id;
                while (!int.TryParse(Console.ReadLine(), out country_id))
                {
                    Console.Write("Invalid input. Enter a valid country id: ");
                }
                string sql = "UPDATE cities SET city_name = @city_name, country_id = @country_id WHERE id = @id";
                int rows = _connection.Execute(sql, new { city_name, country_id, id });
                Console.WriteLine("Updated " + rows + " city(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating city: " + ex.Message);
            }
        }

        private static void update_section()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter section id to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid section id: ");
                }
                Console.Write("Enter new section name: ");
                string section_name = Console.ReadLine();
                string sql = "UPDATE sections SET section_name = @section_name WHERE id = @id";
                int rows = _connection.Execute(sql, new { section_name, id });
                Console.WriteLine("Updated " + rows + " section(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating section: " + ex.Message);
            }
        }

        private static void update_promotion()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter promotion id to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid promotion id: ");
                }
                Console.Write("Enter new section id: ");
                int section_id;
                while (!int.TryParse(Console.ReadLine(), out section_id))
                {
                    Console.Write("Invalid input. Enter a valid section id: ");
                }
                Console.Write("Enter new promotion name: ");
                string promotion_name = Console.ReadLine();
                Console.Write("Enter new country: ");
                string country = Console.ReadLine();
                Console.Write("Enter new start date (yyyy-MM-dd): ");
                DateTime start_date;
                while (!DateTime.TryParse(Console.ReadLine(), out start_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                Console.Write("Enter new end date (yyyy-MM-dd): ");
                DateTime end_date;
                while (!DateTime.TryParse(Console.ReadLine(), out end_date))
                {
                    Console.Write("Invalid date, try again: ");
                }
                string sql = "UPDATE promotions SET section_id = @section_id, promotion_name = @promotion_name, " +
                             "country = @country, start_date = @start_date, end_date = @end_date WHERE id = @id";
                var parameters = new { section_id, promotion_name, country, start_date, end_date, id };
                int rows = _connection.Execute(sql, parameters);
                Console.WriteLine("Updated " + rows + " promotion(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating promotion: " + ex.Message);
            }
        }
        

        private static void delete_customer()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter customer id to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid customer id: ");
                }
                string sql = "DELETE FROM customers WHERE id = @id";
                int rows = _connection.Execute(sql, new { id });
                Console.WriteLine("Deleted " + rows + " customer(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting customer: " + ex.Message);
            }
        }

        private static void delete_country()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter country id to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid country id: ");
                }
                string sql = "DELETE FROM countries WHERE id = @id";
                int rows = _connection.Execute(sql, new { id });
                Console.WriteLine("Deleted " + rows + " country(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting country: " + ex.Message);
            }
        }

        private static void delete_city()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter city id to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid city id: ");
                }
                string sql = "DELETE FROM cities WHERE id = @id";
                int rows = _connection.Execute(sql, new { id });
                Console.WriteLine("Deleted " + rows + " city(ies).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting city: " + ex.Message);
            }
        }

        private static void delete_section()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter section id to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid section id: ");
                }
                string sql = "DELETE FROM sections WHERE id = @id";
                int rows = _connection.Execute(sql, new { id });
                Console.WriteLine("Deleted " + rows + " section(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting section: " + ex.Message);
            }
        }

        private static void delete_promotion()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter promotion id to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.Write("Invalid input. Enter a valid promotion id: ");
                }
                string sql = "DELETE FROM promotions WHERE id = @id";
                int rows = _connection.Execute(sql, new { id });
                Console.WriteLine("Deleted " + rows + " promotion(s).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting promotion: " + ex.Message);
            }
        }
        

        private static void display_cities_by_country()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter country name: ");
                string country_name = Console.ReadLine();
                string sql = @"SELECT c.id, c.city_name, c.country_id 
                               FROM cities c
                               INNER JOIN countries co ON c.country_id = co.id
                               WHERE co.country_name = @country_name";
                IEnumerable<city> cities = _connection.Query<city>(sql, new { country_name });
                Console.WriteLine($"\nCities in {country_name}:");
                foreach (var c in cities)
                {
                    Console.WriteLine($"id: {c.id}, city_name: {c.city_name}, country_id: {c.country_id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying cities by country: " + ex.Message);
            }
        }

        private static void display_sections_by_customer()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter customer id: ");
                int customer_id;
                while (!int.TryParse(Console.ReadLine(), out customer_id))
                {
                    Console.Write("Invalid input. Enter a valid customer id: ");
                }
                string sql = @"SELECT s.id, s.section_name
                               FROM sections s
                               INNER JOIN customer_sections cs ON s.id = cs.section_id
                               WHERE cs.customer_id = @customer_id";
                IEnumerable<section> sections = _connection.Query<section>(sql, new { customer_id });
                Console.WriteLine($"\nSections for customer id {customer_id}:");
                foreach (var s in sections)
                {
                    Console.WriteLine($"id: {s.id}, section_name: {s.section_name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying sections by customer: " + ex.Message);
            }
        }

        private static void display_promotions_by_section()
        {
            if (!is_connected()) return;

            try
            {
                Console.Write("Enter section id: ");
                int section_id;
                while (!int.TryParse(Console.ReadLine(), out section_id))
                {
                    Console.Write("Invalid input. Enter a valid section id: ");
                }
                string sql = @"SELECT p.id, p.promotion_name, p.country, p.start_date, p.end_date, s.section_name
                               FROM promotions p
                               INNER JOIN sections s ON p.section_id = s.id
                               WHERE p.section_id = @section_id";
                var promotions = _connection.Query(sql, new { section_id });
                Console.WriteLine($"\nPromotions for section id {section_id}:");
                foreach (var p in promotions)
                {
                    Console.WriteLine($"id: {p.id}, promotion: {p.promotion_name}, section: {p.section_name}, " +
                                      $"country: {p.country}, start_date: {((DateTime)p.start_date).ToShortDateString()}, " +
                                      $"end_date: {((DateTime)p.end_date).ToShortDateString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying promotions by section: " + ex.Message);
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
