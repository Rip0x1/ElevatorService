   M S B A M L   `   `   `  ����  ElevatorServiceApp#    ElevatorServiceApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Elevator Service��5   	   $	��600��6"   $
��1600��6/    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         GlowAnimation �� TargetProperty W� From	 W� To g� Duration g� AutoReverse g� RepeatBehavior  HoverAnimation��%�  &	        &	 �     �� 5   
   .��5	      W� $ Effect.Opacity�6    0.35
       16)   $ 0:0:1.5D�60   $
 True��6C   $ Forever��6V   5      �� 5   
   .��5      W� $- 'RenderTransform.(ScaleTransform.ScaleX)�6    15       1.16'   $ 0:0:0.3D�60   W� 5      $- 'RenderTransform.(ScaleTransform.ScaleY)�6    15       1.16'   $ 0:0:0.3D�60   5      5      .��5      � 	 � ColumnDefinitions	 5   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   #  MaterialDesignThemes.Wpf.Card    MaterialDesignPaper#���  6   $��5q�6Q   .��5      �� $��15q�6   $��Center=�6%   .+�5      �� $��0 0 0 30q�6   $��Center=�6/   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 Elevator=�6.   $��Center=�6>   $��50��6[   $��50��6g     MaterialDesignBody#���  6r   �� 5      $��Elevator Service��6    $��27(�68   $
��Bold"�6F   $��Center=�6X   $��0 10 0 5q�6u   ��5      �� 5       �� 
StartPoint$	 0,0&�62    �� EndPoint$	 1,1&�6C   .��5    "   �  � Color$ #FFD700��6/    � Offset$ 0Q�6?   � 5!   "   $ #FF4500��6/   $ 1Q�6?   5"       J� Effect 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%         
BlurRadius$ 10Q�6/      ShadowDepth$ 0Q�6?      Color$ #FFD700��6O      Opacity$	 0.3Q�6_   z�5'      �� 5(       �� CenterX$ 50Q�6-    �� CenterY$ 15Q�6:    � Triggers 5*      :� 5+       :� RoutedEvent$ Loaded��6+   .[�5,   "   �� #��[  62   5-      :� 5.      $ 
MouseEnter��6+   .[�5/   "   �� #��[ 62   50      52      �� 53      $)��#Управление лифтами��6    $��14(�6:   #���  6H   $��Center=�6z    J� Opacity$	 0.8Q�6�   54      �� 55      -   5        $��Заявки��55        MaterialDesignFlatButton#��[ 6*   $��5q�6z   ��56      �� 57      .R�58      ��  �� Orientation$ 
Horizontal=�6)   .+�59   "     $
 ClipboardText=�6:   $��0 0 10 0q�6O   $��Center=�6a   �� 5:   "   $��Заявки��6,   $��16(�6:   $
��Bold"�6H   $��Center=�6Z   5;      5<      5>      �� 5?      -   5        $��
Лифты��5?      #��[ 6)   $��5q�6s   ��5@      �� 5A      .R�5B      �� $ 
Horizontal=�6)   .+�5C   "     $
 Elevator=�6:   $��0 0 10 0q�6J   $��Center=�6\   �� 5D   "   $��
Лифты��6,   $��16(�69   $
��Bold"�6G   $��Center=�6Y   5E      5F      5H      �� 5I      -   5        $��Клиенты��5I      #��[ 6+   $��5q�6s   ��5J      �� 5K      .R�5L      �� $ 
Horizontal=�6)   .+�5M   "     $
 AccountGroup=�6:   $��0 0 10 0q�6N   $��Center=�6`   �� 5N   "   $��Клиенты��6,   $��16(�6;   $
��Bold"�6I   $��Center=�6[   5O      5P      5R      �� 5S      -   5        $��Сотрудники��5S      #��[ 6.   $��5q�6x   ��5T      �� 5U      .R�5V      �� $ 
Horizontal=�6)   .+�5W   "     $
 
AccountTie=�6:   $��0 0 10 0q�6L   $��Center=�6^   �� 5X   "   $��Сотрудники��6,   $��16(�6>   $
��Bold"�6L   $��Center=�6^   5Y      5Z      5\      �� 5]      -   5        $��Запчасти��5]      #��[ 6,   $��5q�6r   ��5^      �� 5_      .R�5`      �� $ 
Horizontal=�6)   .+�5a   "     $
 Wrench=�6:   $��0 0 10 0q�6H   $��Center=�6Z   �� 5b   "   $��Запчасти��6,   $��16(�6<   $
��Bold"�6J   $��Center=�6\   5c      5d      5f      �� 5g      -   5         ��Name$ ThemeToggleButton��5g      #��[ 64   $��5q�6�   ��5h      �� 5i      .R�5j      �� $ 
Horizontal=�6)   .+�5k   "     $
 ThemeLightDark=�6:   $��0 0 10 0q�6P   $��Center=�6b   �� 5l   "   $��Смена темы��6,   $��16(�6>   $
��Bold"�6L   $��Center=�6^   5m      5n      5p      �� 5q      -   5        $ ExitApplication��5q      #��[ 62   $��0, 30, 0, 0q�6�   ��5r      �� 5s      .R�5t      �� $ 
Horizontal=�6)   .+�5u   "     $
 ExitRun=�6:   $��0 0 10 0q�6I   $��Center=�6[   �� 5v   "   $��
Выход��6,   $��16(�69   $
��Bold"�6G   $��Center=�6Y   5w      5x      5z      5{      5|       � 5~   
   -   5          �Name$ 	MainFrame��5~      $��1��6#     � NavigationUIVisibility$ Hidden=�63   5      5�      