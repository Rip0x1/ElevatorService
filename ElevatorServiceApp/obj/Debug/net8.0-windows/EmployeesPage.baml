   M S B A M L   `   `   `  ����M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  5      &     ElevatorServiceApp.EmployeesPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ElevatorServiceApp  5        N� Title$  EmployeesPage��5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6   $��0 0 0 10q�6+   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $+��%Добавить сотрудника��5         MaterialDesignRaisedButton#��[  67   $��5q�6k   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6:   $��0 0 5 0q�6F   $��Center=�6W   �� 5   "   $+��%Добавить сотрудника��6,   $��Center=�6G   5      5      5      �� 5      -   5        $��Обновить��5      #��[  6,   $��5q�6`   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "     $ Refresh=�6:   $��0 0 5 0q�6I   $��Center=�6Z   �� 5   "   $��Обновить��6,   $��Center=�6<   5       5!      5#      �� 5$      -   5        $��Экспорт в Excel��5$      #��[  63   $��5q�6g   ��5%      �� 5&      .R�5'      �� $ 
Horizontal=�6)   .+�5(   "     $ 	FileExcel=�6:   $��0 0 5 0q�6K   $��Center=�6\   �� 5)   "   $��Экспорт в Excel��6,   $��Center=�6C   5*      5+      5-      5.      5/        51   
   $��1��6   $��10q�6+   $��0 0 0 10q�68   .��52      �� $ 
Horizontal=�6   .+�53      �� -   5         ��Name$ SearchTextBox��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$J DПоиск по имени, фамилии или должности��61   $	��240��6x   $��5q�6�   6�   54      55      � 57   
   $��2��6   .+�58      &   System.Windows.Controls.DataGrid  -   5          Name$ EmployeesGrid��58         AutoGenerateColumns$ False��6.      
IsReadOnly$
 True��6J   $��0 10 0 0q�6\   	   Columns	 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *
EmployeeId+  5;      $
 Имя��6)    66   �� *	FirstName+  5<      $
 Фамилия��6)    6:   �� *
LastName+  5=      $
 Должность��6)    6<   �� *
Position+  5>      $
 Телефон��6)    6:   �� *Phone+  5?      $
 Email��6)    68   �� *Email+4  .System.Windows.Controls.DataGridTemplateColumn  5@         CellTemplate 5A      �� 5B      .R�5C   "   �� $ 
Horizontal=�6-   .+�5D   &   �� -   5        $ ��Редактировать��5D   -     MaterialDesignFlatButton#��[ 6E   $��5q�6�   ��5E   *   �� 5F   .   .R�5G   2   �� $ 
Horizontal=�6=   .+�5H   6     $
 Edit=�6N   $��0 0 5 0q�6Z   $��Center=�6k   �� 5I   6   $ ��Редактировать��6@   $��Center=�6U   5J   3   5K   /   5M   '   �� 5N   &   -   5        $��Удалить��5N   -   #��[ 6?   $��5q�6�   ��5O   *   �� 5P   .   .R�5Q   2   �� $ 
Horizontal=�6=   .+�5R   6     $ Delete=�6N   $��0 0 5 0q�6\   $��Center=�6m   �� 5S   6   $��Удалить��6@   $��Center=�6O   5T   3   5U   /   5W   '   5X   #   5Y      5[      5]      5^      )	  #MaterialDesignThemes.Wpf.DialogHost	  5`   
   -   5         	 Name$ EmployeeDialog��5`   $   $��3��6<    	  DialogContent 5a      �� 5b      $��20q�6   $	��400��6)   .+�5c      �� -	   5         ��Name$ DialogTitle��5c       $#��Новый сотрудник��65    $  MaterialDesignHeadline6TextBlock#��[ 6L   $��0 0 0 20q�6�   �� 5d      -
   5        $ FirstNameTextBox��5d      $ Имя��68   $
��0 10q�6]   �� 5e      -   5        $ LastNameTextBox��5e      $ Фамилия��67   $
��0 10q�6`   �� 5f      -   5        $ PositionTextBox��5f      $ Должность��67   $
��0 10q�6b   �� 5g      -   5        $ PhoneTextBox��5g      $ Телефон��64   $
��0 10q�6]   �� 5h      -   5        $ EmailTextBox��5h      $ Email��64   $
��0 10q�6[   �� 5i      $ 
Horizontal=�6!   $��Right=�6:   $��0 20 0 0q�6V   .+�5j      �� -   5        $��Сохранить��5j   !   #��[  65   $��5q�6�   �� 5k      $��Отмена��6!   #��[ 62    	  CloseDialogCommand#��Z 6d   $��5q�6�   5l      5m      5o      5p      5q      