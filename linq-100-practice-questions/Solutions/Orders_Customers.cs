using LinqQuestion;

namespace linqQuestions.Solutions
{
    internal class Orders_Customers
    {
        public static IEnumerable<Customer> Question1()
        {
            return ListGenerator.CustomerList.Where(c => c.Orders.Length == 0);
        }

        public static IEnumerable<Order> Question2()
        {
            var avgAllOrders = ListGenerator.OrderList.Average(o => o.Total);

            return ListGenerator.OrderList.Where(o => o.Total > avgAllOrders);
        }

        public static IEnumerable<object> Question3()
        {
            return ListGenerator.CustomerList
               .Select(c => new
               {
                   Customer = c,
                   MostRecentOrder = c.Orders
                       .OrderByDescending(o => o.Date)
                       .FirstOrDefault()
               });
        }

        public static IEnumerable<object> Question4()
        {
            return ListGenerator.CustomerList.Select(
                c => new
                {
                    Customer = c,
                    productCount = c.Orders
                        .SelectMany(o => o.Products)
                        .Select(oi => oi.Id)
                        .Distinct()
                        .Count()
                }
                );
        }

        public static IEnumerable<Customer> Question5()
        {
            return ListGenerator.CustomerList.OrderByDescending(c => c.Orders.Sum(o => o.Total)).Take(5);
        }

        public static IEnumerable<Order> Question6()
        {
            return ListGenerator.OrderList.Where(o => o.Products.Any(p => p.Category == "Electronics"));
        }

        public static IEnumerable<object> Question7()
        {
            return ListGenerator.CustomerList.GroupBy(o => o.Country).Select(g => new
            {
                Country = g.Key,
                CustomerCount = g.Sum(o => o.Orders.Average(o => o.Total))
            });
        }

        public static IEnumerable<Customer> Question8()
        {
            return ListGenerator.CustomerList.Where(c => !(c.Orders.Any(o => o.Date.Month != 12)));
        }

        public static IEnumerable<Product> Question9()
        {
            return ListGenerator.ProductList.Except(ListGenerator.OrderList.SelectMany(o => o.Products).Distinct());
        }

        public static IEnumerable<object> Question10()
        {
            return ListGenerator.OrderList.SelectMany(o => o.Products)
                 .GroupBy(p => p.Id)
                 .Select(
                 g => new
                 {
                     Product = g.First(),
                     productPrice = g.Count() * g.First().UnitPrice
                 }
                 ).Where(p => p.productPrice > 10000);
        }
    }
}