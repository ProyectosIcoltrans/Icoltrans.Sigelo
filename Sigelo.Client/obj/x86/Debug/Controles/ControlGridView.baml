   M S B A M L   `   `   `  ����"  Icoltrans.Sigelo.Win.Vehiculos>    8Icoltrans.Sigelo.Win.Vehiculos.Controles.ControlGridView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      s oInfragisticsWPF4.DataPresenter.v12.2, Version=12.2.20122.2022, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      -   5          >�Name$  RootControl��5      .��5
      �� 
���   �$	��0.5q�6!   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      .+�5      �  � Orientation$ 
Horizontal=�6   
�������.+�5      �� $	��8,4q�5      $��Center=�6"   $
��Bold"�5      $��WordEllipsis=�6'   ���	#FF3E2F58��5      �� *Tag+ �� ElementName$ RootControl��5   C   5      4  .Infragistics.Windows.DataPresenter.XamDataGrid  5      $��1��5   (   :  4Infragistics.Windows.DataPresenter.DataPresenterBase$   IsSynchronizedWithCurrentItem .    
DataSource 6   ��    FieldLayoutSettings 5      <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings  5         AllowAddNew$
 TrueY�5         AllowDelete$
 TrueY�5      	   AddNewRecordLocation$	 OnBottomFixed=�5       
   FieldLayouts
 5#      4  .Infragistics.Windows.DataPresenter.FieldLayout  5$         Fields 5%      .  (Infragistics.Windows.DataPresenter.Field  5&         Name$ fvcPlaca��6)      Width$	 100X�69     5'      $ fvcDescripcion��6)   $	 180X�6?     5(      $ fvcURL��6)   $ 48X�67     5)      $ 
fvcUsuario��6)   $ 48X�6;     5*      $ fvcClave��6)   $ 48X�69   5,      5.      50      51      53      