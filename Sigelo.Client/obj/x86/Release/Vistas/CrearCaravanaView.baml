   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  6    0Icoltrans.Sigelo.Win.Vehiculos.CrearCaravanaView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  m	 iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors 	 8local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	   N� 	KeepAlive  .   � 	Resources @   :Icoltrans.Sigelo.Win.Vehiculos.ConvertidorVehiculoPorSalir   ConvertidorVehiculoPorSalir   booleanToVisibilityConverter  ActualizarStoryboard g� RepeatBehavior �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%@  &	        &	      &	 
       �� �� .��U� $ Forever��$ ActualizarTextBlock��$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�.\�  $ 0��$ 0Q�  $ 0:0:0.25��$ 90Q�  $ 0:0:0.50��$	 180Q�  $ 0:0:0.75��$	 270Q�  $ 0:0:1��$	 360Q� � DataContext ;   5Icoltrans.Sigelo.Win.Vehiculos.CrearCaravanaViewModel  .���  � ColumnDefinitions �� $��0.568* ��� $��0.432* ����� 	 �� 
StartPoint$	 0.027,0.518&�
 �� EndPoint$
 
1.002,0.52&�.���  � Offset$ 0Q� � Color$ 	#F9FFFFFF��� $ 1Q�$ 	#F9FFFFFF��� $	 0.5Q�$ White��.+��� $��15,15,15.5,0q�$	��Top=�$��64(����	#FFBCBCBC$��Crear Caravana��$
��Wrap=��� -    ��Name$ NombreCaravanaStackPanel��$
��30,0q�$��Center=�$��Center=�y���  �� Path$ MostrarNombreCaravana� �� 	Converter# [  �� Mode$ OneWay=�.+��� -    ��Name$ CaravanaTextBlock��$��5q�$��Center=�$��20(�$
��Bold"�
�������$4��.Ingrese la información para crear la caravana��$��WordEllipsis=�:   4Icoltrans.Sigelo.Win.Vehiculos.ControlNombreCaravana  �� -   $$ ListaNombresCaravanaStackPanel��$��1��$
��20,0q�$��Center=� �� Orientation$ Vertical=�y��� $ MostrarListaNombres�$ OneWay=�# [ .+��� $��5q�$��20(�$
��Bold"�
�������$��	VEHICULOS��4  .Infragistics.Windows.DataPresenter.XamDataGrid  -     Name$ VehiculosComboBox��$	��420��$
��15,0q�  BackgroundBrush#��[ :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ScrollingMode$ 	Immediate=�   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ IGTheme    ActiveDataItem �� $" VehiculoPorSalirSeleccionado�$ TwoWay=�   
DataSource �� $ VehiculosPorSalir�$ OneWay=�   FieldLayouts 4  .Infragistics.Windows.DataPresenter.FieldLayout     Fields .	  (Infragistics.Windows.DataPresenter.Field	   	  Name$ fvcPlaca�� 	  Width$
 AutoX� 	  Column$ 0�� 	  Label$ Placa�� 	  Settings 6
  0Infragistics.Windows.DataPresenter.FieldSettings
   
  CellValuePresenterStyle ��   �� 
TargetType;  5Infragistics.Windows.DataPresenter.CellValuePresenter"   .��� ! �� Property! � ��" �� Value$
" Bold"��� ! � ��$" 18(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ fvcDescripcion��$ *X�$ 1��$ 	Vehículo�� 
   �� "   .��� ! � ��$" 16(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ 	Conductor��$
 AutoX�$ 0��# 	  
ColumnSpan$# 2��$ 	Conductor��
$ 	  Row$$ 1�� 
   �� "   .��� ! � ��$
" Bold"��� ! � ��$" 13(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ CDestino��$
 AutoX�$ 0��$# 2��$ Ciudad��$$ 2�� 
   �� "   .��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ fnuManifiesto��$
 AutoX�$ 0��$ 
Manifiesto��$$ 3�� 
  % 
  EditorStyle% �� 0 	 *Infragistics.Windows.Editors.XamTextEditor"   .��� ! �@ TextWrapping$
" Wrap=��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ finNumeroCarnet��$ *X�$ 1��$ Carné��$$ 3�� 
  % �� "   .��� ! �@ TextWrapping$
" Wrap=��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  &   FieldSettings& 
  ' 
  	AllowEdite
 aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$' False ( 
  AllowRecordFiltering$
( True ) 
  CellClickAction$) SelectRecord=�"* 
  FilterLabelIconDropDownType$* MultiSelectExcelStyle=�+ 
  LabelTextAlignment� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$+ Center ,   FieldLayoutSettings, <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings  -   AllowAddNew$- FalseY�.   AllowClipboardOperations� 
 �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
. Copy /   AllowDelete$/ FalseY�0   AllowFieldMoving$0 No=�1   AutoArrangeCells$1 Never=�2   AutoGenerateFields$2 FalseY�3   FilterUIType$3 
LabelIcons=�4   RecordSelectorLocation$
4 None=�5   SelectionTypeCell$5 Default=�6   SelectionTypeField$
6 None=�7   SelectionTypeRecord$7 Extended=��� $��10q�$��20(�$
��Bold"�
�������$
��TIPO���� $
��15,0q�$ 
Horizontal=�.+�	� -   8 	�Name$8 NacionalRadioButton��$��Nacional��$��16(�
�������9 	� 	GroupName$9 TipoCaravana��: d� 	IsChecked: �� *NacionalSeleccionada+$ TwoWay=�	� -   $8 RegionalRadioButton��$
��15,0q�$��Regional��$��16(�
�������$9 TipoCaravana��: �� *RegionalSeleccionada+$ TwoWay=�	� -   $8 UrbanaRadioButton��$��Urbana��$��16(�
�������$9 TipoCaravana��: �� *UrbanaSeleccionada+$ TwoWay=��� $��5q�$��20(�$
��Bold"�
�������$
��RUTA���� -   ; ��Name$; RutaComboBox��$
��15,0q�  ListBoxItemStyle#��[   ComboBoxStyle#��[ < w� TextPath$< Descripcion������ $ Rutas�$ OneWay=�= �� SelectedValue= �� $ RutaSeleccionada�$ TwoWay=����� .R��� $��16(����� *Descripcion+� $��50��$��0,15,0,0q�$��Center=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -	   > ��Name$!> CancelarCrearCaravanaButton��$��1��$	��104�����	#FFB42727  FilterButtonStyle#��[ ���� $" CancelarCrearCaravanaCommand�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��Cancelar��? �� TextAlignment$? Center=��� $��2��$��1��$��40��$��Center=�$��Center=�@ �� Stretch$
@ Fill=����� $	 0.500095,-0.00311067&�$
 0.500095,0.994881&�.��� $ 
0.00436681Q�$ 	#00FFFFFF��� $ 0.49345Q�$ 	#CBBABABA��� $ 1Q�$ 	#00FFFFFF���� -
   $ > GuardarCrearCaravanaButton��$��3��$	��104��$��3,3,0,3q����	#FF499B2C#��[ ���� $! GuardarCrearCaravanaCommand�{��� $ ActivarGuardarCaravana�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��Guardar��$? Center=�� -   A �Name$A VehiculosStackPanel��$��1��$��20,104,20,32q�y��� $ MostrarVehiculos�$ OneWay=�# [ .+��� $��5,5,5,0q�$	��Top=�$��20(�$
��Bold"�
�������$��	Vehiculos��  -   $ VehiculosMarcadosListBox��$��15,36.6,15,48q�#��[ $ 	Immediate=�$ IGTheme  �� $ VehiculosPorSalir�$ OneWay=�    5  /Infragistics.Windows.DataPresenter.UnboundField  $ 40X�$  ��B 	  RowSpan$B 4�� 
  $( False C 
  LabelClickAction$C Nothing=�D 
  LabelPresenterStyleD �� 7  1Infragistics.Windows.DataPresenter.LabelPresenter"   .��� ! � ��" �� E �� 
TargetType"E  .R��� $��Center=�$��Center=�: �� $0 *DataPresenter.DataContext.SeleccionarTodos�$ TwoWay=� �� "   .��� ! � ��" �� "E  .R��� $��Center=�$��Center=�: �� $ DataItem.Marcado�$ TwoWay=��� ! � ��$" Center=��� ! � ��$" Center=�	  $ fvcPlaca��$
 AutoX�$ 1��$ Placa�� 
   �� "   .��� ! � ��$
" Bold"��� ! � ��$" 18(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ fvcDescripcion��$ *X�$ 2��$ 	Vehículo�� 
   �� "   .��� ! � ��$" 16(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ 	Conductor��$
 AutoX�$ 1��$# 2��$ 	Conductor��$$ 1�� 
   �� "   .��� ! � ��$
" Bold"��� ! � ��$" 13(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ CDestino��$
 AutoX�$ 1��$# 2��$ Ciudad��$$ 2�� 
   �� "   .��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ fnuManifiesto��$
 AutoX�$ 1��$ 
Manifiesto��$$ 3�� 
  % �� "   .��� ! �@ TextWrapping$
" Wrap=��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  	  $ finNumeroCarnet��$ *X�$ 2��$ Carné��$$ 3�� 
  % �� "   .��� ! �@ TextWrapping$
" Wrap=��� ! � ��$" 10(��� ! � ��" �� $ DataItem.fidCaravana�# [  & 
  $' False $
( True $) SelectRecord=�$* MultiSelectExcelStyle=�$+ Center ,   $- FalseY�$
. Copy $/ FalseY�$0 No=�$1 Never=�$2 FalseY�$3 
LabelIcons=�$
4 None=�$5 Default=�$
6 None=�$7 Extended=��� -   $> GuardarVehiculoButton��$��44��$	��104��$��0,40,0,0q�$��Center=�$��Bottom=����	#FF499B2C#��[ ���� $ GuardarVehiculoCommand�{��� $ ActivarGuardarVehiculo�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��	Adicionar��$? Center=�� -   $A CaravanasGrid��$��28.526,112.04,29.632,32q�y��� $ MostrarCaravanas�# [ $ OneWay=�.+�  -   $! CaravanasDisponiblesListBox��$��1.106,26.84,0,48q�#��[ $ 	Immediate=�$ IGTheme  �� $ CaravanaSeleccionada�$ TwoWay=� �� $ 	Caravanas�$ OneWay=�    	  $ fvcDescripcion��$ *X�$ Caravana��$B 2�� 
   �� "   .��� ! � ��$
" Bold"��� ! � ��$" 18(�	  $
 Ruta��$
 AutoX�$ 1��$
 Ruta�� 
   �� "   .��� ! � ��$" 13(�	  $ TipoCaravana��$
 AutoX�$ 1��$ Tipo Caravana��$$ 1�� 
   �� "   .��� ! � ��$" 10(�& 
  $' False $
( True $) SelectRecord=�$* MultiSelectExcelStyle=�$+ Center ,   $- FalseY�$
. Copy $/ FalseY�$0 No=�$1 Never=�$2 FalseY�$3 
LabelIcons=�$
4 None=�$5 Default=�$
6 None=�$7 Extended=��� -   F ��Name$F NacionalCheckBox��$
��Left=�$	��Top=�$��Nacional�����	#FF7E7E7E: �� $ FiltoNacional�$ OneWay=��� -   $F RegionalCheckBox��$��70,0,0,0q�$
��Left=�$	��Top=�$��Regional�����	#FF7E7E7E: �� $ FiltroRegional�$ OneWay=��� -   $F UrbanaCheckBox��$��	140,0,0,0q�$
��Left=�$	��Top=�$��Urbana�����	#FF7E7E7E: �� $ FiltroUrbana�$ OneWay=��� -   $ ActualizarStackPanel��$��0,-5,0,0q�$��Right=�$	��Top=�$ 
Horizontal=�.+��� -   $" UltimaActualizacionTextBlock��$��Center=����	#FF878787$��Actualizando���� -   $ ActualizarTextBlock��$��Center=�G � Cursor$
G Hand��$��Webdings)�$��30(�$
��Bold"����	#FF6C6C6CH J� RenderTransformOrigin$H 0.5,0.5&�$��q��I � ToolTip$-I 'clic aquí para actualizar manualmente.��z�V� .~��� �� �� T� .  (System.Windows.Interactivity.InteractionJ   TriggersJ 9  3Microsoft.Expression.Interactivity.Core.DataTrigger  K   Value$K Actualizando��D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTriggerL   BindingL �� *ActualizarStoryboard+$ OneWay=�.  (System.Windows.Interactivity.TriggerBaseM   Actions.M F  @Microsoft.Expression.Interactivity.Media.ControlStoryboardAction  N   ControlStoryboardOption$
N Play=�?  9Microsoft.Expression.Interactivity.Media.StoryboardActionO   
Storyboard#O [ B  <Microsoft.Expression.Interactivity.Core.ChangePropertyAction  P   PropertyName$P Cursor��8  2System.Windows.Interactivity.TargetedTriggerActionQ   
TargetName$Q ActualizarTextBlock��"  System.Windows.Input.CursorsR   WaitS   Value#S ZR   T   
Comparison$T NotEqual=�$K Actualizando��L �� *ActualizarStoryboard+$ OneWay=�.M   $
N Stop=�#O [   $P Cursor��$Q ActualizarTextBlock��U   Hand#S ZU /  )System.Windows.Interactivity.EventTrigger  V   	EventName$V MouseLeftButtonDown��3  -System.Windows.Interactivity.EventTriggerBaseW   
SourceName$W ActualizarStackPanel��.M 6  0System.Windows.Interactivity.InvokeCommandAction  X   CommandX �� *RefrescarListaCommand+� $��0,0,1.474,0q�$��Bottom=�Y � Orientation$Y 
Horizontal=�.+��� -   $> CrearCaravanaButton��$	��104��$��2q�
���   �#��[ ���� $ CrearCaravanaCommand�.���� $��Center=�$��18(�$
��Bold"�
�������$��Crear��$? Center=��� -   $> EliminarCaravanaButton��$	��104��$��2q�
���   �#��[ ���� $ EliminarCaravanaCommand�{��� $ ActivarSeleccionCaravana�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��Eliminar��$? Center=��� -   $> EscoltasCaravanaButton��$	��104��$��2q�
���   �#��[ ���� $ EscoltasCaravanaCommand�{��� $ ActivarSeleccionCaravana�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��Escoltas��$? Center=��� -   $> RefuerzosCaravanaButton��$	��104��$��2q�
���   �#��[ ���� $ RefuerzosCaravanaCommand�{��� $ ActivarSeleccionCaravana�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��	Refuerzos��$? Center=��� -   $> IniciarCaravanaButton��$	��104��$��2q�
���   �#��[ ���� $ IniciarCaravanaCommand�{��� $ ActivarSeleccionCaravana�$ OneWay=�.���� $��Center=�$��18(�$
��Bold"�
�������$��Iniciar��$? Center=��� -   $> CancelarButton��$��90��$��2q�$
��Left=����	#FFB42727#��[ ���� *CancelCommand+.���� $��Center=�$��18(�$
��Bold"�
�������$��Cancelar��$? Center=�