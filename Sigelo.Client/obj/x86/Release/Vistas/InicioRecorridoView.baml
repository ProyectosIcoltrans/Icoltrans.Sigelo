   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  8    2Icoltrans.Sigelo.Win.Vehiculos.InicioRecorridoView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� 	KeepAlive  .   � DataContext =   7Icoltrans.Sigelo.Win.Vehiculos.InicioRecorridoViewModel  .��� -    �Name$ 
LayoutRoot�� � ColumnDefinitions �� $��0.568* ��� $��0.432* �����  �� 
StartPoint$ 0.027,0.518&� �� EndPoint$ 
1.002,0.52&�.���  � Offset$ 0Q� � Color$ 	#F9FFFFFF��� $ 1Q�$ 	#F9FFFFFF��� $	 0.5Q�$ White��.+��� $��2��$��15,15,15.5,0q�$	��Top=�$��64(����	#FFBCBCBC$��Inicio de recorrido��$
��Wrap=�F� $
��0.68��$��Right=�$��Center=����Ф�+xE�Ѥ�+��/ �� Stretch$
 Fill=�	 �� StrokeLineJoin$	 Round=����� $ 0.465295,0.0505441&�$ 0.465295,0.943124&�.��� $ 0Q�$ 	#00D4D4D4��� $	 0.5Q�$ 	#FFD4D4D4��� $ 1Q�$ 	#00D4D4D4���� $��1��$	��400��$��20,0,0,0q�$
��Left=�$��Center=�
 �� Orientation$
 Vertical=�.+��� $��5q�$��20(�$
��Bold"�
�������$��CARAVANA���� -    ��Name$ CaravanaTextBlock��$��10,0,0,0q�$��20(�$
��Bold"�$��WordEllipsis=�����  �� ElementName$ CaravanasComboBox�� �� Path$! SelectedItem.fvcDescripcion��� $��5q�$��20(�$
��Bold"�
�������$
��RUTA���� -   $ RutaTextBlock��$��10,0,0,0q�$��20(�$
��Bold"�$��WordEllipsis=����� $ CaravanasComboBox��$ SelectedItem.Ruta��� $��5q�$��20(�$
��Bold"�
�������$
��TIPO���� -   $ TipoTextBlock��$��10,0,0,0q�$��20(�$
��Bold"�$��WordEllipsis=����� $ CaravanasComboBox��$ SelectedItem.TipoCaravana��� $	��500��$
��30,0q�$��Center=�$��Center=�.+��� $��5q�$
��Left=�$	��Top=�$��20(�$
��Bold"�
�������$��CARAVANAS DISPONIBLES��$��WordEllipsis=��� -    ��Name$ CaravanasComboBox��$	��Top=�   ListBoxItemStyle#��[    ComboBoxStyle#��[  w� TextPath$ Descripcion������ $ CaravanasDisponibles� �� Mode$ OneWay=� �� SelectedItem �� $ CaravanaSeleccionada�$ TwoWay=����� .R��� $	��0,4q��� $
��Bold"���� *fvcDescripcion+ �� $�-�� �� ��� *Ruta+� $��1��$��50��$��	0,0,25,25q�$��Center=�$��Bottom=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ CancelarButton��$��1��$	��104�����	#FFB42727  FilterButtonStyle#��[ ���� $ CancelCommand�.���� $��Center=�$��18(�$
��Bold"�
�������$��Cancelar�� �� TextAlignment$ Center=��� $��2��$��1��$��40��$��Center=�$��Center=�$
 Fill=����� $ 0.500095,-0.00311067&�$ 0.500095,0.994881&�.��� $ 
0.00436681Q�$ 	#00FFFFFF��� $ 0.49345Q�$ 	#CBBABABA��� $ 1Q�$ 	#00FFFFFF���� -   $ GuardarButton��$��3��$	��104��$��3,3,0,3q����	#FF499B2C#��[ ���� $ GrabarCommand�$ OneWay=�{��� $ PermitirGrabar�.���� $��Center=�$��18(�$
��Bold"�
�������$��Guardar��$ Center=�