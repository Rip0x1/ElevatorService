   M S B A M L   `   `   `  ����M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  5      ,    &ElevatorServiceApp.ServiceRequestsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ElevatorServiceApp  5        N� Title$  ServiceRequestsPage��5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6   $��0 0 0 10q�6+   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $#��Добавить заявку��5         MaterialDesignRaisedButton#��[  63   $��5q�6g   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6:   $��0 0 5 0q�6F   $��Center=�6W   �� 5   "   $#��Добавить заявку��6,   $��Center=�6C   5      5      5      �� 5      -   5        $��Обновить��5      #��[  6,   $��5q�6`   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "     $ Refresh=�6:   $��0 0 5 0q�6I   $��Center=�6Z   �� 5   "   $��Обновить��6,   $��Center=�6<   5       5!      5#      �� 5$      -   5        $��Экспорт в Excel��5$      #��[  63   $��5q�6g   ��5%      �� 5&      .R�5'      �� $ 
Horizontal=�6)   .+�5(   "     $ 	FileExcel=�6:   $��0 0 5 0q�6K   $��Center=�6\   �� 5)   "   $��Экспорт в Excel��6,   $��Center=�6C   5*      5+      5-      5.      5/        51   
   $��1��6   $��10q�6+   $��0 0 0 10q�68   .��52      �� $ 
Horizontal=�6   .+�53      �� -   5         ��Name$ SearchTextBox��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$&  Поиск по описанию��61   $	��200��6d   $��5q�6p   6{   �� 54      -   5         ��Name$ StatusFilterComboBox��54      $&  Фильтр по статусу��69   $	��150��6l   $��5q�6x   .@�55      �� $��Все��6#   �� 56      $
��Open��6#   �� 57      $��
InProgress��6#   �� 58      $��Closed��6#   59      5:      5;      � 5=   
   $��2��6   .+�5>      &   System.Windows.Controls.DataGrid  -   5          Name$ RequestsGrid��5>         AutoGenerateColumns$ False��6-   	   
IsReadOnly$
	 True��6I   $��0 10 0 0q�6[   
   Columns
 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	RequestId+  5A      $ Лифт��6)    67   �� *
ElevatorId+  5B      $ Клиент��6)    69   �� *
ClientId+  5C      $ Сотрудник��6)    6<   �� *
EmployeeId+  5D      $ Дата��6)    67   �� *RequestDate+ �� StringFormat$ dd.MM.yyyy HH:mm��  5E      $ Описание��6)    6;   �� *Description+  5F      $ Статус��6)    69   �� *Status+  5G      $ Приоритет��6)    6<   �� *
Priority+4  .System.Windows.Controls.DataGridTemplateColumn  5H         CellTemplate 5I      �� 5J      .R�5K   "   �� $ 
Horizontal=�6-   .+�5L   &   �� -   5        $ ��Редактировать��5L   -     MaterialDesignFlatButton#��[ 6E   $��5q�6�   ��5M   *   �� 5N   .   .R�5O   2   �� $ 
Horizontal=�6=   .+�5P   6     $
 Edit=�6N   $��0 0 5 0q�6Z   $��Center=�6k   �� 5Q   6   $ ��Редактировать��6@   $��Center=�6U   5R   3   5S   /   5U   '   �� 5V   &   -   5        $��Удалить��5V   -   #��[ 6?   $��5q�6�   ��5W   *   �� 5X   .   .R�5Y   2   �� $ 
Horizontal=�6=   .+�5Z   6     $ Delete=�6N   $��0 0 5 0q�6\   $��Center=�6m   �� 5[   6   $��Удалить��6@   $��Center=�6O   5\   3   5]   /   5_   '   5`   #   5a      5c      5e      5f      )	  #MaterialDesignThemes.Wpf.DialogHost	  5h   
   -	   5         	 Name$ RequestDialog��5h   $   $��3��6;    	  DialogContent 5i      �� 5j      $��20q�6   $	��400��6)   .+�5k      �� -
   5         ��Name$ DialogTitle��5k       $��Новая заявка��65    $  MaterialDesignHeadline6TextBlock#��[ 6I   $��0 0 0 20q�6�   �� 5l      -   5        $ ElevatorIdComboBox��5l      $ Лифт��6;   $
��0 10q�6a    �� DisplayMemberPath$ 
ElevatorId��6o   �� 5m      -   5        $ ClientIdComboBox��5m      $ Клиент��69   $
��0 10q�6a   $ DisplayName��6o   �� 5n      -   5        $ EmployeeIdComboBox��5n      $ Сотрудник��6;   $
��0 10q�6f   $ DisplayName��6t   (
  "System.Windows.Controls.DatePicker
  5o      -   5         
 Name$ RequestDatePicker��5o   !   $ Дата заявки��6<   $
��0 10q�6i   �� 5p      -   5        $ DescriptionTextBox��5p      $ Описание��6:    �� AcceptsReturn$
 True��6d    �� TextWrapping$
 Wrap=�6y   $	��100��6�   $
��0 10q�6�   �� 5q      -   5        $ StatusComboBox��5q      $ Статус��67   $
��0 10q�6_   .@�5r      �� $
��Open��6'   �� 5s      $��
InProgress��6'   �� 5t      $��Closed��6'   5u      �� 5v      -   5        $ PriorityComboBox��5v      $ Приоритет��69   $
��0 10q�6d   .@�5w      �� $	��Low��6'   �� 5x      $��Medium��6'   �� 5y      $
��High��6'   5z      �� 5{      $ 
Horizontal=�6!   $��Right=�6:   $��0 20 0 0q�6V   .+�5|      �� -   5        $��Сохранить��5|   !   #��[  65   $��5q�6�   �� 5}      $��Отмена��6!   #��[ 62    	  CloseDialogCommand#��Z 6d   $��5q�6�   5~      5      5�      5�      5�      