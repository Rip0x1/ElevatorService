   M S B A M L   `   `   `  ����M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  5      $    ElevatorServiceApp.ClientsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:ElevatorServiceApp  5        N� Title$  ClientsPage��5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6   $��0 0 0 10q�6+   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $%��Добавить клиента��5         MaterialDesignRaisedButton#��[  64   $��5q�6h   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6:   $��0 0 5 0q�6F   $��Center=�6W   �� 5   "   $%��Добавить клиента��6,   $��Center=�6D   5      5      5      �� 5      -   5        $��Обновить��5      #��[  6,   $��5q�6`   ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "     $ Refresh=�6:   $��0 0 5 0q�6I   $��Center=�6Z   �� 5   "   $��Обновить��6,   $��Center=�6<   5       5!      5#      �� 5$      -   5        $��Экспорт в Excel��5$      #��[  63   $��5q�6g   ��5%      �� 5&      .R�5'      �� $ 
Horizontal=�6)   .+�5(   "     $ 	FileExcel=�6:   $��0 0 5 0q�6K   $��Center=�6\   �� 5)   "   $��Экспорт в Excel��6,   $��Center=�6C   5*      5+      5-      5.      5/        51   
   $��1��6   $��10q�6+   $��0 0 0 10q�68   .��52      �� $ 
Horizontal=�6   .+�53      �� -   5         ��Name$ SearchTextBox��53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$> 8Поиск по названию или контакту��61   $	��200��6q   $��5q�6}   6�   54      55      � 57   
   $��2��6   .+�58      &   System.Windows.Controls.DataGrid  -   5          Name$ ClientsGrid��58         AutoGenerateColumns$ False��6,      
IsReadOnly$
 True��6H   $��0 10 0 0q�6Z   	   Columns	 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *
ClientId+  5;      $'
 !Название компании��6)    6D   �� *CompanyName+  5<      $#
 Контактное лицо��6)    6B   �� *ContactPerson+  5=      $
 Телефон��6)    6:   �� *Phone+  5>      $
 Email��6)    68   �� *Email+  5?      $
 
Адрес��6)    68   �� *	Address+  5@      $
 Статус��6)    69   �� *Status+4  .System.Windows.Controls.DataGridTemplateColumn  5A         CellTemplate 5B      �� 5C      .R�5D   "   �� $ 
Horizontal=�6-   .+�5E   &   �� -   5        $ ��Редактировать��5E   -     MaterialDesignFlatButton#��[ 6E   $��5q�6�   ��5F   *   �� 5G   .   .R�5H   2   �� $ 
Horizontal=�6=   .+�5I   6     $
 Edit=�6N   $��0 0 5 0q�6Z   $��Center=�6k   �� 5J   6   $ ��Редактировать��6@   $��Center=�6U   5K   3   5L   /   5N   '   �� 5O   &   -   5        $��Удалить��5O   -   #��[ 6?   $��5q�6�   ��5P   *   �� 5Q   .   .R�5R   2   �� $ 
Horizontal=�6=   .+�5S   6     $ Delete=�6N   $��0 0 5 0q�6\   $��Center=�6m   �� 5T   6   $��Удалить��6@   $��Center=�6O   5U   3   5V   /   5X   '   5Y   #   5Z      5\      5^      5_      )	  #MaterialDesignThemes.Wpf.DialogHost	  5a   
   -   5         	 Name$ ClientDialog��5a   $   $��3��6:    	  DialogContent 5b      �� 5c      $��20q�6   $	��400��6)   .+�5d      �� -	   5         ��Name$ DialogTitle��5d       $��Новый клиент��65    $  MaterialDesignHeadline6TextBlock#��[ 6I   $��0 0 0 20q�6�   �� 5e      -
   5        $ CompanyNameTextBox��5e      $' !Название компании��6:   $
��0 10q�6m   �� 5f      -   5        $ ContactPersonTextBox��5f      $# Контактное лицо��6<   $
��0 10q�6m   �� 5g      -   5        $ PhoneTextBox��5g      $ Телефон��64   $
��0 10q�6]   �� 5h      -   5        $ EmailTextBox��5h      $ Email��64   $
��0 10q�6[   �� 5i      -   5        $ AddressTextBox��5i      $ 
Адрес��66   $
��0 10q�6]   �� 5j      -   5         ��Name$ StatusComboBox��5j      $ Статус��67   $
��0 10q�6_   .@�5k      �� $��Active��6'   �� 5l      $��Inactive��6'   5m      �� 5n      $ 
Horizontal=�6!   $��Right=�6:   $��0 20 0 0q�6V   .+�5o      �� -   5        $��Сохранить��5o   !   #��[  65   $��5q�6�   �� 5p      $��Отмена��6!   #��[ 62    	  CloseDialogCommand#��Z 6d   $��5q�6�   5q      5r      5t      5u      5v      