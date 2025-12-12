using LinqQuestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace linqQuestions.Solutions
{
    internal class Projects_Assignments
    {
        public IEnumerable<Project> Question1()
        {
            return ListGenerator.ProjectList
                .Where(p => p.EndDate.HasValue && p.StartDate.Year == p.EndDate.Value.Year);
        }

        public double Question2()
        {
            return ListGenerator.ProjectList
                .GroupJoin(
                    ListGenerator.EmployeeProjectList,
                    p => p.Id,
                    ep => ep.ProjectId,
                    (p, ep) => ep.Count()
                )
                .Average();
        }

        public IEnumerable<Project> Question3()
        {
            var assignedProjectIds = ListGenerator.EmployeeProjectList
                .Select(ep => ep.ProjectId)
                .ToHashSet();

            return ListGenerator.ProjectList
                .Where(p => !assignedProjectIds.Contains(p.Id));
        }

        public ProjectCategory? Question4()
        {
            return ListGenerator.ProjectList
                .GroupBy(p => p.Category)
                .MaxBy(g => g.Count())?
                .Key;
        }

        public double Question5()
        {
            return ListGenerator.ProjectList
                .Where(p => p.EndDate.HasValue)
                .Average(p => (p.EndDate.Value - p.StartDate).TotalDays);
        }

        public IEnumerable<Employee> Question6()
        {
            var busyEmployeeIds = ListGenerator.EmployeeProjectList
                .GroupBy(ep => ep.EmployeeId)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key)
                .ToHashSet();

            return ListGenerator.EmployeeList
                .Where(e => busyEmployeeIds.Contains(e.Id));
        }

        public IEnumerable<Project> Question7()
        {
            var juniorEmployeeIds = ListGenerator.EmployeeList
                .Where(e => e.YearsOfExperience <= 5)
                .Select(e => e.Id)
                .ToHashSet();

            var projectsWithJuniors = ListGenerator.EmployeeProjectList
                .Where(ep => juniorEmployeeIds.Contains(ep.EmployeeId))
                .Select(ep => ep.ProjectId)
                .ToHashSet();

            var projectsWithEmployees = ListGenerator.EmployeeProjectList
                .Select(ep => ep.ProjectId)
                .ToHashSet();

            return ListGenerator.ProjectList
                .Where(p => projectsWithEmployees.Contains(p.Id) && !projectsWithJuniors.Contains(p.Id));
        }

        public IEnumerable<Project> Question8()
        {
            var avgBudgetByCategory = ListGenerator.ProjectList
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.Average(p => p.Budget));

            return ListGenerator.ProjectList
                .Where(p => p.Budget > avgBudgetByCategory[p.Category]);
        }

        public IEnumerable<ProjectCategory> Question9()
        {
            return ListGenerator.ProjectList
                 .Where(p => p.EndDate.HasValue && (p.EndDate.Value - p.StartDate).TotalDays < 10)
                 .Select(p => p.Category)
                 .Distinct();
        }

        public IEnumerable<Employee> Question10()
        {
            var employeeStatuses = ListGenerator.EmployeeProjectList.Where(ep => ep.IsActive)
                .Join(
                    ListGenerator.ProjectList,
                    ep => ep.ProjectId,
                    p => p.Id,
                    (ep, p) => new { ep.EmployeeId, p.IsCompleted }
                )
                .GroupBy(x => x.EmployeeId)
                .Where(g => g.Any(x => x.IsCompleted))
                .Select(g => g.Key)
                .ToHashSet();

            return ListGenerator.EmployeeList
                .Where(e => employeeStatuses.Contains(e.Id));
        }
    }
}
}