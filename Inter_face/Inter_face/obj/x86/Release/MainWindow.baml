   M S B A M L   `   `   `  ���� M  IFluent, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent   
Inter_face   Inter_face.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     7Fluent$clr-namespace:Fluent;assembly=Fluent     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  数据转换��$��18(�$	��640��$	��933�� � WindowState$ 	Maximized=� � Icon$+ %/Inter_face;component/icons/title.png�� � 	Resources    
accessicon  lbopicon  laopicon  saopicon  sbopicon  poduicon  podulargeicon  
quxianicon  quxianlargeicon 	 
biaojiicon 
 biaojilargeicon  Checkdatasignicon  checklogicicon 
 upicon  downicon  bothicon  
resulticon  	titleicon  newworkicon  NewSheetTip � RowDefinitions  ReadAccessTip  ReadResourceTip  ReadOperatedTip  SaveResourceTip  SaveOperatedTip��%�  &	        &	 '     &	 O     &	 w     &	 �     &	 �     &	 �     &	     &	 ?    &		 h    &	
 �    &	 �    &	 �    &	     &	 4    &	 Y    &	 ~    &	 �    &	 �    &	 �    &	 �    &	 z    &	 ?    &	     &	 �    �� $��icons/Access.png��$
��Fill=��� $��icons/openBop.png��$
��Fill=��� $��icons/openAop.png��$
��Fill=��� $��icons/saveAop.png��$
��Fill=��� -   $��icons/saveBop.png��$
��Fill=��� $��icons/podu.png��$
��Fill=��� $��icons/podu_l.png��$
��Fill=��� $��icons/quxian.png��$
��Fill=��� $��icons/quxian_l.png��$
��Fill=��� $��icons/biaoji.png��$
��Fill=��� $��icons/biaoji_l.png��$
��Fill=��� $��icons/Checkdatasign.png��$
��Fill=��� $��icons/checklogic.png��$
��Fill=��� $��icons/up.png��$
��Fill=��� $��icons/down.png��$
��Fill=��� $��icons/both.png��$
��Fill=��� $��icons/outputresult.png��$
��Fill=��� $��icons/title.png��$
��Fill=��� $��icons/newwork.png��$
��Fill=�_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�新建数据表�� $��1��$	��Top=�$��15(�$��5q�建立一个新的数据表_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�读取Access数据库�� $��1��$	��Top=�$��15(�$��5q�&$读取一个或多个Access数据库_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�读取原始数据�� $��1��$	��Top=�$��15(�$��5q�&$读取需用于处理的原始数据_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�读取处理后数据�� $��1��$	��Top=�$��15(�$��5q� 读取处理后的原始数据_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�保存原始数据�� $��1��$	��Top=�$��15(�$��5q�#!保存用于处理的原始数据_� .���  �� $��25 ��� $��60 �.+��� $��0��$
��Left=�$��12(�$��	ExtraBold"�保存处理后的数据�� $��1��$	��Top=�$��15(�$��5q� 保存处理后的原始数据.���  �� $	��137 ��� $��* ��� $��30 �.+�   Fluent.Ribbon     SelectedTabIndex$ 0��$��0��   Tabs.    Fluent.RibbonTabItem     Header$ 文件��   Groups.    Fluent.RibbonGroupBox  	   Header$	 读取数据��.@�   Fluent.Button  -   $��CreatNewWork��
   Header$
 新建数据表��   	LargeIcon# [  � ToolTip# [   -   $��loadformdatabase��$
 读取数据库��# [  # [   -   $��loadformSourceZip��$
 读取原始数据��# [ # [   -   $��loadformOpZip��$
 读取处理后数据��# [ # [   $	 保存数据��.@�  -   $��SaveSourcedata��$
 保存原始数据��# [ # [   -   $��
SaveOpdata��$
 保存处理后数据��# [ # [   $ 数据处理��.   $	 打开原始文件��.@�  -   $��
openpdfile��$
 打开坡度文件��# [   -	   $��
openqxfile��$
 打开曲线文件��# [   -
   $��
openbjfile��$
 打开标记文件��# [	   $	 检查文件数据��.@�  -   $��checkdatasign��$
 检查数据标记��# [   -   $��
checklogic��$
 检查数据逻辑��# [   $	 打开合并文件��.@�  -   $��opentemppdfile��$
 打开坡度文件��# [   -   $��opentempqxfile��$
 打开曲线文件��# [   -   $��opentempbjfile��$
 打开标记文件��# [
   $ 数据转换��.   $	 转换为文本文件��.@�  -   $��changeS��$
 转换上行文件��# [   -   $��changeX��$
 转换下行文件��# [   -   $��changeSX��$
 转换上下行文件��# [   $	 输出��.@�  -   $��changemerge��$
 输出结果��# [ � -   $��infoboxpanel��$��1�� � Orientation$ 
Horizontal=�$��Stretch=�$��Stretch=�� -   $��	statugrid��$��2��
����·� � ColumnDefinitions �� $��4* ��� $��4* ��� $��4* �.+��� -   $��ProcessLabel��$�� ��$y�Visible=�$��3q�$��0��$��2��$��9(�$	��Top=�$
��Left=�� -   $��ProgressBar�� � Minimum$ 0Q�$��3q�$��2��$y�Hidden=�$��15��$��Right=�$	��100��$	��150��