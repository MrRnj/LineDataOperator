   M S B A M L   `   `   `  ����C  ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel  5          Inter_face.ACdlWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1vm"clr-namespace:Inter_face.ViewModel     5      -   5          �Name$  	CdlWindow��5       � Title$ 添加长短链��5      $	��640��5      $��160.5��6    � WindowStyle$ 
ToolWindow=�6    � 	Resources 5	      (   "Inter_face.ViewModel.AcdlViewModel   AcdlViewModel��%   &	          5
   
   6   5       � DataContext 5      �� 5   
    �� Source# [  6   .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5      �� 5      $
��0.7* �6   �� 5      $
��0.3* �6    � ColumnDefinitions 5      �� 5      $
��0.5* �6   �� 5      $��30 �6   �� 5      $
��0.3* �6   �� 5      $
��0.2* �6   .+�5      �� -   5        	 ��Name$	 EquleTextBlock��5      $��1��6&   $
��Wrap=�66   
 �� TextAlignment$
 Center=�6J   $��Center=�6a   $��24(�6|   �� 6�    � Language$ zh-cn�6�   $�=��6�   6�   �� 5      -   5         ��Name$ FrontGroupBox��5      $��	前坐标��6$   .��5      � $��8,8,7,8q�6
    5      �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5       $��20 �6   �� 5!      $
��0.3* �6   .+�5#      �� -   5         ��Name$ Fst_hatTextBox��5#      $
��10,2q�6&   $��0��64   $��Center=�6�   $��Center=�6�   ��6D   �� *	Hat_Front+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5$      -   5        $ Fst_FrontTextBox��5$      $
��10,2q�6(   $��1��66   $��Center=�6�   $��Center=�6�   ��6F   �� *	Fst_Front+$ TwoWay=�$ PropertyChanged=�6�   �� 5%      -   5        $ Fst_AfterTextBox��5%      $
��10,2q�6(   $��3��66   $��Center=�6a   $��Center=�6�   ��6F   �� *	Sec_Front+6�   �� 5&      -   5        $	 FstPlusTextBlock��5&      $��0q�6*   $��2��65   $��+��6E   $��Center=�6N   $
 Center=�6i   $��18.667(�6�   5'      5(      �� 5)      -	   5        $ AfterGroupBox��5)      $��2��6$   $��2��68   $��	后坐标��6H   $��0q�6U   .��5*      � $��8,8,7,8q�6
    5+      �� 5,      $
��0.3* �6   �� 5-      $
��0.3* �6   �� 5.      $��20 �6   �� 5/      $
��0.3* �6   .+�51      �� -
   5        $ Sec_hatTextBox��51      $
��10,2q�6&   $��0��64   $��Center=�6�   $��Center=�6�   ��6D   �� *	Hat_After+$ TwoWay=�$ PropertyChanged=�6�   �� 52      -   5        $ Sec_FrontTextBox��52      $��10,2,2,2q�6(   $��1��6:   $��Center=�6�   $��Center=�6�   ��6J   �� *	Fst_After+$ TwoWay=�$ PropertyChanged=�6�   �� 53      -   5        $ Sec_AfterTextBox��53      $
��10,2q�6(   $��3��66   $��Center=�6�   $��Center=�6�   ��6F   �� *	Sec_After+$ TwoWay=�$ PropertyChanged=�6�   �� 54      -   5        $	 SecPlusTextBlock��54      $��0q�6*   $��2��65   $��+��6E   $��Center=�6N   $
 Center=�6i   $��18.667(�6�   55      56      �� 57      -   5         ��Name$ OkButton��57      $��确定��6   $��3��6*   $��1��6:   $
��10,4q�6G   ��6U   �� *AddCdlCommand+$ OneWay=�58      59      