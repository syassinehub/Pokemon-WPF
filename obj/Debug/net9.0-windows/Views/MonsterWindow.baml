   M S B A M L   `   `   `  ����  
PokemonWpf$    PokemonWpf.Views.MonsterWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Monster List��5   	   $	��500��6   $	��800��6+   ���#FAFAFA � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   Opacity   
BlurRadius	 �� 
TargetType
 �� CornerRadius �� Padding �� Triggers P� Property P� Value)  #System.Windows.Controls.DataGridRow��%�  '��       '      �� 5   
   " ��6   .�5	      ��  � ��$ 5q�6'   �� 5
       � ��$
 10,5q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#4CAF506+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $	 0.2Q�65   $ 10Q�6C   5      �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� #��z��6!   $
 8��5   !   # z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      5!       5"      P� 5#       �@J�IsMouseOver$
 True��61   . �5$      ��  � �� �#45A04963   5%      5'      �� 5)   
   "  6   .�5*      ��  � ��
 �����6+   �� 5+       �@�ToolTip$ Click to select this row��6(    5,      P� 5-       �@ 
IsSelected$
 True��60   . �5.      ��  � �� �#FFEB3B63   �� 5/       � ��
 �   �63   50      P� 51       �@J�IsMouseOver$
 True��61   . �52      ��  � �� �#FFECB363   53      55      56      .��58      � $��20q�6    � RowDefinitions 59   
   �� 5:      $
��Auto �6   �� 5;      $��* �6   �� 5<      $
��Auto �6   .+�5?   
   �� $��Monster List��6   $��28(�5@      $
��Bold"�6"   $��Center=�5A      ���#3E4C59$��0,0,0,20q�5C      $��0��6&   �� 5E   
   $��1��6   $
 10��6   
����������#BDBDBD$��1q�5F   '   $ 5q�6;   ._�5G      &   System.Windows.Controls.DataGrid  -   5          Name$ MonsterDataGrid��5G         AutoGenerateColumns$ False��5H         
IsReadOnly$
 True��5I         SelectionMode$ Single=�5J         AlternatingRowBackground �#E3F2FD   RowBackground
 �����   EnableRowVirtualization$
 True��5M         SelectionUnit$ FullRow=�5N         Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     5Q      $
 Name��6)   $
 Auto 6P    67   �� *Name+6P     5R      $ Health��6)   $
 Auto 6T    69   �� *Health+6T   4  .System.Windows.Controls.DataGridTemplateColumn  5S      $ Image��6-   $
 Auto 6<      CellTemplate 5T      �� 5U      .R�5V   "   �� $��1q�6)   ���#E0E0E0$
 5��6S   ._�5W   &   �� $��80��6H   $��80��6S   $��UniformToFill=�6_   ��6,   �� *
ImageUrl+6_   5X   #   5Y      5[        5\      $ Spells��6)   $
 Auto 6X    69   �� *
SpellNames+6X   5^      5_      �� 5a   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��10q�6X   .+�5b      �� -   5        $��Go to Battle Page��5b      $	��180��61   6=   �� 5c      -   5        $��View Spells��5c      $	��150��6+   ���#2196F36L   5d      5e      5f      