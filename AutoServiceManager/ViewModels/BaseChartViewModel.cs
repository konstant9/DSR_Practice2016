using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class BaseChartViewModel : INotifyPropertyChanged
    {
        private List<KeyValuePair<string, int>> _listOfPoints;

        public List<KeyValuePair<string, int>> ListOfPoints
        {
            get { return _listOfPoints;}
            set
            {
                _listOfPoints = value;
                OnPropertyChanged("ListOfPoints");
            }
        }

        private string _diagramTitle;

        public string DiagramTitle
        {
            get { return _diagramTitle;}
            set
            {
                _diagramTitle = value;
                OnPropertyChanged("DiagramTitle");
            }
        }
        protected void ShowDiagram(object statistic)
        {
            switch (statistic.ToString())
            {
                case "Количество заказов по маркам авто":
                {
                    DiagramTitle = "Диаграмма количества заказов по маркам автомобилей";
                        using (var context = new AutoServiceEntities())
                        {
                            var points = new List<KeyValuePair<string, int>>();
                            var amountsByBrands =
                                context.WorksAutoOrder.GroupBy(x => x.CarBrand)
                                    .Select(group => new {brand = group.Key, count = group.Count()});
                            foreach (var amountByBrand in amountsByBrands)
                            {
                                points.Add(new KeyValuePair<string, int>(amountByBrand.brand, amountByBrand.count));
                            }
                            ListOfPoints = points;
                            break;
                        }
                    }
                case "Количество заказов по месяцам":
                    {
                        DiagramTitle = "Диаграмма количества заказов в текущем году по месяцам";
                        using (var context = new AutoServiceEntities())
                        {
                            var points = new List<KeyValuePair<string, int>>();
                            var cultureInfo = new CultureInfo("ru-ru");
                            
                            var amountsByMonths =
                                context.WorksAutoOrder.Where(
                                    x => DbFunctions.TruncateTime(x.WorksStart).Value.Year == DateTime.Now.Year).GroupBy(x => x.WorksStart.Month)
                                    .Select(group => new {month = group.Key, count = group.Count()});
                            foreach (var amountByMonths in amountsByMonths)
                            {
                                points.Add(
                                    new KeyValuePair<string, int>(
                                        cultureInfo.DateTimeFormat.GetMonthName(amountByMonths.month),
                                        amountByMonths.count));
                            }
                            ListOfPoints = points;
                            break;
                        }
                    }
                case "Количество заказов по ценовым категориям":
                    {
                        DiagramTitle = "Диаграмма количества заказов в разных ценовых диапазонах";
                        using (var context = new AutoServiceEntities())
                        {
                            var points = new List<KeyValuePair<string, int>>();
                            //Вариант с разбиением на равные интервалы
                            /*
                            var priceInterval = context.WorksAutoOrder.Select(x=>x.WorksPrice).Max()/10;
                            var leftBound = 0;
                            var rightBound = priceInterval;
                            for (var i = 0; i < 10; i++)
                            {
                                points.Add(new KeyValuePair<string,int>(leftBound+"-"+rightBound,context.WorksAutoOrder.Count(x => x.WorksPrice>=leftBound&&x.WorksPrice<rightBound)));
                                leftBound += priceInterval;
                                rightBound += priceInterval;
                            }*/
                            //Вариант с разбиением на разные ценовые диапазоны(низкий, средний и т.д.)
                            points.Add(new KeyValuePair<string, int>("Низкий",
                                context.WorksAutoOrder.Count(x => x.WorksPrice <= 500)));
                            points.Add(new KeyValuePair<string, int>("Средний",
                                context.WorksAutoOrder.Count(x => x.WorksPrice > 500 && x.WorksPrice <= 3000)));
                            points.Add(new KeyValuePair<string, int>("Выше среднего",
                                context.WorksAutoOrder.Count(x => x.WorksPrice > 3000 && x.WorksPrice <= 10000)));
                            points.Add(new KeyValuePair<string, int>("Высокий",
                                context.WorksAutoOrder.Count(x => x.WorksPrice > 10000)));
                            ListOfPoints = points;
                            break;
                        }
                    }
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
