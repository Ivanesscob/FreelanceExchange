   M S B A M L   `   `   `  ����T  PFreelanceExchange_desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FreelanceExchange_desktopFreelanceExchange_desktop  5   	   +    %FreelanceExchange_desktop.ApplyDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f8metro,http://metro.mahapps.com/winfx/xaml/controls  5   	   3local'clr-namespace:FreelanceExchange_desktop  5   	     � Icon$  /Pics/7556851.ico��5   	    � 
ResizeMode$ NoResize=�5
   	    � Title$% Заполните данные��5   	   $	��260��6"   $	��400��6/    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �� $��20q�6   .+�5   
   �� $��Сообщение:��6   $��0,0,0,5q�6&   �� 5   
   $��60��6    �� TextWrapping$
 Wrap=�6    �� AcceptsReturn . ��6G   �� *	Message+ �� Mode$ TwoWay=��� 5   
   $(��"Предложенная цена:��6   $��0,10,0,5q�6.   �� 5   
   -   5        ��5   @   �� *Price+$ TwoWay=��� 5   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��0,15,0,0q�6J   .+�5      �� $
��ОК��6   $��75��6"   $��0,0,10,0q�6-   ��6?   �� *
YesCommand+�� 5      $��Отмена��6   $��75��6&    �� IsCancel . 61   5      5      5      