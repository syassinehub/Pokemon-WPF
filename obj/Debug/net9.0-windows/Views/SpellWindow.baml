   M S B A M L   `   `   `  ����  
PokemonWpf"    PokemonWpf.Views.SpellWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Spell List��5   	   $	��450��6   $	��600��6)   ���#F5F5F5 � 	Resources 5       �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   Opacity   
BlurRadius �� 
TargetType	 �� CornerRadius
 �� Padding �� Triggers P� Property P� Value)  #System.Windows.Controls.DataGridRow��%�  '��       '      �� 5   
   " ��6   .�5	      ��  � ��$ 5q�6'   �� 5
       � ��$
 10,5q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#4CAF506+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $	 0.2Q�65   $ 10Q�6C   5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $	 8��5   !   #
 z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      5!       5"      P� 5#       �@J�IsMouseOver$
 True��61   . �5$      ��  � �� �#45A04963   5%      5'      �� 5)   
   "  6   .�5*      ��  � ��
 �����6+   �� 5+       �@�ToolTip$ Click to select this row��6(    5,      P� 5-       �@ 
IsSelected$
 True��60   . �5.      ��  � �� �#FFEB3B63   �� 5/       � ��
 �   �63   50      P� 51       �@J�IsMouseOver$
 True��61   . �52      ��  � �� �#FFECB363   53      55      56      .��58      � $��10q�6    � RowDefinitions 59   
   �� 5:      $
��Auto �6   �� 5;      $
��Auto �6   �� 5<      $��* �6   �� 5=      $
��Auto �6   .+�5@   
   �� $��
Spell List��6   $��24(�5A      $
��Bold"�6"   $��Center=�5B      $��10q�61   $��0��6=   �� 5D   
    �� Orientation$ 
Horizontal=�6   $��	10,5,10,5q�6.   $��1��6A   .+�5E      �� $��Filter by Monster:��6   $��Center=�62   $��0,0,10,0q�6M   �� 5F      -   5         ��Name$ MonsterComboBox��5F      $	��200��60   6<   5G      &   System.Windows.Controls.DataGrid  5I   
   -   5          Name$ SpellDataGrid��5I         AutoGenerateColumns$ False��6*   $��10,10,10,10q�5J      $��2��6(      
IsReadOnly$
 True��65      Columns 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5M      $
 Name��6%   $
 Auto 6L    63   �� *Name+6L     5N      $ Damage��6%   $
 Auto 6P    65   �� *Damage+6P     5O      $ Description��6%   $
 Auto 6Z    6:   �� *Description+6Z   5Q      �� 5S   
   -   5        $��View Monsters��5S      $��3��6)   $��Right=�66   $��10q�6R   $	��120��5T      $��30��6   6)   5U      5V      