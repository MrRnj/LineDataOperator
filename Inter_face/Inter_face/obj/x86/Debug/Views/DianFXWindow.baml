   M S B A M L   `   `   `  ����M  IFluent, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5      C ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel 5      ;"clr-namespace:Inter_face.CovertersInter_face.Coverters 5   �      Inter_face.DianFXWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      7Fluent$clr-namespace:Fluent;assembly=Fluent     5      1vm"clr-namespace:Inter_face.ViewModel     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6?   CInter_face_Coverters"clr-namespace:Inter_face.Coverters     6�   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6�   d `GalaSoft.MvvmLight.Extras, Version=5.2.0.37224, Culture=neutral, PublicKeyToken=f46ff315b1088208-Custom http://www.galasoft.ch/mvvmlight  6  -   5          �Name$  Window��5       � Title$ DianFXWindow��5	      $
��1100��5
      $	��182��6   $
��1100��6   $	��200��6,    � 	Resources 5      0 *Inter_face.Coverters.ChartToColorConverter   ChartToColorConverter+ %Inter_face.Coverters.PartIntConverter  PartIntConverter/ )Inter_face.Coverters.StringToIntConverter  StringToIntConverter0  *Inter_face.ViewModel.UpdataDianFXViewModel # UpdataDianFXViewModelDataSource��%t   &	        &	      &	 &     &	 9       5   
   65     5   
   60     5   
   64     5   
   6#   5       � DataContext 5      �� 5   
    �� Mode$ OneWay=�6    �� Source# [ 6    .��5      � -   5         �Name$ 
LayoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��0.35* �6   �� 5      $��0.05* �6   �� 5      $��0.20* �6   �� 5      $��0.05* �6   �� 5      $��0.35* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5       $��* �6   �� 5!      $��40 �6   .+�5#   
   � -   5        $ OpGrid��5#      $��5��6   $��2��6/    5$      �� 5%      $	��60* �6#   �� 5&      $	��40* �6#   .+�5(      �� -   5        	 ��Name$	 CancelButton��5(      $��取消��6+   $��0q�68   $	��100��6C   $��32��6O   $��Center=�6[   $��1��6w   ��6�   �� *CancelCommand+$ OneWay=��� 5)      -   5        $	 OkButton��5)      $��确定��6'   $��0,4,8,4q�64   $	��100��6t   $��Right=�6�   $��32��6�   $��0��6�   ��6E   �� *CommitCommand+$ OneWay=�6�   5*      � 5+   
   -   5        $ LeftGrid��5+      $��1��6!   $��1��61   $��0q�6>    5,      �� 5-      $	��35* �6#   �� 5.      $	��30* �6#   �� 5/      $	��35* �6#   .+�51      �� -   5        
 ��Name$
 StartTextBlock��51      $	��0,8q�60   $
��Wrap=�6=    �� TextAlignment$ Center=�6o   $��13.333(�6�   ��6Q   �� *StartShowPos+6�   �� 52      -   5        $
 LeftEdgeTextBlock��52      $��2��63   $	��0,8q�6C   $
��Wrap=�6P   $ Center=�6d   $��13.333(�6�   ��6{   �� *LeftEdgePos+6�   �� 53      -	   5        $
 StartDisTextBlock��53      $��1��63   $
��10,4q�6C   $
��Wrap=�6Q   $ Center=�6   $��13.333(�6�   ��6e   �� *
StartDis+6�   54      � 55   
   -
   5        $ MidGrid��55      $��3��6    $��1��60   $��0q�6=    56      �� 57      $
��0.3* �6#   �� 58      $
��0.3* �6#   �� 59      $
��0.1* �6#   �� 5:      $
��0.3* �6#   .+�5<      �� -   5         ��Name$ PartITextBox��5<      $��1��6,   $
��8,15q�6<    �� TextWrapping$
 Wrap=�6J   $��Center=�6^   $��Center=�6�   $��16(�6�   ���	#FF0AF718��6�   �� *PartI+ �� 	Converter# [ $ TwoWay=�6  �� 5=      -   5        $ PartIITextBox��5=      $��3��6-   $
��8,15q�6=   $
 Wrap=�6K   $��Center=�6�   $��Center=�6�   $��16(�6�   ���	#FF0AF718��6_   �� *PartII+# [ $ TwoWay=�6  �� 5>      -   5        $
 PlusTextBlock��5>      $��2��6/   $��0,21.75,0,15.75q�6?   $
��Wrap=�6X   $��+��6l   $��Center=�6u   $��Center=�6�   $��18.667(�6�   �� 5?      -   5        $
 HatTextBlock��5?      $	��8,1q�6.   $
��Wrap=�6;   $��Center=�6d   $ Center=�6   $��16(�6�   ��6O   �� *Hat+6�   5@      � 5A   
   -   5        $ 	RightGrid��5A      $��5��6"   $��1��62   $��0q�6?    5B      �� 5C      $	��35* �6#   �� 5D      $	��30* �6#   �� 5E      $	��35* �6#   .+�5G      �� -   5        $
 RightEdgeTextBlock��5G      $��0��64   $	��0,8q�6D   $
��Wrap=�6Q   $ Center=�6e   $��13.333(�6�   ��6|   �� *RightEdgePos+6�   �� 5H      -   5        $
 EndTextBlock��5H      $	��0,8q�6.   $��2��6;   $
��Wrap=�6K   $ Center=�6{   $��13.333(�6�   ��6_   �� *
EndShowPos+6�   �� 5I      -   5        $
 EndDisTextBlock��5I      $��1��61   $
��10,4q�6A   $
��Wrap=�6O   $ Center=�6{   $��13.333(�6�   ��6c   �� *Enddis+6�   5J      � 5K   
   -   5        $ NameGrid��5K      $��3��6!    5L      �� 5M      $	��30* �6#   �� 5N      $	��70* �6#   .+�5P      �� -   5        $ NameTextBox��5P      $��1��6+   $��4q�6;   $��0��6F   $
 Wrap=�6S   ���	#FF0AF718$��Right=�6�   $	��136��6�   $��Center=�6�   $��Center=�6�   ��6   �� *Name+$ TwoWay=�6�   �� 5Q      -   5        $
 NametextBlock��5Q      $��4q�6/   $
��Wrap=�6:   $��分相名：��6N   $��Center=�6Z   $��Center=�6w   5R      �� 5S   
   -   5        $ LeftEdgeDisTextBox��5S      $��2��6.   $��4q�6>   $
 Wrap=�6I   $��Center=�6]   $��45��6�   $��21.8366666666667��6�   $��Center=�6�   $��Bottom=�6  ���	#FF0AF718��6z   �� *	LeftDis+# [ $ TwoWay=�69  �� 5T   
   -   5        $ RightEdgeDisTextBox��5T      $��4��6/   $��4q�6?   $
 Wrap=�6J   $��Center=�6^   $��45��6�   $��21.8366666666667��6�   $��Center=�6�   $��Bottom=�6  ���	#FF0AF718��6{   �� *
RightDis+# [ $ TwoWay=�6:  �� 5U   
   -   5        $
 RightSigntextBlock��5U      $��5��60   $
��Left=�6@   $��16,4,0,5q�6[   $
��Wrap=�6m   $��95.333��6�   $ Center=�6�   $��16(�6�   $
��Bold"�6#  ��6�   �� *	RightSign+��6�   �� *	RightSign+# [  �� 66  �� 6<  �� 6H  6O  �� 5V   
   -   5        $
 LeftSigntextBlock��5V      $��1��6/   $��0,4,16,5q�6?   $
��Wrap=�6Q   $ Center=�6e   $��16(�6|   $��Right=�6�   $��95.333��6�   $
��Bold"�6!  ��6�   �� *
LeftSign+��6�   �� *
LeftSign+# [  �� 64  �� 6:  �� 6F  6M  �� 5W   
   -   5         ��Name$ cdlComboBox��5W      $��4��6(   $��3.8,16,3.8,14.667q�68   $��1��6S   $��Center=�6�   $��Center=�6�   ��6`   �� *
SecArray+ �� SelectedIndex 6�   �� *CurentNumIndex+$ TwoWay=�.  (System.Windows.Interactivity.Interaction   Triggers 5X      /  )System.Windows.Interactivity.EventTrigger  5Y         	EventName$ SelectionChanged��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5Z      /  )GalaSoft.MvvmLight.Command.EventToCommand  -   5        "SelectionChangedEventToCommand��   Command 5Z   T   �� *CheckSecNumCommand+$ OneWay=�5[      5]      5^      5_      