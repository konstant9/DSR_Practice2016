   M S B A M L   `   `   `  ����M  IAutoServiceManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AutoServiceManager.ViewsAutoServiceManager.Views  5   	   M+clr-namespace:AutoServiceManager.ViewModelsAutoServiceManager.ViewModels  5   	   /    )AutoServiceManager.Views.StatisticsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:AutoServiceManager.Views     5   	   B
viewModels+clr-namespace:AutoServiceManager.ViewModels     5   	     � Title$  Статистика��5	   	   $	��480��6   $	��640��6)   $	��640��65   $	��480��6D    � DataContext 5
      7  1AutoServiceManager.ViewModels.StatisticsViewModel  5   
    � 	Resources 5       �� DataType8  2AutoServiceManager.ViewModels.ColumnChartViewModel*   $AutoServiceManager.Views.ColumnChart8  2AutoServiceManager.ViewModels.CircleChartViewModel*   $AutoServiceManager.Views.CircleChart8  2AutoServiceManager.ViewModels.LinearChartViewModel*   $AutoServiceManager.Views.LinearChart��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5       $
��Auto �6   6.   �� 5!      $
��Auto �6   6.   .+�5#   
   �� -   5         ��Name$ StatisticsTextBlock��5#      $��0��61   $��0��6>   $��3��6N   $��Center=�6b    �� TextAlignment$ Center=�6   $��0,5,0,10q�6�   $V��PВыберите тип диаграммы и данные для анализа��6�   $	��Top=�6�   $��18(�6�   �� 5$   
   -   5         ��Name$ StatisticsComboBox��5$      $��1��6/   $��0��6<   $��Right=�6�   $��Center=�6�   $��0,0,5,0q�6�   $	��280��6�   ��6L   �� *ListOfStatistics+	 �� SelectedItem	 6u   �� *StatisticsSelectedItem+6�   �� 5%   
   -   5        $ DiagramSelectComboBox��5%      $��1��62   $��1��6?   $��Center=�6�   $��0,0,5,0q�6�   $	��100��6�   ��6O   �� *ListOfDiagrams+	 6v   �� *DiagramSelectedItem+6�   �� 5&   
   -   5        
 ��Name$
 ShowDiagramButton��5&      $��1��6,   $��2��69   $+��%Построить диаграмму��6I   $��12(�6�   $��Center=�6�   $��0,0,5,0q�6�   $	��150��6�   $��22��6�   ��6g   �� *ShowDiagramCommand+6�   �� 5'   
   $��2��6   $��0��6&   $��3��66   $��0,10,0,0q�6p   ��6J   �� *SelectedViewModel+6p   5(      5)      