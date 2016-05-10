using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoServiceModel
{
    public class CustomerOrder
    {
        public WorksAutoOrder Order { get; }        
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string Phone { get; private set; }

        public CustomerOrder(WorksAutoOrder worksAutoOrder)
        {
            Order = worksAutoOrder;
            var context = new AutoServiceEntities();
            var owner = context.Customers.Where(x => x.Orders.Any(y => y.OrderID == Order.OrderID)).ToList().First();
            Name = owner.Name;
            Surname = owner.Surname;
            Patronymic = owner.Patronymic;
            Birthday = owner.Birthday;
            Phone = owner.Phone;
        }

        public static List<CustomerOrder> GetCustomerOrders(IEnumerable<WorksAutoOrder> worksAutoOrders)
        {
            return worksAutoOrders.Select(item => new CustomerOrder(item)).ToList();
        }
    }
}
