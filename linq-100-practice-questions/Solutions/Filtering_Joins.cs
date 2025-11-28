using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Filtering_Joins
    {
        // Q11. Employees working on more than one project
        public static IEnumerable<Employee> Filtering_Joins_01()
        {
            var employeesOnMultipleProjects = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.EmployeeId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key).ToHashSet();

            return ListGenerator.EmployeeList
                .Where(e => employeesOnMultipleProjects.Contains(e.Id));
        }

        // Q12. Total hours per project (Descending)
        public static IEnumerable<object> Filtering_Joins_02()
        {
            return ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.ProjectId)
                .Select(g => new
                {
                    ProjectId = g.Key,
                    TotalHours = g.Sum(ep => ep.HoursAllocated)
                })
                .Join(ListGenerator.ProjectList,
                      ph => ph.ProjectId,
                      p => p.Id,
                      (ph, p) => new { ProjectName = p.Name, ph.TotalHours })
                .OrderByDescending(x => x.TotalHours);
        }

        // Q13. Projects starting after average start date
        public static IEnumerable<Project> Filtering_Joins_03()
        {
            var projects = ListGenerator.ProjectList;
            if (!projects.Any()) return Enumerable.Empty<Project>();

            double avgTicks = projects.Average(p => p.StartDate.Ticks);
            DateTime avgDate = new DateTime((long)avgTicks);

            return projects.Where(p => p.StartDate > avgDate);
        }

        // Q14. Employees borrowed books from >= 2 genres
        public static IEnumerable<Employee> Filtering_Joins_04()
        {
            return ListGenerator.BookLoanList
                .Join(ListGenerator.BookList,
                      bl => bl.BookId, b => b.Id, (bl, b) => new { bl.EmployeeId, b.Genre })
                .GroupBy(x => x.EmployeeId)
                .Where(g => g.Select(x => x.Genre).Distinct().Count() >= 2)
                .Join(ListGenerator.EmployeeList,
                      g => g.Key, e => e.Id, (g, e) => e);
        }

        // Q15. Project with longest duration
        public static Project? Filtering_Joins_05()
        {
            return ListGenerator.ProjectList
                .MaxBy(p => (p.EndDate ?? DateTime.Now) - p.StartDate);
        }

        // Q16. Customers placed orders every quarter of 2024
        public static IEnumerable<Customer> Filtering_Joins_06()
        {
            return ListGenerator.CustomerList
                .Where(c => c.Orders
                    .Where(o => o.Date.Year == 2024)
                    .Select(o => (o.Date.Month - 1) / 3 + 1)
                    .Distinct()
                    .Count() == 4);
        }

        // Q17. Total books borrowed per month
        public static IEnumerable<object> Filtering_Joins_07()
        {
            return ListGenerator.BookLoanList
                .GroupBy(bl => new { bl.LoanDate.Year, bl.LoanDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                });
        }

        // Q18. Employees with BOTH active and inactive projects (FIXED)
        public static IEnumerable<Employee> Filtering_Joins_08()
        {
            var targetEmployeeIds = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.EmployeeId)
                .Where(g => g.Any(ep => ep.IsActive) && g.Any(ep => !ep.IsActive))
                .Select(g => g.Key).ToHashSet();

            return ListGenerator.EmployeeList
                .Where(e => targetEmployeeIds.Contains(e.Id));
        }

        // Q19. Projects where EVERY assigned employee is active (FIXED)
        public static IEnumerable<Project> Filtering_Joins_09()
        {
            var validProjectIds = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.ProjectId)
                .Where(g => g.All(ep => ep.IsActive))
                .Select(g => g.Key).ToHashSet();

            return ListGenerator.ProjectList
                .Where(p => validProjectIds.Contains(p.Id));
        }

        // Q20. Employees who are 'Leads' and 'Developers' (FIXED)
        public static IEnumerable<Employee> Filtering_Joins_10()
        {
            var targetEmployeeIds = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.EmployeeId)
                .Where(g =>
                {
                    var roles = g.Select(ep => ep.Role).Distinct().ToList();
                    return roles.Contains(ProjectRole.Lead) && roles.Contains(ProjectRole.Developer);
                })
                .Select(g => g.Key).ToHashSet();

            return ListGenerator.EmployeeList
               .Where(e => targetEmployeeIds.Contains(e.Id));
        }
    }
}