   M S B A M L   `   `   `  ����B  >CarRent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CarRent.ViewsCarRent.Views  5   	       CarRent.Views.StartWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CarRent.Views     5   	     � Title$  Choose Role��5   	   $	��150��6   $	��350��6*   $��35(�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $
��USER��66   $��0��6E   $��10 25q�6U   ��6   �� *LogAsUserCommand+6U   �� 5   
   -   5        $��LogAsAdminBtn��5   @   $��ADMIN��6   $��1��6!   $��10 25q�61   6U   G� 5   
   -   5        $��	AdminPBox��5      $y�Hidden=�6'   $��2��6;   $��10 25q�6O   6�   �� 5   
   -   5        $��
PassTBlock��5   &   
�������$��Enter Password��68   $y�Hidden=�6N   $��2��6b   $��25 25q�6v    J� IsHitTestVisible$ False��6�   �� 5   
    �� 	IsDefault$
 True��6   $��0��6"   $��0��6-   ��67   �� *LogAsAdminCommand+��6]   ��  �� ElementName$ 	AdminPBox���� 5   
   -   5        $��HidePBoxBtn��5   6    �� IsCancel$
 True��6   $��0��6!   $��0��6,   66   5      5      