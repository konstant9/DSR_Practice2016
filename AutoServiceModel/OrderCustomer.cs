using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceModel
{
    public class OrderCustomer
    {
        public WorksAutoOrder Order { get; }
        
        
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string Phone { get; private set; }

        public OrderCustomer(WorksAutoOrder worksAutoOrder)
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


        public static List<OrderCustomer> ConvertToList(IQueryable<WorksAutoOrder> worksAutoOrders)
        {
            var list = new List<OrderCustomer>();
            foreach (var item in worksAutoOrders)
            {
                list.Add(new OrderCustomer(item));
            }
            return list;
        } 
    }
}
