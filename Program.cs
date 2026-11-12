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
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== EPHRS - BRANCH ===");
            Console.WriteLine("1. Create (Add Employee)");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search/Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees(employees);
                    break;
                case "3":
                    SearchEmployees();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
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

    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();
        
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        
        Console.Write("Enter Department: ");
        string department = Console.ReadLine();
        
        Console.Write("Enter Job Title: ");
        string jobTitle = Console.ReadLine();
        
        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal monthlySalary);
        
        Console.Write("Enter Tax Rate (e.g., 0.12): ");
        double.TryParse(Console.ReadLine(), out double taxRate);
        
        Console.Write("Enter Hire Date (YYYY-MM-DD): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime hireDate);
        
        Console.Write("Enter Is Active (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);
        
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Employee employee = new Employee
        {
            Id = nextId++,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = monthlySalary,
            TaxRate = taxRate,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        };
        
        employees.Add(employee);
        
        Console.WriteLine("\nEmployee added successfully!");
    }

    static void ReadEmployees(List<Employee> employeesList)
    {
        Console.WriteLine("--- Employees ---");
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach(Employee employee in employeesList) {
            Console.WriteLine($"--- Employee {employee.Id} ---");
            Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Job Title: {employee.JobTitle}");
            Console.WriteLine($"Monthly Salary: {employee.MonthlySalary:C}");
            Console.WriteLine($"Tax Rate: {employee.TaxRate:P0}");
            Console.WriteLine($"Hire Date: {employee.HireDate.ToShortDateString()}");
            Console.WriteLine($"Is Active: {employee.IsActive}");
            Console.WriteLine($"Email: {employee.Email}\n");
        }
    }

    static void SearchEmployees()
    {
        Console.WriteLine("--- Search & Filter Employees ---");
        Console.WriteLine("1. Search by Name");
        Console.WriteLine("2. Filter by Department");
        Console.WriteLine("3. Search by ID");
        Console.Write("\nSelect an option: ");
        string choice = Console.ReadLine();
        Console.WriteLine();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        List<Employee> results = new List<Employee>();

        switch (choice)
        {
            case "1":
                Console.Write("Enter name (First or Last): ");
                string nameQuery = Console.ReadLine()?.Trim().ToLower();
                results = employees.Where(e => 
                    e.FirstName.ToLower().Contains(nameQuery) || 
                    e.LastName.ToLower().Contains(nameQuery)).ToList();
                break;

            case "2":
                Console.Write("Enter Department: ");
                string deptQuery = Console.ReadLine()?.Trim().ToLower();
                results = employees.Where(e => 
                    e.Department.ToLower().Contains(deptQuery)).ToList();
                break;

            case "3":
                Console.Write("Enter Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out int idQuery))
                {
                    results = employees.Where(e => e.Id == idQuery).ToList();
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                    return;
                }
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        ReadEmployees(results);
    }

    static void UpdateEmployee()
    {
        ReadEmployees(employees);
        Console.Write("\nEnter the ID of the employee to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee employee = employees.FirstOrDefault(b => b.Id == id);
            if (employee != null)
            {
                Console.Write($"Enter new first name (current: {employee.FirstName}): ");
                string newFirstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFirstName)) 
                    employee.FirstName = newFirstName;

                Console.Write($"Enter new last name (current: {employee.LastName}): ");
                string newLastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newLastName))
                    employee.LastName = newLastName;

                Console.Write($"Enter new department (current: {employee.Department}): ");
                string newDept = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDept))
                    employee.Department = newDept;

                Console.Write($"Enter new job title (current: {employee.JobTitle}): ");
                string newJob = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newJob))
                    employee.JobTitle = newJob;

                Console.Write($"Enter new monthly salary (current: {employee.MonthlySalary}): ");
                string salaryInput = Console.ReadLine();
                if (decimal.TryParse(salaryInput, out decimal newSalary))
                    employee.MonthlySalary = newSalary;

                Console.Write($"Enter new tax rate (current: {employee.TaxRate}): ");
                string taxInput = Console.ReadLine();
                if (double.TryParse(taxInput, out double newTaxRate))
                    employee.TaxRate = newTaxRate;

                Console.Write($"Enter new hire date (current: {employee.HireDate.ToShortDateString()}): ");
                string dateInput = Console.ReadLine();
                if (DateTime.TryParse(dateInput, out DateTime newHireDate))
                    employee.HireDate = newHireDate;

                Console.Write($"Enter new active status (current: {employee.IsActive}): ");
                string activeInput = Console.ReadLine();
                if (bool.TryParse(activeInput, out bool newIsActive))
                    employee.IsActive = newIsActive;

                Console.Write($"Enter new email (current: {employee.Email}): ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail))
                    employee.Email = newEmail;
                
                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteEmployee()
    {
        ReadEmployees(employees);
        Console.Write("\nEnter the ID of the employee to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            employees.RemoveAll(b => b.Id == id);
            Console.WriteLine("\nEmployee deleted successfully (if ID existed).");
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}
