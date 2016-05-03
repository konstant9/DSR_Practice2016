using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
