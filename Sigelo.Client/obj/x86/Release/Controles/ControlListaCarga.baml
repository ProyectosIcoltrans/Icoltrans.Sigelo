   M S B A M L   `   `   `  ���� "  Icoltrans.Sigelo.Win.Vehiculos6    0Icoltrans.Sigelo.Win.Vehiculos.ControlListaCarga   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m	 iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors 	 .��� -     �Name$  
LayoutRoot�����	#FFABABAB � RowDefinitions �� $��30 ��� $��* �.+���  �� Orientation$ 
Horizontal=�$��0,0,0,8q�$��Right=�$��Center=�.+��� -    ��Name$ InformeOperaciones��$ 
Horizontal=�$��Center=� � Cursor$
 Hand��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseLeftButtonDown��3  -System.Windows.Interactivity.EventTriggerBase   
SourceName$ InformeOperaciones��.  (System.Windows.Interactivity.TriggerBase   Actions. C  =Microsoft.Expression.Interactivity.Core.LaunchUriOrFileAction  	   Path	 �� *RutaInformeOperaciones+.+��� 
�������$��Ver Reporte��$��10(�$��0,0,2,0q�$��Center=��� $G��A/Icoltrans.Sigelo.Win.Vehiculos;component/Resources/kthememgr.png��$
��Fill=�$��26��$��26��$��0,0,2,0q��� -   $ ExportarExcel��$ 
Horizontal=�$��Center=�$
 Hand��$��10,0,0,0q�   $ MouseLeftButtonDown��$ ExportarExcel��. 6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 �� *ExportarExcelCommand+.+��� 
�������$��Exportar��$��10(�$��0,0,2,0q�$��Center=��� $J��D/Icoltrans.Sigelo.Win.Vehiculos;component/Resources/floppy_mount.png��$
��Fill=�$��26��$��26��$��0,0,2,0q�4  .Infragistics.Windows.DataPresenter.XamDataGrid  $��1��:  4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ	  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme	    BackgroundBrush#��[     
DataSource �� *Carga+ � 	Resources ?
  9Infragistics.Windows.DataPresenter.SummaryResultPresenter �� 
TargetType �� Property �� Value@  :Infragistics.Windows.DataPresenter.SummaryResultsPresenter��%  '
        '  �     �� " 
 .���  � ��$ Right=���  � ��$ Right=���  � ��$ 14(���  � ��$
 Bold"���  � ��$
 Auto���� "  .���  � ��$ Right=���  � ��$ Right=���  � ��$
 Auto����  � ��$ 0q���  � ��$ 0q�   FieldLayouts 4  .Infragistics.Windows.DataPresenter.FieldLayout     SummaryDefinitions :  4Infragistics.Windows.DataPresenter.SummaryDefinition  :  4Infragistics.Windows.DataPresenter.SummaryCalculator
   Sum   
Calculator# Z    SourceFieldName$ finUnidades��   DisplayAreae
 aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.SummaryDisplayAreas, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$ BottomFixed    StringFormat$ 	{0:#,##0}��   Position$ UseSummaryPositionField=�   PositionFieldName$ finUnidades��  # Z $ frlPeso��$ BottomFixed $ {0:n}��$ UseSummaryPositionField=�$ frlPeso��  # Z $ 
frlVolumen��$ BottomFixed $ {0:n}��$ UseSummaryPositionField=�$ 
frlVolumen��   Fields 5  /Infragistics.Windows.DataPresenter.UnboundField  .  (Infragistics.Windows.DataPresenter.Field   Width$
 AutoX�   RowSpan$ 2��   Settings 6  0Infragistics.Windows.DataPresenter.FieldSettings     AllowRecordFiltering� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False     LabelClickAction$  Nothing=�!   CellValuePresenterStyle! �� ;  5Infragistics.Windows.DataPresenter.CellValuePresenter"  .���  � �� �� .R��� $��e����  � ��$
 Bold"���  � ��$ Webdings)���  � ��$ 38(���  � ��$ Center=���  � ��$ Bottom=�  "   Name$" 
fvcFactura��#   Label$# Factura��$   Column$$ 1��
%   Row$% 0��$ *X�   ! �� "  .���  � ��$
 Bold"���  � ��$ 14(�  $" fvcDescripcion��$# Cliente��$$ 1��$% 1��$ *X�   ! �� "  .���  � ��$ 11(�&   EditorStyle& �� 0 	 *Infragistics.Windows.Editors.XamTextEditor"  .���  �@ TextWrapping$
 Wrap=�  $" Punto��$# Punto��$$ 2��$% 0��$ *X�   ! �� "  .���  � ��$
 Bold"���  � ��$ 16(�& �� "  .���  �@ TextWrapping$
 Wrap=�  $" fvcDireccion��$# 
Dirección��$$ 2��$% 1��$ *X�   ! �� "  .���  � ��$ 14(�& �� "  .���  �@ TextWrapping$
 Wrap=�  $" finUnidades��$# Unidad��$$ 3��$ 2��$ 90X�   ! �� "  .���  � ��$ 14(���  � ��$ Center=���  � ��$ Center=�& �� 3 	 -Infragistics.Windows.Editors.XamNumericEditor"  .��� . 	 (Infragistics.Windows.Editors.ValueEditor �@ Format$ #,##0��  $" frlPeso��$
# Peso��$$ 4��$ 2��$ 90X�   ! �� "  .���  � ��$ 14(���  � ��$ Center=���  � ��$ Center=�& �� "  .���  �@ Format$ n��  $" 
frlVolumen��$# Volumen��$$ 5��$ 2��$ 90X�   ! �� "  .���  � ��$ 14(���  � ��$ Center=���  � ��$ Center=�& �� "  .���  �@ Format$ n��'   FieldSettings'   (   	AllowEdit$( False )   CellClickAction$) SelectRecord=�*   LabelTextAlignment� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$* Center $
 True "+   FilterLabelIconDropDownType$+ MultiSelectExcelStyle=�,   SummaryUIType$, SingleSelect=�-   SummaryDisplayArea$- BottomFixed .   FieldLayoutSettings. <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings  /   AutoGenerateFields$/ FalseY�0   AllowAddNew$0 FalseY�1   AllowDelete$1 FalseY�2   AllowFieldMoving$2 No=�3   SelectionTypeCell$3 Default=�4   SelectionTypeField$
4 None=�5   SelectionTypeRecord$5 Extended=�6   AutoArrangeCells$6 Never=�7   RecordSelectorLocation$
7 None=�8   FilterUIType$8 
LabelIcons=�9   AllowClipboardOperations� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
9 Copy 