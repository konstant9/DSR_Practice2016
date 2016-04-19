using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
            dataGrid.CanUserAddRows = false;
            FillDataGrid();
        }

        private void numberTextBlock1_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void numberTextBlock2_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void numberTextBlock3_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void numberTextBlock4_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void numberTextBlock5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pageNumberTextBox.Visibility = Visibility.Visible;
            pageNumberTextBox.Focus();
        }

        private void pageNumberTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            pageNumberTextBox.Visibility = Visibility.Hidden;
            pageNumberTextBox.Text = string.Empty;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageNumberTextBox.IsFocused)
                Keyboard.ClearFocus();
        }

        private void pageNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                int rowCount;
                if (int.TryParse(pageNumberTextBox.Text, out rowCount))
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
            pageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " + _numberOfPages;
        }

        private void FillDataGrid()
        {
            using (var context = new AutoServiceEntities())
            {
                var totalAmountOfRows = context.AutoOrder.Count();
                _numberOfPages = totalAmountOfRows%_rowCount != 0
                    ? totalAmountOfRows/_rowCount + 1
                    : totalAmountOfRows/_rowCount;
                dataGrid.ItemsSource = totalAmountOfRows < _rowCount ? context.AutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage * _rowCount).ToList() 
                    : context.AutoOrder.OrderBy(x => x.OrderID).Skip(_currentPage * _rowCount).Take(_rowCount).ToList();
            }
            pageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " + _numberOfPages;
        }

        private void firstPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void lastPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void previousPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void nextPageTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
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
                var orderId = ((AutoOrder)(dataGrid.Items[row.GetIndex()])).OrderID;
                var owner = context.Customers.Where(x => x.Orders.Any(y => y.OrderID == orderId)).ToList().First();

                var stackPanel = new StackPanel();
                var ownerTextBlock = new TextBlock { Text = "Владелец:", FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
                var surnameTextBlock = new TextBlock { Text = "Фамилия: " + owner.Surname };
                var nameTextBlock = new TextBlock { Text = "Имя: " + owner.Name };
                var patronymicTextBlock = new TextBlock { Text = owner.Patronymic == null ? "Отчество не указано" : "Отчество: " + owner.Patronymic };
                var birthdayTextBlock = new TextBlock
                {
                    Text = owner.Birthday == null ? "Дата рождения не указана" :
                        $"Дата рождения: {((DateTime)owner.Birthday).Day}/{((DateTime)owner.Birthday).Month}/{((DateTime)owner.Birthday).Year}"
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

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "OrderID":
                    {
                        e.Column.Header = "Номер" +Environment.NewLine +"заказа";
                        e.Column.CellStyle = (Style) dataGrid.FindResource("NumericColumns");
                        break;
                    }
                case "CarBrand":
                    {
                        e.Column.Header = "Марка";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)dataGrid.FindResource("WrappedTextBlockStyle");

                        break;
                    }
                case "CarModel":
                    {
                        e.Column.Header = "Модель";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)dataGrid.FindResource("WrappedTextBlockStyle");
                        break;
                    }
                case "YearMade":
                    {
                        e.Column.Header = "Год" + Environment.NewLine + "выпуска";
                        e.Column.CellStyle = (Style)dataGrid.FindResource("NumericColumns");
                        break;
                    }
                case "TransmissionType":
                    {
                        e.Column.Header = "Тип" + Environment.NewLine + "трансмиссии";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)dataGrid.FindResource("WrappedTextBlockStyle");
                        break;
                    }
                case "EnginePower":
                    {
                        e.Column.Header = "Мощность" + Environment.NewLine + "двигателя";
                        e.Column.CellStyle = (Style)dataGrid.FindResource("NumericColumns");
                        e.Column.CanUserSort = true;

                        (((DataGridBoundColumn)(e.Column)).Binding).TargetNullValue = "Не указана";
                        break;
                    }
                case "WorksName":
                    {
                        e.Column.Header = "Наименование работ";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style) dataGrid.FindResource("WrappedTextBlockStyle");
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        break;
                    }
                case "WorksStart":
                    {
                        e.Column.Header = "Время начала";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)dataGrid.FindResource("WrappedTextBlockStyle");
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToHeader);
                        break;
                    }
                case "WorksFinish":
                    {
                        e.Column.Header = "Время" + Environment.NewLine + " окончания ";
                        ((DataGridTextColumn)e.Column).ElementStyle = (Style)dataGrid.FindResource("WrappedTextBlockStyle");
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToHeader);
                        (((DataGridBoundColumn)(e.Column)).Binding).TargetNullValue = "Заказ выполняется";
                        e.Column.CanUserSort = true;
                        break;
                    }
                case "WorksPrice":
                    {
                        e.Column.Header = "Цена";
                        e.Column.CellStyle = (Style)dataGrid.FindResource("NumericColumns");
                        break;
                    }
            }
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            searchComboBox.SelectedItem = null;
            searchTextBox.Text = string.Empty;
            _currentPage = 0;
            FilterDataGrid();
            _isSearchAccomplished = false;
        }

        private void FilterDataGrid()
        {
            if (columnFilterComboBox.SelectedItem != null)
            {
                if (stringValueFilterComboBox.Visibility == Visibility.Visible)
                {
                        if (stringValueFilterComboBox.SelectedItem == null) return;

                    _isFiltered = true;
                    switch (((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString())
                        {
                            case "Марка":
                            {
                                using (var context = new AutoServiceEntities())
                                {
                                    var query =
                                        context.AutoOrder.Where(
                                            x =>
                                                x.CarBrand == stringValueFilterComboBox.SelectedItem.ToString());
                                    FillDataGrid(query);
                                }
                                break;
                            }
                            case "Модель":
                            {
                                using (var context = new AutoServiceEntities())
                                {
                                    var query =
                                        context.AutoOrder.Where(
                                            x =>
                                                x.CarModel == stringValueFilterComboBox.SelectedItem.ToString());
                                    FillDataGrid(query);
                                }
                                break;
                            }
                            case "Тип трансмиссии":

                            {
                                using (var context = new AutoServiceEntities())

                                {
                                    var query =
                                        context.AutoOrder.Where(
                                            x =>
                                                x.TransmissionType == stringValueFilterComboBox.SelectedItem.ToString());
                                    FillDataGrid(query);
                                }
                                break;
                            }
                        }
                    
                }
                else
                {
                    if (numericValueFilterComboBox.SelectedItem == null || numericValueTextBox.Text == string.Empty)
                        return;
                    if (((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString() == "Время начала"
                        || ((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString() == "Время окончания")
                    {
                        DateTime date;
                        if (!DateTime.TryParse(numericValueTextBox.Text, out date))
                            return;

                        _isFiltered = true;
                        if (((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString() == "Время начала")
                        {
                            switch (((ComboBoxItem) numericValueFilterComboBox.SelectedItem).Content.ToString())
                            {
                                case ">=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
                                                x =>
                                                    (x.WorksStart > date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                                case "<":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.AutoOrder.Where(
                                                x =>
                                                    (x.WorksStart < date) || (x.WorksStart == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            switch (((ComboBoxItem) numericValueFilterComboBox.SelectedItem).Content.ToString())
                            {
                                case ">=":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
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
                                            context.AutoOrder.Where(
                                                x =>
                                                    (x.WorksFinish > date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                                case "<":
                                {
                                    using (var context = new AutoServiceEntities())

                                    {
                                        var query =
                                            context.AutoOrder.Where(
                                                x =>
                                                    (x.WorksFinish < date) || (x.WorksFinish == null));
                                        FillDataGrid(query);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        int value;
                        if (!int.TryParse(numericValueTextBox.Text, out value))
                            return;

                        
                        if (((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString() == "Мощность двигателя")
                        {
                            _isFiltered = true;
                            switch (((ComboBoxItem)numericValueFilterComboBox.SelectedItem).Content.ToString())
                            {
                                case ">=":
                                    {
                                        using (var context = new AutoServiceEntities())

                                        {
                                            var query =
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
                                                    x =>
                                                        (x.EnginePower < value) || (x.EnginePower == null));
                                            FillDataGrid(query);
                                        }
                                        break;
                                    }
                            }
                            return;
                        }
                        if (((ComboBoxItem) columnFilterComboBox.SelectedItem).Content.ToString() == "Год выпуска")
                        {
                            if (value < 1930 || value > Convert.ToInt16(DateTime.Now.Date.Year))
                                return;
                            _isFiltered = true;
                            switch (((ComboBoxItem)numericValueFilterComboBox.SelectedItem).Content.ToString())
                            {
                                case ">=":
                                    {
                                        using (var context = new AutoServiceEntities())

                                        {
                                            var query =
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
                                                    x =>
                                                        x.YearMade < value);
                                            FillDataGrid(query);
                                        }
                                        break;
                                    }
                            }
                            return;
                        }
                        if (((ComboBoxItem)columnFilterComboBox.SelectedItem).Content.ToString() == "Цена")
                        {
                            _isFiltered = true;
                            switch (((ComboBoxItem)numericValueFilterComboBox.SelectedItem).Content.ToString())
                            {
                                case ">=":
                                    {
                                        using (var context = new AutoServiceEntities())

                                        {
                                            var query =
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
                                                context.AutoOrder.Where(
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
            }
        }

        private void FillDataGrid(IQueryable<AutoOrder> query)
        {
            var totalAmountOfRows = query.Count();
            _numberOfPages = totalAmountOfRows%_rowCount != 0
                ? totalAmountOfRows/_rowCount + 1
                : totalAmountOfRows/_rowCount;
            dataGrid.ItemsSource = totalAmountOfRows < _rowCount
                ? query.OrderBy(x => x.OrderID).Skip(_currentPage*_rowCount).ToList()
                : query.OrderBy(x => x.OrderID)
                    .Skip(_currentPage*_rowCount)
                    .Take(_rowCount)
                    .ToList();
            pageNumberTextBlock.Text = "Страница " + (_currentPage + 1) + " из " +
                                       _numberOfPages;
        }

        private void columnFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numericValueTextBox.Text = string.Empty;
            numericValueFilterComboBox.SelectedItem = null;
            stringValueFilterComboBox.SelectedItem = null;
            if(columnFilterComboBox.SelectedItem != null)
                if (((ComboBoxItem)columnFilterComboBox.SelectedItem).Content.ToString() == "Марка" ||
                    ((ComboBoxItem)columnFilterComboBox.SelectedItem).Content.ToString() == "Модель" ||
                    ((ComboBoxItem)columnFilterComboBox.SelectedItem).Content.ToString() == "Тип трансмиссии")
                {
                    numericValueFilterComboBox.Visibility = Visibility.Hidden;
                    numericValueTextBox.Visibility = Visibility.Hidden;
                    stringValueFilterComboBox.Visibility = Visibility.Visible;
                    var context = new AutoServiceEntities();
                    switch (((ComboBoxItem)columnFilterComboBox.SelectedItem).Content.ToString())
                    {
                        case "Марка":
                            stringValueFilterComboBox.ItemsSource =
                                context.AutoOrder.Select(x=>x.CarBrand).Distinct().ToList();
                            break;
                        case "Модель":
                            stringValueFilterComboBox.ItemsSource =
                                context.AutoOrder.Select(x => x.CarModel).Distinct().ToList();
                            break;
                        case "Тип трансмиссии":
                            stringValueFilterComboBox.ItemsSource =
                                context.AutoOrder.Select(x => x.TransmissionType).Distinct().ToList();
                            break;
                    }
                }
                else
                {
                    numericValueFilterComboBox.Visibility = Visibility.Visible;
                    numericValueTextBox.Visibility = Visibility.Visible;
                    stringValueFilterComboBox.Visibility = Visibility.Hidden;
                }
        }

        private void resetButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (_isFiltered)
            {
                _isFiltered = false;
                FillDataGrid();
                return;
            }
            if (_isSearchAccomplished)
            {
                _isSearchAccomplished = false;
                FillDataGrid();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            columnFilterComboBox.SelectedItem = null;
            numericValueTextBox.Text = string.Empty;
            numericValueFilterComboBox.SelectedItem = null;
            stringValueFilterComboBox.SelectedItem = null;
            if (searchComboBox.SelectedItem == null || searchTextBox.Text == string.Empty)
                return;
            _currentPage = 0;
            SearchThroughDataGrid();
            _isSearchAccomplished = true;
        }

        private void SearchThroughDataGrid()
        {
            switch (((ComboBoxItem) searchComboBox.SelectedItem).Content.ToString())
            {
                case "Марка":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.AutoOrder.Where(x => x.CarBrand.Contains(searchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
                case "Модель":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.AutoOrder.Where(x => x.CarModel.Contains(searchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
                case "Наименование работ":
                    using (var context = new AutoServiceEntities())
                    {
                        var query = context.AutoOrder.Where(x => x.WorksName.Contains(searchTextBox.Text));
                        FillDataGrid(query);
                    }
                    break;
            }
        }

        private void searchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchComboBox.SelectedItem == null)
                return;
            if (((ComboBoxItem)searchComboBox.SelectedItem).Content.ToString() == "Наименование работ")
                searchComboBox.FontSize = 10;
            else
                searchComboBox.FontSize = 12;
            
        }
    }
}
