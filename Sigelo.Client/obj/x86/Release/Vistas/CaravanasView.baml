   M S B A M L   `   `   `  ���� Y  UIcoltrans.Sigelo.Win.Vehiculos, Version=5.3.0.6, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Icoltrans.Sigelo.Win.VehiculosIcoltrans.Sigelo.Win.Vehiculos  p lSystem.Windows.Controls.Input.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35pTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.ToolkitSystem.Windows.Controls 2    ,Icoltrans.Sigelo.Win.Vehiculos.CaravanasView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     c _Microsoft.Expression.Drawing, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35=ed4http://schemas.microsoft.com/expression/2010/drawing  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 s
 oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter 
 m iInfragisticsWPF4.Editors.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  8local,clr-namespace:Icoltrans.Sigelo.Win.Vehiculos  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ]myTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.Toolkit    N� 	KeepAlive  .  � 	Resources <   6Icoltrans.Sigelo.Win.Vehiculos.ConvertidorAutorizacion   convertidorAutorizacion;   5Icoltrans.Sigelo.Win.Vehiculos.ConvertidorColorEstado  ConvertidorColorEstado@   :Icoltrans.Sigelo.Win.Vehiculos.ConvertidorFondoColorEstado  ConvertidorFondoColorEstado=   7Icoltrans.Sigelo.Win.Vehiculos.ConvertidorUltimoReporte  ConvertidorUltimoReporte@   :Icoltrans.Sigelo.Win.Vehiculos.ConvertidorSiguienteReporte  ConvertidorSiguienteReporte   BooleanToVisibilityConverter  ActualizarStoryboard g� RepeatBehavior �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%|  &	        &	      &	 
     &	      &	      &	      &	                �� �� .��U� $ Forever��$ ActualizarTextBlock��$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�.\�  $ 0��$ 0Q�  $ 0:0:0.25��$ 90Q�  $ 0:0:0.50��$	 180Q�  $ 0:0:0.75��$	 270Q�  $ 0:0:1��$	 360Q� � DataContext 7   1Icoltrans.Sigelo.Win.Vehiculos.CaravanasViewModel   J� CommandBindings )  #System.Windows.Input.CommandBinding  -   	   Command$	 Search��
 J� InputBindings
 �� 
 �� Key$ D1��2	   ,Icoltrans.Sigelo.Win.Vehiculos.OpcionesBarra 	  
EstadoRojo �� CommandParameter# Z  �� 	Modifiers$ Controlf� �� Command �� *AccionesToolBarCommand+�� $ D2�� 	  EstadoAmarillo# Z $ Controlf� �� *AccionesToolBarCommand+�� $ D3�� 	  EstadoVerde# Z $ Controlf� �� *AccionesToolBarCommand+�� $ R�� 	  EstadoRegional# Z $ Controlf� �� *AccionesToolBarCommand+�� $ N�� 	  EstadoNacional# Z $ Controlf� �� *AccionesToolBarCommand+�� $ U�� 	  EstadoUrbana# Z $ Controlf� �� *AccionesToolBarCommand+�� $ M�� 	  EstadoMarcada# Z $ Controlf� �� *AccionesToolBarCommand+�� $ B��$ Search��$ Controlf��� $ Enter�� 	  CambiarMarca# Z $ Controlf� �� *AccionesToolBarCommand+�� $ Enter�� 	  MostrarDetalle# Z  �� *AccionesToolBarCommand+.��� -    �Name$ 
LayoutRoot��  BackgroundBrush#��[  � ColumnDefinitions �� $
��152* ��� $
��163* ��� $	��19* � � RowDefinitions �� $��30 ��� $��30 ��� $��* ��� -    ��Name$ 
DetalleRow��$��0 �.
 	 (System.Windows.Interactivity.Interaction 
  Triggers 9  3Microsoft.Expression.Interactivity.Core.DataTrigger    System.Windows.Visibility   Visible   Value# Z D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger   Binding ��   �� Path$  
Visibility�! �� ElementName$! DetalleCaravanaTabControl��. 	 (System.Windows.Interactivity.TriggerBase"   Actions." B  <Microsoft.Expression.Interactivity.Core.ChangePropertyAction  #   PropertyName$# Height��$   Value$$ *��  %   
Comparison$% NotEqual=�# Z  �� $  
Visibility�$! DetalleCaravanaTabControl��."   $# Height��$$ 0���� $��60 �.+��� $��2��
��� � ��� $��2��$��2,1,0,1q����� & �� 
StartPoint$& 0.5,0&�' �� EndPoint$' 0.5,1&�.��� ( � Offset$( 1Q�) � Color$) 	#48FFFFFF��� �� $��2��$��2,1,0,1q�* �� Orientation$* 
Horizontal=�.+��� -   + ��Name$+ EstadoRojoCheckBox��$��12��$��12��$��10,0,0,0q�$��Center=�  RedBrush#��[ $��0q� 	 StateCheckBox#��[	 , d� 	IsChecked, �� *
FiltroRojo+�� -   $+ EstadoAmarilloCheckBox��$��12��$��12��$��5,0,0,0q�$��Center=� 
 YellowBrush#��[
 $��0q�#��[	 , �� *FiltroAmarillo+�� -   $+ EstadoVerdeCheckBox��$��12��$��12��$��5,0,0,0q�$��Center=�  
GreenBrush#��[ $��0q�#��[	 , �� *FiltroVerde+�� -   $+ NacionalCheckBox��$��20,0,0,0q�$��Center=�$��Nacional��
�������, �� *FiltroNacional+�� -   $+ RegionalCheckBox��$��5,0,0,0q�$
��Left=�$��Center=�$��Regional��
�������, �� *FiltroRegional+�� -	   $+ UrbanaCheckBox��$��5,0,0,0q�$
��Left=�$��Center=�$��Urbana��
�������, �� *FiltroUrbana+4   .Icoltrans.Sigelo.Win.Vehiculos.ControlMarcador  -
   -  Name$- Marcados��$��15��$��15��$
��15,0q�.   Marcado. �� $  FiltroMarcados�/ �� Mode$/ TwoWay=��� $��5,0,0,0q�$��Center=�
�������$��Origen��� $��2��$	��150��$��5,0,0,0q�$��Center=�
������� �� �� $��26.199 �.+�-  'System.Windows.Controls.AutoCompleteBox  -   0  Name$0 OrigenesAutoCompleteBox��$��Center=�$��0q�1   
FilterMode$1 
StartsWith=�2   MinimumPrefixLength$2 1��3   ValueMemberPath$3 Descripcion����Z� 4   ItemsSource4 �� *
Origenes+5   SelectedItem5 �� $  FiltroOrigen�$/ TwoWay=�6   ItemTemplate6 �� .R��� �� $��16(���� *Descripcion+�� �� $��12(�
���������� *Id+    �� $  SelectedItem�$! OrigenesAutoCompleteBox�� Z� ."   $
# Text��$$  ��F� $��1��$��15��$��15��$��6.207,0,5,0q�$��Center=������`�� ��j �`�� �_� �K� @]� P�n @]� ��S�b@@]� �_� ��j S@B �.�e�}@�S�b@P�n ��K� @B `�� �.�e�}@`�� ��j � � �O� ѱ*� �%� 	7 �� Stretch$
7 Fill=����	#FFBABABA$	��2.5���� $��5,0,0,0q�$��Center=�
�������$��Destino��� $��2��$	��150��$��5,0,0,0q�$��Center=�
������� �� �� $��26.199 �.+�  -   $0 DestinosAutoCompleteBox��$��Center=�$��0q�$1 
StartsWith=�$2 1��$3 Descripcion����Z� 4 �� *
Destinos+5 �� $  FiltroDestino�$/ TwoWay=�6 �� .R��� �� $��16(���� *Descripcion+�� �� $��12(�
���������� *Id+    �� $  SelectedItem�$! DestinosAutoCompleteBox�� Z� ."   $
# Text��$$  ��F� $��1��$��15��$��15��$��6.207,0,5,0q�$��Center=������`�� ��j �`�� �_� �K� @]� P�n @]� ��S�b@@]� �_� ��j S@B �.�e�}@�S�b@P�n ��K� @B `�� �.�e�}@`�� ��j � � �O� ѱ*� �%� 	$
7 Fill=����	#FFBABABA$	��2.5���� $��10,0,0,0q�$��Center=�
��������� .�	Buscar en�� �� .�descripción� $	��170��$��5,0,0,0q�$��Center=�
������� �� �� $��26.199 �.+�  -   $0 CaravanasAutoCompleteBox��$��Center=�$��0q�$1 Contains=�$2 1��8 � ToolTip$08 *Busqueda en la Descripción de la Caravana��$#3 Model.Caravana.fvcDescripcion����Z� 4 �� *Seguimientos+5 �� *DescripcionBuscada+$/ TwoWay=�6 �� .R��� $��WordEllipsis=��� $��16(���� *Model.Caravana.fvcDescripcion+�� �� $��12(�
���������� *Model.Caravana.OrigenDestino+$/ OneWay=�    �� $  SelectedItem�$! CaravanasAutoCompleteBox�� Z� ."   $
# Text��$$  ��F� $��1��$��15��$��15��$��6.207,0,5,0q�$��Center=������`�� ��j �`�� �_� �K� @]� P�n @]� ��S�b@@]� �_� ��j S@B �.�e�}@�S�b@P�n ��K� @B `�� �.�e�}@`�� ��j � � �O� ѱ*� �%� 	$
7 Fill=����	#FFBABABA$	��2.5���� $��10,0,0,0q�$��Center=�9 � Cursor$
9 Hand��
�������$��Limpiar��$��	Underline{�$8 Limpiar Filtro�� / 	 )System.Windows.Interactivity.EventTrigger  :   	EventName$: MouseLeftButtonDown��." 6 	 0System.Windows.Interactivity.InvokeCommandAction  ;   Command; �� $  ClearFilterCommand��� $��1��$��2��
��� � ��� $��1��$��2��$��2,1,0,1q����� $& 0.5,0&�$' 0.5,1&�.��� $( 1Q�$) 	#48FFFFFF��� �� $��1��$��2��$��2,1,0,1q�$* 
Horizontal=�.+��� $��10,0,0,0q�$��Center=�$��	ExtraBold"�
�������$��MAPA:���� -   $+ AlarmaCheckBox��$��10,0,0,0q�$��Center=�$��Alarmas��
��������� $��20,0,0,0q�$��Center=�
�������$��Placa��� $	��100��$��5,0,0,0q�$��Center=�
������� �� �� $��26.199 �.+��� -   < ��Name$< PlacaTextBox��$��20��$��0q�$8 Busqueda en el Mapa����Z� ���� $  	PlacaMapa�$/ TwoWay=�= �� UpdateSourceTrigger$= PropertyChanged=�.��
 �� $ Enter�� �� *SearchTextboxEnterKeyCommand+.��F� $��1��$��15��$��15��$��0,2.5,4.793,2.5q�$��Center=������`�� ��j �`�� �_� �K� @]� P�n @]� ��S�b@@]� �_� ��j S@B �.�e�}@�S�b@P�n ��K� @B `�� �.�e�}@`�� ��j � � �O� ѱ*� �%� 	$
7 Fill=����	#FFBABABA$	��2.5��$��Right=��� -   $��ImageViewer1��$��1��$��0,0,1,1q�$��Right=�$��80��$��2��$
��Fill=�$��2������ > �� 	UriSource$)> #../Resources/drawable-ldpi-icon.png?�8 _� .���� $* Vertical=�.+��� $��0��$��0,2,0,3q�$��14(�$
��Bold"����� *UsuarioActual.NomEmpleado+�� $��1������ *UsuarioActual.DesCompania+�� $��2������ *UsuarioActual.DesSucursal+�� $��3������ *UsuarioActual.DesCentro+�� $��5������ *UsuarioActual.DesCargo+� $��2��$��1��$��2�� �� $��* ��� $��30 �.+�4 
 .Infragistics.Windows.DataPresenter.XamDataGrid  -   $��gridCaravanas��$��0��$��0q�: 
 4Infragistics.Windows.DataPresenter.DataPresenterBase?   ScrollingMode$? 	Immediate=�@   ThemeJ 
 DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$@ IGTheme A � ForceCursorA . B   IsUndoEnabledB . C   ActiveDataItemC �� $  CaravanaSeleccionada�$/ TwoWay=�D   
DataSourceD �� *Seguimientos+ ; 
 5Infragistics.Windows.DataPresenter.DataRecordCellAreaE �� 
TargetTypeF �� PropertyG �� ValueH �� 	Converter8 
 2Infragistics.Windows.DataPresenter.HeaderPresenter��%�   '        7  '  �     �� "E  .��� F �@ BackgroundHover
G ���� �� F �@ BorderHoverBrush
G ���� �� F � ��G �� $  DataItem.Model.Caravana�8H   �� "E  .��� F � ��
G � � �I J� OpacityMaskI �� $' 0.5,1&�$& 0.5,0&�.��� $) Black��$( 1Q�� $) 	#FF138913������ $' 0.5,1&�$& 0.5,0&�.��� $) Black��$( 1Q�� $) Green������ $' 0.5,1&�$& 0.5,0&�.��� $) Black��$( 1Q�� $) Green��� $) Green��J   FieldLayoutsJ 4 
 .Infragistics.Windows.DataPresenter.FieldLayout  K   FieldsK 5 
 /Infragistics.Windows.DataPresenter.UnboundField  . 
 (Infragistics.Windows.DataPresenter.FieldL   Width$L 10X�M   Column$M 0��N   Label$N  ��O   RowSpan$O 3��P   SettingsP 6 
 0Infragistics.Windows.DataPresenter.FieldSettings  Q   AllowRecordFilteringe aInfragisticsWPF4.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$
Q True R   CellValuePresenterStyleR �� ; 
 5Infragistics.Windows.DataPresenter.CellValuePresenter"E  .��� F � ��G �� S �� 
TargetType"S  .R��� $��0,2,2,2q����� $  DataItem.Model.Caravana�#H [   $L 17X�$M 1��$N  ��$O 3��P   $Q False R �� "E  .��� F � ��G �� "S  .R�  $��15��$��15��$��0,5,0,0q�$
��Left=�$	��Top=�. �� $  DataItem.Marcado�$/ TwoWay=�  $L *X�T   BindingMode$T OneWay=�U   BindingPath$#U Model.Caravana.fvcDescripcion�$M 2��$N Caravana��
V   Row$V 0��P   R �� "E  .��� F � ��G �� "S  .R��� $��16(�$
��Bold"�$
��Wrap=�$
9 Hand������ $,  &DataItem.Model.Caravana.fvcDescripcion�  $L *X�$T OneWay=�$U Model.Caravana.Satelital�$M 2��$N Caravana��$V 3��P   R �� "E  .��� F � ��G �� "S  .R��� $
9 Hand���� -   ���� $*  $DataItem.Model.Caravana.URLSatelital�.�.��� $
9 Hand������ $'  !DataItem.Model.Caravana.Satelital�  $L *X�$T OneWay=�$U Model.Caravana.fvcEstado�$M 2��$N Estado��$V 1��P   R �� "E  .��� F � ��G �� "S  .R��� $��14(�$
��Wrap=����� $'  !DataItem.Model.Caravana.fvcEstado�  $L *X�$T OneWay=�$"U Model.Caravana.OrigenDestino�$M 2��$N Origen / Destino��$V 2��P   R �� "E  .��� F � ��G �� "S  .R��� $��10(�$
��Wrap=����� $+  %DataItem.Model.Caravana.OrigenDestino�  $	L 140X�$M 3��$N Último reporte��$O 4��P   $Q False R �� "E  .��� F � ��G �� "S  .R��� W ��Name$W ActualContent��$��Center=�$��Center=����� *DataItem.Model.Caravana+#H [    $: MouseLeftButtonDown��3 	 -System.Windows.Interactivity.EventTriggerBaseX   
SourceName$X ActualContent��."   ; �� $$  DataItem.ReportarActualCommand�  $	L 140X�$M 4��$N Siguiente reporte��$O 4��P   $Q False R �� "E  .��� F � ��G �� "S  .R��� $W SiguienteContent��$��Center=�$��Center=����� *DataItem.Model.Caravana+#H [    $: MouseLeftButtonDown��$X SiguienteContent��."   ; �� $'  !DataItem.ReportarSiguienteCommand�  $L 40X�$T OneWay=�$M 5��$N  ��$O 4��P   $Q False R �� "E  .��� F � ��G �� "S  .R��� $W ActualizarMapaContent��$��Center=�$��Center=�$
9 Hand��$%8 clic aquí para ver en el mapa.��.���� 
Y J� Uid$Y imgLupa��$��40��$��40������ $)> #../Resources/search-icon-png-20.png?�   $: MouseLeftButtonDown��$X ActualizarMapaContent��."   ; �� $$  DataItem.ReportarEnMapaCommand�Z   FieldSettingsZ   $
Q True [   	AllowEdit$[ False \   CellClickAction$\ SelectRecord=�"]   FilterLabelIconDropDownType$] MultiSelectExcelStyle=�^   LabelClickAction$^ Nothing=�_   LabelTextAlignment�   �Infragistics.Windows.Helpers.NullableConverter`1[[System.Windows.TextAlignment, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$_ Center  `   AllowGroupBy$
` True a   FieldLayoutSettingsa <! 
 6Infragistics.Windows.DataPresenter.FieldLayoutSettings!  b !  AllowAddNew$b FalseY�c !  AllowClipboardOperations�"  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.DataPresenter.AllowClipboardOperations, InfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$
c Copy" d !  AllowDelete$d FalseY�e !  AllowFieldMoving$e No=�f !  AutoArrangeCells$f Never=�g !  AutoGenerateFields$g FalseY�h !  FilterUIType$h 
LabelIcons=�i !  RecordSelectorLocation$
i None=�j !  SelectionTypeCell$j Default=�k !  SelectionTypeField$
k None=�l !  SelectionTypeRecord$l Single=�m !  RecordFilterScope$m 
AllRecords=�
 d� # Z n d� MouseAction$n LeftDoubleClicke� �� $  AccionesToolBarCommand�$/ OneWay=��� $��1�����	#FFABABAB�� $��1��$��0,0,15,0q�$��Right=�$��Center=�
��������� $�Caravanas: �� �� $
��Bold"���� *CaravanasFiltradas+$/ OneWay=� �� $� �� �� $
��Bold"�$�/�� �� $� �� �� $
��Bold"���� *TotalCaravanas+$/ OneWay=� �� $� - Vehiculos: �� �� $
��Bold"���� *TotalVehiculos+$/ OneWay=�0#   *Icoltrans.Sigelo.Win.Vehiculos.ControlMapa#  -   o # Name$o MapWebBrowser��$��2��$��1��$��2��p #  MostarAlarmasp �� $! AlarmaCheckBox��$  	IsChecked�q #  Placaq �� $! PlacaTextBox��$
  Text�y��� $  MostrarMapa�#H [ �� -   r ��Name$r DetalleCaravanaTabControl��$��3��$��1��$��2��$��0,5,0,55q�$��2��y��� $  MostrarDetalle�#H [ .@��� -   s ��Name$s PuntosReporteTabItem��$��Puntos de reporte��.��>$   8Icoltrans.Sigelo.Win.Vehiculos.ControlListaPuntosReporte$  �� -   $s PuntosEntregaTabItem��$��Puntos de entrega��.��>%   8Icoltrans.Sigelo.Win.Vehiculos.ControlListaPuntosEntrega%  �� -   $s EscoltasTabItem��$��Escoltas��.��9&   3Icoltrans.Sigelo.Win.Vehiculos.ControlListaEscoltas&  �� -   $s ConductoresTabItem��$��Conductores��.��<'   6Icoltrans.Sigelo.Win.Vehiculos.ControlListaConductores'  �� -   $s VehiculosTabItem��$��	Vehiculos��.��:(   4Icoltrans.Sigelo.Win.Vehiculos.ControlListaVehiculos(  �� -   $s CargaTabItem��$��Carga��.��6)   0Icoltrans.Sigelo.Win.Vehiculos.ControlListaCarga)  �� $��4��$��4��
��� � ��� $��4��$��4��$��1q����� $& 0.5,0&�$' 0.5,1&�.��� $( 0Q�$) 	#48FFFFFF��� $( 1Q��� $��4��$��3��$* 
Horizontal=�.+��� -   t ��Name$t CrearCaravanaButton��$	��120��7*   1Icoltrans.Sigelo.Win.Vehiculos.OpcionesNavegacionu *  CrearCaravana#��Zu $��Crear caravana��
�������  
MenuButton#��[ ���� $  NavgationCommand�{��� v �� ConverterParameter$*v $28677578-780D-4104-AC87-C2D25F70B3BE��#H [  $/ OneWay=�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�w �� StrokeLineJoin$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -   $t InicioRecorridoButton��$	��120��x *  InicioRecorrido#��Zx $��Inicio de recorrido��
�������#��[ ���� $  NavgationCommand�{��� $*v $62DF7957-E0FF-4412-99E9-ABF92635F6A8��#H [  $/ OneWay=�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -   $t EscoltasButton��$	��120��y *  Escoltas#��Zy $��Cambio de escoltas��
�������#��[ ���� $  NavgationCommand�{��� $*v $E0DAA341-CF2C-4DDB-AD6C-A1CF17366782��#H [  $/ OneWay=�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -   $t RefuerzosButton��$	��120��z *  	Refuerzos#��Zz $��Cambio de refuerzos��
�������#��[ ���� $  NavgationCommand�{��� $*v $93D5F49B-09BE-43A7-8DC9-FE31098E2052��#H [  $/ OneWay=�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -   $t CambiarCaravanaButton��$	��120��{ *  CambiarCaravana#��Z{ $��Cambio de caravana��
�������#��[ ���� $  NavgationCommand�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -    $t FinRecorridoButton��$	��120��| *  FinRecorrido#��Z| $��Fin del recorrido��
�������#��[ ���� $  NavgationCommand�{��� $*v $704C2CFB-5460-4CBE-AD33-A08BABE84F58��#H [  $/ OneWay=�F� $
��0.64��$��40��$	��2,0q�$��Center=����Ф�+xE�Ѥ�+��/$
7 Fill=�$w Round=�$��0.640055������ $& 0.465295,0.0505441&�$' 0.465295,0.943124&�.��� $( 0Q�$) 	#00FFFFFF��� $( 0.493023Q�$) 	#FFD4D4D4��� $( 1Q�$) 	#00FFFFFF���� -!   $t AsignarSatelitalButton��$	��120��} *  AsignarSatelital#��Z} $��Asignar Satelital��
�������#��[ ���� $  NavgationCommand��� -"   ~ ��Name$~ ActualizarStackPanel��$��4��$��2��$��1��$��0,0,5,8q�$��Right=�$��Bottom=�$��46��$��31��.+��� -#    ��Name$ ActualizarTextBlock��$��Center=�$
9 Hand��$��Webdings)�$��30(�$
��Bold"�
�������� J� RenderTransformOrigin$� 0.5,0.5&�$��q��$-8 'clic aquí para actualizar manualmente.��z�V� .~��� �� �� T� �� -$   $" UltimaActualizacionTextBlock��
����������� $  ActualizarStoryboard�   $ Actualizando�� �� $  ActualizarStoryboard�$/ OneWay=�." F+  @Microsoft.Expression.Interactivity.Media.ControlStoryboardAction+  � +  ControlStoryboardOption$
� Play=�?,  9Microsoft.Expression.Interactivity.Media.StoryboardAction� ,  
Storyboard#� [   $# Cursor��8- 	 2System.Windows.Interactivity.TargetedTriggerAction� -  
TargetName$� ActualizarTextBlock��".  System.Windows.Input.Cursors� .  Wait#$ Z�   $% NotEqual=�$ Actualizando�� �� $  ActualizarStoryboard�$/ OneWay=�." +  $
� Stop=�#� [   $# Cursor��$� ActualizarTextBlock��� .  Hand#$ Z�   $: MouseLeftButtonDown��$X ActualizarStackPanel��."   ; �� $  RefrescarListaCommand�$/ OneWay=��� $��3��$��1��$��5��$��0q�$��Stretch=�$	��Top=�#��[ 
�������$��1q�$9 SizeNS��� �� ResizeDirection$
� Rows=�$��2���� $��2��$��0��$��5��$��0q�#��[ 
�������$��1q�$9 SizeWE��$� Columns=�