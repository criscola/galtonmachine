   M S B A M L   `   `   `  ���� F  BMvvmExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MvvmExample.HelpersMvvmExample.Helpers  =#clr-namespace:MvvmExample.ViewModelMvvmExample.ViewModel  &     MvvmExample.ViewModel.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5helpers!clr-namespace:MvvmExample.Helpers     2vm#clr-namespace:MvvmExample.ViewModel       � WindowStartupLocation$  CenterScreen=� � Title$ 
MainWindow��$	��400��$	��400��
�������   ViewModelMain � DataContext# �    � 	Resources )  #MvvmExample.ViewModel.ViewModelMain��%   &	          .���  � RowDefinitions �� $��0.939* ��� $
��Auto �.+��� $,��&Classic INotifyPropertyChanged Example��$��Center=�$��Center=�.���  �� �� $
��Auto �.+��� $
��Auto=�.���� .+���  �� Orientation$ 
Horizontal=�.+���  �� DisplayMemberPath$ 	FirstName��$
��Left=����� *People+ �� SelectedItem �� *SelectedPerson+&   System.Windows.Controls.DataGrid  $
��Left=�$��5,0,0,0q����� *People+ �� *SelectedPerson+�� $ 	FirstName��$��5,0,0,5q�$	��Top=����� *People+ �� *SelectedPerson+�� -    ��Name$	 tb1��$��5q����� *TextProperty1+	 �� UpdateSourceTrigger$	 PropertyChanged=��� $
��Bold"�$��5q�$��The TextBox says '���� ��� *TextProperty1+�� $�'���� $��1��$��
Add person��$��5q�|�.  ���� *AddUserCommand+���� *Text+
 �� ElementName$	
 tb1���� -   $��Next example��$	��Top=�$��Right=�$
��Bold"�
���  ��$��1��$��5q�