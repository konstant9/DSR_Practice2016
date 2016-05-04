using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AutoServiceManager.Views;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields
        private int _numberOfPages;
        private int _rowCount = 20;
        private int _currentPage = 0;
        private bool _isFiltered = false;
        private bool _isSearchAccomplished = false;
        private bool _isSorted = false;
        #endregion

        #region Properties
        private bool _isToolTipOpen;
        public bool IsToolTipOpen
        {
            get { return _isToolTipOpen;}
            set
            {
                _isToolTipOpen = value;
                OnPropertyChanged("IsToolTipOpen");
            }
        }

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

        public List<string> NumericColumnFilterComboBoxItems { get; set; } = new List<string>
        {
            ">",
            "<",
            "=",
            ">=",
            "<="
        };

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

        public List<string> SearchComboBoxItems { get; set; } = new List<string>
        {
            "Марка",
            "Модель",
            "Наименование работ"
        };

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
        public List<string> SortDirectionComboBoxItems { get; set; } = new List<string>
        {
            "По возрастанию",
            "По убыванию"
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

        private string _filterText;
        public string FilterText
        {
            get { return _filterText;}
            set
            {
                _filterText = value;
                OnPropertyChanged("FilterText");
            }   
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText;}
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
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

        private string _toolTipText = "lol";
        public string ToolTipText
        {
            get { return _toolTipText; }
            set
            {
                _toolTipText = value;
                OnPropertyChanged("ToolTipText");
            }
        }

        private List<OrderCustomer> _autoServiceList;
        public List<OrderCustomer> AutoServiceList
        {
            get { return _autoServiceList; }
            set
            {
                _autoServiceList = value;
                OnPropertyChanged("AutoServiceList");
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
                    ShowRecords();
                });
            }
        }

        public ICommand TwentyRecordsCommand {
            get
            {
                return new RelayCommand(x =>
                {
                    if (_rowCount == 20)
                        return;
                    _rowCount = 20;
                    _currentPage = 0;
                    ShowRecords();
                });
            } }

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
                    ShowRecords();
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
                    ShowRecords();
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
                    ShowRecords();
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
                    ShowRecords();
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
                    ShowRecords();
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
                    ShowRecords();
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
                    ShowRecords();
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
                    NumericColumnFilterComboBoxSelectedItem = null;
                    StringColumnFilterComboBoxSelectedItem = null;
                    ColumnFilterComboBoxSelectedItem = null;
                    FilterText = string.Empty;
                    if (SearchComboBoxSelectedItem == null || SearchText == string.Empty)
                        return;
                    _currentPage = 0;
                    _isSearchAccomplished = true;
                    _isFiltered = false;
                    SearchThroughDataGrid();
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
                            FillDataGrid();
                        }
                    }
                    if (_isFiltered)
                    {
                        _isFiltered = false;
                        _currentPage = 0;
                        ColumnFilterComboBoxSelectedItem = null;
                        StringColumnFilterComboBoxSelectedItem = null;
                        FilterText = string.Empty;
                        NumericColumnFilterComboBoxSelectedItem = string.Empty;
                        FillDataGrid();
                        return;
                    }
                    if (_isSearchAccomplished)
                    {
                        _isSearchAccomplished = false;
                        _currentPage = 0;
                        SearchComboBoxSelectedItem = null;
                        SearchText = string.Empty;
                        FillDataGrid();
                    }
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
                    //Не решил, что лучше, при сортировке сбрасывать на первую страницу, или сортировать ту, что есть
                    /*_currentPage = 0;
                    FillDataGrid();*/
                    SortDataGrid();
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

        //public ICommand CloseToolTipCommand { get; set; }
        public MainViewModel()
        {
            FillDataGrid();
            /*CloseToolTipCommand = new RelayCommand(x =>
            {
                if (IsToolTipOpen)
                    IsToolTipOpen = false;                
            });*/
        }

        #region Methods
        private void ShowRecords()
        {
            if (_isFiltered)
            {
                FilterDataGrid();
                return;
            }
            if (_isSearchAccomplished)
            {
                SearchThroughDataGrid();
                return;
            }
            FillDataGrid();
        }

        private void SwapFilterComboBoxes(string filterBy)
        {
            if (filterBy == "Модель" || filterBy == "Марка" || filterBy == "Тип трансмиссии")
            {
                NumericFilterComboBoxVisibility = Visibility.Hidden;
                NumericFilterTextBoxVisibility = Visibility.Hidden;
                var context = new AutoServiceEntities();
                switch (filterBy)
                {
                    case "Марка":
                        {
                            StringColumnFilterComboBoxItems = context.WorksAutoOrder.Select(x => x.CarBrand).Distinct().ToList();
                            break;
                        }
                    case "Модель":
                        {
                            StringColumnFilterComboBoxItems = context.WorksAutoOrder.Select(x => x.CarModel).Distinct().ToList();
                            break;
                        }
                    case "Тип трансмиссии":
                        {
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
            {
                var totalAmountOfRows = context.WorksAutoOrder.Count(); 
                _numberOfPages = totalAmountOfRows%_rowCount != 0 
                    ? totalAmountOfRows/_rowCount + 1 
                    : totalAmountOfRows/_rowCount; 
                var query = totalAmountOfRows<_rowCount? context.WorksAutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage* _rowCount)
                    : context.WorksAutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage* _rowCount).Take(_rowCount);
                AutoServiceList = OrderCustomer.ConvertToList(query);
            }
            if (_isSorted)
                SortDataGrid();
            PageCountInfo = "Страница " + (_currentPage + 1) + " из " + _numberOfPages; 
        }

        private void FillDataGrid(IQueryable<WorksAutoOrder> query)
        { 
             var totalAmountOfRows = query.Count(); 
             _numberOfPages = totalAmountOfRows%_rowCount != 0 
                 ? totalAmountOfRows/_rowCount + 1 
                    : totalAmountOfRows/_rowCount;
            var q = totalAmountOfRows < _rowCount
                ? query.OrderBy(x => x.OrderID).Skip(_currentPage*_rowCount)
                : query.OrderBy(x => x.OrderID)
                    .Skip(_currentPage*_rowCount)
                    .Take(_rowCount);
            AutoServiceList = OrderCustomer.ConvertToList(q);
            if (_isSorted)
                SortDataGrid();
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
                    _isFiltered = true;
                    _currentPage = 0;
                    _isSearchAccomplished = false;
                }
                switch (ColumnFilterComboBoxSelectedItem)
                {
                    case "Марка":
                        {
                            using (var context = new AutoServiceEntities())
                            {
                                var query =
                                    context.WorksAutoOrder.Where(
                                        x =>
                                            x.CarBrand == StringColumnFilterComboBoxSelectedItem);
                                FillDataGrid(query);
                            }
                            break;
                        }
                    case "Модель":
                        {
                            using (var context = new AutoServiceEntities())
                            {
                                var query =
                                    context.WorksAutoOrder.Where(
                                        x =>
                                            x.CarModel == StringColumnFilterComboBoxSelectedItem);
                                FillDataGrid(query);
                            }
                            break;
                        }
                    case "Тип трансмиссии":

                        {
                            using (var context = new AutoServiceEntities())

                            {
                                var query =
                                    context.WorksAutoOrder.Where(
                                        x =>
                                            x.TransmissionType == StringColumnFilterComboBoxSelectedItem);
                                FillDataGrid(query);
                            }
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
                        _isFiltered = true;
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    if (ColumnFilterComboBoxSelectedItem == "Время начала")
                    {
                        switch (NumericColumnFilterComboBoxSelectedItem)
                        {
                            case ">=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (x.WorksStart >= date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "<=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (x.WorksStart <= date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    DbFunctions.TruncateTime(x.WorksStart) == date.Date);
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case ">":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (DbFunctions.TruncateTime(x.WorksStart) > date.Date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "<":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (DbFunctions.TruncateTime(x.WorksStart) < date.Date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                        }
                        return;
                    }
                    else
                    {
                        switch (NumericColumnFilterComboBoxSelectedItem)
                        {
                            case ">=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (x.WorksFinish >= date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "<=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (x.WorksFinish <= date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    DbFunctions.TruncateTime(x.WorksFinish) == date.Date);
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case ">":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (DbFunctions.TruncateTime(x.WorksFinish) > date.Date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            case "<":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.WorksAutoOrder.Where(
                                                x =>
                                                    (DbFunctions.TruncateTime(x.WorksFinish) < date.Date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
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
                        _isFiltered = true;
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    switch (NumericColumnFilterComboBoxSelectedItem)
                    {
                        case ">=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                (x.EnginePower >= value) || (x.EnginePower == null));
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                (x.EnginePower <= value) || (x.EnginePower == null));
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.EnginePower == value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case ">":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                (x.EnginePower > value) || (x.EnginePower == null));
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                (x.EnginePower < value) || (x.EnginePower == null));
                                    FillDataGrid(query);
                                }
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
                        _isFiltered = true;
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    switch (NumericColumnFilterComboBoxSelectedItem)
                    {
                        case ">=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.YearMade >= value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.YearMade <= value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.YearMade == value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case ">":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.YearMade > value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.YearMade < value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                    }
                    return;
                }
                if (ColumnFilterComboBoxSelectedItem == "Цена")
                {
                    if (!_isFiltered)
                    {
                        _isFiltered = true;
                        _currentPage = 0;
                        _isSearchAccomplished = false;
                    }
                    switch (NumericColumnFilterComboBoxSelectedItem)
                    {
                        case ">=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.WorksPrice >= value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.WorksPrice <= value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "=":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.WorksPrice == value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case ">":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.WorksPrice > value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        case "<":
                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.WorksAutoOrder.Where(
                                            x =>
                                                x.WorksPrice < value);
                                    FillDataGrid(query);
                                }
                                break;
                            }
                    }
                }
            }
        }

        private void SearchThroughDataGrid()
        {
            switch (SearchComboBoxSelectedItem)
            {
                case "Марка":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.CarBrand.Contains(SearchText));
                        FillDataGrid(query);
                    }
                    break;
                case "Модель":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.CarModel.Contains(SearchText));
                        FillDataGrid(query);
                    }
                    break;
                case "Наименование работ":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.WorksName.Contains(SearchText));
                        FillDataGrid(query);
                    }
                    break;
            }
        }

        private void SortDataGrid()
        {
            switch (SortComboBoxSelectedItem)
            {
                case "Номер заказа":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.OrderID).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.OrderID).ToList();
                        break;
                    }
                case "Марка":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.CarBrand).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.CarBrand).ToList();
                        break;
                    }
                case "Модель":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.CarModel).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.CarModel).ToList();
                        break;
                    }
                case "Тип трансмиссии":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.TransmissionType).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.TransmissionType).ToList();
                        break;
                    }
                case "Мощность двигателя":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.EnginePower).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.EnginePower).ToList();
                        break;
                    }
                case "Год выпуска":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.YearMade).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.YearMade).ToList();
                        break;
                    }
                case "Наименование работ":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.WorksName).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.WorksName).ToList();
                        break;
                    }
                case "Время начала":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.WorksStart).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.WorksStart).ToList();
                        break;
                    }
                case "Время окончания":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.WorksFinish).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.WorksFinish).ToList();
                        break;
                    }
                case "Цена":
                    {
                        AutoServiceList = SortDirectionComboBoxSelectedItem == "По возрастанию" ?
                                AutoServiceList.OrderBy(x => x.Order.WorksPrice).ToList() :
                                AutoServiceList.OrderByDescending(x => x.Order.WorksPrice).ToList();
                        break;
                    }
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
