using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Complex_Filtering_Comparisons
    {
        public static IEnumerable<Employee> Question1()
        {
            var BooksLoan = ListGenerator.BookLoanList;
            var Employees = ListGenerator.EmployeeList;
            var result = BooksLoan
                .GroupBy(b => b.EmployeeId)
                .Select(g => new { EmployeeId = g.Key, NumberOfBooks = g.Count() });

            var avg = result.Average(e => e.NumberOfBooks);

            return result.Where(re => re.NumberOfBooks > avg)
                         .Join(
                                 Employees,
                                 res => res.EmployeeId,
                                 e => e.Id,
                                 (res, e) => e);
        }

        public static IEnumerable<Employee> Question2()
        {
            return ListGenerator.EmployeeList.Join(
                ListGenerator.EmployeeList,
                subordinate => subordinate.ManagerId,
                manager => manager.Id,
                (subordinate, manager) => new { Subordinate = subordinate, Manager = manager }
            )
            .Where(x => x.Manager.HireDate > x.Subordinate.HireDate)
            .Select(x => x.Subordinate);
        }

        public static IEnumerable<Book> Question3()
        {
            return ListGenerator.BookLoanList
                 .Where(b => b.ReturnDate > b.DueDate)
                 .Select(b => b.BookId)
                 .Distinct()
                 .Join(
                     ListGenerator.BookList,
                     bId => bId,
                     book => book.Id,
                     (bId, book) => book);
        }

        public static IEnumerable<Employee> Question4()
        {
            return ListGenerator.EmployeeProjectList
                  .Join(
                       ListGenerator.ProjectList,
                       ep => ep.ProjectId,
                       p => p.Id,
                       (ep, p) => new { empId = ep.EmployeeId, p.Category })
                  .GroupBy(e => e.empId)
                  .Where(g => g.Select(g => g.Category).Distinct().Count() >= 2).Select(e => e.Key)
                  .Join(
                    ListGenerator.EmployeeList,
                    empId => empId,
                    emp => emp.Id,
                    (empId, emp) => emp);
        }

        public static decimal Question5()
        {
            var totalProjects = ListGenerator.ProjectList.Count;
            if (totalProjects == 0) return 0;
            var res = ListGenerator.ProjectList.Count(p => p.IsCompleted == true);
            return (decimal)res / totalProjects;
        }

        public static IEnumerable<Employee> Question6()
        {
            return ListGenerator.EmployeeProjectList
                .GroupBy(e => e.EmployeeId)
                .Select(g => new { empId = g.Key, sumHours = g.Sum(x => x.HoursAllocated) })
                .Join(ListGenerator.EmployeeList, empId => empId.empId, employee => employee.Id, (empId, employee) => new { employee, Hours = empId.sumHours })
                .OrderByDescending(e => e.employee.YearsOfExperience == 0 ? 0m : (decimal)e.Hours / e.employee.YearsOfExperience)
                .Select(e => e.employee)
                .Take(3);
        }

        public static IEnumerable<Employee> Question7()
        {
            var fantasyBookIds = ListGenerator.BookList
                .Where(b => b.Genre == BookGenre.Fantasy)
                .Select(b => b.Id)
                .Distinct()
                .ToList();

            var totalFantasyBooksCount = fantasyBookIds.Count;

            return ListGenerator.BookLoanList
                .Where(bl => fantasyBookIds.Contains(bl.BookId))
                .GroupBy(bl => bl.EmployeeId)
                .Where(g => g.Select(b => b.BookId).Distinct().Count() == totalFantasyBooksCount)
                .Join(ListGenerator.EmployeeList,
                        g => g.Key,
                        e => e.Id,
                        (g, e) => e);
        }

        public static IEnumerable<Employee> Question8()
        {
            var bookLoans = ListGenerator.BookLoanList;
            return bookLoans
                 .GroupBy(b => b.EmployeeId)
                 .Where(g => g.Select(b => b.LoanDate.Month).Distinct().Count() > 3)
                 .Select(g => g.Key)
                 .Join(
                      ListGenerator.EmployeeList,
                      empId => empId,
                      emp => emp.Id,
                      (empId, emp) => emp);
        }

        public static IEnumerable<Project> Question9()
        {
            return ListGenerator.EmployeeProjectList
                    .GroupBy(ep => ep.EmployeeId)
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.Select(ep => ep.ProjectId))
                    .Distinct()
                    .Join(ListGenerator.ProjectList,
                        pId => pId,
                        p => p.Id,
                        (pId, p) => p);
        }

        public static Employee Question10()
        {
            return ListGenerator.EmployeeList
                    .Join(ListGenerator.EmployeeList,
                        manager => manager.Id,
                        employee => employee.ManagerId,
                        (manager, employee) => manager)
                    .Distinct()
                    .OrderBy(m => m.HireDate)
                    .FirstOrDefault();
        }
    }
}