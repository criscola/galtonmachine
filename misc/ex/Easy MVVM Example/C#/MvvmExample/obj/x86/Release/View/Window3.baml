   M S B A M L   `   `   `  ����   MvvmExample    MvvmExample.View.Window3   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � WindowStartupLocation$  CenterScreen=� � Title$ Window3��$	��350��$	��400��
�������.���  � RowDefinitions �� $��0.939* ��� $
��Auto �.+��� $��POCO ViewModel��$��Center=�$��Center=�.���  �� �� $
��Auto �.+��� $
��Auto=�.���� .+���  �� Orientation$ 
Horizontal=�.+���  �� DisplayMemberPath$ 	FirstName��$
��Left=����� *People+ �� SelectedItem �� *SelectedPerson+&   System.Windows.Controls.DataGrid  $
��Left=�$��10,0,0,0q����� *People+ �� *SelectedPerson+�� $ 	FirstName��$��5,0,0,5q�$	��Top=����� *People+ �� *SelectedPerson+�� -    ��Name$	 tb1��$��5q����� *TextProperty1+ �� UpdateSourceTrigger$ PropertyChanged=��� $
��Bold"�$
��Wrap=�$��5q�$��The TextBox says '���� ��� *TextProperty1+�� $*�$', but it should be showing the time���� $��1��$��
Add Person��$��5q�|�.  ���� *AddUserCommand+���� *Text+ �� ElementName$	 tb1���� $��Next example��$	��Top=�$��Right=�$
��Bold"�
���  ��$��1��$��5q����� *NextExampleCommand+