   M S B A M L   `   `   `  ����C  ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel  5      ;"clr-namespace:Inter_face.CovertersInter_face.Coverters  5          Inter_face.StationWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1vm"clr-namespace:Inter_face.ViewModel     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      CInter_face_Coverters"clr-namespace:Inter_face.Coverters     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6B   d `GalaSoft.MvvmLight.Extras, Version=5.2.0.37224, Culture=neutral, PublicKeyToken=f46ff315b1088208-Custom http://www.galasoft.ch/mvvmlight  6�   -   5          �Name$  Window��5       � Title$ 更改车站信号机��5	      $	��520��6    � WindowStartupLocation$ CenterOwner=�6    � 
ResizeMode$ NoResize=�6B    � 	Resources 5
      /  )Inter_face.Coverters.StringToIntConverter   StringToIntConverter1   +Inter_face.ViewModel.StationSignalViewModel  StationSignalViewModel  StationSignalDataTemplate � ColumnDefinitions � RowDefinitions ��Name �Name	 ��Name
 �� TextWrapping �� Mode �� 	Converter �� SelectionOpacity ��Name �� SelectedValue �� SelectedIndex.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions/  )GalaSoft.MvvmLight.Command.EventToCommand   Command   CommandParameter ��Name d� 	IsChecked��%�  &	        &	      &	 &     7   7   7   7     5      61     5      6!   �� 5      .R�5      � $
��Auto��6   $��60��6    5   	   �� 5   
   $	��100 �6   �� 5   
   $	��100 �6   �� 5   
   $	��290 �6    5   	   �� 5   
   $��30 �6   �� 5   
   $��30 �6   .+�5   	   �� $ StationNameLabel��6   $��4q�6Q   $��Center=�6\   $��2��6w   ��6)   �� *StationNameProperty+6w   �� 5   	   $ PosLabel��6   $��1��6M   $��4q�6]   $��2��6h   $��Center=�6y   ��6!   �� *StationPositionProperty+6y   � 5   	   $ InGrid��6   $��2��6   $	��0,1q�6.    5   
   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��50 �6   �� 5      $��20 �6   �� 5       $��50 �6   �� 5!      $��50 �6   �� 5"      $��30 �6   .+�5$   
   �� $	 	InTextBox��6   $
��Auto��6%   $��0q�63   $

 Wrap=�6>   $��Stretch=�6�   $��12(�6�   $
��Left=�6�   $��Center=�6  ��6R   �� *InSignalProperty.Mark+$ TwoWay=�{�6�   �� *InSignalProperty.IsSelected+6  �� 5%   
   $ InLabel��6   $��1��6N   $��0q�6^   $��8(�6i   $��Center=�6v   $
��Auto��6�   $
��10,5q�6�   ��6!   �� *InSignalProperty.Guanhao+6�   �� 5&   
   $	 InP1TextBox��6   $��2��6'   $��0q�67   $

 Wrap=�6B   $��12(�6�   $
��Left=�6  $��Center=�6$  ��6V   �� *InSignalProperty.PartImenber+8   $ TwoWay=�{�6�   �� *InSignalProperty.IsSelected+6$  �� 5'   
   $ InPlus1Label��6   $��+��6&   $��3��62   $��0q�6B   $��12(�6M   $��Center=�6[   $��2,5,2,3q�6x   $��Center=�6�   �� 5(   
   $	 InP2Text��6   $��4��6$   $��0q�64   $

 Wrap=�6?   $
 -2.6Q�6�   $��Center=�6  ��6S   �� *InSignalProperty.PartII+8  $ TwoWay=�{�6�   �� *InSignalProperty.IsSelected+6  �� 5)   
   $ 
InComboBox��6   $��5��6(   $��4,0,0,0q�68   $
��Auto��6I   $��Stretch=�6V   ��6t   �� *%#InSignalProperty.SecNumbersProperty+ 6�   �� *InSignalProperty.SectionNum+$ OneWayToSource=� 6�   �� * InSignalProperty.SelectedIndex+$ TwoWay=�{�6A  �� *InSignalProperty.IsSelected+ 5*        5+      $ SelectionChanged��6   . 5,        "SelectionChangedEventToCommand�� 6K   �� **(InSignalProperty.SceNumberChangedCommand+$ OneWay=� 6�   �� *InSignalProperty.TypeProperty+5-      5/      �� 50   
   $ 
InCheckBox��6   $�� ��6'   $��6��62   $��6q�6B   $��Center=�6M   $��Center=�6j    6�   �� *#!InSignalProperty.IsEnableProperty+$ TwoWay=�51   
   � 52   	   $ OutGrid��6   $��2��6   $	��0,1q�6/   $��1��6<    53   
   �� 54      $��50 �6   �� 55      $��30 �6   �� 56      $��50 �6   �� 57      $��20 �6   �� 58      $��50 �6   �� 59      $��50 �6   �� 5:      $��30 �6   .+�5<   
   �� $	 
OutTextBox��6   $
��Auto��6&   $��0q�64   $

 Wrap=�6?   $��Stretch=�6�   $��12(�6�   $
��Left=�6�   $��Center=�6  ��6S   �� *OutSignalProperty.Mark+$ TwoWay=�{�6�   �� *OutSignalProperty.IsSelected+6  �� 5=   
   $ OutLabel��6   $��1��6P   $��0q�6`   $��8(�6k   $��Center=�6x   $
��Auto��6�   $	��8,5q�6�   ��6"   �� *OutSignalProperty.Guanhao+6�   �� 5>   
   $	 OutP1TextBox��6   $��2��6(   $��0q�68   $

 Wrap=�6C   $��12(�6�   $
��Left=�6  $��Center=�6'  ��6W   �� *OutSignalProperty.PartImenber+8  $ TwoWay=�{�6�   �� *OutSignalProperty.IsSelected+6'  �� 5?   
   $ OutPlus1Label��6   $��+��6'   $��3��63   $��0q�6C   $��12(�6N   $��Center=�6\   $��2,5,1,5q�6y   $��Center=�6�   �� 5@   
   $	 	OutP2Text��6   $��4��6%   $��0q�65   $

 Wrap=�6@   $
 -2.6Q�6�   $��Center=�6  ��6T   �� *OutSignalProperty.PartII+8  $ TwoWay=�{�6�   �� *OutSignalProperty.IsSelected+6  �� 5A   
   $ OutComboBox��6   $��5��6)   $��4,0,0,0q�69   $
��Auto��6J   $��Stretch=�6W   ��6u   �� *&$OutSignalProperty.SecNumbersProperty+$ TwoWay=� 6�   �� *OutSignalProperty.SectionNum+$ OneWayToSource=� 6  �� *!OutSignalProperty.SelectedIndex+$ TwoWay=�{�6R  �� *OutSignalProperty.IsSelected+ 5B        5C      $ SelectionChanged��6   . 5D        #SelectionChangedEventToCommand1�� 6L   �� *+)OutSignalProperty.SceNumberChangedCommand+$ OneWay=� 6�   �� * OutSignalProperty.TypeProperty+5E      5G      �� 5H   
   $ outCheckBox��6   $�� ��6(   $��6��63   $��6,6,9,6q�6C    6T   �� *$"OutSignalProperty.IsEnableProperty+$ TwoWay=�5I   
   5J   	   5K      5L       � DataContext 5M      �� 5N   
   $ OneWay=�6    �� Source# [ 6    .��5P      � -   5        $ 
LayoutRoot��5P       5Q      �� 5R      $��0.108* �6   �� 5S      $��0.592* �6   �� 5T      $��0.15* �6   �� 5U      $��0.15* �6    5W      �� 5X      $
��0.9* �6   �� 5Y      $
��0.1* �6   .+�5[      �� -   5         ��Name$ StationSignalListBox��5[      $��4��6*   $��4q�6>   #���  6y   ��6I   �� *StationSignalCollection+ �� SelectedItem 6�   �� *CurrentStationSignal+$ OneWayToSource=��� 5\      -   5         ��Name$ UpdataButton��5\      $��更新��6!   $��3��6.   $��4q�6>   $��1��6I   ��6V   �� *UpDataCommand+$ OneWay=��� 5]      -   5        $ showldhButton��5]      $��L��6"   $��4q�6.   $��1��69   $��26.667(�6F   ��6X   �� *ShowLdhCommand+$ OneWay=�5^      5_      