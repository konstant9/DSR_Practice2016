using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AutoServiceManager.Views;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private int _numberOfPages;
        private int _rowCount = 20;
        private int _currentPage;
        private bool _isSorted;
        private bool _isFiltered;
        private bool _isSearchAccomplished;
        private List<WorksAutoOrder> _listOfOrders;

        #endregion

        #region Fields and Properties

        private int _customNumberOfPages;
        public int CustomNumberOfPages
        {
            get { return _customNumberOfPages; }
            set
            {
                _customNumberOfPages = value;
                OnPropertyChanged("CustomNumberOfPages");
            }
        }

        private string _pageCountInfo;
        public string PageCountInfo
        {
            get { return _pageCountInfo; }
            set
            {
                _pageCountInfo = value;
                OnPropertyChanged("PageCountInfo");
            }
        }

        private bool _textBoxIsFocused;
        public bool TextBoxIsFocused
        {
            get { return _textBoxIsFocused; }
            set
            {
                if(value == false)
                    PageTextBoxVisibility = Visibility.Hidden;
                _textBoxIsFocused = value;
                OnPropertyChanged("TextBoxIsFocused");
            }
        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged("FilterText");
            }
        }

        private string _columnFilterComboBoxSelectedItem;
        public string ColumnFilterComboBoxSelectedItem
        {
            get { return _columnFilterComboBoxSelectedItem;}
            set
            {
                SwapFilterComboBoxes(value);
                _columnFilterComboBoxSelectedItem = value;
                OnPropertyChanged("ColumnFilterComboBoxSelectedItem");
            }
        }

        public List<string> ColumnFilterComboBoxItems { get; set; } = new List<string>
        {
            "Марка",
            "Модель",
            "Тип трансмиссии",
            "Мощность двигателя",
            "Год выпуска",
            "Время начала",
            "Время окончания",
            "Цена"
        };

        private string _stringColumnFilterComboBoxSelectedItem;
        public string StringColumnFilterComboBoxSelectedItem
        {
            get { return _stringColumnFilterComboBoxSelectedItem; }
            set
            {
                _stringColumnFilterComboBoxSelectedItem = value;
                OnPropertyChanged("StringColumnFilterComboBoxSelectedItem");
            }
        }

        private List<string> _stringColumnFilterComboBoxItems; 
        public List<string> StringColumnFilterComboBoxItems
        {
            get
            {
                return _stringColumnFilterComboBoxItems;
            }
            set
            {
                _stringColumnFilterComboBoxItems = value;
                OnPropertyChanged("StringColumnFilterComboBoxItems");
            }
        }

        private string _numericColumnFilterComboBoxSelectedItem;
        public string NumericColumnFilterComboBoxSelectedItem
        {
            get { return _numericColumnFilterComboBoxSelectedItem; }
            set
            {
                _numericColumnFilterComboBoxSelectedItem = value;
                OnPropertyChanged("NumericColumnFilterComboBoxSelectedItem");
            }
        }

        public List<string> NumericColumnFilterComboBoxItems { get; set; } = new List<string>
        {
            ">",
            "<",
            "=",
            ">=",
            "<="
        };

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        private string _searchComboBoxSelectedItem;
        public string SearchComboBoxSelectedItem
        {
            get { return _searchComboBoxSelectedItem; }
            set
            {
                _searchComboBoxSelectedItem = value;
                OnPropertyChanged("SearchComboBoxSelectedItem");
            }
        }

        public List<string> SearchComboBoxItems { get; set; } = new List<string>
        {
            "Номер заказа",
            "Марка",
            "Модель",
            "Наименование работ"
        };

        private string _sortComboBoxSelectedItem;
        public string SortComboBoxSelectedItem
        {
            get { return _sortComboBoxSelectedItem; }
            set
            {
                _sortComboBoxSelectedItem = value;
                OnPropertyChanged("SortComboBoxSelectedItem");
            }
        }

        public List<string> SortComboBoxItems { get; set; } = new List<string>
        {
            "Номер заказа",
            "Марка",
            "Модель",
            "Мощность двигателя",
            "Тип трансмиссии",
            "Год выпуска",
            "Наименование работ",
            "Время начала",
            "Время окончания",
            "Цена"
        };

        private string _sortDirectionComboBoxSelectedItem;
        public string SortDirectionComboBoxSelectedItem
        {
            get { return _sortDirectionComboBoxSelectedItem; }
            set
            {
                _sortDirectionComboBoxSelectedItem = value;
                OnPropertyChanged("SortDirectionComboBoxSelectedItem");
            }
        }

        public List<string> SortDirectionComboBoxItems { get; set; } = new List<string>
        {
            "По возрастанию",
            "По убыванию"
        };

        private List<CustomerOrder> _customerOrdersList;
        public List<CustomerOrder> CustomerOrdersList
        {
            get { return _customerOrdersList; }
            set
            {
                _customerOrdersList = value;
                OnPropertyChanged("CustomerOrdersList");
            }
        }

        private Visibility _pageTextBoxVisibility = Visibility.Hidden;
        public Visibility PageTextBoxVisibility
        {
            get { return _pageTextBoxVisibility; }
            set
            {
                _pageTextBoxVisibility = value;
                OnPropertyChanged("PageTextBoxVisibility");
            }
        }

        private Visibility _numericFilterTextBoxVisibility = Visibility.Hidden;
        public Visibility NumericFilterTextBoxVisibility
        {
            get { return _numericFilterTextBoxVisibility; }
            set
            {
                _numericFilterTextBoxVisibility = value;
                OnPropertyChanged("NumericFilterTextBoxVisibility");
            }
        }

        private Visibility _numericFilterComboBoxVisibility = Visibility.Hidden;
        public Visibility NumericFilterComboBoxVisibility
        {
            get { return _numericFilterComboBoxVisibility; }
            set
            {
                _numericFilterComboBoxVisibility = value;
                OnPropertyChanged("NumericFilterComboBoxVisibility");
            }
        }
        
        #endregion
        
        #region Commands

        public ICommand FiveRecordsCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == 5)
                        return;
                    _rowCount = 5;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand TwentyRecordsCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == 20)
                        return;
                    _rowCount = 20;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand FiftyRecordsCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == 50)
                        return;
                    _rowCount = 50;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand HundredRecordsCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == 100)
                        return;
                    _rowCount = 100;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand CustomRecordsCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == CustomNumberOfPages || CustomNumberOfPages <= 0)
                    {
                        TextBoxIsFocused = false;
                        PageTextBoxVisibility = Visibility.Hidden;
                        return;
                    }
                    _rowCount = CustomNumberOfPages;
                    _currentPage = 0;
                    FillDataGrid();
                    TextBoxIsFocused = false;
                    CustomNumberOfPages = 0;
                });
            }
        }
        public ICommand CustomRecordsClickCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    PageTextBoxVisibility = Visibility.Visible;
                    TextBoxIsFocused = true;
                });
            }
        }
        public ICommand FirstPageCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_currentPage == 0)
                        return;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand LastPageCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_currentPage == _numberOfPages - 1)
                        return;
                    _currentPage = _numberOfPages - 1;
                    FillDataGrid();
                });
            }
        }
        public ICommand NextPageCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_currentPage == _numberOfPages - 1)
                        return;
                    if (_currentPage < _numberOfPages - 1)
                        _currentPage++;
                    FillDataGrid();
                });
            }
        }
        public ICommand PreviousPageCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_currentPage == 0)
                        return;
                    if (_currentPage > 0)
                        _currentPage--;
                    FillDataGrid();
                });
            }
        }
        public ICommand FilterClickCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    SearchComboBoxSelectedItem = null;
                    SearchText = string.Empty;
                    if (_isFiltered)
                        _currentPage = 0;
                    FilterDataGrid();
                });
            }
        }
        public ICommand SearchClickCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    
                    if (SearchComboBoxSelectedItem == null || SearchText == string.Empty)
                        return;
                    NumericColumnFilterComboBoxSelectedItem = null;
                    StringColumnFilterComboBoxSelectedItem = null;
                    ColumnFilterComboBoxSelectedItem = null;
                    FilterText = string.Empty;
                    _currentPage = 0;
                    _isFiltered = false;
                    _isSearchAccomplished = true;
                    SearchThroughDataGrid();
                });
            }
        }
        public ICommand SortClickCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (SortComboBoxSelectedItem == null || SortDirectionComboBoxSelectedItem == null)
                        return;
                    _isSorted = true;
                    _currentPage = 0;
                    FillDataGrid();
                });
            }
        }
        public ICommand ResetClickCommand
        {
            get 
            {
                return new RelayCommand(x =>
                {
                    if (_isSorted)
                    {
                        _isSorted = false;
                        SortComboBoxSelectedItem = null;
                        SortDirectionComboBoxSelectedItem = null;
                        if (!_isFiltered && !_isSearchAccomplished)
                        {
                            _currentPage = 0;
                            _listOfOrders = null;
                            FillDataGrid();
                        }
                    }
                    if (_isFiltered)
                    {
                        _currentPage = 0;
                        _isFiltered = false;
                        ColumnFilterComboBoxSelectedItem = null;
                        StringColumnFilterComboBoxSelectedItem = null;
                        FilterText = string.Empty;
                        NumericColumnFilterComboBoxSelectedItem = string.Empty;
                        _listOfOrders = null;
                        FillDataGrid();
                        return;
                    }
                    if (_isSearchAccomplished)
                    {
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                        SearchComboBoxSelectedItem = null;
                        SearchText = string.Empty;
                        _listOfOrders = null;
                        FillDataGrid();
                    }
                });
            }
        }
        public ICommand WindowClickCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    if (TextBoxIsFocused)
                        TextBoxIsFocused = false;
                });
            }
        }
        public ICommand StatisticsButtonClickCommand
        {
            get
            {
                return  new RelayCommand(x =>
                {
                    var view = new StatisticsWindow();
                    view.ShowDialog();
                });
            }
        }
        
        #endregion

        public MainViewModel()
        {
            FillDataGrid();
        }

        #region Methods

        private void SwapFilterComboBoxes(string filterBy)
        {
            if (filterBy == "Модель" || filterBy == "Марка" || filterBy == "Тип трансмиссии")
            {
                NumericFilterComboBoxVisibility = Visibility.Hidden;
                NumericFilterTextBoxVisibility = Visibility.Hidden;
                using (var context = new AutoServiceEntities())
                {
                    switch (filterBy)
                    {
                        case "Марка":
                            StringColumnFilterComboBoxItems = context.WorksAutoOrder.Select(x => x.CarBrand).Distinct().ToList();
                            break;
                        case "Модель":
                            StringColumnFilterComboBoxItems = context.WorksAutoOrder.Select(x => x.CarModel).Distinct().ToList();
                            break;
                        case "Тип трансмиссии":
                            StringColumnFilterComboBoxItems = context.WorksAutoOrder.Select(x => x.TransmissionType).Distinct().ToList();
                            break;
                    }
                }
            }
            else
            {
                NumericFilterComboBoxVisibility = Visibility.Visible;
                NumericFilterTextBoxVisibility = Visibility.Visible;
            }
        }

        private void FillDataGrid()
        {
            using (var context = new AutoServiceEntities())
                if (_listOfOrders == null)
                    _listOfOrders = context.WorksAutoOrder.Select(x => x).ToList();
            if (_isSorted)
                SortDataGrid();
            var totalAmountOfRows = _listOfOrders.Count;
            _numberOfPages = totalAmountOfRows%_rowCount != 0
                ? totalAmountOfRows/_rowCount + 1
                : totalAmountOfRows/_rowCount;
            var ordersOnCurrentPage = totalAmountOfRows < _rowCount
                ? _listOfOrders.Skip(_currentPage*_rowCount)
                : _listOfOrders.Skip(_currentPage*_rowCount).Take(_rowCount);
            CustomerOrdersList = CustomerOrder.GetCustomerOrders(ordersOnCurrentPage);
            PageCountInfo = "Страница " + (_currentPage + 1) + " из " + _numberOfPages; 
        }

        private void FilterDataGrid()
        {
            if (ColumnFilterComboBoxSelectedItem == null) return;
            if (NumericFilterComboBoxVisibility == Visibility.Hidden)
            {
                if (StringColumnFilterComboBoxSelectedItem == null) return;
                if (!_isFiltered)
                {
                    _currentPage = 0;
                    _isSearchAccomplished = false;
                }
                using (var context = new AutoServiceEntities())
                {
                    switch (ColumnFilterComboBoxSelectedItem)
                    {
                        case "Марка":
                                _listOfOrders = context.WorksAutoOrder.Where(x => x.CarBrand == StringColumnFilterComboBoxSelectedItem).ToList();
                                FillDataGrid();
                                break;
                        case "Модель":
                                _listOfOrders = context.WorksAutoOrder.Where(x => x.CarModel == StringColumnFilterComboBoxSelectedItem).ToList();
                                FillDataGrid();
                                break;
                        case "Тип трансмиссии":
                                _listOfOrders = context.WorksAutoOrder.Where(x => x.TransmissionType == StringColumnFilterComboBoxSelectedItem).ToList();
                                FillDataGrid();
                                break;
                    }
                }
            }
            else
            {
                if (NumericColumnFilterComboBoxSelectedItem == null || FilterText == string.Empty)
                    return;
                if (ColumnFilterComboBoxSelectedItem == "Время начала"
                    || ColumnFilterComboBoxSelectedItem == "Время окончания")
                {
                    DateTime date;
                    if (!DateTime.TryParse(FilterText, out date))
                        return;

                    if (!_isFiltered)
                    {
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }

                    if (ColumnFilterComboBoxSelectedItem == "Время начала")
                    {
                        using (var context = new AutoServiceEntities())
                        {
                            switch (NumericColumnFilterComboBoxSelectedItem)
                            {
                                case ">=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => (x.WorksStart >= date)).ToList();
                                        FillDataGrid();
                                        break;
                                case "<=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => (x.WorksStart <= date)).ToList();
                                        FillDataGrid();
                                        break;
                                case "=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => DbFunctions.TruncateTime(x.WorksStart) == date.Date).ToList();
                                        FillDataGrid();
                                        break;
                                case ">":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => DbFunctions.TruncateTime(x.WorksStart) > date.Date).ToList();
                                        FillDataGrid();
                                        break;
                                case "<":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => (DbFunctions.TruncateTime(x.WorksStart) < date.Date)).ToList();
                                        FillDataGrid();
                                        break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        using (var context = new AutoServiceEntities())
                        {
                            switch (NumericColumnFilterComboBoxSelectedItem)
                            {
                                case ">=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x =>(x.WorksFinish >= date) || (x.WorksFinish == null)).ToList();
                                        FillDataGrid();
                                        break;
                                case "<=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => (x.WorksFinish <= date)).ToList();
                                        FillDataGrid();
                                        break;
                                case "=":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => DbFunctions.TruncateTime(x.WorksFinish) == date.Date).ToList();
                                        FillDataGrid();
                                        break;
                                case ">":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => 
                                                        (DbFunctions.TruncateTime(x.WorksFinish) > date.Date) ||
                                                        (x.WorksFinish == null)).ToList();
                                        FillDataGrid();
                                        break;
                                case "<":
                                        _listOfOrders = context.WorksAutoOrder.Where(x => (DbFunctions.TruncateTime(x.WorksFinish) < date.Date)).ToList();
                                        FillDataGrid();
                                        break;
                            }
                        }
                        return;
                    }
                }
                int value;
                if (!int.TryParse(FilterText, out value))
                    return;
                if (ColumnFilterComboBoxSelectedItem == "Мощность двигателя")
                {
                    if (!_isFiltered)
                    {
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    using (var context = new AutoServiceEntities())
                    {
                        switch (NumericColumnFilterComboBoxSelectedItem)
                        {
                            case ">=":
                                _listOfOrders = context.WorksAutoOrder.Where( x => (x.EnginePower >= value) || (x.EnginePower == null)).ToList();
                                    FillDataGrid();
                                    break;
                            case "<=":
                                    _listOfOrders = context.WorksAutoOrder.Where( x => (x.EnginePower <= value) || (x.EnginePower == null)).ToList();
                                    FillDataGrid();
                                    break;
                            case "=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.EnginePower == value).ToList();
                                    FillDataGrid();
                                    break;
                            case ">":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => (x.EnginePower > value) || (x.EnginePower == null)).ToList();
                                    FillDataGrid();
                                    break;
                            case "<":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => (x.EnginePower < value) || (x.EnginePower == null)).ToList();
                                    FillDataGrid();
                                    break;
                        }
                    }
                    return;
                }
                if (ColumnFilterComboBoxSelectedItem == "Год выпуска")
                {
                    if (value < 1930 || value > Convert.ToInt16(DateTime.Now.Date.Year))
                        return;
                    if (!_isFiltered)
                    {
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    using (var context = new AutoServiceEntities())
                    {
                        switch (NumericColumnFilterComboBoxSelectedItem)
                        {
                            case ">=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.YearMade >= value).ToList();
                                    FillDataGrid();
                                    break;
                            case "<=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.YearMade <= value).ToList();
                                    FillDataGrid();
                                    break;
                            case "=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.YearMade == value).ToList();
                                    FillDataGrid();
                                    break;
                            case ">":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.YearMade > value).ToList();
                                    FillDataGrid();
                                    break;
                            case "<":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.YearMade < value).ToList();
                                    FillDataGrid();
                                    break;
                        }
                    }
                    return;
                }
                if (ColumnFilterComboBoxSelectedItem == "Цена")
                {
                    if (!_isFiltered)
                    {
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    using (var context = new AutoServiceEntities())
                    {
                        switch (NumericColumnFilterComboBoxSelectedItem)
                        {
                            case ">=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksPrice >= value).ToList();
                                    FillDataGrid();
                                    break;
                            case "<=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksPrice <= value).ToList();
                                    FillDataGrid();
                                    break;
                            case "=":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksPrice == value).ToList();
                                    FillDataGrid();
                                    break;
                            case ">":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksPrice > value).ToList();
                                    FillDataGrid();
                                    break;
                            case "<":
                                    _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksPrice < value).ToList();
                                    FillDataGrid();
                                    break;
                        }
                    }
                }
            }
        }

        private void SearchThroughDataGrid()
        {
            using (var context = new AutoServiceEntities())
            {
                switch (SearchComboBoxSelectedItem)
                {
                    case "Номер заказа":
                        int orderID;
                        if (!int.TryParse(SearchText, out orderID) || orderID<=0)
                            return;
                        _listOfOrders = context.WorksAutoOrder.Where(x => x.OrderID == orderID).ToList();
                        FillDataGrid();
                        break;
                    case "Марка":
                        _listOfOrders = context.WorksAutoOrder.Where(x => x.CarBrand.Contains(SearchText)).ToList();
                        FillDataGrid();
                        break;
                    case "Модель":
                        _listOfOrders = context.WorksAutoOrder.Where(x => x.CarModel.Contains(SearchText)).ToList();
                        FillDataGrid();
                        break;
                    case "Наименование работ":
                        _listOfOrders = context.WorksAutoOrder.Where(x => x.WorksName.Contains(SearchText)).ToList();
                        FillDataGrid();
                        break;
                }
            }
        }

        private void SortDataGrid()
        {
            switch (SortComboBoxSelectedItem)
            {
                case "Номер заказа":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.OrderID).ToList() :
                                _listOfOrders.OrderByDescending(x => x.OrderID).ToList();
                        break;
                case "Марка":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.CarBrand).ToList() :
                                _listOfOrders.OrderByDescending(x => x.CarBrand).ToList();
                        break;
                case "Модель":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.CarModel).ToList() :
                                _listOfOrders.OrderByDescending(x => x.CarModel).ToList();
                        break;
                case "Тип трансмиссии":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.TransmissionType).ToList() :
                                _listOfOrders.OrderByDescending(x => x.TransmissionType).ToList();
                        break;
                case "Мощность двигателя":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.EnginePower).ToList() :
                                _listOfOrders.OrderByDescending(x => x.EnginePower).ToList();
                        break;
                case "Год выпуска":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.YearMade).ToList() :
                                _listOfOrders.OrderByDescending(x => x.YearMade).ToList();
                        break;
                case "Наименование работ":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.WorksName).ToList() :
                                _listOfOrders.OrderByDescending(x => x.WorksName).ToList();
                        break;
                case "Время начала":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.WorksStart).ToList() :
                                _listOfOrders.OrderByDescending(x => x.WorksStart).ToList();
                        break;
                case "Время окончания":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.WorksFinish == null).ThenBy(x => x.WorksFinish).ToList() :
                                _listOfOrders.OrderByDescending(x => x.WorksFinish == null).ThenByDescending(x => x.WorksFinish).ToList();
                        break;
                case "Цена":
                        _listOfOrders = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                _listOfOrders.OrderBy(x => x.WorksPrice).ToList() :
                                _listOfOrders.OrderByDescending(x => x.WorksPrice).ToList();
                        break;
            }
        }

        #endregion

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