   M S B A M L   `   `   `  ���� M  IElevatorServiceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ElevatorServiceAppElevatorServiceApp  "    ElevatorServiceApp.PartsPage   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ,local clr-namespace:ElevatorServiceApp    N� Title$  	PartsPage��.��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�#  MaterialDesignThemes.Wpf.Card  $��10q�$��0 0 0 10q�.����  �� Orientation$ 
Horizontal=�.+��� -   $'��!Добавить запчасть��   MaterialDesignRaisedButton#��[  $��5q����� .R��� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�$��0 0 5 0q�$��Center=��� $'��!Добавить запчасть��$��Center=��� -   $��Обновить��#��[  $��5q����� .R��� $ 
Horizontal=�.+�  $ Refresh=�$��0 0 5 0q�$��Center=��� $��Обновить��$��Center=��� -   $��Экспорт в Excel��#��[  $��5q����� .R��� $ 
Horizontal=�.+�  $ 	FileExcel=�$��0 0 5 0q�$��Center=��� $��Экспорт в Excel��$��Center=�  $��1��$��10q�$��0 0 0 10q�.���� $ 
Horizontal=�$
��Left=�.+��� -    ��Name$ SearchTextBox��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$: 4Поиск по названию или номеру��$	��200��$��5q��� -   $ MinQuantityTextBox��$" Мин. количество��$	��120��$��5q��� -   $ MinPriceTextBox��$ Мин. цена��$	��120��$��5q��� -   $ MaxPriceTextBox��$ Макс. цена��$	��120��$��5q�� $��2��.+�&   System.Windows.Controls.DataGrid  -     Name$ 	PartsGrid��   AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��0 10 0 0q�	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PartId+  $
 Название�� �� *
PartName+  $
 Номер детали�� �� *
PartNumber+  $,
 &Количество на складе�� �� *QuantityInStock+  $
 Цена�� �� *Price+ �� StringFormat$ C��4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ 
Horizontal=�.+��� -	   $ ��Редактировать��  MaterialDesignFlatButton#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 Edit=�$��0 0 5 0q�$��Center=��� $ ��Редактировать��$��Center=��� -
   $��Удалить��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $ Delete=�$��0 0 5 0q�$��Center=��� $��Удалить��$��Center=�)	  #MaterialDesignThemes.Wpf.DialogHost	  -    	 Name$ 
PartDialog��$��3�� 	  DialogContent �� $��20q�$	��400��.+��� -    ��Name$ DialogTitle��$!��Новая запчасть�� $  MaterialDesignHeadline6TextBlock#��[ $��0 0 0 20q��� -   $ PartNameTextBox��$ Название��$
��0 10q��� -   $ PartNumberTextBox��$ Номер детали��$
��0 10q��� -   $ QuantityInStockTextBox��$, &Количество на складе��$
��0 10q��� -   $ PriceTextBox��$ Цена��$
��0 10q��� $ 
Horizontal=�$��Right=�$��0 20 0 0q�.+��� -   $��Сохранить��#��[  $��5q��� $��Отмена��#��[  	  CloseDialogCommand#��Z $��5q�