   M S B A M L   `   `   `  ����C  ?joindata, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Inter_face.ViewModelInter_face.ViewModel  5      ;"clr-namespace:Inter_face.CovertersInter_face.Coverters  5      $    Inter_face.ModifyCdldataWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1vm"clr-namespace:Inter_face.ViewModel     5      CInter_face_Coverters"clr-namespace:Inter_face.Coverters     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6B   d `GalaSoft.MvvmLight.Extras, Version=5.2.0.37224, Culture=neutral, PublicKeyToken=f46ff315b1088208-Custom http://www.galasoft.ch/mvvmlight  6�   -   5          �Name$  Window��5       � Title$ 修改长短链��5      $	��485��5	      $	��435��6    � WindowStyle$ 
ToolWindow=�6    � 	Resources 5
      6  0Inter_face.Coverters.ClddataToShowStyleConverter   ClddataToShowStyleConverter-   'Inter_face.ViewModel.ModifyCdlViewModel   ModifycdlViewModelDataSource  CdlDataTemplate �Name ��Name �� TextWrapping �� 
IsReadOnly �� 	Converter	 �� Mode��%U  &	        &	      &	 &     7     5   
   6;     5   
   6    �� 5   
   .R�5      � $
 grid��6   $��50��6!   .+�5      �� $ CdlDataTextBlock��6   $��Center=�64   $
 Wrap=�6Q   $��Center=�6�   $��18.667(�6�    . ���	#00000000��6e   �� 8   $	 OneWay=���6�   Z� 6  5      5      5      
 � DataContext
 5      �� 5   
    �� Source# [ 6   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand  -   5        LoadToCommand��   Command 5   ;   �� *LoadDataCommand+$	 OneWay=�5      .��5      � -   5        $ 
LayoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $��0.75* �6   �� 5      $��5 �6   �� 5       $��0.25* �6    � RowDefinitions 5"   
   �� 5#      $��30 �6   �� 5$      $��50 �6   �� 5%      $��50 �6   �� 5&      $��50 �6   �� 5'      $��50 �6   �� 5(      $��50 �6   �� 5)      $��* �6   �� 5*      $��50 �6   �� 5+      $��50 �6   .+�5-   
   �� -   5         ��Name$ 
cdllistbox��5-      $��	8,8,2.7,8q�6&   $��1��69   $��8��6F   #���  6�   $��2��6�   ��6W   �� *CdlCollectionProperty+ �� SelectedIndex 6�   �� *SeletedItem+$	 TwoWay=�6�   �� 5.   
   -   5         ��Name$ UpButton��5.      $��上移��6#   $��3��60   $��8q�6@   $��1��6K   ��6X   �� *UpcdlDataCommand+$	 OneWay=��� 5/   
   -   5        $ 
DownButton��5/      $��下移��6%   $��3��62   $��8q�6B   $��2��6M   ��6Z   �� *DowncdlDataCommand+$	 OneWay=��� 50   
   -   5        $ DeleteButton��50      $��删除��6'   $��3��64   $��8q�6D   $��3��6O   ��6\   �� *DeletedataCommand+$	 OneWay=��� 51   
   -   5        $ ModifyButton��51      $��修改��6'   $��3��64   $��8q�6D   $��4��6O   ��6\   �� *ChangeCdlCommand+$	 OneWay=��� 52   
   -	   5        $ 	AddButton��52      $��插入��6$   $��3��61   $��8q�6A   $��5��6L   ��6Y   �� *InsertCdlDataCommand+$	 OneWay=��� 53   
   -
   5        $ 
SaveButton��53      $��保存��6%   $��3��62   $��8q�6B   $��7��6M   ��6Z   �� *SaveDataCommand+$	 OneWay=��� 54   
   -   5        $ CancelButton��54      $��取消��6'   $��3��64   $��8q�6D   $��8��6O   �� 55   
   $��2��6   $��Stretch=�6'   $��9��6E   56      57      