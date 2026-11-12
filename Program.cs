using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
    public decimal MonthlySalary { get; set; }
    public double TaxRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
}

class Program
{
    static List<Employee> books = new List<Employee>()
    {
    };

    static int nextId = 6;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== LIBRARY BOOK MANAGER ===");
            Console.WriteLine("1. Create (Add Book)");
            Console.WriteLine("2. Read (View All Books)");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateBook();
                    break;
                case "2":
                    ReadBooks();
                    break;
                case "3":
                    UpdateBook();
                    break;
                case "4":
                    DeleteBook();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (running) 
            { 
                Console.WriteLine("\nPress Enter to continue..."); 
                Console.ReadLine(); 
            }
        }
    }

    static void CreateBook()
    {
        Console.WriteLine("--- Add New Book ---");
        Console.Write("Enter Title: ");
        string title = Console.ReadLine();
        Console.Write("Enter Author: ");
        string author = Console.ReadLine();
        Console.Write("Enter Publication Year: ");
        int.TryParse(Console.ReadLine(), out int year);
        Console.Write("Enter Price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);

        Book book = new Book {
            Id = nextId++,
            Title = title,
            Author = author,
            Year = year,
            Price = price,
        };
        books.Add(book);
        
        Console.WriteLine("\nBook added successfully!");
    }

    static void ReadBooks()
    {
        Console.WriteLine("--- Current Library Catalog ---");
        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach(Book book in books) {
            Console.WriteLine($"--- Book {book.Id} ---");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Year: {book.Year}");
            Console.WriteLine($"Price: Php{book.Price}");
        }
    }

    static void UpdateBook()
    {
        ReadBooks();
        Console.Write("\nEnter the ID of the book to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Console.Write($"Enter new title (current: {book.Title}): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle)) 
                    book.Title = newTitle;

                Console.Write($"Enter new author (current: {book.Author}): ");
                string newAuthor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAuthor))
                    book.Author = newAuthor;

                Console.Write($"Enter new year (current: {book.Year}): ");
                int newYear = 0;
                if(int.TryParse(Console.ReadLine(), out newYear))
                    book.Year = newYear;

                Console.Write($"Enter new price (current: {book.Price}): ");
                decimal newPrice = 0.0M;
                if(decimal.TryParse(Console.ReadLine(), out newPrice))
                    book.Price = newPrice;
                
                Console.WriteLine("\nBook updated successfully!");
            }
            else
            {
                Console.WriteLine("\nBook not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteBook()
    {
        ReadBooks();
        Console.Write("\nEnter the ID of the book to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            books.RemoveAll(b => b.Id == id);
            Console.WriteLine("\nBook deleted successfully (if ID existed).");
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}
