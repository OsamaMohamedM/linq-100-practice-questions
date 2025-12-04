using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Set_Operations_Advanced_Queries
    {
        public static IEnumerable<Book> Question1()
        {
            var avgBorrowingBook = (decimal)ListGenerator.BookLoanList.Count / ListGenerator.BookList.Count;
            return ListGenerator.BookLoanList
                 .GroupBy(bl => bl.BookId)
                 .Where(g => g.Count() > avgBorrowingBook)
                 .Join(ListGenerator.BookList,
                       g => g.Key,
                       b => b.Id,
                       (g, b) => b);
        }

        public static IEnumerable<Employee> Question2()

        {
            var validProjectIds = ListGenerator.ProjectList.Select(p => p.Id);
            var workedOnProjectIds = ListGenerator.EmployeeProjectList.Select(ep => ep.ProjectId);

            var orphanedProjectIds = workedOnProjectIds.Except(validProjectIds);

            return ListGenerator.EmployeeProjectList
                .Join(orphanedProjectIds,
                      ep => ep.ProjectId,
                      orphanId => orphanId,
                      (ep, orphanId) => ep.EmployeeId)
                .Distinct()
                .Join(ListGenerator.EmployeeList,
                      empId => empId,
                      emp => emp.Id,
                      (empId, emp) => emp);
        }

        public static IEnumerable<Employee> Question3()
        {
            return ListGenerator.EmployeeProjectList
                  .GroupBy(p => p.ProjectId)
                  .Where(g => g.Count() == 1)
                  .SelectMany(g => g.Select(g => g.EmployeeId))
                  .Join(
                         ListGenerator.EmployeeList,
                         empId => empId,
                         emp => emp.Id,
                         (empId, emp) => emp);
        }

        public static IEnumerable<Project> Question4()
        {
            return ListGenerator.ProjectList
        .Where(p => p.Category == ProjectCategory.AI_ML || p.Category == ProjectCategory.Infrastructure);
        }

        public static IEnumerable<Employee> Question5()
        {
            var empProject = ListGenerator.EmployeeProjectList
                 .GroupBy(p => p.EmployeeId)
                 .Select(g => g.Key);

            var empBooks = ListGenerator.BookLoanList
                .GroupBy(p => p.EmployeeId)
                .Select(g => g.Key);

            var empIntersect = empBooks.Intersect(empProject);

            return empIntersect.Join(
                ListGenerator.EmployeeList,
                empId => empId,
                emp => emp.Id,
                (empId, emp) => emp);
        }

        public static bool Question6(int empId1, int empId2)
        {
            var projectsEmp1 = ListGenerator.EmployeeProjectList
                .Where(ep => ep.EmployeeId == empId1)
                .Select(ep => ep.ProjectId)
                .OrderBy(id => id);

            var projectsEmp2 = ListGenerator.EmployeeProjectList
                .Where(ep => ep.EmployeeId == empId2)
                .Select(ep => ep.ProjectId)
                .OrderBy(id => id);

            return projectsEmp1.SequenceEqual(projectsEmp2);
        }

        public static ILookup<int, Employee> Question7()
        {
            return ListGenerator.EmployeeList.ToLookup(e => e.ManagerId ?? 0);
        }

        public static Dictionary<long, string?> Question8()
        {
            return ListGenerator.ProductList.ToDictionary(p => p.Id, p => p.Name);
        }

        public static IEnumerable<Product> Question9()
        {
            return ListGenerator.ProductList.OrderByDescending(p => p.UnitPrice * p.UnitsInStock).Take((int)Math.Ceiling(0.20 * ListGenerator.ProductList.Count));
        }

        public static string Question10()
        {
            return string.Join(", ", ListGenerator.ProjectList.Select(p => $"{p.Name}:{p.Budget}"));
        }
    }
}