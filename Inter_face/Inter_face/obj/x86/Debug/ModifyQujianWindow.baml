   M S B A M L   `   `   `  ����C  ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel  5   	   ;"clr-namespace:Inter_face.CovertersInter_face.Coverters  5      'clr-namespace:Inter_face
Inter_face  5   	   #    Inter_face.ModifyQujianWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1vm"clr-namespace:Inter_face.ViewModel     5   	   CInter_face_Coverters"clr-namespace:Inter_face.Coverters     5      *localclr-namespace:Inter_face     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     60   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6m   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6�   d `GalaSoft.MvvmLight.Extras, Version=5.2.0.37224, Culture=neutral, PublicKeyToken=f46ff315b1088208-Custom http://www.galasoft.ch/mvvmlight  6�   -   5          �Name$  ModifyQujianWindow1��5   :   � Title$ 修改区间��5   	   $��233.5��6   $��582.425��6%    � 
ResizeMode$ NoResize=�65    � 	Resources 5      0  *Inter_face.ViewModel.ModifyQujianViewModel #  ModifyQujianViewModelDataSource6  0Inter_face.Coverters.FloatToEndPositionConverter  FloatToEndPositionConverter6  0Inter_face.Coverters.FloatToStartStringConverter  FloatToStartStringConverter.  (Inter_face.Coverters.LenToPart1Converter  LenToPart1Converter.  (Inter_face.Coverters.LenToPart2Converter  LenToPart2Converter1  +Inter_face.Coverters.PosToPart1LenConverter  PosToPart1LenConverter1  +Inter_face.Coverters.PosToPart2LenConverter  PosToPart2LenConverter1  +Inter_face.Coverters.ValueToLengthConverter  ValueToLengthConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �       5	   
   6#     5
   
   6;     5   
   6;     5   
   63     5   
   63     5   
   66     5   
   66     5   
   66   5       � DataContext 5      �� 5   
    �� Mode$ OneWay=�6    �� Source# [  6    .	  (System.Windows.Interactivity.Interaction 	  Triggers 5      /
  )System.Windows.Interactivity.EventTrigger
  5   
    
  	EventName$ Closed��6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5      /  )GalaSoft.MvvmLight.Command.EventToCommand  -   5        ClosedEventToCommand��
   Command
 5   B   �� *unregistCommand+$ OneWay=�5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��100 �6   �� 5      $	��100 �6    � RowDefinitions 5    
   �� 5!      $��0.75* �6   �� 5"      $��0.25* �6   .+�5$   
   � -   5         �Name$ 
LayoutRoot��5$      $��3��6#    5%      �� 5&      $
��0.3* �6    �� 5'      $��0.40* �6    �� 5(      $
��0.3* �6     5*      �� 5+      $
��0.1* �6#   �� 5,      $
��0.8* �6#   �� 5-      $
��0.1* �6#   .+�5/      �� -   5         ��Name$ 	DivSlider��5/      $��1��6(   $��0q�68   $��1��6C    � SmallChange$ 1Q�6m    � LargeChange$	 100Q�6}   $��Center=�6�    � Minimum 6P   �� *
MinValue+ � Value 6�   �� *CurrentPosition+$ TwoWay=� � Maximum 6�   �� *
MaxValue+ 50      
  51      $ ValueChanged��6%   .	 52        -   5        ValueChangedEventToCommand��
 52   T   �� *ValueChangedCommand+$ OneWay=�53      55      �� 56      -   5         ��Name$ 
StartLabel��56      $��0q�6(   $��1��63   $��Center=�6@   $��Center=�6[   ��6x   �� *StartPosition+�� 57      -   5        $ EndLabel��57      $��2��6&   $��0q�66   $��1��6A   $��Center=�6N   $��Center=�6k   ��6�   �� *EndPosition+� 58      -   5        $ DivGrid��58      $��1��6$   $��0q�64    59      �� 5:      ��5;      `� 5<       `� 	Converter# [ 6+   .1�5=   "   ��  �� Path$ MinValue�6*   �� 5>   "   $ MaxValue�6*   �� 5?   "   $ CurrentPosition�6*   5@      5B      �� 5C      $��5 �6'   �� 5D      ��5E      `� 5F      # [ 6+   .1�5G   "   �� $ MinValue�6*   �� 5H   "   $ MaxValue�6*   �� 5I   "   $ CurrentPosition�6*   5J      5L       5N      �� 5O      $��0.196* �6$   �� 5P      $��0.471* �6$   �� 5Q      $��5 �6$   �� 5R      $��0.333* �6$   .+�5T      �� $��0q�6   $��Center=�6#   $��Center=�6>   $��2��6b   .��5U      `� # [ 6#   .1�5V      �� $ MinValue�6"   �� 5W      $ MaxValue�6"   �� 5X      $ CurrentPosition�6"   5Y      5Z      �� 5[      -	   5        $ ParttwoLabel��5[      $��2��6.   $��0q�6>   $��Center=�6I   $��Center=�6d   $��2��6�   .��5\      `� # [ 6#   .1�5]      �� $ MinValue�6"   �� 5^      $ MaxValue�6"   �� 5_      $ CurrentPosition�6"   5`      5a      �� 5b      -
   5        $ Hlabel��5b      $�� ��6(   $��2��63   
���   �$��3��6m   �� 5c      -   5        $ SLabel��5c      $�� ��6(   $��1��63   $��3��6C   #��� ��6n   $��0,0,0,0q�6�   $��1��6�   5d      �� 5e      -   5         ��Name$ 
LenTextBox��5e      $��1��6*   $��0q�6:   $��2��6E    �� TextWrapping$
 Wrap=�6R   $��Center=�6f   $��Center=�6�   $	��100��6�   $��Center=�6�   .��6�   .��5f      `� # [ 6    `� Mode$ TwoWay=�6S    `� UpdateSourceTrigger$ PropertyChanged=�6a   .1�5g      �� $ StartPosition�6   �� 5h      $ CurrentPosition�6   5i      5j      �� 5k      -   5        $ LcLabel��5k      $��	-55,4,0,4q�6%   $��Center=�68   $��2��6x   $��1��6�   $
��Left=�6�   $	��140��6�   ��6Z   �� *	CurrentLC+6�   5l      �� 5m   
   -   5         ��Name$ OkButton��5m      $��确定��6#   $��1��60   $	��4,9q�6@   $��1��6M   ��6Z   �� *InsertCommand+$ OneWay=��� 5n   
   -   5        $ CancelButton��5n      $��取消��6'   $��2��64   $	��4,9q�6D   $��1��6Q    �� IsCancel . ��6^   �� *CancelCommand+$ OneWay=�6�   5o      5p      