   M S B A M L   `   `   `  ����T  PFreelanceExchange_desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FreelanceExchange_desktop.PagesFreelanceExchange_desktop.Pages  5      k:clr-namespace:FreelanceExchange_desktop.Helpers.Converters,FreelanceExchange_desktop.Helpers.Converters  5      9    3FreelanceExchange_desktop.Pages.ResponsesForCusomer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:FreelanceExchange_desktop.Pages     5      Q
converters:clr-namespace:FreelanceExchange_desktop.Helpers.Converters     5        N� Title$  ResponsesForCusomer��5
       � 	Resources 5      N   HFreelanceExchange_desktop.Helpers.Converters.BoolToSelectedTextConverter   BoolToSelectedTextConverter��%   &	          5   
   61   5      .��5      � ���#F5F5F5.+�5   
   �� .+�5      �� $(��"Отклики на задание��6   $��28(�5      $
��Bold"�5      $��0,0,0,20q�5      �� 5      $��0,0,0,20q�6c   ��6   �� *	Responses+ �� SelectedItem 69   �� *SelectedResponse+��5      �� 5      .R�5      ��  ��Name$ 
CardBorder��6!   ���#CCC$��1q�5       �� CornerRadius$ 10��5       �� Padding$ 15q�5      $��0,0,0,10q�5      
���������5       �� 5!   "    �� 
TargetType" ��6(    �� Triggers 5"   &   �� 5#   *    �� Binding 66   �� *
IsSelected+	 �� Value	 True6U   .a�5$   .   �� 
 �� Property
 � �� �� Value
 � � �6L   �� 5%   .   
 � ��$ 2q�6P   �� 5&   .   
 � �� �#D0F0C06K   5'   +   5)   #   ._�5,      ��  �� Orientation$ Vertical=�6)   .+�5-   "   �� $ 
Horizontal=�6-   .+�5.   &   �� $��16(�6k   $��SemiBold"�6y   ��60   �� *ProposedPrice+ �� StringFormat$ Цена: {0:C}��6y   �� 5/   &   
�������$��20,0,0,0q�6�   ��60   �� *	CreatedAt+$ от {0:dd.MM.yyyy HH:mm}��6�   50   #   �� 52   "   $
��Wrap=�6E   $��	0,10,0,10q�6Y   $��14(�6l   ��6,   �� *	Message+6l   �� 53   "   $��0,5,0,0q�6�   ��6,   �� *
IsSelected+ �� 	Converter# [  6�   �� 54   "   $��Принять��6)   $��Right=�55      $	��100��56      $��30��57      ���#0078D7
�������
������ $
��Bold"�5;       � Cursor$
 Hand��5<      ��5=      �� *DataContext.ApplyCommand+ �� RelativeSource ��  �� AncestorType$
 PageL�5>      5?      5@      5B      �� 5E      $��
Назад��6   $	��100��6%    �� IsCancel . $��30��5F   $   $
��Left=�60   
�������
�������
������ 6p   5G      5H      5I      