   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   R NSimple Accounts Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Simple_Accounts_ManagerSimple_Accounts_Manager 5   	   (   "Simple_Accounts_Manager.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Simple_Accounts_Manager     5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow    SaveWindowPosition  .    AccentColorBrush   	GlowBrush# �   5
   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � Title$ Simple Accounts Manager��5   	   $	��450��6)   $	��950��66      RightWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� -   5        .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��20��6    $��20��6+   ��5       ��  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground�	 J� OpacityMask	 5      !� 5      
 j� Stretch$

 Fill=�6*     appbar_save !� Visual# [ 69   5      �� 5      $��4 0 0 0q�6    $��Center=�61   $
��Save��6L   5      5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5        � RowDefinitions 5"   
   �� 5#      �� 5$      $��65 �6   �� 5%      $��20 �6   .+�5(   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5(   +   $��Stretch=�6=   $��1q�6Z   $��0��6n   $��1��6|      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6   ��  � ContextMenu 5)      �� 5*      .@�5+      m� -   5        $��Delete the account��5+      6;   m� 5,      -   5        $��Edit the account��5,      69   5-      5/      �� 50   
   -   5         ��Name$ 
ExportList��50      $��Export list��6%   $��0,0,10,5q�6;   $��26��6M   $��Bottom=�6Y   $��Right=�6t   $	��100��6�   $��1��6�   $��1��6�   �� 51   
   -   5        $ PasswordGenerator��51      $��Password Generator��6,   $��	0,0,140,3q�6I   $��26��6\   $��Bottom=�6h   $��Right=�6�   $	��125��6�   $��1��6�   $��1��6�   6�   �� 52   
   -   5        $ 
AddAccount��52      $��Add an account��6%   $��	0,5,140,0q�6>   $��26��6Q   $	��Top=�6]   $��Right=�6u   $	��106��6�   $��1��6�   $��1��6�   6�   �� 53   
   -	   5        $ Backup��53      $��Backup��6!   $��0,5,10,0q�62   $��26��6D   $	��Top=�6P   $��Right=�6h   $	��100��6�   $��1��6�   $��1��6�   �� 54   
   $��2��6   $��1��6"   .@�55   
   �� .��6   $"Simple Accounts Manager by: Warix36<   56      57      58      