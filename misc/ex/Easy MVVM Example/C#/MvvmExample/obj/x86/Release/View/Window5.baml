   M S B A M L   `   `   `  ���� F  BMvvmExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MvvmExample.HelpersMvvmExample.Helpers      MvvmExample.View.Window5   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5helpers!clr-namespace:MvvmExample.Helpers       � WindowStartupLocation$  CenterScreen=�
������� � Title$ Window5��$	��350��$	��500�� � 	Resources 3   -MvvmExample.Helpers.SelectedItemToItemsSource   SelectedItemToItemsSource  UserGrid �� CornerRadius � RowDefinitions � ColumnDefinitions �� BindingGroupName �� UpdateSourceTrigger �� Orientation	 �� RelativeSource
 �� AncestorType��%�  &	        &	        �� .R��� 
�������
���   �$��1q�$ 5��._�� $��5q� �� �� �� ��  �� �� .+��� $��
First name���� $��	Last name��$��1���� $	��Age��$��2���� $��1������ *	FirstName+$ Group1��$ Explicit=��� $��1��$��1������ *
LastName+$ Group1��$ Explicit=��� $��1��$��2������ *Age+$ Group1��$ Explicit=��� $ 
Horizontal=�$��3��$��2��$��Right=�.+��� 
���  ��$��Cancel��$	��4,0q����� *DataContext.CancelCommand+	 �� "
 ���� 
���  ��$��Delete��$	��4,0q����� *DataContext.DeleteUserCommand+	 �� "
 ���� 
��� � �$
��Save��$	��4,0q����� *DataContext.SaveCommand+	 �� "
 ��.���  �� $��0.939* ��� $
��Auto �.+��� $#��Business Object via ViewModel��$
��Left=�$	��Top=�.���  �� �� $
��Auto �.+��� $
��Auto=�.���� .+���  �� Padding$ 5q�$
��Bold"�$��18(����� *
BoStatus+�� $ 
Horizontal=�$	��0,5q�.+�&   System.Windows.Controls.DataGrid  -     Name$	 dg1��   CanUserAddRows .     CanUserDeleteRows .     SelectionMode$ Single=�$	��Top=����� *People+ �� SelectedItem �� *SelectedPerson+ �� SelectedIndex �� *SelectedIndex+�� #��[ $	��Top=�$��10,0,0,0q� � BindingGroup �� *UpdateBindingGroup+ �� Mode$ OneWay=����� *SelectedPerson+ �� 	Converter# [  �� -    ��Name$	 tb1��$��5q�$
��Left=�$	��200������ *ReportTitle+$ PropertyChanged=��� $
��Bold"�$
��Wrap=�$��5q�$��The TextBox says '���� ��� *ReportTitle+�� $�'���� $��1��$��
Add person��$��5q�|�.  $
��Left=�$	��200������ *AddUserCommand+