   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  1    +Icoltrans.Sigelo.Win.Vehiculos.EscoltasView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos       N� 	KeepAlive  .   � DataContext 6   0Icoltrans.Sigelo.Win.Vehiculos.EscoltasViewModel  .���  � ColumnDefinitions �� $��0.568* ��� $��0.432* �����  �� EndPoint$ 
1.002,0.52&� �� 
StartPoint$ 0.027,0.518&�.���  � Color$ 	#F9FFFFFF�� � Offset$ 0Q�� $ 	#F9FFFFFF��$ 1Q�� $ White��$	 0.5Q�.+��� -    ��Name$ TituloTextBlock��$
��Wrap=�$��Cambio de Escoltas��$	��Top=����	#FFBCBCBC$��64(�$��15,15,15.5,0q�F�  �� Stretch$
 Fill=����Ф�+xE�Ѥ�+��/	 �� StrokeLineJoin$	 Round=�$
��0.68��$��Center=�$��Right=����� $ 0.465295,0.0505441&�$ 0.465295,0.943124&�.��� $ 	#00D4D4D4��$ 0Q�� $ 	#FFD4D4D4��$	 0.5Q�� $ 	#00D4D4D4��$ 1Q��� $
��Left=�$��Center=�
 �� Orientation$
 Vertical=�$��20,0,0,0q�$��1��$	��400��.+��� $��	FUNCIONES��$��20(�$
��Bold"�$��5q�
��������� -    ��Name$ FuncionesListBox��$��0,0,0,10q�$	��390��$	��150��   RadioButtonList#��[   �� DisplayMemberPath$ Descripcion������ *FuncionesEscolta+ �� Mode$ OneWay=� �� SelectedItem �� *FuncionEscoltaSeleccionada+$ TwoWay=��� $��PERSONAL DISPONIBLE��$��20(�$
��Bold"�$��5q�
��������� $
 
Horizontal=�.+��� -    ��Name$ EscoltasComboBox��$	��360�� w� TextPath$ Descripcion��  ListBoxItemStyle#��[   ComboBoxStyle#��[ ���� *EscoltasDisponibles+$ OneWay=� �� *EscoltaSeleccionado+$ TwoWay=����� .R��� $	��0,4q��� ��� *Nombre+ �� $�-�� �� $
��Bold"���� *Cedula+�� -    ��Name$ AgregarButton��  CircularButtonStyle#��[ $��30��$��30��$��+��$��Arial Black)�$��29.333(����	#FF48CE2E
����������� *AdicionarCommand+�� $��Center=�$��Center=�$
��30,0q�.+��� -   $ CaravanaTextBlock��$��WordEllipsis=�$��20(�$
��Bold"�$��5q�
�������$��Center=����� *NombreCaravana+$ OneWay=��� -   $ EscoltasCaravanaListBox��$	��400��  ListBoxItemStyle2#��[ ��Z� ���� *EscoltasCaravana+$ OneWay=����� .R�� $	��0,5q� �� �� $��35 �.+��� $��14(�
�������$��WordEllipsis=��� ��� *Escolta.Nombre+ �� $�(��$
��Bold"� �� $
��Bold"���� *Funcion.Descripcion+ �� $�)��$
��Bold"��� $��1��  RemoveButtonStyle#��[ $��Right=�$��20��$��20��$��r��$��Black"�$��11(����	#FFCE532E$��Webdings)�
����������� *RetirarCommand+$ OneWay=����� *Escolta.Cedula+� $��	0,0,25,25q�$��50��$��Bottom=�$��1��$��Center=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $ CancelarButton��$��1��  FilterButtonStyle#��[ ���	#FFB42727$	��104������ *CancelCommand+.���� $��Center=� �� TextAlignment$ Center=�
�������$��18(�$��Cancelar��$
��Bold"��� $
 Fill=�$��Center=�$��40��$��Center=�$��1��$��2������ $ 0.500095,-0.00311067&�$ 0.500095,0.994881&�.��� $ 	#00FFFFFF��$ 
0.00436681Q�� $ 	#CBBABABA��$ 0.49345Q�� $ 	#00FFFFFF��$ 1Q��� -   $ GuardarButton��$��3��#��[ ���	#FF499B2C$	��104��$��3,3,0,3q����� *GrabarCommand+.���� $��Center=�$ Center=�
�������$��18(�$��Guardar��$
��Bold"�