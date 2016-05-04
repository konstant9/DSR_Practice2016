using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace AutoServiceManager.ViewModels
{

    class StatisticsViewModel : INotifyPropertyChanged
    {

        public List<string> ListOfDiagrams { get; } = new List<string>
        {
            "Круговая",
            "Столбчатая",
            "Линейная"
        };

        private string _diagramSelectedItem;

        public string DiagramSelectedItem
        {
            get
            {
                return _diagramSelectedItem; 
            }
            set
            {
                _diagramSelectedItem = value;
                OnPropertyChanged("DiagramSelectedItem");
            }
        }

        public List<string> ListOfStatistics { get; } = new List<string>
        {
            "Количество заказов по маркам авто",
            "Количество заказов по месяцам",
            "Количество заказов по ценовым категориям"
        };

        public List<BaseChartViewModel> ListOfViewModels { get; set; } = new List<BaseChartViewModel>
            {
                new CircleChartViewModel(),
                new ColumnChartViewModel(),
                new LinearChartViewModel()
            };

        private string _statisticsSelectedItem;

        public string StatisticsSelectedItem
        {
            get
            {
                return _statisticsSelectedItem;
            }
            set
            {
                _statisticsSelectedItem = value;
                OnPropertyChanged("StatisticsSelectedItem");
            }
        }

        private BaseChartViewModel _selectedViewModel;

        public BaseChartViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        public ICommand ShowDiagramCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    ShowDiagram();
                });
            }
        }
        

        public void ShowDiagram()
        {
            if (DiagramSelectedItem == null || StatisticsSelectedItem == null)
                return;
            switch (DiagramSelectedItem)
            {
                case "Столбчатая":
                    SelectedViewModel = ListOfViewModels[1];
                    Mediator.Mediator.NotifyColleagues("ColumnDiagramStatistics", StatisticsSelectedItem);
                    break;
                case "Круговая":
                    SelectedViewModel = ListOfViewModels[0];
                    Mediator.Mediator.NotifyColleagues("CircleDiagramStatistics", StatisticsSelectedItem);
                    break;
                case "Линейная":
                    SelectedViewModel = ListOfViewModels[2];
                    Mediator.Mediator.NotifyColleagues("LinearDiagramStatistics", StatisticsSelectedItem);
                    break;
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
