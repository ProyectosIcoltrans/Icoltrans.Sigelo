   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  5    /Icoltrans.Sigelo.Win.Vehiculos.FinRecorridoView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     >local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos       N� 	KeepAlive  .   � DataContext :   4Icoltrans.Sigelo.Win.Vehiculos.FinRecorridoViewModel  .���  � ColumnDefinitions �� $��0.568* ��� $��0.432* �����  �� EndPoint$ 
1.002,0.52&� �� 
StartPoint$ 0.027,0.518&�.���  � Color$ 	#F9FFFFFF�� � Offset$ 0Q�� $ 	#F9FFFFFF��$ 1Q�� $ White��$	 0.5Q�.+��� $
��Wrap=�$��Fin de recorrido��$	��Top=����	#FFBCBCBC$��64(�$��15,15,15.5,0q�$��2��F�  �� Stretch$
 Fill=����Ф�+xE�Ѥ�+��/ �� StrokeLineJoin$ Round=�$
��0.68��$��Center=�$��Right=����� $ 0.465295,0.0505441&�$ 0.465295,0.943124&�.��� $ 	#00D4D4D4��$ 0Q�� $ 	#FFD4D4D4��$	 0.5Q�� $ 	#00D4D4D4��$ 1Q��� $
��Left=�$��Center=�	 �� Orientation$	 Vertical=�$��20,0,0,0q�$��1��$	��400��.+��� $��CARAVANA��$��20(�$
��Bold"�$��5q�
��������� -   
 ��Name$
 CaravanaTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"�����  �� Path$ NombreCaravana� �� Mode$ OneWay=��� $
��RUTA��$��20(�$
��Bold"�$��5q�
��������� -   $
 RutaTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $
 Ruta�$ OneWay=��� $
��TIPO��$��20(�$
��Bold"�$��5q�
��������� -   $
 TipoTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $
 Tipo�$ OneWay=��� $��ORIGEN��$��20(�$
��Bold"�$��5q�
��������� -   $
 OrigenTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $ Origen�$ OneWay=��� $��DESTINO��$��20(�$
��Bold"�$��5q�
��������� -   $
 DestinoTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $ Destino�$ OneWay=��� $��ÚLTIMO PUNTO��$��20(�$
��Bold"�$��5q�
��������� -   $
 UltimoPuntoTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $ UltimoPunto�$ OneWay=��� $%��ÚLTIMO FECHA DE ACTUALIZACIÓN��$��20(�$
��Bold"�$��5q�
��������� -   $"
 UltimaActualizacionTextBlock��$��20(�$��10,0,0,0q�$��WordEllipsis=�$
��Bold"����� $ UltimaActualizacion�$ OneWay=�� $��	0,0,25,25q�$��50��$��Bottom=�$��1��$��Center=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ CancelarButton��$��1��   FilterButtonStyle#��[  ���	#FFB42727$	��104������ *CancelCommand+.���� $��Center=� �� TextAlignment$ Center=�
�������$��18(�$��Cancelar��$
��Bold"��� $
 Fill=�$��Center=�$��40��$��Center=�$��1��$��2������ $ 0.500095,-0.00311067&�$ 0.500095,0.994881&�.��� $ 	#00FFFFFF��$ 
0.00436681Q�� $ 	#CBBABABA��$ 0.49345Q�� $ 	#00FFFFFF��$ 1Q��� -	   $ GuardarButton��$��3��#��[  ���	#FF499B2C$	��104��$��3,3,0,3q�{��� $ HabilitarGrabar����� $ GrabarCommand�.���� $��Center=�$ Center=�
�������$��18(�$��Guardar��$
��Bold"��� -
   $
 MensajeTextBlock��$��WordEllipsis=�$��25(�$
��Bold"�
�������$��Center=�$��Center=����� $ Mensaje�$ OneWay=�