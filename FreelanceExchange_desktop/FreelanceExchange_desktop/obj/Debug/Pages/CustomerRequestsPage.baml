   M S B A M L   `   `   `  ����T  PFreelanceExchange_desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FreelanceExchange_desktop.PagesFreelanceExchange_desktop.Pages  5      :    4FreelanceExchange_desktop.Pages.CustomerRequestsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:FreelanceExchange_desktop.Pages     5        N� Title$  CustomerRequestsPage��5	      .��5      �  � RowDefinitions 5      �� 5   
   $��* �6   �� 5   
   $
��Auto �6   .+�5   
   �� -   5        $��0��5   5   ��6   �� *	UserTasks+ �� SelectedItem 5      �� *SelectedTask+��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      �� 5       �� Binding 6&   �� *
StatusId+ �� Value 26C   .a�5      ��  �� Property � �� �� Value �#D0F0C06;   5      5      ��5      �� 5      .R�5       �� 	 �� Orientation$	 
Horizontal=�6!   $��5q�6:   $��Center=�6E   .+�5!      �� $	��120��69   $��90��6E   $��5q�6Q   ��6    �� *Image+6Q   �� 5"      $��10,0,0,0q�6%   $��Center=�67   .+�5#      �� $
��Bold"�6?   $��16(�6Q   ��6(   �� *Title+6Q   �� 5$      $
��Wrap=�6E   $	��400��6Y   $��0,5,0,0q�6h   ��6(   �� *Description+6h   �� 5%      $��0,5,0,0q�6q   ��6(   �� *	CreatedAt+
 �� StringFormat$/
 )Дата создания: {0:dd.MM.yyyy}��6q   �� 5&      $��0,5,0,0q�6^   ��6(   �� *Budget+$
 Бюджет: {0:C}��6^   5'      5(      5)      5+      �� 5.   
   $��1��6   $	 
Horizontal=�6"   $��Right=�6;   $��10q�6W   .+�5/   
   �� $��Добавить��6   $	��100��6C   $��5q�6O   ��6$   �� *
AddCommand+6O   �� 50   
   $��Удалить��6   $	��100��6E   $��5q�6Q   ��6#   �� *DeleteCommand+6Q   51      52      54      