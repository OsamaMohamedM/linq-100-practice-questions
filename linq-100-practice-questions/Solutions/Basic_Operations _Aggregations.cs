using LinqQuestion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linqQuestions.Solutions
{
    internal class Basic_Operations__Aggregations
    {
        /// <summary>
        /// Q1. Finds the total monetary value of all products currently in stock (Price × Quantity).
        /// </summary>
        /// <returns>The total calculated value of the inventory as a decimal.</returns>
        public static decimal Quesiton1()
        {
            var products = ListGenerator.ProductList;

            // Uses the Sum aggregator directly on a projected value (UnitPrice * UnitsInStock) to
            // calculate the total inventory value in a single, efficient pass.
            var totalUnitsInStock = products.Sum(p => p.UnitPrice * p.UnitsInStock);
            return totalUnitsInStock;
        }

        /// <summary>
        /// Q2. Gets the first product whose unit price exceeds the average price of all products.
        /// </summary>
        /// <returns>The first matching Product object, or a default Product if none is found.</returns>
        public static Product Quesiton2()
        {
            var products = ListGenerator.ProductList;

            // 1. Calculate the company-wide average product price.
            var Avg_Products = products.Average(p => p.UnitPrice);

            // 2. Use FirstOrDefault to find the first product satisfying the condition, and
            // provides a safe default value to prevent null reference errors.
            var product = products.FirstOrDefault(
                p => p.UnitPrice > Avg_Products,
                new Product { Id = -1, Category = "UnKown", Name = "Unkown", UnitPrice = 0, UnitsInStock = 0 }
            );
            return product;
        }

        /// <summary>
        /// Q3. Checks if there is at least one employee currently marked as inactive.
        /// </summary>
        /// <returns>True if any employee's IsActive property is false; otherwise, False.</returns>
        public static bool Question3()
        {
            var Employees = ListGenerator.EmployeeList;

            // Uses the Any() quantifier method for efficient existence checking. The method stops
            // processing as soon as the first inactive employee is found.
            return Employees.Any(e => !e.IsActive);
        }

        /// <summary>
        /// Q4. Retrieves the first 5 books when ordered alphabetically by their title.
        /// </summary>
        /// <returns>An IEnumerable of the top 5 books.</returns>
        public static IEnumerable<Book> Question4()
        {
            var books = ListGenerator.BookList;

            // Chains OrderBy() for alphabetical sorting and Take(5) to limit the result set.
            return books.OrderBy(b => b.Title).Take(5);
        }

        /// <summary>
        /// Q5. Returns all unique country names where customers reside.
        /// </summary>
        /// <returns>An IEnumerable of unique country strings.</returns>
        public static IEnumerable<string?> Question5()
        {
            var customers = ListGenerator.CustomerList;

            // Selects the 'Country' property from all customers, then uses Distinct() to ensure
            // only unique country names are returned.
            var countries = customers.Select(c => c.Country).Distinct();
            return countries;
        }

        /// <summary>
        /// Q6. Finds all employees who have been hired within the last 3 years.
        /// </summary>
        /// <returns>An IEnumerable of Employee objects hired within the specified timeframe.</returns>
        public static IEnumerable<Employee> Question6()
        {
            var employees = ListGenerator.EmployeeList;

            // Filters employees where the HireDate is greater than 3 years ago (DateTime.Now.AddYears(-3)).
            return employees.Where(e => e.HireDate > DateTime.Now.AddYears(-3));
        }

        /// <summary>
        /// Q7. Finds all customers who have placed orders with a total value greater than 1000 in aggregate.
        /// </summary>
        /// <returns>An IEnumerable of Customer objects that meet the spending criteria.</returns>
        public static IEnumerable<Customer> Question7()
        {
            var customers = ListGenerator.CustomerList;

            // Filters customers by aggregating (Summing) the Total property of all their Orders.
            // This is an example of Filtering based on Aggregation.
            return customers.Where(c => c.Orders.Sum(o => o.Total) > 1000);
        }

        /// <summary>
        /// Q8. Counts how many books are considered "out of stock" (CopiesInStock is 0) or
        /// unavailable (IsAvailable is false).
        /// </summary>
        /// <returns>The total count of books meeting either of the two unavailability criteria.</returns>
        public static int Question8()
        {
            var books = ListGenerator.BookList;

            // Uses the Count(predicate) method to count elements matching complex OR (||) logic in
            // a single query.
            return books.Count(b => b.CopiesInStock == 0 || !b.IsAvailable);
        }

        // Struct definition used for the return type of Question 9
        public struct AverageSalaryByDepartment
        {
            public string Department;
            public decimal AverageSalary;
        }

        /// <summary>
        /// Q9. Calculates the average salary of employees, grouped by each department.
        /// </summary>
        /// <returns>
        /// An IEnumerable of structs containing the Department name and its Average Salary.
        /// </returns>
        public static IEnumerable<AverageSalaryByDepartment> Question9()
        {
            var Employees = ListGenerator.EmployeeList;

            // Groups employees by department, then selects a new structure for each group,
            // calculating the average salary within that specific group.
            var averageSalariesByDepartment = Employees.GroupBy(e => e.Department).Select(
                g => new AverageSalaryByDepartment
                {
                    Department = g.Key.ToString(),
                    AverageSalary = g.Average(e => e.Salary)
                }
            );

            return averageSalariesByDepartment;
        }

        // Struct definition used for the return type of Question 10
        public struct TopProductByCategory
        {
            public string Category;
            public List<Product> TopProducts;
        }

        /// <summary>
        /// Q10. Finds the top 3 most expensive products within each product category.
        /// </summary>
        /// <returns>
        /// An IEnumerable of structs, each containing a category and its top 3 most expensive products.
        /// </returns>
        public static IEnumerable<TopProductByCategory> Question10()
        {
            var products = ListGenerator.ProductList;

            // Groups products by category, then within the Select clause for each group, orders
            // them descending by UnitPrice and takes the Top 3 (Take(3)).
            var topProductsByCategory = products.GroupBy(p => p.Category).Select(
                g => new TopProductByCategory
                {
                    Category = g.Key ?? "Unknown", // Handles potentially null Category names
                    TopProducts = g.OrderByDescending(p => p.UnitPrice).Take(3).ToList()
                }
            );
            return topProductsByCategory;
        }
    }
}