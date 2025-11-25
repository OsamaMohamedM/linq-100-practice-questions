# LINQ 100 Practice Questions

This project provides a comprehensive set of 100 practice questions to master LINQ (Language Integrated Query) in C#. It includes a pre-populated in-memory database with various entities, allowing you to practice a wide range of LINQ queries from basic to advanced levels.

## Data Model

The project uses a rich data model to simulate real-world scenarios. The following entities are included:

- **Book:** Represents books in a library.
  - `Id` (int): Unique identifier for the book.
  - `Title` (string): Title of the book.
  - `Author` (string): Author of the book.
  - `Genre` (BookGenre): Genre of the book (enum).
  - `Price` (decimal): Price of the book.
  - `Pages` (int): Number of pages in the book.
  - `IsAvailable` (bool): Whether the book is available for loan.
  - `PublicationDate` (DateTime): Date of publication.
  - `Rating` (double): Rating of the book (0.0 - 5.0).
  - `CopiesInStock` (int): Number of copies in stock.

- **BookLoan:** Represents the relationship between an employee and a book they have borrowed.
  - `Id` (int): Unique identifier for the loan.
  - `EmployeeId` (int): ID of the employee who borrowed the book.
  - `BookId` (int): ID of the borrowed book.
  - `LoanDate` (DateTime): Date the book was loaned.
  - `DueDate` (DateTime): Date the book is due to be returned.
  - `ReturnDate` (DateTime?): Date the book was returned.
  - `IsReturned` (bool): Whether the book has been returned.
  - `LateFee` (decimal?): Late fee if the book was returned late.

- **Customer:** Represents customers.
  - `Id` (string): Unique identifier for the customer.
  - `Name` (string): Name of the customer.
  - `Address` (string): Address of the customer.
  - `City` (string): City of the customer.
  - `Region` (string): Region of the customer.
  - `PostalCode` (string): Postal code of the customer.
  - `Country` (string): Country of the customer.
  - `Phone` (string): Phone number of the customer.
  - `Fax` (string): Fax number of the customer.
  - `Orders` (Order[]): Array of orders placed by the customer.

- **Department:** Represents employee departments (enum).
  - `IT`
  - `HR`
  - `Finance`
  - `Marketing`
  - `Sales`

- **Employee:** Represents employees.
  - `Id` (int): Unique identifier for the employee.
  - `Name` (string): Name of the employee.
  - `Department` (Department): Department of the employee.
  - `Salary` (decimal): Salary of the employee.
  - `YearsOfExperience` (int): Years of experience of the employee.
  - `IsRemote` (bool): Whether the employee works remotely.
  - `HireDate` (DateTime): Date the employee was hired.
  - `ManagerId` (int?): ID of the employee's manager.
  - `IsActive` (bool): Whether the employee is currently active.

- **EmployeeProject:** Represents the relationship between an employee and a project they are working on.
  - `Id` (int): Unique identifier for the assignment.
  - `EmployeeId` (int): ID of the assigned employee.
  - `ProjectId` (int): ID of the project.
  - `Role` (ProjectRole): Role of the employee in the project (enum).
  - `HoursAllocated` (int): Number of hours allocated to the employee for the project.
  - `AssignedDate` (DateTime): Date the employee was assigned to the project.
  - `IsActive` (bool): Whether the assignment is currently active.

- **Order:** Represents an order placed by a customer.
  - `Id` (int): Unique identifier for the order.
  - `Date` (DateTime): Date the order was placed.
  - `Total` (decimal): Total amount of the order.
  - `Products` (List<Product>): List of products in the order.

- **Product:** Represents products.
  - `Id` (long): Unique identifier for the product.
  - `Name` (string): Name of the product.
  - `Category` (string): Category of the product.
  - `UnitPrice` (decimal): Unit price of the product.
  - `UnitsInStock` (int): Number of units in stock.

- **Project:** Represents projects.
  - `Id` (int): Unique identifier for the project.
  - `Name` (string): Name of the project.
  - `Category` (ProjectCategory): Category of the project (enum).
  - `Budget` (decimal): Budget of the project.
  - `TeamSize` (int): Size of the project team.
  _ `IsCompleted` (bool): Whether the project is completed.
  - `StartDate` (DateTime): Start date of the project.
  - `EndDate` (DateTime?): End date of the project.
  - `Priority` (int): Priority of the project (1-5).
  - `CompletionPercentage` (double): Completion percentage of the project.

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/OsamaMohamedM/linq-100-practice-questions.git
   ```
2. **Open the solution:**
   Open the `LinqQuestions.slnx` file in Visual Studio.
3. **Build the project:**
   Build the solution to restore the necessary packages.
4. **Run the application:**
   Run the `linq-100-practice-questions` project. The `Main` method in `Program.cs` is the entry point where you can write and test your LINQ queries.

## The Questions

The 100 LINQ practice questions are available in the `linq-100-practice-questions/Questions/100-LINQ-Practice-Questions.html.pdf` file. The questions are divided into the following categories:

- **Basic Operations & Aggregations (Q1-Q10)**
- **Filtering & Joins (Q11-Q20)**
- **Advanced Grouping & Calculations (Q21-Q30)**
- **Complex Filtering & Comparisons (Q31-Q40)**
- **Set Operations & Advanced Queries (Q41-Q50)**
- **Orders & Customers (Q51-Q70)**
- **Books & Library (Q71-Q80)**
- **Employees & Departments (Q81-Q90)**
- **Projects & Assignments (Q91-Q100)**
