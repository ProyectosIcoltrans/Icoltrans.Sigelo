   M S B A M L   `   `   `  ���� "  Icoltrans.Sigelo.Win.Vehiculos<    6Icoltrans.Sigelo.Win.Vehiculos.ControlListaConductores   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     >�Name$  UserControl��.��4  .Infragistics.Windows.DataPresenter.XamDataGrid     BackgroundBrush#��[  :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme    ActiveDataItem ��  �� Mode$ TwoWay=� �� Path$( "CambioNombre.ConductorSeleccionado�   
DataSource �� $, &CaravanaSeleccionada.Model.Conductores�   FieldLayouts 4  .Infragistics.Windows.DataPresenter.FieldLayout     Fields 5  /Infragistics.Windows.DataPresenter.UnboundField  .  (Infragistics.Windows.DataPresenter.Field	   Width$
	 AutoX�
   RowSpan$
 2��   Settings 6  0Infragistics.Windows.DataPresenter.FieldSettings     AllowRecordFilteringe aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False    LabelClickAction$ Nothing=�   CellValuePresenterStyle ��  �� 
TargetType;	  5Infragistics.Windows.DataPresenter.CellValuePresenter" 	 .���  �� Property � �� �� Value �� .R��� $��m����  � ��$
 Bold"���  � ��$ Webdings)���  � ��$ 38(���  � ��$ Center=���  � ��$ Bottom=�     Name$ Numero��   Column$ 1��   Label$ Identificación��    �� " 	 .���  � ��$ Center=�   EditorStyle �� 0
  *Infragistics.Windows.Editors.XamTextEditor" 
 .��� .  (Infragistics.Windows.Editors.ValueEditor �@ Format$ #,##0����  �@
 TextAlignment$ Center=���  � ��$ 14(���  � ��$
 Bold"�  $ 	Documento��$ 1��
   Row$ 1��    �� " 	 .���  � ��$ Center=���  � ��$ 9(�  $ NombreCompleto��$	 *X�$ 2��$ 	Conductor��    �� " 	 .���  � ��$ Center=���  � ��$ 14(���  � ��$
 Bold"� �� " 
 .���  �@
 TextWrapping$
 Wrap=�  $ LicenciaCompleta��$	 *X�$ 2��$ Licencia��$ 1��    �� " 	 .���  � ��$ Center=���  � ��$ 10(� �� " 
 .���  �@
 TextWrapping$
 Wrap=�  $ DireccionCompleta��$
	 AutoX�$ 3��$ 
Dirección��    �� " 	 .���  � ��$ Center=���  � ��$ 12(�  $ 	Telefonos��$
	 AutoX�$ 3��$ 	Teléfono��$ 1��    �� " 	 .���  � ��$ Center=���  � ��$ 10(�   FieldSettings      	AllowEdit$ False $
 True    CellClickAction$ SelectRecord=�"   FilterLabelIconDropDownType$ MultiSelectExcelStyle=�   LabelTextAlignment�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$ Center    FieldLayoutSettings <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings     AllowAddNew$ FalseY�   AllowClipboardOperations�  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
 Copy    AllowDelete$ FalseY�    AllowFieldMoving$  No=�!   AutoArrangeCells$! Never=�"   AutoGenerateFields$" FalseY�#   FilterUIType$# 
LabelIcons=�$   RecordSelectorLocation$
$ None=�%   SelectionTypeCell$% Default=�&   SelectionTypeField$
& None=�'   SelectionTypeRecord$' Single=�