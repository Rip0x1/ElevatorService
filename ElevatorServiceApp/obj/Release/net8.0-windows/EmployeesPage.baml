   M S B A M L   `   `   `  ���� M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  &     ElevatorServiceApp.EmployeesPage   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ,local clr-namespace:ElevatorServiceApp    N� Title$  EmployeesPage��.��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�#  MaterialDesignThemes.Wpf.Card  $��10q�$��0 0 0 10q�.����  �� Orientation$ 
Horizontal=�.+��� -   $+��%Добавить сотрудника��   MaterialDesignRaisedButton#��[  $��5q����� .R��� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�$��0 0 5 0q�$��Center=��� $+��%Добавить сотрудника��$��Center=��� -   $��Обновить��#��[  $��5q����� .R��� $ 
Horizontal=�.+�  $ Refresh=�$��0 0 5 0q�$��Center=��� $��Обновить��$��Center=��� -   $��Экспорт в Excel��#��[  $��5q����� .R��� $ 
Horizontal=�.+�  $ 	FileExcel=�$��0 0 5 0q�$��Center=��� $��Экспорт в Excel��$��Center=�  $��1��$��10q�$��0 0 0 10q�.���� $ 
Horizontal=�.+��� -    ��Name$ SearchTextBox��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$J DПоиск по имени, фамилии или должности��$	��240��$��5q�� $��2��.+�&   System.Windows.Controls.DataGrid  -     Name$ EmployeesGrid��   AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��0 10 0 0q�	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
EmployeeId+  $
 Имя�� �� *	FirstName+  $
 Фамилия�� �� *
LastName+  $
 Должность�� �� *
Position+  $
 Телефон�� �� *Phone+  $
 Email�� �� *Email+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ 
Horizontal=�.+��� -   $ ��Редактировать��  MaterialDesignFlatButton#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 Edit=�$��0 0 5 0q�$��Center=��� $ ��Редактировать��$��Center=��� -   $��Удалить��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $ Delete=�$��0 0 5 0q�$��Center=��� $��Удалить��$��Center=�)	  #MaterialDesignThemes.Wpf.DialogHost	  -    	 Name$ EmployeeDialog��$��3�� 	  DialogContent �� $��20q�$	��400��.+��� -	    ��Name$ DialogTitle��$#��Новый сотрудник�� $  MaterialDesignHeadline6TextBlock#��[ $��0 0 0 20q��� -
   $ FirstNameTextBox��$ Имя��$
��0 10q��� -   $ LastNameTextBox��$ Фамилия��$
��0 10q��� -   $ PositionTextBox��$ Должность��$
��0 10q��� -   $ PhoneTextBox��$ Телефон��$
��0 10q��� -   $ EmailTextBox��$ Email��$
��0 10q��� $ 
Horizontal=�$��Right=�$��0 20 0 0q�.+��� -   $��Сохранить��#��[  $��5q��� $��Отмена��#��[  	  CloseDialogCommand#��Z $��5q�