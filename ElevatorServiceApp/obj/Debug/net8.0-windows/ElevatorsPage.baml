   M S B A M L   `   `   `  ����M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  5      &     ElevatorServiceApp.ElevatorsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ElevatorServiceApp  5        N� Title$  ElevatorsPage��5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6   $��0 0 0 10q�6+   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��Добавить лифт��5         MaterialDesignRaisedButton#��[  61   $��5q�6e   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6:   $��0 0 5 0q�6F   $��Center=�6W   �� 5   "   $��Добавить лифт��6,   $��Center=�6A   5      5      5      �� 5      -   5        $��Обновить��5      #��[  6,   $��5q�6`   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "     $ Refresh=�6:   $��0 0 5 0q�6I   $��Center=�6Z   �� 5   "   $��Обновить��6,   $��Center=�6<   5       5!      5#      �� 5$      -   5        $��Экспорт в Excel��5$      #��[  63   $��5q�6g   ��5%      �� 5&      .R�5'      �� $ 
Horizontal=�6)   .+�5(   "     $ 	FileExcel=�6:   $��0 0 5 0q�6K   $��Center=�6\   �� 5)   "   $��Экспорт в Excel��6,   $��Center=�6C   5*      5+      5-      5.      5/        51   
   $��1��6   $��10q�6+   $��0 0 0 10q�68   .��52      �� $ 
Horizontal=�6   .+�53      �� -   5         ��Name$ SearchTextBox��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Поиск по ID��61   $	��200��6^   $��5q�6j   6u   �� 54      -   5         ��Name$ StatusFilterComboBox��54      $&  Фильтр по статусу��69   $	��150��6l   $��5q�6x   .@�55      �� $��Все��6#   �� 56      $��Active��6#   �� 57      $��Inactive��6#   58      59      5:      � 5<   
   $��2��6   .+�5=      &   System.Windows.Controls.DataGrid  -   5          Name$ ElevatorsGrid��5=         AutoGenerateColumns$ False��6.   	   
IsReadOnly$
	 True��6J   $��0 10 0 0q�6\   
   Columns
 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *
ElevatorId+  5@      $! Серийный номер��6)    6A   �� *SerialNumber+  5A      $ Клиент��6)    69   �� *
ClientId+  5B      $ Модель��6)    69   �� *Model+  5C      $ Статус��6)    69   �� *Status+  5D      $! Дата установки��6)    6A   �� *InstallationDate+ �� StringFormat$ 
dd.MM.yyyy��4  .System.Windows.Controls.DataGridTemplateColumn  5E         CellTemplate 5F      �� 5G      .R�5H   "   �� $ 
Horizontal=�6-   .+�5I   &   �� -   5        $ ��Редактировать��5I   -     MaterialDesignFlatButton#��[ 6E   $��5q�6�   ��5J   *   �� 5K   .   .R�5L   2   �� $ 
Horizontal=�6=   .+�5M   6     $
 Edit=�6N   $��0 0 5 0q�6Z   $��Center=�6k   �� 5N   6   $ ��Редактировать��6@   $��Center=�6U   5O   3   5P   /   5R   '   �� 5S   &   -   5        $��Удалить��5S   -   #��[ 6?   $��5q�6�   ��5T   *   �� 5U   .   .R�5V   2   �� $ 
Horizontal=�6=   .+�5W   6     $ Delete=�6N   $��0 0 5 0q�6\   $��Center=�6m   �� 5X   6   $��Удалить��6@   $��Center=�6O   5Y   3   5Z   /   5\   '   5]   #   5^      5`      5b      5c      )	  #MaterialDesignThemes.Wpf.DialogHost	  5e   
   -	   5         	 Name$ ElevatorDialog��5e   $   $��3��6<    	  DialogContent 5f      �� 5g      $��20q�6   $	��400��6)   .+�5h      �� -
   5         ��Name$ DialogTitle��5h       $��Новый лифт��65    $  MaterialDesignHeadline6TextBlock#��[ 6G   $��0 0 0 20q�6�   �� 5i      -   5        $ SerialNumberTextBox��5i      $! Серийный номер��6;   $
��0 10q�6k   �� 5j      -   5        $ ClientIdComboBox��5j      $ Клиент��69   $
��0 10q�6a    �� DisplayMemberPath$ DisplayName��6o   �� 5k      -   5        $ ModelTextBox��5k      $ Модель��64   $
��0 10q�6\   �� 5l      -   5        $ StatusComboBox��5l      $ Статус��67   $
��0 10q�6_   .@�5m      �� $��Active��6'   �� 5n      $��Inactive��6'   5o      (
  "System.Windows.Controls.DatePicker
  5p      -   5         
 Name$ InstallationDatePicker��5p   !   $! Дата установки��6A   $
��0 10q�6q   �� 5q      $ 
Horizontal=�6!   $��Right=�6:   $��0 20 0 0q�6V   .+�5r      �� -   5        $��Сохранить��5r   !   #��[  65   $��5q�6�   �� 5s      $��Отмена��6!   #��[ 62    	  CloseDialogCommand#��Z 6d   $��5q�6�   5t      5u      5w      5x      5y      