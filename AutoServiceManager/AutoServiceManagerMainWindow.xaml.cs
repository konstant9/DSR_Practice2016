using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using AutoServiceModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace AutoServiceManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _rowCount = 20;
        private int _numberOfPages;
        private int _currentPage;
        private bool _isFiltered = false;
        private bool _isSearchAccomplished = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrdersInfoDataGrid.CanUserAddRows = false;
            FillDataGrid();
        }

        private void RecordsNumberBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_rowCount == 5)
                return;
            _rowCount = 5;
            _currentPage = 0;
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

        private void RecordsNumberBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_rowCount == 20)
                return;
            _rowCount = 20;
            _currentPage = 0;
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

        private void RecordsNumberBlock3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_rowCount == 50)
                return;
            _rowCount = 50;
            _currentPage = 0;
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

        private void RecordsNumberBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_rowCount == 100)
                return;
            _rowCount = 100;
            _currentPage = 0;
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

        private void RecordsNumberBlock5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RecordsNumberTextBox.Visibility = Visibility.Visible;
            RecordsNumberTextBox.Focus();
        }

        private void RecordsNumberTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RecordsNumberTextBox.Visibility = Visibility.Hidden;
            RecordsNumberTextBox.Text = string.Empty;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RecordsNumberTextBox.IsFocused)
                Keyboard.ClearFocus();
        }

        private void pageNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                int rowCount;
                if (int.TryParse(RecordsNumberTextBox.Text, out rowCount))
                {
                    if (_rowCount == rowCount || rowCount <= 0)
                    {
                        Keyboard.ClearFocus();
                        return;
                    }
                    _rowCount = rowCount;
                    _currentPage = 0;
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
                Keyboard.ClearFocus();
            }
            PageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " + _numberOfPages;
        }

        private void FillDataGrid()
        {
            using (var context = new AutoServiceEntities())
            {
                var totalAmountOfRows = context.WorksAutoOrder.Count();
                _numberOfPages = totalAmountOfRows%_rowCount != 0
                    ? totalAmountOfRows/_rowCount + 1
                    : totalAmountOfRows/_rowCount;
                OrdersInfoDataGrid.ItemsSource = totalAmountOfRows < _rowCount ? context.WorksAutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage * _rowCount).ToList() 
                    : context.WorksAutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage * _rowCount).Take(_rowCount).ToList();
            }
            PageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " + _numberOfPages;
        }

        private void FirstPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentPage == 0)
                return;
            _currentPage = 0;
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

        private void LastPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentPage == _numberOfPages - 1)
                return;
            _currentPage = _numberOfPages - 1;
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

        private void PreviousPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentPage == 0)
                return;
            if (_currentPage > 0)
                _currentPage--;
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

        private void NextPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentPage == _numberOfPages - 1)
                return;
            if (_currentPage < _numberOfPages - 1)
                _currentPage++;
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
        private void Row_MouseEnter(object sender, MouseEventArgs e)
        {
            var row = (DataGridRow) e.Source;
            using (var context = new AutoServiceEntities())
            {
                var orderId = ((WorksAutoOrder)(OrdersInfoDataGrid.Items[row.GetIndex()])).OrderID;
                var owner = context.Customers.Where(x => x.Orders.Any(y => y.OrderID == orderId)).ToList().First();

                var stackPanel = new StackPanel();
                var ownerTextBlock = new TextBlock { Text = "Владелец:", FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
                var surnameTextBlock = new TextBlock { Text = "Фамилия: " + owner.Surname };
                var nameTextBlock = new TextBlock { Text = "Имя: " + owner.Name };
                var patronymicTextBlock = new TextBlock { Text = owner.Patronymic == null ? "Отчество не указано" : "Отчество: " + owner.Patronymic };
                var birthdayTextBlock = new TextBlock
                {
                    Text = owner.Birthday == null ? "Дата рождения не указана" :
                        $"Дата рождения: {((DateTime)owner.Birthday).Day}.{((DateTime)owner.Birthday).Month}.{((DateTime)owner.Birthday).Year}"
                };


                var phoneTextBlock = new TextBlock { Text = "Телефон: " + owner.Phone };

                stackPanel.Children.Add(ownerTextBlock);
                stackPanel.Children.Add(surnameTextBlock);
                stackPanel.Children.Add(nameTextBlock);
                stackPanel.Children.Add(patronymicTextBlock);
                stackPanel.Children.Add(birthdayTextBlock);
                stackPanel.Children.Add(phoneTextBlock);


                var toolTip = new ToolTip
                {
                    Content = stackPanel,
                    Placement = PlacementMode.Mouse,
                    Background = Brushes.White
                };
                row.ToolTip = toolTip;
                ToolTipService.SetInitialShowDelay(row, 3000);
                ToolTipService.SetShowDuration(row, 10000);
            }
        }

        private void Row_MouseLeave(object sender, MouseEventArgs e)
        {
            var row = (DataGridRow) e.Source;
            ((ToolTip) row.ToolTip).IsOpen = false;
        }

        private void OrdersInfoDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "OrderID":
                    {
                        e.Column.Header = "Номер" +Environment.NewLine +"заказа";
                        e.Column.CellStyle = (Style) OrdersInfoDataGrid.FindResource("NumericColumns");
                        break;
                    }
                case "CarBrand":
                    {
                        e.Column.Header = "Марка";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");

                        break;
                    }
                case "CarModel":
                    {
                        e.Column.Header = "Модель";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");
                        break;
                    }
                case "YearMade":
                    {
                        e.Column.Header = "Год" + Environment.NewLine + "выпуска";
                        e.Column.CellStyle = (Style)OrdersInfoDataGrid.FindResource("NumericColumns");
                        break;
                    }
                case "TransmissionType":
                    {
                        e.Column.Header = "Тип" + Environment.NewLine + "трансмиссии";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");
                        break;
                    }
                case "EnginePower":
                    {
                        e.Column.Header = "Мощность" + Environment.NewLine + "двигателя";
                        e.Column.CellStyle = (Style)OrdersInfoDataGrid.FindResource("NumericColumns");
                        e.Column.CanUserSort = true;

                        (((DataGridBoundColumn)(e.Column)).Binding).TargetNullValue = "Не указана";
                        break;
                    }
                case "WorksName":
                    {
                        e.Column.Header = "Наименование работ";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                    }
                case "WorksStart":
                    {
                        e.Column.Header = "Время начала";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");
                        ((DataGridTextColumn)e.Column).Binding.StringFormat = "{0:dd.MM.yyyy hh:mm}";
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToHeader);
                        break;
                    }
                case "WorksFinish":
                    {
                        e.Column.Header = "Время" + Environment.NewLine + " окончания ";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)OrdersInfoDataGrid.FindResource("WrappedTextBlockStyle");
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToHeader);
                        ((DataGridTextColumn)e.Column).Binding.StringFormat = "{0:dd.MM.yyyy hh:mm}" ;
                        (((DataGridBoundColumn)(e.Column)).Binding).TargetNullValue = "Заказ выполняется";
                        e.Column.CanUserSort = true;
                        break;
                    }
                case "WorksPrice":
                    {
                        e.Column.Header = "Цена";
                        e.Column.CellStyle = (Style)OrdersInfoDataGrid.FindResource("NumericColumns");
                        break;
                    }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchComboBox.SelectedItem = null;
            SearchTextBox.Text = string.Empty;
            if (!FilterDataGrid())
                return;
            _currentPage = 0;
            _isSearchAccomplished = false;

        }

        private bool FilterDataGrid()
        {
            if (ColumnFilterComboBox.SelectedItem == null) return false;
            if (StringValueFilterComboBox.Visibility == Visibility.Visible)
            {
                if (StringValueFilterComboBox.SelectedItem == null) return false;

                _isFiltered = true;
                switch (((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString())
                {
                    case "Марка":
                    {
                        using (var context = new AutoServiceEntities())
                        {
                            var query =
                                context.WorksAutoOrder.Where(
                                    x =>
                                        x.CarBrand == StringValueFilterComboBox.SelectedItem.ToString());
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
                                        x.CarModel == StringValueFilterComboBox.SelectedItem.ToString());
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
                                        x.TransmissionType == StringValueFilterComboBox.SelectedItem.ToString());
                            FillDataGrid(query);
                        }
                        break;
                    }
                }
                    
            }
            else
            {
                if (NumericValueFilterComboBox.SelectedItem == null || NumericValueTextBox.Text == string.Empty)
                    return false;
                if (((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString() == "Время начала"
                    || ((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString() == "Время окончания")
                {
                    DateTime date;
                    if (!DateTime.TryParse(NumericValueTextBox.Text, out date))
                        return false;

                    _isFiltered = true;
                    if (((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString() == "Время начала")
                    {
                        switch (((ComboBoxItem) NumericValueFilterComboBox.SelectedItem).Content.ToString())
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
                        return true;
                    }
                    else
                    {
                        switch (((ComboBoxItem) NumericValueFilterComboBox.SelectedItem).Content.ToString())
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
                                                (DbFunctions.TruncateTime(x.WorksStart) > date.Date) || (x.WorksFinish == null));
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
                                                (DbFunctions.TruncateTime(x.WorksStart) < date.Date) || (x.WorksFinish == null));
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        }
                        return true;
                    }
                }
                int value;
                if (!int.TryParse(NumericValueTextBox.Text, out value))
                    return false;

                        
                if (((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString() == "Мощность двигателя")
                {
                    _isFiltered = true;
                    switch (((ComboBoxItem)NumericValueFilterComboBox.SelectedItem).Content.ToString())
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
                    return true;
                }
                if (((ComboBoxItem) ColumnFilterComboBox.SelectedItem).Content.ToString() == "Год выпуска")
                {
                    if (value < 1930 || value > Convert.ToInt16(DateTime.Now.Date.Year))
                        return false;
                    _isFiltered = true;
                    switch (((ComboBoxItem)NumericValueFilterComboBox.SelectedItem).Content.ToString())
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
                    return true;
                }
                if (((ComboBoxItem)ColumnFilterComboBox.SelectedItem).Content.ToString() == "Цена")
                {
                    _isFiltered = true;
                    switch (((ComboBoxItem)NumericValueFilterComboBox.SelectedItem).Content.ToString())
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

                    return true;
                }
            }
            return false;
        }

        private void FillDataGrid(IQueryable<WorksAutoOrder> query)
        {
            var totalAmountOfRows = query.Count();
            _numberOfPages = totalAmountOfRows%_rowCount != 0
                ? totalAmountOfRows/_rowCount + 1
                : totalAmountOfRows/_rowCount;
            OrdersInfoDataGrid.ItemsSource = totalAmountOfRows < _rowCount
                ? query.OrderBy(x => x.OrderID).Skip(_currentPage*_rowCount).ToList()
                : query.OrderBy(x => x.OrderID)
                    .Skip(_currentPage*_rowCount)
                    .Take(_rowCount)
                    .ToList();
            PageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " +
                                       _numberOfPages;
        }

        private void ColumnFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumericValueTextBox.Text = string.Empty;
            NumericValueFilterComboBox.SelectedItem = null;
            StringValueFilterComboBox.SelectedItem = null;
            if(ColumnFilterComboBox.SelectedItem != null)
                if (((ComboBoxItem)ColumnFilterComboBox.SelectedItem).Content.ToString() == "Марка" ||
                    ((ComboBoxItem)ColumnFilterComboBox.SelectedItem).Content.ToString() == "Модель" ||
                    ((ComboBoxItem)ColumnFilterComboBox.SelectedItem).Content.ToString() == "Тип трансмиссии")
                {
                    NumericValueFilterComboBox.Visibility = Visibility.Hidden;
                    NumericValueTextBox.Visibility = Visibility.Hidden;
                    StringValueFilterComboBox.Visibility = Visibility.Visible;
                    var context = new AutoServiceEntities();
                    switch (((ComboBoxItem)ColumnFilterComboBox.SelectedItem).Content.ToString())
                    {
                        case "Марка":
                            StringValueFilterComboBox.ItemsSource =
                                context.WorksAutoOrder.Select(x=>x.CarBrand).Distinct().ToList();
                            break;
                        case "Модель":
                            StringValueFilterComboBox.ItemsSource =
                                context.WorksAutoOrder.Select(x => x.CarModel).Distinct().ToList();
                            break;
                        case "Тип трансмиссии":
                            StringValueFilterComboBox.ItemsSource =
                                context.WorksAutoOrder.Select(x => x.TransmissionType).Distinct().ToList();
                            break;
                    }
                }
                else
                {
                    NumericValueFilterComboBox.Visibility = Visibility.Visible;
                    NumericValueTextBox.Visibility = Visibility.Visible;
                    StringValueFilterComboBox.Visibility = Visibility.Hidden;
                }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isFiltered)
            {
                _isFiltered = false;
                _currentPage = 0;
                ColumnFilterComboBox.SelectedItem = null;
                StringValueFilterComboBox.SelectedItem = null;
                NumericValueTextBox.Text = string.Empty;
                NumericValueFilterComboBox.SelectedItem = string.Empty;
                FillDataGrid();
                return;
            }
            if (_isSearchAccomplished)
            {
                _isSearchAccomplished = false;
                SearchComboBox.SelectedItem = null;
                SearchTextBox.Text = string.Empty;
                _currentPage = 0;
                FillDataGrid();
            }
        }

        private void CearchButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnFilterComboBox.SelectedItem = null;
            NumericValueTextBox.Text = string.Empty;
            NumericValueFilterComboBox.SelectedItem = null;
            StringValueFilterComboBox.SelectedItem = null;
            if (SearchComboBox.SelectedItem == null || SearchTextBox.Text == string.Empty)
                return;
            _currentPage = 0;
            SearchThroughDataGrid();
            _isSearchAccomplished = true;
        }

        private void SearchThroughDataGrid()
        {
            switch (((ComboBoxItem) SearchComboBox.SelectedItem).Content.ToString())
            {
                case "Марка":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.CarBrand.Contains(SearchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
                case "Модель":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.CarModel.Contains(SearchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
                case "Наименование работ":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.WorksAutoOrder.Where(x => x.WorksName.Contains(SearchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
            }
        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchComboBox.SelectedItem == null)
                return;
            if (((ComboBoxItem)SearchComboBox.SelectedItem).Content.ToString() == "Наименование работ")
                SearchComboBox.FontSize = 10;
            else
                SearchComboBox.FontSize = 12;
            
        }
    }
}
