   M S B A M L   `   `   `  ����T  PFreelanceExchange_desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:FreelanceExchange_desktop.Pages.Auth$FreelanceExchange_desktop.Pages.Auth  5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      7    1FreelanceExchange_desktop.Pages.Auth.Registration   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:FreelanceExchange_desktop.Pages.Auth     5      8metro,http://metro.mahapps.com/winfx/xaml/controls  5      JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5        N� Title$  Registration��5       N� 
Background 5      �� 5   
    �� ImageSource$ /Pics/background.jpeg��6    j� Stretch$
 Fill=�69   .��5      � .+�5   
   �� ���	#800000006   �� 5   
   $	��300��6   $	��434��6   ���	#CCFFFFFF �� CornerRadius$ 12��6A    �� Padding$ 
20 20 20 0q�6S   $��Center=�6h   $��Center=�6�   ���	#FFCCCCCC$��3q�5   )   ._�5      �� .+�5      �� $��Регистрация��6   $��30(�6/   $��0 0 0 10q�6=   $��Center=�6O   $��Segoe UI)�6l   �� 5      *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Введите логин��6   $��5 5 5 10q�6K   
���   ���6q   �� *NewUser.Username+ �� Mode$ TwoWay=��� 5      $! Введите пароль��6   $��5 5 5 10q�6L   
���   ���6r   �� *NewUser.Password+$ TwoWay=��� 5      $ Имя��6   $��5 5 5 10q�6A   
���   ���6g   �� *NewUser.FirstName+$ TwoWay=��� 5      $ Фамилия��6   $��5 5 5 10q�6E   
���   ���6k   �� *NewUser.LastName+$ TwoWay=��� 5      $ 
Почта��6   $��5 5 5 10q�6C   
���   ���6i   �� *NewUser.Email+$ TwoWay=�(  "System.Windows.Controls.DatePicker  5      $ Дата рождения��6   $��5 5 5 10q�6N   
���   �
�������   Text 6�   �� *NewUser.BirthDate+$ TwoWay=��� 5      $- 'Заказчик\Исполнитель��6   $��5 5 5 10q�6S   
���   ���6y   �� *Roles+	 �� SelectedItem	 6�   �� *SelectedRole+$ TwoWay=��� 5      $*��$Зарегистрироваться��6   $��5 5 5 10q�66   
���   �
���������6o   �� *
RegCommand+�� 5      $)��#Есть аккаунт? Войди��6   #��[" �6c   
����  �
������ $��0q�6�   
 � Cursor$

 Hand��6�   ��67   �� *NavigateToSignInCommand+6�   5      5      5       5!      