   M S B A M L   `   `   `  ����M  IAutoServiceManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AutoServiceManager.ViewModelsAutoServiceManager.ViewModels  5   	   #    AutoServiceManager.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   B
viewModels+clr-namespace:AutoServiceManager.ViewModels     5   	   -   5          �Name$"  AutoServiceManagerMainWindow��5   	    � Title$6 0АРМ Менеджера Автосервиса��5   	   $	��480��6+   $	��800��68   $	��480��6D   $	��800��6T    � DataContext 5	      1   +AutoServiceManager.ViewModels.MainViewModel  5
   
    J� InputBindings 5      d� 5   
    d� MouseAction$ 	LeftClicke�6    �� Command 6/   �� *WindowClickCommand+ � 	Resources 5         NumericCells �� 
TargetType �� Property	 �� Value  StringCells  FocusedTextBox
 �� Triggers �� Binding �� Value �� RelativeSource��%  &	        &	 �     &	 3    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$	 Right=�6.   �� 5       � ��$	 Center=�62   5      �� 5   
   " ��6$   .�5      ��  �@��TextAlignment$	 Center=�6.   6?   �� 5       � ��$	 Center=�62   6C   �� 5       � ��$
	 Wrap=�6-   5      �� 5   
   " ��6'   
 5      �� 5       6   �� *TextBoxIsFocused+ True6C   .a�5      ��  �@+�FocusedElement	 5      ��  �� *Self+5      5       5!      .��5"      � -   5         �Name$ MainGrid��5"      |�.   � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   �� 5&      $��* �6   �� 5'      $
��Auto �6    � ColumnDefinitions 5)   
   �� 5*      $
��Auto �6   �� 5+      $��* �6   �� 5,      $
��Auto �6   .+�5.   
   �� -   5         ��Name$ FiltersTabControl��5.      $��0��60   $��0��6=   $��3��6M   $��73��6a   $��0,5,5,0q�6m   $	��Top=�6~   ���	#FFACACAC.@�5/      �� $��Сортировка��6   .��50      � 
������� 51      �� 52      $��* �6+   67   �� 53      $
��Auto �6+   6:   �� 54      $
��Auto �6+   6:   �� 55      $
��Auto �6+   6:   .+�57      �� -   5         ��Name$ SortForTextBlock��57       $��0��6:   $��Right=�6J   $
��Wrap=�6f   $"��Сортировать по:��6z   $��Center=�6�   $��0,0,5,0q�6�   �� 58      -   5         ��Name$ SortComboBox��58      $��1��65   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6�   ��6E   �� *SortComboBoxItems+ �� SelectedItem 6o   �� *SortComboBoxSelectedItem+6�   �� 59      -   5        $ SortDirectionComboBox��59      $��2��6>   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6�   ��6N   �� *SortDirectionComboBoxItems+ 6�   �� *#!SortDirectionComboBoxSelectedItem+6�   �� 5:      -   5         ��Name$ 
SortButton��5:      $��3��61   $��Сортировать��6A    � Cursor$
 Hand��6|   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6�   ��6W   �� *SortClickCommand+6�   5;      5<      �� 5=      $��Фильтрация��6   .��5>      � 
������� 5?      �� 5@      $��* �6+   67   �� 5A      $
��Auto �6+   6:   �� 5B      $
��Auto �6+   6:   �� 5C      $
��Auto �6+   6:   .+�5E      �� -   5        $ FilterTextBlock��5E       $��0��69   $
��Wrap=�6I   $"��Фильтровать по:��6]   $��Center=�6t   $��Right=�6�   $��0,0,5,0q�6�   �� 5F      -	   5        $ ColumnFilterComboBox��5F      $��1��6=   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6�   ��6M   �� *ColumnFilterComboBoxItems+ 6   �� *" ColumnFilterComboBoxSelectedItem+6�   �� 5G      -
   5        $ StringValueFilterComboBox��5G      $��2��6B   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6  ��6R   �� *!StringColumnFilterComboBoxItems+ 6�   �� *(&StringColumnFilterComboBoxSelectedItem+6  �� 5H      -   5         ��Name$ NumericValueTextBox��5H      $��2��6;    �� TextWrapping$
 Wrap=�6�   $��Center=�6�   $��Right=�6�   $��0,0,5,0q�6�   $��24��6  $	��100��6  y�6K   �� * NumericFilterTextBoxVisibility+��6�   �� *
FilterText+ �� Mode$ TwoWay=�6  �� 5I      -   5        $  NumericValueFilterComboBox��5I      $��2��6C   $��Center=�6  $
��Left=�6  $��0,0,0,0q�6:  $��50��6K  $��24��6V  y�6S   �� *!NumericFilterComboBoxVisibility+��6�   �� *" NumericColumnFilterComboBoxItems+ 6�   �� *)'NumericColumnFilterComboBoxSelectedItem+6V  �� 5J      -   5        $ FilterButton��5J      $��3��63   $��Фильтровать��6C   $��0,0,5,0q�6�   $��Center=�6�   $��24��6�   $
 Hand��6�   $	��150��6�   ��6Y   �� *FilterClickCommand+6�   5L      5M      �� 5N      $��
Поиск��6   .��5O      � 
������� 5P      �� 5Q      $��* �6+   67   �� 5R      $
��Auto �6+   6:   �� 5S      $
��Auto �6+   6:   �� 5T      $
��Auto �6+   6:   .+�5V      �� -   5        $ SearchForTextBlock��5V       $��0��6<   $
��Wrap=�6L   $��Поиск по:��6`   $��Center=�6q   $��Right=�6�   $��0,0,5,0q�6�   �� 5W      -   5        $ SearchComboBox��5W      $��1��67   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��24��6�   ��6G   �� *SearchComboBoxItems+ 6s   �� *SearchComboBoxSelectedItem+6�   �� 5X      -   5        $ SearchTextBox��5X      $��2��65   $
 Wrap=�6o   $��Center=�6�   $��0,0,5,0q�6�   $��24��6�   $	��150��6�   ��6E   �� *
SearchText+$ TwoWay=�6�   �� 5Y      -   5        $ SearchButton��5Y      $��3��63   $��Искать��6C   $
 Hand��6|   $��Center=�6�   $��0,0,5,0q�6�   $��24��6�   $	��150��6�   ��6T   �� *SearchClickCommand+6�   5Z      5[      5\      �� 5]   
   $��1��6   $��0��6"    �� Orientation$ 
Horizontal=�62   $��5q�6K   .+�5^      �� -   5        $ ResetButton��5^      $%��Сбросить фильтры��6*   $	��Top=�6k   $��32��6�   $
 Hand��6�   $
��Left=�6�   $	��117��6�   ��6E   �� *ResetClickCommand+6�   �� 5_      -   5        $ StatisticsButton��5_      $��Статистика��6`   $��5,0,0,0q�6u   $	��Top=�6�   $��32��6�   $
 Hand��6�   $
��Left=�6�   $	��117��6�   ��6/   �� *StatisticsButtonClickCommand+6�   5`      &   System.Windows.Controls.DataGrid  5a   
   -   5          Name$ OrdersInfoDataGrid��5a      $��2��6/   $��0��6<   $��3��6L      RowHeaderWidth$ 0Q�6�      CanUserReorderColumns .     CanUserResizeColumns .    CanUserAddRows .      CanUserResizeRows  .  !   CanUserSortColumns! .  "   HeadersVisibility$" Column=�6$  #   ColumnHeaderHeight$# 40Q�6?  $   AutoGenerateColumns$ .  $��0,1,0.333,26.667q�6s  $��2��6�  ��6`   �� *CustomerOrdersList+%   ColumnHeaderStyle% 5b      �� 5c      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5d      ��  � ��	 5e      �� 5f      .R�5g   "   �� $
��Wrap=�6,   & �� TextAlignment$& Center=�6Q   ��6@   �� 6j   5h      5j      �� 5k       � ��$	 Center=�6C   5l      '   RowStyle' 5n      �� 5o      )  #System.Windows.Controls.DataGridRow"  6   .�5p      ��  �@�ToolTip	 5q      �� 5r      .+�5s   "   �� $��Владелец:��6,   $
��Bold"�6?   $& Center=�6Q   �� 5t   "   ��6,   �� *	Surname+( �� StringFormat$( Фамилия: {0}���� 5u   "   ��6,   �� *Name+$( Имя: {0}���� 5v   "   ��6,   �� *
Patronymic+$( Отчество: {0}��) �� TargetNullValue$*) $Отчество не указано���� 5w   "   ��6,   �� *
Birthday+$/( )Дата рождения: {0:dd.MM.yyyy}��$3) -Дата рождения не указана���� 5x   "   ��6,   �� *Phone+$( Телефон: {0}��5y      5{      �� 5|       �@_�	StaysOpen	 .  6:   �� 5}       �@^�ShowDuration$
	 5000��6D   �� 5~       �@^�InitialShowDelay$
	 2000��6H   �� 5       �@^�BetweenShowDelay$
	 2000��6H   �� 5�       �@^�	Placement$	 
MousePoint=�6A   �� 5�       �@^�VerticalOffset$	 20��6F   5�      *   Columns* 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn+   Header$+ Номер заказа��6%   ,   Width5  /System.Windows.Controls.DataGridLengthConverter$, 48 6;   1  +System.Windows.Controls.DataGridBoundColumn-   ElementStyle#- [  6m   .   Binding. 6F   �� / �� Path$/ Order.OrderID�6m     5�      $+ 
Марка��6%   #- [ 6\   . 64   �� $/ Order.CarBrand�6\     5�      $+ Модель��6%   #- [ 6]   . 65   �� $/ Order.CarModel�6]     5�      $)+ #Мощность двигателя��6%   $, 69 6A   #- [  6w   . 6L   �� $/ Order.EnginePower�6w     5�      $#+ Тип трансмиссии��6%   $, 81 6>   #- [ 6y   . 6I   �� $/ Order.TransmissionType�6y     5�      $+ Год выпуска��6%   $, 56 6:   #- [  6m   . 6E   �� $/ Order.YearMade�6m     5�      $)+ #Наименование работ��6%   $, * 6A   #- [ 6t   . 6K   �� $/ Order.WorksName�6t     5�      $+ Время начала��6%   $, 74 6;   #- [ 6�   . 6F   �� $/ Order.WorksStart�$( {0:dd.MM.yyyy HH:mm}��6�     5�      $#+ Время окончания��6%   $, 74 6>   #- [ 6�   . 6I   �� $/ Order.WorksFinish�$( {0:dd.MM.yyyy HH:mm}��$') !Заказ выполняется��6�     5�      $+ Цена��6%   $
, Auto 63   #- [  6j   . 6@   �� $/ Order.WorksPrice�6j   5�      �� 5�   
   $��3��6   $��0��6"   $ 
Horizontal=�62   $��5q�6K   .+�5�      �� -   5        $ RecordsNumberTextBlock��5�      $
��Left=�68   $
��Wrap=�6S   $@��:Количество заказов на странице:��6g   $��Bottom=�6�   �� 5�      -   5        $ FiveRecordsTextBlock��5�      $
��Left=�66   $��5,0,0,0q�6Q   $
��Wrap=�6b   $��5��6v   $��Bottom=�6   
����  �$��	Underline{�6�   $
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6J    6#   �� *FiveRecordsCommand+6J   5�      �� 5�      -   5        $ TwentyRecordsTextBlock��5�      $
��Left=�68   $��5,0,0,0q�6S   $
��Wrap=�6d   $��20��6x   $��Bottom=�6�   
����  �$��	Underline{�6�   $
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6L    6#   �� *TwentyRecordsCommand+6L   5�      �� 5�      -   5        $ FiftyRecordsTextBlock��5�      $
��Left=�67   $��5,0,0,0q�6R   $
��Wrap=�6c   $��50��6w   $��Bottom=�6�   
����  �$��	Underline{�6�   $
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6K    6#   �� *FiftyRecordsCommand+6K   5�      �� 5�      -   5        $ HundredRecordsTextBlock��5�      $
��Left=�69   $��5,0,0,0q�6T   $
��Wrap=�6e   $	��100��6y   $��Bottom=�6�   $��	Underline{�6�   
����  �$
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6M    6#   �� *HundredRecordsCommand+6M   5�      �� 5�      -   5        $ CustomRecordsTextBlock��5�      $
��Left=�68   $��5,0,0,0q�6S   $
��Wrap=�6d   $	��...��6x   $��Bottom=�6�   
����  �$
 Hand��6�    5�      d� 5�      $ 	LeftClicke�6Q    6#   �� *CustomRecordsClickCommand+6Q   5�      �� 5�      -   5        $ CustomRecordsTextBox��5�      $��	-10,0,0,0q�64   $
��Left=�6�   $��17��6�   $
 Wrap=�6�   $��Bottom=�6  $��36��6!  0 J� RenderTransformOrigin$0 0.5,0.5&�6,  1 J� IsHitTestVisible1 .  #��[ 6e  ��6G   �� *CustomNumberOfPages+$ TwoWay=�2 �� UpdateSourceTrigger$2 PropertyChanged=�y�6�   �� *PageTextBoxVisibility+.��6�   5�      �� 5�      
3 �� Key$3 Enter��6J    6!   �� *CustomRecordsCommand+6J   .��5�   )   5�      5�      �� 5�   
   -   5        $ PageNumberTextBlock��5�   1   $��3��6   $��1��6!   $��0,5,5,5q�6n   $��Center=�6   $
��Wrap=�6�   ��6N   �� *PageCountInfo+6�   �� 5�   
   $��3��6   $��2��6"   $ 
Horizontal=�62   $��5q�6K   .+�5�      �� -   5        $ FirstPageTextBlock��5�      $��Right=�64   $
��Wrap=�6P   $��<<Первая��6d   $��Bottom=�6z   $��	Underline{�6�   
����  �$
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6H    6#   �� *FirstPageCommand+6H   5�      �� 5�      -   5        $ PreviousPageTextBlock��5�      $��Right=�67   $��5,0,0,0q�6S   $
��Wrap=�6d   $��<Предыдущая��6x   $��Bottom=�6�   $��	Underline{�6�   
����  �$
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6K    6#   �� *PreviousPageCommand+6K   5�      �� 5�      -   5        $ NextPageTextBlock��5�      $��Right=�63   $��5,0,0,0q�6O   $
��Wrap=�6`   $��Следующая>��6t   $��Bottom=�6�   $��	Underline{�6�   
����  �$
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6G    6#   �� *NextPageCommand+6G   5�      �� 5�      -    5        $ LastPageTextBlock��5�      $��Right=�63   $��5,0,0,0q�6O   $
��Wrap=�6`   $��Последняя>>��6t   $��Bottom=�6�   $��	Underline{�6�   
����  �$
 Hand��6�   |�.  5�      d� 5�      $ 	LeftClicke�6G    6#   �� *LastPageCommand+6G   5�      5�      5�      5�      