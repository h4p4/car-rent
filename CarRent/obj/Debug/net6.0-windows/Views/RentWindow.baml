   M S B A M L   `   `   `  ����B  >CarRent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CarRent.ViewsCarRent.Views  5   	       CarRent.Views.RentWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CarRent.Views     5   	     � Title$  
RentWindow��5   	   $	��280��6   $	��500��6)    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��2��6   $��
30 15 30 0q�6)   $
��Left=�6>    �� Orientation$ 
Horizontal=�6Y   .+�5      �� .+�5      �� $5��/Ваш номер телефона (без +7):��6   $��15(�6@   �� 5      $��0 10 0 0q�6b   $��30��6t   ��6   �� *RenterPhoneNumber+ �� UpdateSourceTrigger$ PropertyChanged=�6t   �� 5      $E��?Ваше имя (при первом бронировании):��6   $��15(�6G   �� 5      $��0 10 0 0q�6[   $��30��6m   ��6   �� *
RenterName+$ PropertyChanged=�6m   5      �� 5      
�������$��1 0 0 0q�6-   $	��6 0q�6G   �� 5      .+�5      �� $$��Количество дней:��6   $��15(�64   �� 5      -   5        $��DaysCountCBox��5   �    �� MaxDropDownHeight$	 100��6l   $��15(�6�   $��0 10 0 0q�6�   $��29��6�   $
��Left=�6�    �� SelectedIndex 6   �� *SelectedDaysCount+$ PropertyChanged=���6�   �� *DaysCountItemSource+�� 5      $!��Итого к оплате:��6   $��15(�63   �� 5       $��0 10 0 0q�6   $ 
Horizontal=�6/   .+�5!      �� $��20(�6`   $��SemiBold"�6n   ��6    �� *	FullPrice+$ PropertyChanged=�6n   �� 5"      $�� руб��6    $��20(�6,   $��SemiBold"�6:   5#      5$      5%      �� 5'   
   -   5        $��RentBtn��5'   A   $��1��64   $��30q�6P   $ ��Забронировать��6\   $��20(�6t   ��6   �� *RentCarCommand+6t   �� 5(   
   -   5        $��	CancelBtn��5(   .   $��1��6   $��1��6   $��30q�6?   $��Отмена��6K   $��20(�6\   5)      5*      