   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem >    8Icoltrans.Sigelo.Win.Vehiculos.ControlListaPuntosEntrega   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     >local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  s	 oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter 	 0sys&clr-namespace:System;assembly=mscorlib 	 m
 iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors 
   � 	Resources  9   3Icoltrans.Sigelo.Win.Vehiculos.ConvertidorColorCita   ConvertidorColorCita;   5Icoltrans.Sigelo.Win.Vehiculos.ConvertidorCitaEntrega  ConvertidorCitaEntrega7   1Icoltrans.Sigelo.Win.Vehiculos.ConvertidorEntrega  ConvertidorEntrega8   2Icoltrans.Sigelo.Win.Vehiculos.ConvertidorDataItem  ConvertidorDataItem��%<   &	        &	      &	 
     &	              .��� -    �Name$ 
LayoutRoot�����	#FFABABAB � RowDefinitions �� $��30 ��� .+���  �� Orientation$ 
Horizontal=�$��0,0,0,8q�$��Right=�$��Center=�.+��� -    ��Name$ ReportarStack��$ 
Horizontal=�$��Right=� � Cursor$
 Hand��$��10,0,0,0q�$��Center=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseLeftButtonDown��3  -System.Windows.Interactivity.EventTriggerBase   
SourceName$ ReportarStack��.  (System.Windows.Interactivity.TriggerBase	   Actions.	 C	  =Microsoft.Expression.Interactivity.Core.LaunchUriOrFileAction	  
 	  Path
 �� *RutaReportarNovedades+.+��� 
�������$��Reportar Novedades��$��10(�$��0,0,2,0q�$��Center=��� $H��B/Icoltrans.Sigelo.Win.Vehiculos;component/Resources/article-24.png��$
��Fill=�$��26��$��26��$��0,0,2,0q��� -   $ InformeNovedades��$ 
Horizontal=�$��Right=�$
 Hand��$��10,0,0,0q�$��Center=�   $ MouseLeftButtonDown��$ InformeNovedades��.	 	  
 �� * RutaInformeNovedadesManifiesto+.+��� 
�������$��Ver Manifiesto��$��10(�$��0,0,2,0q�$��Center=��� $I��C/Icoltrans.Sigelo.Win.Vehiculos;component/Resources/text-editor.png��$
��Fill=�$��26��$��26��$��0,0,2,0q�4
 	 .Infragistics.Windows.DataPresenter.XamDataGrid
  $��1��: 	 4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ 	 DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme   BackgroundBrush#��[    
DataSource ��  �� Path$ PuntosEntrega� �� Mode$ OneWay=�   FieldLayouts 4 	 .Infragistics.Windows.DataPresenter.FieldLayout     Fields . 	 (Infragistics.Windows.DataPresenter.Field     Name$ Destino��   Column$ 0��   RowSpan$ 2��   Width$ 8X�   Settings 6 	 0Infragistics.Windows.DataPresenter.FieldSettings     AllowRecordFilteringe aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False    LabelClickAction$ Nothing=�   CellValuePresenterStyle ��  �� 
TargetType; 	 5Infragistics.Windows.DataPresenter.CellValuePresenter"  .���  �� Property � �� �� Value ��  �� 
TargetType"  .R��� $��0,0,2,0q�����  �� 	Converter# [    $ 
fvcFactura��   Label$ Factura��$ 1��
    Row$  0��$	 130X�    �� "  .���  � ��$ 14(���  � ��$
 Bold"�  $ fvcDescripcion��$ Cliente��$ 1��$  1��$	 130X�    �� "  .���  � ��$ 12(�!   EditorStyle! �� 0 
 *Infragistics.Windows.Editors.XamTextEditor"  .���  �@ TextWrapping$
 Wrap=�  $ Punto��$ Punto de Entrega��$ 2��$  0��$ *X�    �� "  .���  � ��$ 16(���  � ��$
 Bold"�! �� "  .���  �@ TextWrapping$
 Wrap=�  $ fvcDireccion��$ 
Ubicación��$ 2��$  1��$ *X�    �� "  .���  � ��$ 14(�! �� "  .���  �@ TextWrapping$
 Wrap=�  $ fdtCompromisoCliente��$ 
Compromiso��$ 3��$ 2��$	 100X�   ! �� "  .��� . 
 (Infragistics.Windows.Editors.ValueEditor �@ Format$ MMM dd��  $ fdtHoraInicial��$
 Cita��$ 4��$ 2��$	 130X�   "   
EditorType""   �� "  .���  � ��$ Center=���  � ��$ Center=�! �� "  .��� 1 
 +Infragistics.Windows.Editors.TextEditorBase# �@ ValueToDisplayTextConverter# [ ��  �@ TextAlignment$ Center=�  $ 
fdtEntrega��$ Estado��$ 5��$  0��$	 100X�   ""   �� "  .���  � ��$ Center=���  � ��$
 Bold"���  � ��$ 16(���  � ��$ Webdings)�! �� "  .��� # �@ ValueToDisplayTextConverter# [ ��  �@ TextAlignment$ Center=���  � ��$
 Bold"���  � ��$ 16(���  � ��$ Webdings)�  $ SiglaNovedad��$ Novedad��$ 5��$  1��$	 100X�    �� "  .���  �@�ToolTip �� # [ $ DataItem�# �� ConverterParameter$# fvcDescripcionNovedad��  $ finUnidades��$	 Und��$ 6��$ 2��$ 90X�    �� "  .���  � ��$ 14(�! �� 3 
 -Infragistics.Windows.Editors.XamNumericEditor"  .���  �@ Format$ #,##0��$   FieldSettings$   %   	AllowEdit$% False &   CellClickAction$& SelectRecord=�'   LabelTextAlignment�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$' Center $
 True "(   FilterLabelIconDropDownType$( MultiSelectExcelStyle=�)   LabelTextTrimming�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextTrimming, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$) CharacterEllipsis *   FieldLayoutSettings* < 	 6Infragistics.Windows.DataPresenter.FieldLayoutSettings  +   AutoGenerateFields$+ FalseY�,   AllowAddNew$, FalseY�-   AllowDelete$- FalseY�.   AllowFieldMoving$. No=�/   SelectionTypeCell$/ Default=�0   SelectionTypeField$
0 None=�1   SelectionTypeRecord$1 Extended=�2   AutoArrangeCells$2 Never=�3   RecordSelectorLocation$
3 None=�4   FilterUIType$4 
LabelIcons=�5   AllowClipboardOperations�  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
5 Copy 