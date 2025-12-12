using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Employees_Departments
    {
        public IEnumerable<Employee> Question1()
        {
            var avgSalaryByDept = ListGenerator.EmployeeList
                 .GroupBy(e => e.Department)
                 .Select(g => new
                 {
                     g.Key,
                     AverageSalary = g.Average(e => e.Salary)
                 });

            return ListGenerator.EmployeeList
                 .Where(e => e.Salary < avgSalaryByDept
                 .First(d => d.Key == e.Department).AverageSalary);
        }

        public IEnumerable<Department> Question2()
        {
            return ListGenerator.EmployeeList
                 .GroupBy(e => e.Department)
                 .Where(g => g.Count() > 10)
                 .Select(g => g.Key);
        }

        public IEnumerable<Employee?> Question3()
        {
            return ListGenerator.EmployeeList
                .GroupBy(e => e.Department)
                .Select(g => g.MaxBy(e => e.Salary));
        }

        public IEnumerable<Employee> Question4()
        {
            return ListGenerator.EmployeeList
                .Join(
                ListGenerator.EmployeeList,
                e => e.ManagerId,
                emg => emg.Id,
                (e, emg) => new { Employee = e, Manager = emg }
                ).Where(em => em.Employee.HireDate.Year == em.Manager.HireDate.Year).Select(em => em.Employee);
        }

        public IEnumerable<object> Question5()
        {
            return ListGenerator.EmployeeList.GroupBy(e => e.HireDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Count = g.Count()
                });
        }

        public IEnumerable<Employee> Question6()
        {
            var emp = ListGenerator.EmployeeProjectList.Select(e => e.EmployeeId).Distinct().ToHashSet();

            return ListGenerator.EmployeeList.Where(e => !emp.Contains(e.Id));
        }

        public Department? Question7()
        {
            return ListGenerator.EmployeeList
                .GroupBy(e => e.Department)
                .MaxBy(g => g.Sum(e => e.Salary))?
                .Key;
        }

        public IEnumerable<Employee> Question8()
        {
            return ListGenerator.EmployeeList
                .Join(
                ListGenerator.EmployeeList,
                e => e.ManagerId,
                emg => emg.Id,
                (Employee, Manager) => Manager
                ).Where(e => e.YearsOfExperience > 10).Distinct();
        }

        public IEnumerable<Employee> Question9()
        {
            var mang = ListGenerator.EmployeeList.GroupBy(e => e.ManagerId).Select(g => g.Key).ToHashSet();
            var nosub = ListGenerator.EmployeeList.Where(e => !mang.Contains(e.Id));
            var noproject = Question6();

            return nosub.Union(noproject);
        }
    }
}