using System.Collections.Generic;
using System.Linq;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class CircleChartViewModel:BaseChartViewModel
    {
        public CircleChartViewModel()
        {
            var context = new AutoServiceEntities();
            ListOfPoints = new List<KeyValuePair<string, int>>();
            var amountsByBrands = context.WorksAutoOrder.GroupBy(x => x.CarBrand).Select(group => new { brand = group.Key, count = group.Count() });
            foreach (var amountByBrand in amountsByBrands)
            {
                ListOfPoints.Add(new KeyValuePair<string, int>(amountByBrand.brand, amountByBrand.count));
            }
            //Mediator.Mediator.Register("CircleDiagramStatistics", ShowDiagram);            
        }
        /*
        private void ShowDiagram(object statistic)
        {
            switch (statistic.ToString())
            {
                case "Количество заказов по маркам авто":
                    {

                        var context = new AutoServiceEntities();
                        ListOfPoints = new List<KeyValuePair<string, int>>();
                        var amountsByBrands = context.WorksAutoOrder.GroupBy(x => x.CarBrand).Select(group => new { brand = group.Key, count = group.Count() });
                        foreach (var amountByBrand in amountsByBrands)
                        {
                            ListOfPoints.Add(new KeyValuePair<string, int>(amountByBrand.brand, amountByBrand.count));
                        }
                        break;
                    }
                case "Количество заказов по месяцам":
                    {

                        var context = new AutoServiceEntities();
                        ListOfPoints = new List<KeyValuePair<string, int>>();
                        var amountsByMonths = context.WorksAutoOrder.GroupBy(x => x.WorksStart.Month).Select(group => new { month = group.Key, count = group.Count() });
                        foreach (var amountByMonths in amountsByMonths)
                        {
                            ListOfPoints.Add(new KeyValuePair<string, int>(amountByMonths.month.ToString(), amountByMonths.count));
                        }
                        break;
                    }
            }
        }*/
    }
}
