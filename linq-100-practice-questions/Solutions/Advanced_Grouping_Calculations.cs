using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Advanced_Grouping_Calculations
    {
        public static object Question1()
        {
            return ListGenerator.EmployeeProjectList
                 .GroupBy(ep => ep.EmployeeId)
                 .Select(g => new
                 {
                     EmployeeId = g.Key,
                     TotalHours = g.Sum(ep => ep.HoursAllocated)
                 })
                 .Join(ListGenerator.EmployeeList,
                       g => g.EmployeeId,
                       e => e.Id,
                       (g, e) => new { Employee = e, TotalHours = g.TotalHours })
                 .MaxBy(x => x.TotalHours);
        }

        public static IEnumerable<object> Question2()
        {
            var employeeTotals = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.EmployeeId)
                .Select(g => new { Id = g.Key, Total = g.Sum(ep => ep.HoursAllocated) })
                .ToList();
            if (!employeeTotals.Any()) return Enumerable.Empty<object>();

            var avgTotal = employeeTotals.Average(x => x.Total);

            return employeeTotals
                .Where(x => x.Total > avgTotal)
                .Join(ListGenerator.EmployeeList,
                      t => t.Id, e => e.Id,
                      (t, e) => new { Employee = e, TotalHours = t.Total });
        }

        public static IEnumerable<BookGenre> Question3()
        {
            return ListGenerator.BookLoanList
                .Join(ListGenerator.BookList,
                      bl => bl.BookId,
                      b => b.Id,
                      (bl, b) => b.Genre)
                .GroupBy(genre => genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(3)
                .Select(s => s.Genre);
        }

        public static IEnumerable<Employee> Question4()
        {
            return ListGenerator.EmployeeList
            .GroupJoin(ListGenerator.BookLoanList,
                    e => e.Id,
                    bl => bl.EmployeeId,
                    (e, loans) => new { Employee = e, Loans = loans })
            .Where(x => !x.Loans.Any())
            .Select(x => x.Employee);
        }

        public static bool Question5()
        {
            return ListGenerator.EmployeeList
                .Where(e => e.Department == Department.IT)
                .All(e => e.IsActive);
        }

        public static double Question6()
        {
            var relevantEmployees = ListGenerator.EmployeeList.Where(e => e.YearsOfExperience > 5);
            if (!relevantEmployees.Any()) return 0;

            return relevantEmployees
                .Join(ListGenerator.BookLoanList, e => e.Id, bl => bl.EmployeeId, (e, bl) => bl.BookId)
                .Join(ListGenerator.BookList, bl => bl, b => b.Id, (bl, b) => b.Rating)
                .Average();
        }

        public static decimal Question7()
        {
            var projects = ListGenerator.ProjectList;
            if (!projects.Any()) return 0;

            var result = projects.Aggregate(
                (Min: projects[0].Budget, Max: projects[0].Budget),
                (acc, current) =>
                (
                    Math.Min(current.Budget, acc.Min),
                    Math.Max(current.Budget, acc.Max)
                )
            );
            return result.Max - result.Min;
        }

        public static IEnumerable<Employee> Question8()
        {
            var grouped = ListGenerator.EmployeeProjectList
                .GroupBy(e => e.EmployeeId)
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    TotalHours = g.Sum(ep => ep.HoursAllocated)
                })
                .OrderByDescending(e => e.TotalHours)
                .ToList();

            int takeCount = (int)Math.Ceiling(grouped.Count * 0.10);

            return grouped.Take(takeCount).Join(
                ListGenerator.EmployeeList,
                g => g.EmployeeId,
                e => e.Id,
                (g, e) => e
            );
        }

        public static IEnumerable<Book> Question9()
        {
            return ListGenerator.ProjectList
                .Where(p => p.Category == ProjectCategory.AI_ML)
                .Join(ListGenerator.EmployeeProjectList, p => p.Id, ep => ep.ProjectId, (p, ep) => ep.EmployeeId)
                .Distinct()
                .Join(ListGenerator.BookLoanList, eId => eId, bl => bl.EmployeeId, (eId, bl) => bl.BookId)
                .Distinct()
                .Join(ListGenerator.BookList, bId => bId, b => b.Id, (bId, b) => b);
        }

        public static int Question10()
        {
            return ListGenerator.CustomerList.Count(c =>
                c.Orders.Any(o => o.Date.Year == 2024) &&
                c.Orders.Any(o => o.Date.Year == 2025));
        }
    }
}