   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  9    3Icoltrans.Sigelo.Win.Vehiculos.ControlCarreteraView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  8local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    N� 	KeepAlive  .   � DataContext >   8Icoltrans.Sigelo.Win.Vehiculos.ControlCarreteraViewModel  .���  � ColumnDefinitions �� $��2* ��� $��* �����  �� 
StartPoint$ 0.027,0.518&� �� EndPoint$ 
1.002,0.52&�.���  � Offset$ 0Q� � Color$ 	#F9FFFFFF��� $ 1Q�$ 	#F9FFFFFF��� $	 0.5Q�$ White��.+��� $��2��$��15,15,15.5,0q�$	��Top=�$��64(����	#FFBCBCBC$��Control Caravana��$
��Wrap=�F� $
��0.68��$��Right=�$��Center=����Ф�+xE�Ѥ�+��/ �� Stretch$
 Fill=� �� StrokeLineJoin$ Round=����� $ 0.465295,0.0505441&�$ 0.465295,0.943124&�.��� $ 0Q�$ 	#00D4D4D4��� $	 0.5Q�$ 	#FFD4D4D4��� $ 1Q�$ 	#00D4D4D4���� $��1��$	��350��$��20,0,0,0q�$
��Left=�$��Center=�	 �� Orientation$	 Vertical=�.+��� $��5q�$��20(�$
��Bold"�
�������$��PUNTOS DE REPORTE���� -   
 ��Name$
 PuntosReporteComboBox��   ListBoxItemStyle#��[    ComboBoxStyle#��[  w� TextPath$ fvcDescripcion������ *PuntosControl+ �� SelectedItem �� *PuntoControl+ �� Mode$ TwoWay=����� .R��� $	��0,4q��� $
��Bold"���� *fvcDescripcion+�� $��5q�$��20(�$
��Bold"�
�������$��ESTADOS DEL VEHICULO���� -   $
 EstadosVehiculosComboBox��#��[  #��[ $ Descripcion������ *	Estados+ �� *Estado+$ TwoWay=����� .R��� $	��0,4q��� ��� *fvcDescripcion+ �� $	� - �� �� $
��Bold"���� *fsmIdEstadoVehiculo+�� $��5q�$��20(�$
��Bold"�
�������$��RAZONES DE ESTADO���� -   $
 RazonesComboBox��#��[  #��[ $ Descripcion������ *	Razones+ �� *Razon+$ TwoWay=����� .R��� $	��0,4q��� ��� *fvcDescripcion+ �� $	� - �� �� $
��Bold"���� *fsmIdRazonEstadoV+�� $��5q�$��20(�$
��Bold"�
�������$��OBSERVACIONES���� -    ��Name$ ObservacionesTextBox��$	��100��$	��8,0q�
������� �� TextWrapping$
 Wrap=����� *Observaciones+$ TwoWay=��� $��5q�$��20(�$
��Bold"�
�������$��SELECCIONADO���� $	��100��$	��8,0q�
������� �� 
IsReadOnly . $
 Wrap=����� *Seleccionados+�� $
��15,0q�$��Center=�.+��� -    ��Name$ CaravanaTextBlock��$��5q�$��Center=�$��20(�$
��Bold"�
�������$��WordEllipsis=����� *NombreCaravana+4  .Infragistics.Windows.DataPresenter.XamDataGrid  $��2��$	��432��  BackgroundBrush#��[ :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme    
DataSource �� *PuntosEntrega+   FieldLayouts 4  .Infragistics.Windows.DataPresenter.FieldLayout     Fields 5  /Infragistics.Windows.DataPresenter.UnboundField  .  (Infragistics.Windows.DataPresenter.Field   Width$ 40X�   Label$  ��   Settings 6  0Infragistics.Windows.DataPresenter.FieldSettings     AllowRecordFilteringe aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�	  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False	    LabelClickAction$ Nothing=�   LabelPresenterStyle ��  �� 
TargetType7
  1Infragistics.Windows.DataPresenter.LabelPresenter" 
 .���  �� Property � �� �� Value ��   �� 
TargetType"  
 .R��� $��Center=�$��Bottom=�! d� 	IsChecked! �� " �� Path$0" *DataPresenter.DataContext.SeleccionarTodos�$ TwoWay=�#   CellValuePresenterStyle# �� ;  5Infragistics.Windows.DataPresenter.CellValuePresenter"  .���  � �� �� "   .R��� $��Center=�$��Bottom=�! �� $" DataItem.Seleccionado�$ TwoWay=���  � ��$ Center=���  � ��$ Bottom=�  $   Name$$ fvcDescripcion��$ *X�%   Column$% 1��$ Descripción��   &   EditorStyle& �� 0  *Infragistics.Windows.Editors.XamTextEditor"  .���  �@ TextWrapping$
 Wrap=���  � ��$
 Bold"���  � ��$ 14(�  $$ fnuManifiesto��$ *X�$% 1��$ 
Manifiesto��
'   Row$' 1��   & �� "  .���  �@ TextWrapping$
 Wrap=���  � ��$ 12(�  $$ fvcDireccion��$	 200X�$% 2��$ 
Dirección��$' 0��   & �� "  .���  �@ TextWrapping$
 Wrap=�  $$ fvcTelefono��$	 200X�$% 2��$ 	Télefono��$' 1��   & �� "  .���  �@ TextWrapping$
 Wrap=���  � ��$ 10(�  $$ fvcCodigoEDI��$ 80X�$% 3��$	 EDI��$' 0��   & �� "  .���  �@ TextWrapping$
 Wrap=���  � ��$ 10(�  $$ fsmOrden��$ 80X�$% 3��$ Orden��$' 1��   & �� "  .���  �@ TextWrapping$
 Wrap=���  � ��$ 10(�  $$ finUnidades��$ 80X�$% 4��$
 Und.��$' 0��   & �� 3  -Infragistics.Windows.Editors.XamNumericEditor"  .��� .  (Infragistics.Windows.Editors.ValueEditor �@ Format$ #,##0����  � ��$ 12(�  $$ frlKilos��$ 80X�$% 4��$
 Kls.��$' 1��   & �� "  .���  �@ Format$ N2����  � ��$ 12(�(   FieldSettings(   )   	AllowEdit$) False	 $
 True	 *   CellClickAction$* SelectRecord=�"+   FilterLabelIconDropDownType$+ MultiSelectExcelStyle=�,   LabelTextAlignment�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$, Center -   FieldLayoutSettings- <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings  .   AllowAddNew$. FalseY�/   AllowClipboardOperations�  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
/ Copy 0   AllowDelete$0 FalseY�1   AllowFieldMoving$1 No=�2   AutoArrangeCells$2 Never=�3   AutoGenerateFields$3 FalseY�4   FilterUIType$4 
LabelIcons=�5   RecordSelectorLocation$
5 None=�6   SelectionTypeCell$6 Default=�7   SelectionTypeField$
7 None=�8   SelectionTypeRecord$8 Extended=�� $��1��$��50��$��	0,0,25,25q�$��Center=�$��Bottom=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   9 ��Name$9 CancelarButton��$��1��$	��104�����	#FFB42727: �� IsCancel: .   FilterButtonStyle#��[ ���� *CancelCommand+.���� $��Center=�$��18(�$
��Bold"�
�������$��Cancelar��; �� TextAlignment$; Center=��� $��2��$��1��$��40��$��Center=�$��Center=�$
 Fill=����� $ 0.500095,-0.00311067&�$ 0.500095,0.994881&�.��� $ 
0.00436681Q�$ 	#00FFFFFF��� $ 0.49345Q�$ 	#CBBABABA��� $ 1Q�$ 	#00FFFFFF���� -   $9 GuardarButton��$��3��$	��104��$��3,3,0,3q����	#FF499B2C< �� 	IsDefault< . #��[ ���� $" GrabarCommand�.���� $��Center=�$��18(�$
��Bold"�
�������$��Guardar��$; Center=�