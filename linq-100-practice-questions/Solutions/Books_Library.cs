using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Books_Library_Optimized
    {
        public IEnumerable<Book> Question1()
        {
            var borrowedBookIds = ListGenerator.BookLoanList.Select(b => b.BookId);

            return ListGenerator.BookList
                .ExceptBy(borrowedBookIds, b => b.Id);
        }

        public IEnumerable<Employee> Question2()
        {
            return ListGenerator.BookLoanList
                .GroupBy(b => new { b.EmployeeId, Month = b.LoanDate.Month })
                .Where(g => g.Count() > 2)
                .Select(g => g.Key.EmployeeId)
                .Distinct()
                .Join(
                    ListGenerator.EmployeeList,
                    id => id,
                    e => e.Id,
                    (id, e) => e
                );
        }

        public BookGenre Question3()
        {
            return ListGenerator.BookLoanList
                .Join(
                    ListGenerator.BookList,
                    loan => loan.BookId,
                    book => book.Id,
                    (loan, book) => book.Genre
                )
                .GroupBy(genre => genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Genre)
                .FirstOrDefault();
        }

        public IEnumerable<Book> Question4()
        {
            return ListGenerator.BookLoanList
                .GroupBy(b => b.BookId)
                .Where(g => g.Select(b => b.EmployeeId).Distinct().Count() >= 5)
                .Join(
                    ListGenerator.BookList,
                    g => g.Key,
                    b => b.Id,
                    (g, b) => b
                );
        }

        public double Question5()
        {
            return ListGenerator.BookLoanList
                .Where(b => b.IsReturned && b.ReturnDate.HasValue)
                .Average(b => (b.ReturnDate.Value - b.LoanDate).TotalDays);
        }

        public IEnumerable<Employee> Question6()
        {
            return ListGenerator.BookLoanList
               .Where(b => b.ReturnDate.HasValue && (b.ReturnDate.Value - b.LoanDate).TotalDays > 30) // Note: Q says > 30, your code was >= 30
               .Join(ListGenerator.EmployeeList,
                     l => l.EmployeeId,
                     e => e.Id,
                     (l, e) => e)
               .Distinct();
        }

        public int Question7()
        {
            var result = ListGenerator.BookLoanList
                .Where(b => b.ReturnDate > b.DueDate)
                .GroupBy(b => b.ReturnDate.Value.Month)
                .Select(g => new { Month = g.Key, LateCount = g.Count() })
                .OrderByDescending(x => x.LateCount)
                .FirstOrDefault();

            return result?.Month ?? 0;
        }

        public IEnumerable<Employee> Question8()
        {
            return ListGenerator.BookLoanList
                .Join(ListGenerator.BookList,
                      l => l.BookId,
                      b => b.Id,
                      (l, b) => new { l.EmployeeId, b.Genre })
                .GroupBy(x => x.EmployeeId)
                .Where(g => g.Select(x => x.Genre).Distinct().Count() == 1)
                .Join(ListGenerator.EmployeeList,
                      g => g.Key,
                      e => e.Id,
                      (g, e) => e);
        }

        public int Question9()
        {
            return ListGenerator.BookLoanList.Count(b => !b.IsReturned);
        }

        public IEnumerable<Employee> Question10()
        {
            var employees2023 = ListGenerator.BookLoanList
                .Where(b => b.LoanDate.Year == 2023)
                .Select(b => b.EmployeeId);

            var employees2024 = ListGenerator.BookLoanList
                .Where(b => b.LoanDate.Year == 2024)
                .Select(b => b.EmployeeId);

            return employees2023
                         .Intersect(employees2024)
                         .Join(
                              ListGenerator.EmployeeList,
                               id => id,
                               emp => emp.Id,
                               (id, emp) => emp);
        }
    }
}