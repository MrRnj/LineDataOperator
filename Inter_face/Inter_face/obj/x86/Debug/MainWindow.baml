   M S B A M L   `   `   `  ����M  IFluent, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5   	   C ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel 5   	   ;"clr-namespace:Inter_face.CovertersInter_face.Coverters 5   	   7 clr-namespace:Inter_face.BackUpsInter_face.BackUps 5
   	      Inter_face.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7Fluent$clr-namespace:Fluent;assembly=Fluent     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1vm"clr-namespace:Inter_face.ViewModel     5   	   CInter_face_Coverters"clr-namespace:Inter_face.Coverters     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   -Backup clr-namespace:Inter_face.BackUps  5
   	   d `GalaSoft.MvvmLight.Extras, Version=5.2.0.37224, Culture=neutral, PublicKeyToken=f46ff315b1088208-Custom http://www.galasoft.ch/mvvmlight  5   	   -   5          � Title$  数据转换��5   	   $��18(�6   $	��640��5      $	��933��6#    � WindowState$ 	Maximized=�6/    � Icon$ icons/title.png��6G    � 	Resources 5      *  $Inter_face.Coverters.BoolenConverter   BoolenConverter: 4Inter_face.Coverters.TypeToMenuVisibilityConvertercs # TypeToMenuVisibilityConvertercs= 7Inter_face.Coverters.MovingDistenceStringToIntConverter & "MovingDistenceStringToIntConverter6 0Inter_face.Coverters.BoolenToVisibilityConverter  BoolenToVisibilityConverter5 /Inter_face.Coverters.DmToDfxVisibilityConverter  DmToDfxVisibilityConverter6 0Inter_face.Coverters.ISDToSelectionModeConverter  ISDToSelectionModeConverter1 +Inter_face.Coverters.IntToUnBoolenConverter  IntToUnBoolenConverter/ )Inter_face.Coverters.IntToBoolenConverter  IntToBoolenConverter4	 .Inter_face.Coverters.intTocursorstyleConverter  intTocursorstyleConverter7
 1Inter_face.Coverters.StationDataToBoolenConverter  	 StationDataToBoolenConverter3 -Inter_face.Coverters.IDatamodelToAliConverter 
 IDatamodelToAliConverter4 .Inter_face.Coverters.DmToShowDetailUpConverter  DmToShowDetailUpConverter3 -Inter_face.Coverters.DmToPartsLengthConverter  DmToPartsLengthConverter. (Inter_face.Coverters.DmToLengthConverter  DmToLengthConverter4 .Inter_face.Coverters.DmToQjvisibilityConverter  DmToQjvisibilityConverter2 ,Inter_face.Coverters.DmToXhSelectedConverter  DmToXhSelectedConverter/ )Inter_face.Coverters.StringToIntConverter  StringToIntConverter, &Inter_face.Coverters.DmToTypeConverter  DmToTypeConverter+ %Inter_face.Coverters.DmToKeyConverter  DmToKeyConverter+ %Inter_face.Coverters.DmToLdhConverter  DmToLdhConverter* $Inter_face.Coverters.DmToLcConverter  DmToLcConverter4 .Inter_face.Coverters.DmToXhvisibilityConverter  DmToXhvisibilityConverter? 9Inter_face.Coverters.DatasCollectionToVisibilityConverter ( $DatasCollectionToVisibilityConverter2 ,Inter_face.Coverters.DmToShowDetailConverter  DmToShowDetailConverter0 *Inter_face.Coverters.AdjustLengthConverter  AdjustLengthConverter0 *Inter_face.Coverters.DmToPositionConverter  DmToPositionConverter2 ,Inter_face.Coverters.DmToVisibilityConverter  DmToVisibilityConverter/ )Inter_face.Coverters.DmToStretchConverter  DmToStretchConverter7 1Inter_face.Coverters.IDatamodelToToolTipConverter   IDatamodelToToolTipConverter0 *Inter_face.Coverters.DmToPathDataConverter  DmToPathDataConverter< 6Inter_face.Coverters.ActrueLengthToShowLengthConverter % !ActrueLengthToShowLengthConverter+  %Inter_face.Coverters.IDMToDMConverter  IDMToDMConverter+!  %Inter_face.ViewModel.ViewModelLocator   Locator ! 
accessicon " lbopicon # laopicon $ saopicon % sbopicon & poduicon ' podulargeicon ( 
quxianicon ) quxianlargeicon * 
biaojiicon + biaojilargeicon , Checkdatasignicon - checklogicicon 
. upicon / downicon 0 bothicon 1 
resulticon 2 	titleicon 3 newworkicon 4 	curveicon 5 stationicon 6 lineicon 7 ruleicon 8 signalupicon 9 signaldownicon : stationsignalicon ; exportsignaldataicon < checkcdl = 	AutoCheck 	> Upway ? Downway 
@ SaveXH A ExportToSvg B Sections C ModifyDianfx D 	Modifysig E 	DeleteDfx F 	DeleteSig G moveSig H 
confirmdiv I 	InsertDfx J 	InsertSig K 	saveother L opensignals M 	opentrack N creatsignals 
O editUm 
P UmBdis Q NewSheetTip � RowDefinitions R ReadAccessTip S ReadResourceTip T ReadOperatedTip U SaveResourceTip V SaveOperatedTip W ExportToSvgTip X DetailDataTemplate �Name � ColumnDefinitions � ToolTip �� 
LineHeight	 �� 	Converter
 �� Mode (�Name (� Stretch ��Name �� StrokeDashCap �� ConverterParameter �� ElementName �� Stretch �� StrokeDashArray J� Opacity �� AngleX T� X Y SingleDataTemplate."  (System.Windows.Interactivity.Interaction "  Triggers/#  )System.Windows.Interactivity.EventTrigger.$  (System.Windows.Interactivity.TriggerBase $  Actions/%  )GalaSoft.MvvmLight.Command.EventToCommand ��Name J� RenderTransformOrigin d� 	IsChecked �� ScaleX �� ScaleY #  	EventName %  Command ��Name  ��Name Z DetailItemsPanelTemplate! �� SelectedItem" �� SelectedIndex# �� SelectionMode$ � ContextMenu% m� CommandParameter& m� Command' %  PassEventArgsToCommand( K� IsItemsHost) $� Orientation [ 
ListBorder* �� Color \ DetailListBoxStyle+ �� 
TargetType, �� Property- �� Value. �� 
TargetType/ ��Name0 �� Padding1 J� SnapsToDevicePixels2 �� Triggers3 P� Property4 P� Value5 �� 
TargetName��% 0  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 
    &	     &	 0    &	 C    &	 V    &	 i    &	 |    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	     &	 '    &	 :    &	 M    &	  `    &	! s    &	" �    &	# �    &	$ #    &	% ^    &	& �    &	' �    &	( "    &	) \    &	* �    &	+ �    &	,     &	- O    &	. �    &	/ �    &	0 �    &	1 3    &	2 s    &	3 �    &	4 �    &	5      &	6 [    &	7 �    &	8 �    &	9     &	: E    &	; �    &	< �    &	=  	    &	> =	    &	? v	    &	@ �	    &	A �	    &	B )
    &	C d
    &	D �
    &	E �
    &	F     &	G _    &	H �    &	I �    &	J     &	K R    &	L �    &	M �    &	N     &	O N    &	P �    &	Q �    &	R 9    &	S �    &	T ,    &	U �    &	V     &	W �    &	X     7  7  7  7  7  7  7  7  7  7 
 7  7  &	Y �    7  7  7   &	Z _(    &	[ �(    &	\ �(    7 [   5   
   6/     5   
   6?     5   
   6B     5   
   6;     5   
   6:     5   
   6;     5   
   66     5   
   64   	  5   
   69   
  5   
   6<     5   
   68     5   
   69     5   
   68     5   
   63     5   
   69     5   
   67     5    
   64     5!   
   61     5"   
   60     5#   
   60     5$   
   6/     5%   
   69     5&   
   6D     5'   
   67     5(   
   65     5)   
   65     5*   
   67     5+   
   64     5,   
   6<     5-   
   65     5.   
   6A      5/   
   60   !  50   
   6   �� 51   
   $��icons/Access.png��6#   $
��Fill=�6=   �� 52   
   $��icons/openBop.png��6!   $
��Fill=�6<   �� 53   
   $��icons/openAop.png��6!   $
��Fill=�6<   �� 54   
   $��icons/saveAop.png��6!   $
��Fill=�6<   �� 55   
   -   5        $��icons/saveBop.png��55   !   $
��Fill=�6<   6K   �� 56   
   $��icons/podu.png��6!   $
��Fill=�69   �� 57   
   $��icons/podu_l.png��6&   $
��Fill=�6@   �� 58   
   $��icons/quxian.png��6#   $
��Fill=�6=   �� 59   
   $��icons/quxian_l.png��6(   $
��Fill=�6D   �� 5:   
   $��icons/biaoji.png��6#   $
��Fill=�6=   �� 5;   
   $��icons/biaoji_l.png��6(   $
��Fill=�6D   �� 5<   
   $��icons/Checkdatasign.png��6*   $
��Fill=�6K   �� 5=   
   $��icons/checklogic.png��6'   $
��Fill=�6E   �� 5>   
   $��icons/up.png��6   $
��Fill=�65   �� 5?   
   $��icons/down.png��6!   $
��Fill=�69   �� 5@   
   $��icons/both.png��6!   $
��Fill=�69   �� 5A   
   $��icons/outputresult.png��6#   $
��Fill=�6C   �� 5B   
   $��icons/title.png��6"   $
��Fill=�6;   �� 5C   
   $��icons/newwork.png��6$   $
��Fill=�6?   �� 5D   
   $��icons/curve.png��6"   $
��Fill=�6;   �� 5E   
   $��icons/station.png��6$   $
��Fill=�6?   �� 5F   
   $��icons/Line.png��6!   $
��Fill=�69   �� 5G   
   $��icons/rule.png��6!   $
��Fill=�69   �� 5H   
   $��icons/singleup.png��6%   $
��Fill=�6A   �� 5I   
   $��icons/singledown.png��6'   $
��Fill=�6E   �� 5J   
   $��icons/stationsigle.png��6*   $
��Fill=�6J   �� 5K   
   $ ��icons/exportsignaldata.png��6-   $
��Fill=�6Q   �� 5L   
   $��icons/cdl.png��6!   $
��Fill=�68   �� 5M   
   $��icons/Automatic.png��6"   $
��Fill=�6?   �� 5N   
   $��icons/upway.png��6   $
��Fill=�67   �� 5O   
   $��icons/downway.png��6    $
��Fill=�6;   �� 5P   
   $��icons/SaveXH.png��6   $
��Fill=�69   �� 5Q   
   $��icons/exportopng.png��6$   $
��Fill=�6B   �� 5R   
   $��icons/fenduan.png��6!   $
��Fill=�6<   �� 5S   
   $��icons/ModifyDianfx.png��6%   $
��Fill=�6E   �� 5T   
   $��icons/Modifysig.png��6"   $
��Fill=�6?   �� 5U   
   $��icons/DeleteDfx.png��6"   $
��Fill=�6?   �� 5V   
   $��icons/deleteSig.net.png��6"   $
��Fill=�6C   �� 5W   
   $��icons/moveSig.png��6    $
��Fill=�6;   �� 5X   
   $��icons/confirmdiv.png��6#   $
��Fill=�6A   �� 5Y   
   $��icons/InsertDfx.png��6"   $
��Fill=�6?   �� 5Z   
   $��icons/InsertSig.png��6"   $
��Fill=�6?   �� 5[   
   $��icons/saveother.png��6"   $
��Fill=�6?   �� 5\   
   $��icons/opensignals.png��6$   $
��Fill=�6C   �� 5]   
   $��icons/opentrack.png��6"   $��Uniform=�6?   �� 5^   
   $��icons/creatsignals.png��6%   $
��None=�6E   �� 5_   
   $��icons/UmTrain.png��6   $��UniformToFill=�6:   �� 5`   
   $��icons/stop.png��6   $��Uniform=�67   _� 5a   
   .��5b      �  5c      �� 5d      $��25 �6$   �� 5e      $��60 �6$   .+�5g      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�新建数据表��6o   6   �� 5h      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $!�建立一个新的数据表��6`   6t   5i      5j      _� 5k   
   .��5l      �  5m      �� 5n      $��25 �6$   �� 5o      $��60 �6$   .+�5q      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�读取Access数据库��6o   6�   �� 5r      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $*�$读取一个或多个Access数据库��6`   6{   5s      5t      _� 5u   
   .��5v      �  5w      �� 5x      $��25 �6$   �� 5y      $��60 �6$   .+�5{      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�读取原始数据��6o   6�   �� 5|      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $*�$读取需用于处理的原始数据��6`   6w   5}      5~      _� 5   
   .��5�      �  5�      �� 5�      $��25 �6$   �� 5�      $��60 �6$   .+�5�      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�读取处理后数据��6o   6�   �� 5�      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $$�读取处理后的原始数据��6`   6u   5�      5�      _� 5�   
   .��5�      �  5�      �� 5�      $��25 �6$   �� 5�      $��60 �6$   .+�5�      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�保存原始数据��6o   6�   �� 5�      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $'�!保存用于处理的原始数据��6`   6v   5�      5�      _� 5�   
   .��5�      �  5�      �� 5�      $��25 �6$   �� 5�      $��60 �6$   .+�5�      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�保存处理后的数据��6o   6�   �� 5�      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $$�保存处理后的原始数据��6`   6u   5�      5�      _� 5�   
   .��5�      �  5�      �� 5�      $��25 �6$   �� 5�      $��60 �6$   .+�5�      �� $��0��6   $
��Left=�6)   $��12(�6E   $��	ExtraBold"�6S   �� 6k   $�输出数据图形��6o   6�   �� 5�      $��1��6   $	��Top=�6)   $��15(�6B   $��5q�6P   �� 6\   $*�$将处理后的数据输出为图形��6`   6w   5�      5�      �� 5�   
   .R�5�      � $
 grid��6   $
��-1,0q�6!    5�      �� 5�      $��10 �6$   �� 5�      $��30 �6$   �� 5�      $��10 �6$    5�      �� 5�      $��1 �6'   �� 5�      $��* �6'   �� 5�      $��1 �6'    5�      �� 5�      .+�5�      �� $��12(�6$   $
��Bold"�62   $ 13.333��6�   ��6D   �� 8	   $
 OneWay=�6�   5�      .+�5�      (� $ DeTailViewbox��6   $
 Fill=�61   $��0��6@   $��3��6P   $��0��6d   $��2��6q   .��5�      �� $ canvas��6   $��40��6-   ��69   �� 8	  $
 OneWay=�.+�5�      F� $��50��6o  $ Square=�6�  ��6   �� $ 4��8	  $
 OneWay=���6�   �� $ 3��8	  $
 OneWay=���6�   �� *ActualWidth+$ canvas�� 6  �� 8	  $
 OneTime=���6{  �� $ 2��8	  $
 OneWay=���6�  �� $ 0��8	  $
 OneWay=� 6P  �� $ 1��8	  $
 OneWay=�6�  5�      5�      F� 5�      ���� � �� �    @��	$��0q�6-   $
 Fill=�68   
�������$��0��6U   $
��Left=�6e   $��1��6�   
���   �$ Square=�6�   $��3��6  $��3��6  $y�Visible=�6(   6�   �� 8	  $
 OneTime=�z�5�      V� 5�      .~�5�      �� �� 5�      $ -0.01Q�6,   �� 5�      T� 5�      $ 0.001Q�61   5�      5�      �� 5�      $��3��6   $��0��60   $��0q�6�   $��2��6�   $
��Wrap=�6�   $��9.333(�6�   $	��Top=�6�   $��10��6�   ��6@   �� 8	 	 $
 OneWay=���6  �� 8	 
 $
 OneWay=��� 5�      $��3��6   $��0��60   $��Center=�6@   $��0q�6]   $��0��6h   $
��Wrap=�6u   $��9.333(�6�   $	��Top=�6�   $��10��6�   $��2��6  ��6�   �� 8	  $
 OneWay=�6  5�      5�      �� 5�   
   .R�5�      � $��60��6    5�      #  5�      . 5�      %  DoubleClickEventToCommand��60   5�       5�      �� 5�      $��30 �6'   �� 5�      $	��100 �6'   �� 5�      $��* �6'   .+�5�      �� $ ShowDataCheckBox��6   $��Center=�65   $��Center=�6P   $ 0.5,0.5&�6m    6�   �� *ShowDataProperty+z�5�      V� 5�      .~�5�      �� $	 1.5Q�6-   $	 1.5Q�6:   �� 5�      �� 5�      T� 5�      5�       5�      #  5�      $ Click��6)   . 5�      %  CheckChangedEventToCommand�� 6X   �� *ShowDataChangedCommand+$
 OneWay=�5�      5�      �� 5�      $ 	TypeLabel��6   $��1��6P   $
��4,10q�6`   $��Stretch=�6n   $��Center=�6�   $��Center=�6�   ��6+   �� *TypeNameProperty+6�   �� 5�      $  DetailListBox��6   ��.  $��2��6W   #��� Z 6g   $��0q�6�   #��� X 6�   $��26.667(�6  $
��Bold"�6   ��6�   �� *DataCollection+! 62  �� *CurrentDataProperty+" 6_  �� *SelectedIndex+$
 TwoWay=�# 6�  �� *	TypeNum+8	   $ 5�      �� 5�      y�6&   �� *	TypeNum+8	  .@�5�      m� $��插入信号机��6'   $% InsertXH��6�   y�66   �� *QjMenuVisibility+& 6^   �� *	ExCommand+$
 OneWay=�{�6�   �� *
IsSingleQJ+m� 5�      $��插入电分相��6'   $% 	InsertDFX��6�   y�66   �� *QjMenuVisibility+& 6^   �� *	ExCommand+$
 OneWay=�{�6�   �� *
IsSingleQJ+m� 5�      $��删除信号机��6'   $% DeleteXH��6�   y�66   �� *XhMenuVisibility+& 6^   �� *	ExCommand+$
 OneWay=�6�   m� 5�      $��移动信号机��6'   $% MoveXH��6�   {�66   �� *CanMoveSignal+y�6Z   �� *XhMenuVisibility+& 6�   �� *	ExCommand+$
 OneWay=�6�   m� 5�      $��调整分区长度��6'   $% AdjustQJ��6�   y�67   �� *QjMenuVisibility+& 6_   �� *	ExCommand+$
 OneWay=�{�6�   �� *IsShowQujian+8	  m� 5�      $��删除电分相��6'   $% 	DeleteDFX��6�   y�66   �� *DfxMenuVisibility+& 6_   �� *	ExCommand+$
 OneWay=�6�   m� 5�      $��编辑电分相��6'   $% 	ModifyDFX��6�   y�66   �� *DfxMenuVisibility+& 6_   �� *	ExCommand+$
 OneWay=�6�   5�       5�      #  5�      $ SelectionChanged��6)   . 5�      %  "SelectionChangedEventToCommand��' .  6\   �� *SelectionChangedCommand+$
 OneWay=�6�   5�      5�      5�      5�      �� 5�   
   .R�5�      $� ( . $) 
Horizontal=�68   $��0q�6Q   5�      �� 5�   
   $* #828790��6-   �� 5   
   "+ ��6+   .�5     �� , � ��#- � ��6+   �� 5     , � ��8-   6,   �� 5     , � ��$- 1q�60   �� 5     , � ��#- � ��6+   �� 5     , � ��$
- Auto=�6K   �� 5     , � ��$
- Auto=�6I   �� 5     , � ��- . 6>   �� 5     , �@��PanningMode$
- Both=�69   �� 5	     , �@��IsFlicksEnabled- .  67   �� 5
     , � ��$- Center=�69   �� 5     , � ��- 5     �� 5     ". ��6&   .R�5     �� $/ Bd��6!   #��z��6-   #��z��6Y   #��z��6�   $0 1q�6�   1 . ._�5     �� |�.  .��5  "   �� #1 z1 61   5     5     2 5     P� 5     3 � {�4 .  . �5  "   �� $5 Bd��6?   #- � ��6O   , � ��5     P� 5     3 �@��
IsGrouping4 . . �5  "   �� , � ��- .  6R   5     5     5     5     5      5      #  5!  
   $ Closing��6   . 5"     %  -   5        ClosedEventToCommand�� 5"  B   �� *(&DatasCollectionProperty.DisposeCommand+$
 OneWay=�5#     6 � Cursor6 5%     �� 5&  
   #	 [ 6   7 �� Path$$7 DatasCollectionProperty.Cursor�6I   8 � DataContext8 5(     �� 5)  
   $
7 Main�6   9 �� Source#9 [  6   .��5+     �  5,  
   �� 5-     $
��191* �6   �� 5.     $
��734* �6    50  
   �� 51     $	��160 �6   �� 52     $��0.483* �6   �� 53     $��5 �6   �� 54     $
��0.5* �6   �� 55     $��30 �6   .+�57  
   �� $��Stretch=�6   $��2��65   $��2��6B   y�6V   �� *'%DatasCollectionProperty.CountProperty+#	 [ &   Fluent.Ribbon&  58  
   $��0��6   $��2��6%   $��Stretch=�69   $	��160��6U   : &  Tabs.: 59     '   Fluent.RibbonTabItem'  ; '  Header$; 文件��6#   < '  
IsSelected< . = '  Groups.= 5:     (   Fluent.RibbonGroupBox(  > (  Header$> 读取数据��6(   $
��Auto��66   .@�5;     )   Fluent.Button)  -   5        ? ) Name$? CreatNewWork��5;  $   @ )  Header$@ 新建数据表��6V   A )  	LargeIcon#A [3 6e   # [Q 6�   )  5<     -   5        $? loadformdatabase��5<  $   $@ 读取数据库��6]   #A � ! 6l   # [R 6�   B J� 	AllowDropB .  6�   )  5=     -   5        $? loadformSourceZip��5=  $   $@ 读取原始数据��6`   #A [" 6p   # [S 6�   B . 6�   )  5>     -   5        $? loadformOpZip��5>  $   $@ 读取处理后数据��6X   #A [# 6i   # [T 6�   B . 6�   5?     (  5@     $> 保存数据��6(   .@�5A     )  -   5        $? SaveSourcedata��5A  $   $@ 保存原始数据��6Z   #A [% 6j   # [U 6�   )  5B     -	   5        $? 
SaveOpdata��5B  $   $@ 保存处理后数据��6R   #A [$ 6c   # [V 6�   )  5C     -
   5        $? ExportToSvg��5C  $   $@ 输出数据��69   #A [A 6G   # � W 6p    5D     #  5E     $ Click��6-   . 5F  "   %  -   5        ClickEventToCommand�� 5F  U   �� *,*DatasCollectionProperty.ExportToSvgCommand+$
 OneWay=�5G     5I     5J     5L     '  5M     $; 数据处理��6#   < . .= 5N     (  $> 打开原始文件��6(   .@�5O     )  -   5        $? 
openpdfile��5O  $   $@ 打开坡度文件��68   #A [& 6H   6n   )  5P     -   5        $? 
openqxfile��5P  $   $@ 打开曲线文件��68   #A [( 6H   6p   )  5Q     -   5        $? 
openbjfile��5Q  $   $@ 打开标记文件��68   #A [* 6H   6p   5R     (  5S     $> 调整数据��6(   .@�5T     )  -   5        $? modifycdldata��5T  $   $@ 核对长短链��6;   #A [< 6J   ��6p   �� *.,DatasCollectionProperty.ModifycdlDataCommand+$
 OneWay=�)  5U     -   5        $? autofitcdldata��5U  $   $@ 自动调整��6<   #A [= 6J   ��6q   �� *(&DatasCollectionProperty.AutoFitCommand+$
 OneWay=�*   Fluent.ToggleButton*  5V     -   5        C * Name$C showcdldata��5V  *   D *  Header$D 显示路段分布��6?   E *  	LargeIcon#E [B 6O   ��6u   �� *(&DatasCollectionProperty.LoadLdhCommand+$
 OneWay=� 6�   �� *0.DatasCollectionProperty.IsLdhMapLoadedProperty+$
 TwoWay=�5W     (  5X     $> 检查数据��6(   .@�5Y     )  -   5        $? checkdatasign��5Y  $   $@ 检查读取标记��6;   #A [, 6K   6z   )  5Z     -   5        $? 
checklogic��5Z  $   $@ 检查数据逻辑��68   #A [- 6H   6t   5[     (  5\     $> 打开合并文件��6(   .@�5]     )  -   5        $? opentemppdfile��5]  $   $@ 打开坡度文件��6<   #A [' 6L   6w   )  5^     -   5        $? opentempqxfile��5^  $   $@ 打开曲线文件��6<   #A [) 6L   6y   )  5_     -   5        $? opentempbjfile��5_  $   $@ 打开标记文件��6<   #A [+ 6L   6y   5`     5a     '  5b     $; 数据转换��6#   < . .= 5c     (  $> 转换为文本文件��6(   .@�5d     )  -   5        $? changeS��5d  $   $@ 转换上行文件��65   #A [. 6E   6i   )  5e     -   5        $? changeX��5e  $   $@ 转换下行文件��65   #A [/ 6E   6k   )  5f     -   5        $? changeSX��5f  $   $@ 转换上下行文件��66   #A [0 6G   6m   5g     (  5h     $> 输出��6(   .@�5i     )  -   5        $? changemerge��5i  $   $@ 输出结果��69   #A [1 6G   6o   )  5j     -   5        $? 	exportoto��5j  $   $@ 输出OT格式��67   #A [M 6G   ��6n   �� *1/DatasCollectionProperty.ExportToOTformatCommand+$
 OneWay=�5k     5l     '  5m     -   5        F ' Name$F ShowDataRibbonTabItem��5m  #   $��Stretch=�6B   $��Stretch=�6`   $; 数据显示��6�   < . .= 5n     (  -   5        G ( Name$G LineDataRibbonGroupBox��5n  (   $> 线路数据��6H   .@�5o     *  -   5        $C LoadPDToggleButton��5o  *   $��线路数据��6F   $ 显示坡度数据��62  $D 线路数据��6C  #E [6 6Q  $	��6,0q�6w   6p   �� *.,DatasCollectionProperty.IsPoduLoadedProperty+$
 TwoWay=�H �� UpdateSourceTrigger$H PropertyChanged=���6�   �� *-+DatasCollectionProperty.LoadPoduDataCommand+$
 OneWay=�6w  *  5p     -   5        $C LoadQXToggleButton��5p  *   $��曲线数据��6F   $ 显示曲线��6  $D 曲线数据��6   #E [4 6.  $	��6,0q�6U   6p   �� *0.DatasCollectionProperty.IsQuXianLoadedProperty+$
 TwoWay=���6�   �� */-DatasCollectionProperty.LoadQuXianDataCommand+$
 OneWay=�{�6b  �� *'%DatasCollectionProperty.containpddata+*  5q     -    5        $C LoadCZToggleButton��5q  *   $��车站数据��6F   $ 显示车站数据��6  $D 车站数据��6   #E [5 6.  $	��6,0q�6W   6p   �� *1/DatasCollectionProperty.IsCheZhanLoadedProperty+$
 TwoWay=���6�   �� *,*DatasCollectionProperty.LoadStationCommand+$
 OneWay=�{�6d  �� *'%DatasCollectionProperty.containpddata+*  5r     -!   5        $C PositionToggleButton��5r  *   $��里程数据��6H   #E [7 6/  $D 里程数据��6U  $ 显示里程数据��6c   6r   �� *1/DatasCollectionProperty.IsLiChengLoadedProperty+$
 TwoWay=�$H Default=���6�   �� *-+DatasCollectionProperty.LoadPositionCommand+$
 OneWay=�{�6t  �� *'%DatasCollectionProperty.containpddata+5s     (  5t     -"   5        $G SingleRibbonGroupBox��5t  (   $> 信号机布置��6F   .@�5u     *  -#   5        $C loadxhdataButton��5u  *   $D 载入上行��6D   #E [? 6R   $��11(�6  ��6w   �� *+)DatasCollectionProperty.LoadXinhaoCommand+$
 OneWay=� 6�   �� *0.DatasCollectionProperty.IsXinhaoLoadedProperty+$
 TwoWay=�{�6"  �� *+)DatasCollectionProperty.CanloadxhProperty+*  5v     -$   5        $C loadxhsdatabutton��5v  *   $D 载入下行��6E   #E [> 6S   ��6v   �� *,*DatasCollectionProperty.LoadXinHaoSCommand+$
 OneWay=� 6�   �� *)'DatasCollectionProperty.IsXinhaoSLoaded+$
 TwoWay=�{�6  �� *+)DatasCollectionProperty.CanloadxhProperty+)  5w     -%   5        $? ModifyStationsignalButton��5w  $   $@ 修改车站信号机��6G   $ 修改车站信号机��6X   #A [: 6�   $
��Auto��6�   ��6j   �� *42DatasCollectionProperty.ModifyStationSignalCommand+$
 OneWay=�{�6�   �� *+)DatasCollectionProperty.CanloadxhProperty+5x     (  5y     -&   5        $G signalDataRibbonGroupBox��5y  (   $> 信号机数据操作��6e   .@�5z     )  -'   5        $? creatnewsignalButton��5z  $   $@ 新建数据��6B   $ 新建信号机数据��6P   #A � N 6�   $
��Auto��6�   ��6b   �� */-DatasCollectionProperty.CreatNewXhdataCommand+$
 OneWay=�6�   )  5{     -(   5        $? opensignalButton��5{  $   $@ 打开数据��6>   $ 打开信号机数据��6L   #A � L 6�   $
��Auto��6�   ��6^   �� *+)DatasCollectionProperty.OpenXhDataCommand+$
 OneWay=�6�   )  5|     -)   5        $? SaveStationsignalButton��5|  $   $@ 保存��6E   $ 保存信号机��6Q   #A [@ 6�   $
��Auto��6�   ��6a   �� *+)DatasCollectionProperty.SaveXhDataCommand+$
 OneWay=�{�6�   �� *)'DatasCollectionProperty.IsXinhaoChanged+)  5}     -*   5        $? SaveothersignalButton��5}  $   $@ 另存��6C   $ 另存��6O   #A � K 6�   $
��Auto��6�   ��6\   �� *75DatasCollectionProperty.SaveXhdataToAntherFileCommand+$
 OneWay=�6�   +   Fluent.SplitButton+  5~     -+   5        I + Name$I exportSplitButton��5~  )   ,   Fluent.DropDownButtonJ ,  	LargeIcon#J [; 6_   K ,  HeaderK 6�   �� *&$DatasCollectionProperty.ExportXhText+L +  CommandL 6�   �� *1/DatasCollectionProperty.ExportSignalDataCommand+$
 OneWay=�.@�5     -   Fluent.MenuItem-  $��分页输出��6*   M m� IsCheckableM . N m� 	IsCheckedN 6K   �� *)'DatasCollectionProperty.exPortTypeIndex+#	 [ $
 OneWay=�& 6�   �� **(DatasCollectionProperty.ManyClickCommand+$
 OneWay=�-  5�     $��单页输出��6*   M . N 6K   �� *)'DatasCollectionProperty.exPortTypeIndex+#	 [ $
 OneWay=�& 6�   �� *,*DatasCollectionProperty.SingleClickCommand+$
 OneWay=�5�     5�     5�     '  5�     -,   5        $F CheckDataRibbonTabItem��5�  #   $��Stretch=�6C   $��Stretch=�6a   $; 检算��6}   < . .= 5�     (  --   5        $G MuRibbonGroupBox��5�  (   $> 动车组检算��6]   .@�5�     )  -.   5        $? BulitTrainbutton��5�  $   $��编辑车辆��6>   $ 编辑车辆数据��6M   $
��Auto��6^   $��5,5,10,5q�6l   #A � O 6�   $@ 编辑车辆��6�   ��6~   �� *.,DatasCollectionProperty.EditTrainInfoCommand+$
 OneWay=�6�   )  5�     -/   5        $? CheckUmDisbutton��5�  $   $��计算制动距离��6>   $ 计算制动距离��6O   $
��Auto��6a   $��5,5,0,5q�6o   #A � P 6�   $@ 计算制动距离��6�   ��6�   �� *31DatasCollectionProperty.CalculetUmBreakDisCommand+$
 OneWay=�6�   5�     5�     '  5�     -0   5        $F ModifyXinhaoRibbonTabItem��5�  #   $; 修改信号机��6a   $��Stretch=�6p   $��Stretch=�6�   < . O '  GroupO 6�   �� $ XinhaoContextualTabGroup��.= 5�     (  -1   5        $G DetailRibbonGroupBox��5�  (   $> 基本信息��6a   $	��159��6o   .@�5�     .   Fluent.TextBox.  -2   5        P . Name$P LiChenTextBox��5�  %   $��Center=�6<   $��20.26��6�   $	��151��6�   Q .  
IsReadOnlyQ . R .  IsUndoEnabledR .  $��Hidden=�6  /   Fluent.RibbonControlS /  Header$S 	里程：��6Q  T .  TextT 6^   �� *B@DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty+#	 [ 6Q  .  5�     -3   5        $P 	LxTextBox��5�  %   $��Center=�68   $��20.26��6�   $	��151��6�   Q . R .  $��Hidden=�6  $S 	类型：��6O  T 6Z   �� *B@DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty+#	 [ 6O  .  5�     -4   5        $P 	BhTextBox��5�  %   $��Center=�68   $��20.26��6�   $	��151��6�   Q . R .  $��Hidden=�6  $S 	编号：��6N  T 6Z   �� *B@DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty+#	 [ 6N  5�     (  5�     -5   5        $G ModifyRibbonGroupBox��5�  (   $	��130��6F   $> 修改��6R   .@�5�     0   Fluent.RibbonToolBar0  $��83��6+   $	��130��67   $��-3,0,0,0q�6C   U 0  LayoutDefinitionsU 5�     *1   $Fluent.RibbonToolBarLayoutDefinition1  5�     V 1  Rows.V 5�  "   2   Fluent.RibbonToolBarRow2  W 2  Children.W 5�  &   +3   %Fluent.RibbonToolBarControlDefinition3  X 3  Target$X distenceSpinner��6L   3  5�  &   $X CommitButton��6L   5�  #   2  5�  "   .W 5�  &   3  $X ModifySignalButton��6L   3  5�  &   $X DeleteSignalButton��6L   5�  #   5�     Y 0  Children.Y 5�     4   Fluent.Spinner4  -6   5        Z 4 Name$Z distenceSpinner��5�  )   [ 4  	Increment$[ 20Q�6B   \ 4  Maximum$
\ 5000Q�6Q   ] 4  Minimum$] -5000Q�6`   ^ /  Size$^ Small=�6p   $��55.743��6�   $��-35,-3,-30,3q�6�   _ 4  Value_ 6�   �� *)'DatasCollectionProperty.MoveingDistence+#	 [ $
 TwoWay=�)  5�     -7   5        $? CommitButton��5�  (   $��Button��6>   $
��Auto��6O   $��23��6]   $��44,-3,-69,3q�6h   $��Stretch=�6}   ` )  Icon#` � G 6�   ��6�   �� *,*DatasCollectionProperty.MoveSignalsCommand+$
 OneWay=�6�   )  5�     -8   5        $? ModifySignalButton��5�  (   $��修改单个信号机��6D   $��20��6V   a )  Size$a Middle=�6b   $
��Left=�6p   $��-24,-5,0,-6q�6�   $ 修改单个信号机��6�   #` � D 6%  ��6�   �� *+)DatasCollectionProperty.MoveXinHaoCommand+$
 OneWay=�6%  )  5�     -9   5        $? DeleteSignalButton��5�  (   $��删除信号机��6D   $��20��6T   $��
10,-5,-8,0q�6`   $��Right=�6t   $ 删除信号机��6�   #` � F 6�   ��6�   �� *-+DatasCollectionProperty.RemoveXinHaoCommand+$
 OneWay=�6�   5�     5�     5�     '  5�     -:   5        $F ModifyQjRibbonTabItem��5�  #   $��Stretch=�6B   $��Stretch=�6`   $; 修改信号机区间��6�   < . O 6�   �� $$ QujianRibbonContextualTabGroup��.= 5�     (  -;   5        $G QjInfoRibbonGroupBox��5�  (   $> 区间数据��6a   .@�5�     .  -<   5        $P QidianTextBox��5�  %   $��30��6<   $	��150��6H   Q . R .  $��Hidden=�6|   $��0,2,0,0q�6�   $S 起点里程：��6�   T 6�   �� *B@DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty+#	 [ .  5�     -=   5        $P LengthTextBox��5�  %   $��30��6<   $	��150��6H   Q . R .  $��Hidden=�6|   $��0,2,0,0q�6�   $S 区间长度：��6�   T 6�   �� *B@DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty+#	 [ 5�     (  5�     ->   5        $G ModifyQjRibbonGroupBox��5�  (   $> 修改数据��6c   $	��250��6q   .@�5�     0  $��0q�6+   $	��242��6?   $��85��6K   U 5�     1  5�     .V 5�  "   2  .W 5�  &   3  $X PartLengthTextBox��6L   3  5�  &   $X 
DivSpinner��6L   3  5�  &   $X 	DivButton��6L   5�  #   2  5�  "   .W 5�  &   3  $X InsertButton��6L   3  5�  &   $X InsertDFXButton��6L   5�  #   5�     .Y 5�     .  -?   5        $P PartLengthTextBox��5�  )   $��	129,0,0,0q�6D   $S 分段长：��6`   $	��100��6n   $��22��6}   $��	0,-22,0,0q�6�   $	��Top=�6�   $
��Left=�6�   $	��100��6�   .T 6  T 5�     `� 5�  "   b `� 	Converter#b [ 6/   .1�5�  &   �� $F7 @DatasCollectionProperty.CurrentDatasProperty.CurrentDataProperty�6.   6x   �� 5�  &   $&7  DatasCollectionProperty.DivParts�6.   6X   5�  #   .T 5�  3   5�     4  5�     -@   5        $Z 
DivSpinner��5�  )   $] 1Q�6=   $��Right=�6I   $��22��6e   $��0,-22.005,34.887,0q�6q   $	��Top=�6�   $��53.243��6�   $\ 50Q�6  _ 6�   �� *" DatasCollectionProperty.DivParts+$
 TwoWay=�6  )  5�     -A   5        $? 	DivButton��5�  (   $��Button��6;   $a Small=�6L   $ 	0.5,0.455&�6Y   $��-18,-21.833,0,0q�6{   $	��Top=�6�   $! 增加相应数量信号机��6�   #` � H 6
  ��6�   �� **(DatasCollectionProperty.AddXinhaoCommand+$
 OneWay=�6
  )  5�     -B   5        $? InsertButton��5�  (   $��插入信号机��6>   $
��Auto��6N   $a Middle=�6\   $��Right=�6j   $��	0,-20,0,0q�6�   $ 插入信号机��6�   #` � J 6�   ��6�   �� *-+DatasCollectionProperty.InsertXinhaoCommand+$
 OneWay=�6�   )  5�     -C   5        $? InsertDFXButton��5�  (   $��插入电分相��6A   $��20��6Q   $a Middle=�6]   $��Right=�6k   $��	0,-20,0,0q�6�   $ 插入电分相��6�   #` � I 6�   ��6�   �� *20DatasCollectionProperty.BeginInsertDianFXCommand+$
 OneWay=�6�   5�     5�     5�     '  5�     -D   5        $F ModifyDfxRibbonTabItem��5�  #   $��Stretch=�6C   $��Stretch=�6a   $; 修改电分相��6�   < . O 6�   �� $ DianFxContextualTabGroup��.= 5�     (  -E   5        $G DfxInfoRibbonGroupBox��5�  (   $> 电分相数据��6G   $
��Auto��6V   $	��170��6d   $��8(�6p   .@�5�     )  -F   5        $? ModifyDianfxButton��5�  $   $��修改电分相��6@   $a Large=�6P   $
��Left=�6]   $��8,4,0,4q�6x   $ 修改电分相��6�   $
��Auto��6�   $
��Auto��6�   $��10.667(�6�   #A � C 6=  $@ 修改电分相��6h  ��6�   �� */-DatasCollectionProperty.BeginModifyDFXCommand+$
 OneWay=�6h  )  5�     -G   5        $? DeleteDianfxButton��5�  $   $��删除电分相��6@   $a Large=�6P   $��Right=�6]   $��13,4,0,6q�6y   $ 删除电分相��6�   $
��Auto��6�   $
��Auto��6�   $��10.667(�6�   #A � E 6  $@ 删除电分相��6:  ��6�   �� **(DatasCollectionProperty.DeleteDFXCommand+$
 OneWay=�6:  5�     5�     c &  ContextualGroupsc 5�     %5   Fluent.RibbonContextualTabGroup5  5�     -H   5        d 5 Name$$d QujianRibbonContextualTabGroup��5�  2   e 5  Header$e 信号机区间��6Z   ���	#FF2F3BF3���	#FF59F323y�6�   �� *;9DatasCollectionProperty.CurrentDatasProperty.IsShowQujian+#	 [ 5  5�     -I   5        $d XinhaoContextualTabGroup��5�  2   $e 信号机上下文��6T   ���	#FF10E01D���	#FFD6EB16y�6�   �� *<:DatasCollectionProperty.CurrentDatasProperty.IsShowSignale+#	 [ 5  5�     -J   5        $d DianFxContextualTabGroup��5�  2   $e 电分相上下文��6T   ���	#FFEF3A9E���	#FFD24FF1y�6�   �� *;9DatasCollectionProperty.CurrentDatasProperty.IsShowDianFX+#	 [ 5�     � 5�  
   -K   5        f �Name$f infoboxpanel��5�  !   $��1��6   g � Orientation$g 
Horizontal=�67   $��Stretch=�5�     $	��Top=�6%   B . $	��435��5�     $��2��6%   $��3��69   �� 5�  
   -L   5        $  DataLlistBox��5�     $��8,0,-8,0q�6(   $��3��6:   #��� Y 6�   $��2��6�   ��6G   �� *)'DatasCollectionProperty.DatasCollection+y�6�   �� *'%DatasCollectionProperty.CountProperty+#	 [ ! 6M  �� *.,DatasCollectionProperty.CurrentDatasProperty+$
 TwoWay=�$H Default=�" 6�  �� *'%DatasCollectionProperty.SelectedIndex+$
 TwoWay=�� 5�  
   -M   5        $ 	statugrid��5�     $��4��6   
����·�$��2��6I    5�     �� 5�     $��4* �6#   �� 5�     $��4* �6#   �� 5�     $��4* �6#   .+�5�     �� -N   5        $ ProcessLabel��5�     $�� ��6*   $y�Visible=�65   $��3q�6K   $��0��6V   $��2��6f   $��9(�6z   $	��Top=�6�   $
��Left=�6�   � 5�     -O   5        h �Name$h ProgressBar��5�     i � Minimum$i 0Q�6/   $��3q�6;   $��2��6F   $y�Hidden=�6V   $��15��6j   $��Right=�6y   $	��100��6�   $	��150��6�   5�     5�     5�     