using System.Collections.Generic;
using System.Linq;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class LinearChartViewModel:BaseChartViewModel
    {
        public LinearChartViewModel()
        {
            var context = new AutoServiceEntities();
            ListOfPoints = new List<KeyValuePair<string, int>>();
            var amountsByBrands = context.WorksAutoOrder.GroupBy(x => x.CarBrand).Select(group => new { brand = group.Key, count = group.Count() });
            foreach (var amountByBrand in amountsByBrands)
            {
                ListOfPoints.Add(new KeyValuePair<string, int>(amountByBrand.brand, amountByBrand.count));
            }
        }
    }
}
