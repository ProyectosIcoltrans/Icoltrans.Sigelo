   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Icoltrans.Sigelo.Win.Vehiculos.Controles(Icoltrans.Sigelo.Win.Vehiculos.Controles  O,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  i9clr-namespace:Icoltrans.Sigelo.Win.Vehiculos.VistaModelos+Icoltrans.Sigelo.Win.Vehiculos.VistaModelos  9    3Icoltrans.Sigelo.Win.Vehiculos.Vistas.SatelitalView   D	Controles6clr-namespace:Icoltrans.Sigelo.Win.Vehiculos.Controles  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m	 iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors 	 8local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 LVistaModelos9clr-namespace:Icoltrans.Sigelo.Win.Vehiculos.VistaModelos 	   N� 	KeepAlive  .   � 	Resources     booleanToVisibilityConverter  ActualizarStoryboard g� RepeatBehavior �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%1  &	        &	      �� �� .��U� $ Forever��$ ActualizarTextBlock��$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�.\�  $ 0��$ 0Q�  $ 0:0:0.25��$ 90Q�  $ 0:0:0.50��$	 180Q�  $ 0:0:0.75��$	 270Q�  $ 0:0:1��$	 360Q� � DataContext D   >Icoltrans.Sigelo.Win.Vehiculos.VistaModelos.SatelitalViewModel  .���  � ColumnDefinitions �� $��0.568* ��� $��0.432* ����� 	 �� EndPoint$	 
1.002,0.52&�
 �� 
StartPoint$
 0.027,0.518&�.���  � Color$ 	#F9FFFFFF�� � Offset$ 0Q�� $ 	#F9FFFFFF��$ 1Q�� $ White��$	 0.5Q�.+��� $��15,15,15.5,0q�$	��Top=�$��64(����	#FFBCBCBC$��Asignar Satelital��$
��Wrap=��� $
��Left=�$��Center=� �� Orientation$ Vertical=�$��20,0,0,0q�$��1��$	��400��y���  �� Path$ MostrarVehiculoSatelital� �� Mode$ OneWay=� �� 	Converter# [  .+��� $��PLACA��$��20(�$
��Bold"�$��5q�
��������� .+��� -    ��Name$ TxtPlaca��$	��390��$��30�� � ToolTip$ Busqueda de placas������ $ Placa�$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.�� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *SearchTextboxEnterKeyCommand+.���� $��EMPRESAS SATELITALES��$��20(�$
��Bold"�$��5q�
��������� $ 
Horizontal=�.+��� -    ��Name$ SatelitalesComboBox��$	��360��  ListBoxItemStyle#��[   ComboBoxStyle#��[  w� TextPath$ fvcDescripcion������ *Satelitales+ �� SelectedItem �� *	Satelital+$ TwoWay=����� .R��� $	��0,4q��� $
��Bold"���� *fvcDescripcion+�� -    ��Name$ AgregarButton��  CircularButtonStyle#��[ $��30��$��30��$��+��$��Arial Black)�$��29.333(����	#FF48CE2E
����������� *CrearSatelitalCommand+�� $��USUARIO��$��20(�$
��Bold"�$��5q�
��������� .+��� -   $ 
TxtUsuario��$	��390��$��30������ *	Usuario+�� $��CLAVE��$��20(�$
��Bold"�$��5q�
��������� .+��� -   $ TxtClave��$	��390��$��30������ *Clave+� $��	0,0,25,25q�$��50��$��Bottom=�$��1��$��Center=�y��� $ MostrarVehiculoSatelital�$ OneWay=�# [   �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $ CancelarButton��$��1��  FilterButtonStyle#��[ ���	#FFB42727$	��104������ *CancelCommand+.���� $��Center=� �� TextAlignment$ Center=�
�������$��18(�$��Cancelar��$
��Bold"���  �� Stretch$
 Fill=�$��Center=�$��40��$��Center=�$��1��$��2������ $
 0.500095,-0.00311067&�$	 0.500095,0.994881&�.��� $ 	#00FFFFFF��$ 
0.00436681Q�� $ 	#CBBABABA��$ 0.49345Q�� $ 	#00FFFFFF��$ 1Q��� -   $ GuardarButton��$��3��#��[ ���	#FF499B2C$	��104��$��3,3,0,3q����� *!GuardarVehiculoSatelitalCommand+.���� $��Center=�$ Center=�
�������$��18(�$��Guardar��$
��Bold"��� $��20(�$
��Bold"�$��5q�
���  ������ *ValidacionVehiculosSatelital+�� $
��15,0q�$��Center=�y��� $ MostrarVehiculoSatelital�$ OneWay=�# [  .+�4  .Infragistics.Windows.DataPresenter.XamDataGrid  
 � Tag$ Satelitales Asignados��$	��432��  BackgroundBrush#��[ $��1��:  4Infragistics.Windows.DataPresenter.DataPresenterBase$   IsSynchronizedWithCurrentItem .    ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme     
DataSource  �� *VehiculoSatelitales+!   FieldLayouts! 4  .Infragistics.Windows.DataPresenter.FieldLayout  "   Fields" .  (Infragistics.Windows.DataPresenter.Field  #   Name$# fvcPlaca��$   Label$$ Placa��%   Width$	% 100X�  $# fvcDescripcion��$$ 	Satelital��$	% 160X�  $# fvcURL��$	$ URL��$	% 250X�  $# 
fvcUsuario��$$ Usuario��$	% 100X�  $# fvcClave��$$ Contraseña��$	% 100X�  $# fidVehiculosSatelital��$	% 100X�&   
Visibility$& Hidden=��� $
��Left=�$��Center=�$ Vertical=�$��20,0,0,0q�$��1��$	��400��y��� $ ActivarGuardarSatelital�$ OneWay=�# [  .+��� $��EMPRESA SATELITAL��$��20(�$
��Bold"�$��5q�
��������� .+��� -   $ TxtDescripcion��$	��390��$��30������ *Descripcion+�� $��
PAGINA WEB��$��20(�$
��Bold"�$��5q�
��������� .+��� -	   $ TxtURL��$	��390��$��30������ *URL+� $��	0,0,25,25q�$��50��$��Bottom=�$��1��$��Center=�y��� $ ActivarGuardarSatelital�$ OneWay=�# [   �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -
   $ CancelarSatelitalButton��$��1��#��[ ���	#FFB42727$	��104������ *CancelarCrearSatelitalCommand+.���� $��Center=�$ Center=�
�������$��18(�$��Cancelar��$
��Bold"��� $
 Fill=�$��Center=�$��40��$��Center=�$��1��$��2������ $
 0.500095,-0.00311067&�$	 0.500095,0.994881&�.��� $ 	#00FFFFFF��$ 
0.00436681Q�� $ 	#CBBABABA��$ 0.49345Q�� $ 	#00FFFFFF��$ 1Q��� -   $ GuardarSatelitalButton��$��3��#��[ ���	#FF499B2C$	��104��$��3,3,0,3q����� *GuardarCrearSatelitalCommand+.���� $��Center=�$ Center=�
�������$��18(�$��Guardar��$
��Bold"��� $��20(�$
��Bold"�$��5q�
���  ������ *ValidacionSatelital+�� $
��15,0q�$��Center=�y��� $ ActivarGuardarSatelital�$ OneWay=�# [  .+�  $ Satelitales��$	��432��#��[ $��1�� . $ IGTheme   �� *Satelitales+!   "   $# fvcDescripcion��$$ 	Satelital��$	% 160X�  $# fvcURL��$	$ URL��$	% 250X�  $# fidSatelital��$	% 100X�$& Hidden=�