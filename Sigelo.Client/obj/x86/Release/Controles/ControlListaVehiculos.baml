   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  :    4Icoltrans.Sigelo.Win.Vehiculos.ControlListaVehiculos   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  8local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     >�Name$  UserControl�� � 	Resources 9   3Icoltrans.Sigelo.Win.Vehiculos.ConvertidorPrioridad   ConvertidorPrioridad<   6Icoltrans.Sigelo.Win.Vehiculos.ConvertidorTipoVehiculo  ConvertidorTipoVehiculo��%   &	        &	          .��4  .Infragistics.Windows.DataPresenter.XamDataGrid  $��2��  BackgroundBrush#��[ :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme    ActiveDataItem ��  �� Path$' !CambioNombre.VehiculoSeleccionado� �� Mode$ TwoWay=�   
DataSource �� *&$CaravanaSeleccionada.Model.Vehiculos+   FieldLayouts 4  .Infragistics.Windows.DataPresenter.FieldLayout  	   Fields	 .  (Infragistics.Windows.DataPresenter.Field  
   Name$
 VinculacionId��   Width$
 AutoX�   Column$ 0��   Label$  ��   Settings 6  0Infragistics.Windows.DataPresenter.FieldSettings     AllowRecordFilteringe aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�	  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False	    LabelClickAction$ Nothing=�   CellValuePresenterStyle ��  �� 
TargetType;
  5Infragistics.Windows.DataPresenter.CellValuePresenter" 
 .���  �� Property � �� �� Value$ Center=���  � �� ��  �� 
TargetType" 
 .R��� $��Center=� �� Orientation$ 
Horizontal=�.+��� $��Center=�$��Webdings)�$��16(�$��i��y���  �� 	Converter# [  �� ���� # [   $
 Vinculacion��$
 AutoX�$ 0��$ Vinculación��
   Row$ 1��    �� " 
 .���  � ��$ Center=�  $
 PlacaCompleta��$
 AutoX�$ 1��$ Placa��$ 0��    �� " 
 .���  � ��$ Center=���  � ��$ 14(���  � ��$
 Bold"�  $
 
Manifiesto��$
 AutoX�$ 1��$ 
Manifiesto��$ 1��    �� " 
 .���  � ��$ Center=�   EditorStyle �� 3  -Infragistics.Windows.Editors.XamNumericEditor"  .��� .  (Infragistics.Windows.Editors.ValueEditor �@ Format$ #,##0����  � ��$ 14(�  $

 Tipo��$
 AutoX�$ 2��$
 Tipo��$ 0��  $
 Complemento��$
 AutoX�$ 2��$ Complem.��$ 1��  $
 
Carroceria��$
 AutoX�$ 3��$ Carroc.��$ 0��  $
 Marca��$
 AutoX�$ 3��$ Marca��$ 1��  $

 Ejes��$
 AutoX�$ 4��$
 Ejes��$ 0��  $
 Modelo��$
 AutoX�$ 4��$
 Año��$ 1��  $
 Color��$
 AutoX�$ 5��$ Color��$ 0��  $	
 GPS��$
 AutoX�$ 5��$	 GPS��$ 1��  $
 Motor��$ *X�$ 6��$ Motor��$ 0��  $
 Chasis��$ *X�$ 6��$ Chasis��$ 1��  $
 Volumen��$
 AutoX�$ 7��$ Volumen��$ 0��  $

 Peso��$
 AutoX�$ 7��$
 Peso��$ 1��   FieldSettings      	AllowEdit$ False	 $
 True	    CellClickAction$ SelectRecord=�"   FilterLabelIconDropDownType$ MultiSelectExcelStyle=�   LabelTextAlignment�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$ Center    FieldLayoutSettings <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings      AllowAddNew$  FalseY�!   AllowClipboardOperations�  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
! Copy "   AllowDelete$" FalseY�#   AllowFieldMoving$# No=�$   AutoArrangeCells$$ Never=�%   AutoGenerateFields$% FalseY�&   FilterUIType$& 
LabelIcons=�'   RecordSelectorLocation$
' None=�(   SelectionTypeCell$( Default=�)   SelectionTypeField$
) None=�*   SelectionTypeRecord$* Single=�