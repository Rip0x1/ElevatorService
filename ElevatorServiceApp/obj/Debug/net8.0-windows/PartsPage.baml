   M S B A M L   `   `   `  ����M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  5      "    ElevatorServiceApp.PartsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ElevatorServiceApp  5        N� Title$  	PartsPage��5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6   $��0 0 0 10q�6+   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $'��!Добавить запчасть��5         MaterialDesignRaisedButton#��[  65   $��5q�6i   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6:   $��0 0 5 0q�6F   $��Center=�6W   �� 5   "   $'��!Добавить запчасть��6,   $��Center=�6E   5      5      5      �� 5      -   5        $��Обновить��5      #��[  6,   $��5q�6`   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "     $ Refresh=�6:   $��0 0 5 0q�6I   $��Center=�6Z   �� 5   "   $��Обновить��6,   $��Center=�6<   5       5!      5#      �� 5$      -   5        $��Экспорт в Excel��5$      #��[  63   $��5q�6g   ��5%      �� 5&      .R�5'      �� $ 
Horizontal=�6)   .+�5(   "     $ 	FileExcel=�6:   $��0 0 5 0q�6K   $��Center=�6\   �� 5)   "   $��Экспорт в Excel��6,   $��Center=�6C   5*      5+      5-      5.      5/        51   
   $��1��6   $��10q�6+   $��0 0 0 10q�68   .��52      �� $ 
Horizontal=�6   $
��Left=�62   .+�53      �� -   5         ��Name$ SearchTextBox��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$: 4Поиск по названию или номеру��61   $	��200��6o   $��5q�6{   6�   �� 54      -   5        $ MinQuantityTextBox��54      $" Мин. количество��66   $	��120��6g   $��5q�6s   6~   �� 55      -   5        $ MinPriceTextBox��55      $ Мин. цена��63   $	��120��6^   $��5q�6j   6u   �� 56      -   5        $ MaxPriceTextBox��56      $ Макс. цена��63   $	��120��6_   $��5q�6k   6v   57      58      � 5:   
   $��2��6   .+�5;      &   System.Windows.Controls.DataGrid  -   5          Name$ 	PartsGrid��5;         AutoGenerateColumns$ False��6*      
IsReadOnly$
 True��6F   $��0 10 0 0q�6X   	   Columns	 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *PartId+  5>      $
 Название��6)    6;   �� *
PartName+  5?      $
 Номер детали��6)    6?   �� *
PartNumber+  5@      $,
 &Количество на складе��6)    6G   �� *QuantityInStock+  5A      $
 Цена��6)    67   �� *Price+ �� StringFormat$ C��4  .System.Windows.Controls.DataGridTemplateColumn  5B         CellTemplate 5C      �� 5D      .R�5E   "   �� $ 
Horizontal=�6-   .+�5F   &   �� -	   5        $ ��Редактировать��5F   -     MaterialDesignFlatButton#��[ 6E   $��5q�6�   ��5G   *   �� 5H   .   .R�5I   2   �� $ 
Horizontal=�6=   .+�5J   6     $
 Edit=�6N   $��0 0 5 0q�6Z   $��Center=�6k   �� 5K   6   $ ��Редактировать��6@   $��Center=�6U   5L   3   5M   /   5O   '   �� 5P   &   -
   5        $��Удалить��5P   -   #��[ 6?   $��5q�6�   ��5Q   *   �� 5R   .   .R�5S   2   �� $ 
Horizontal=�6=   .+�5T   6     $ Delete=�6N   $��0 0 5 0q�6\   $��Center=�6m   �� 5U   6   $��Удалить��6@   $��Center=�6O   5V   3   5W   /   5Y   '   5Z   #   5[      5]      5_      5`      )	  #MaterialDesignThemes.Wpf.DialogHost	  5b   
   -   5         	 Name$ 
PartDialog��5b   $   $��3��68    	  DialogContent 5c      �� 5d      $��20q�6   $	��400��6)   .+�5e      �� -   5         ��Name$ DialogTitle��5e       $!��Новая запчасть��65    $  MaterialDesignHeadline6TextBlock#��[ 6K   $��0 0 0 20q�6�   �� 5f      -   5        $ PartNameTextBox��5f      $ Название��67   $
��0 10q�6a   �� 5g      -   5        $ PartNumberTextBox��5g      $ Номер детали��69   $
��0 10q�6g   �� 5h      -   5        $ QuantityInStockTextBox��5h      $, &Количество на складе��6>   $
��0 10q�6t   �� 5i      -   5        $ PriceTextBox��5i      $ Цена��64   $
��0 10q�6Z   �� 5j      $ 
Horizontal=�6!   $��Right=�6:   $��0 20 0 0q�6V   .+�5k      �� -   5        $��Сохранить��5k   !   #��[  65   $��5q�6�   �� 5l      $��Отмена��6!   #��[ 62    	  CloseDialogCommand#��Z 6d   $��5q�6�   5m      5n      5p      5q      5r      