   M S B A M L   `   `   `  ����T  PFreelanceExchange_desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:FreelanceExchange_desktop.Helpers.Converters,FreelanceExchange_desktop.Helpers.Converters  5      Q-clr-namespace:FreelanceExchange_desktop.PagesFreelanceExchange_desktop.Pages  5      5    /FreelanceExchange_desktop.Pages.FullRequestPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q
converters:clr-namespace:FreelanceExchange_desktop.Helpers.Converters     5      ?local-clr-namespace:FreelanceExchange_desktop.Pages     5        N� Title$  FullRequestPage��5
       � 	Resources 5      L   FFreelanceExchange_desktop.Helpers.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
   6/   5      .��5      �� .��5   
   � $��30q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5       �� $��0��6   $��0��6!   $��5��61   $	��200��5!      $	��150��6    $��10q�6-   ��5"      �� *SelectedTask.Image+�� 5$      $��0��6   $��1��6%   $��24(�5&      $
��Bold"�6&   $
��10,0q�5'      $
��Wrap=�6&   ��5%      �� *SelectedTask.Title+5'   &   �� 5)      $��1��6   $��1��6%   $
��10,5q�5+      $
��Wrap=�6&   ��5*      �� *SelectedTask.Description+5+   &   �� 5-      $��2��6   $��1��6%   $
��10,5q�5/      ��5.      �� *SelectedTask.CreatedAt+ �� StringFormat$* $Создано: {0:dd.MM.yyyy HH:mm}��5/      �� 51      $��3��6   $��1��6%   $
��10,5q�53      ��52      �� *SelectedTask.Budget+$ Бюджет: {0:C}��53      �� 55      $��6��6   $��1��6"   $��Подать заявку��56      $��	10,20,0,0q�57      $��10q�6(   $
��Left=�58      $	��150��59      ��5:      �� *EnableCommand+y�5;      �� *IsFreelancer+   BooleanToVisibilityConverter �� 	Converter# [ 5<      5=      5>      