   M S B A M L   `   `   `  ����   ElevatorServiceApp#    ElevatorServiceApp.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Elevator Service��$	��600��$
��1600�� � WindowStartupLocation$ CenterScreen=� � 	Resources    GlowAnimation �� TargetProperty W� From	 W� To g� Duration g� AutoReverse g� RepeatBehavior  HoverAnimation��%  &	        &	 W     �� .��W� $ Effect.Opacity� 0.3 1$ 0:0:1.5D�$
 True��$ Forever���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1 1.1$ 0:0:0.3D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1 1.1$ 0:0:0.3D�.��� 	 � ColumnDefinitions	 �� $	��300 ��� $��* �.+�#  MaterialDesignThemes.Wpf.Card    MaterialDesignPaper#���  $��5q�.���� $��15q�$��Center=�.+��� $��0 0 0 30q�$��Center=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 Elevator=�$��Center=�$��50��$��50��  MaterialDesignBody#���  �� $��Elevator Service��$��27(�$
��Bold"�$��Center=�$��0 10 0 5q�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #FFD700�� � Offset$ 0Q�� $ #FF4500��$ 1Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   ShadowDepth$ 0Q�   Color$ #FFD700��   Opacity$	 0.3Q�z���  �� CenterX$ 50Q� �� CenterY$ 15Q� � Triggers :�  :� RoutedEvent$ Loaded��.[��� #��[  :� $ 
MouseEnter��.[��� #��[ �� $)��#Управление лифтами��$��14(�#���  $��Center=� J� Opacity$	 0.8Q��� -   $��Заявки��  MaterialDesignFlatButton#��[ $��5q����� .R���  �� Orientation$ 
Horizontal=�.+�  $
 ClipboardText=�$��0 0 10 0q�$��Center=��� $��Заявки��$��16(�$
��Bold"�$��Center=��� -   $��
Лифты��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 Elevator=�$��0 0 10 0q�$��Center=��� $��
Лифты��$��16(�$
��Bold"�$��Center=��� -   $��Клиенты��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 AccountGroup=�$��0 0 10 0q�$��Center=��� $��Клиенты��$��16(�$
��Bold"�$��Center=��� -   $��Сотрудники��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 
AccountTie=�$��0 0 10 0q�$��Center=��� $��Сотрудники��$��16(�$
��Bold"�$��Center=��� -   $��Запчасти��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 Wrench=�$��0 0 10 0q�$��Center=��� $��Запчасти��$��16(�$
��Bold"�$��Center=��� -    ��Name$ ThemeToggleButton��#��[ $��5q����� .R��� $ 
Horizontal=�.+�  $
 ThemeLightDark=�$��0 0 10 0q�$��Center=��� $��Смена темы��$��16(�$
��Bold"�$��Center=��� -   $ ExitApplication��#��[ $��0, 30, 0, 0q����� .R��� $ 
Horizontal=�.+�  $
 ExitRun=�$��0 0 10 0q�$��Center=��� $��
Выход��$��16(�$
��Bold"�$��Center=� � -     �Name$ 	MainFrame��$��1��  � NavigationUIVisibility$ Hidden=�