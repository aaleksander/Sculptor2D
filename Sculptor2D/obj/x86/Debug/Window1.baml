   M S B A M L   `   `   `  ����L  HSculptor2D, Version=1.0.5249.32162, Culture=neutral, PublicKeyToken=null)clr-namespace:DrawLibraryDrawLibrary  5      5clr-namespace:DrawLibrary.ToolsDrawLibrary.Tools  5      9!clr-namespace:DrawLibrary.BrushesDrawLibrary.Brushes  5      /clr-namespace:KeyBindingsWPFKeyBindingsWPF  5          Sculptor2D.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      )cnvclr-namespace:DrawLibrary     5      1toolsclr-namespace:DrawLibrary.Tools     5      5brushes!clr-namespace:DrawLibrary.Brushes     5      .localclr-namespace:KeyBindingsWPF     5        � Title$  
Sculptor2D��5      $	��768��6   $
��1024��6"    � WindowStartupLocation$ CenterScreen=�5	       � UseLayoutRounding .  J� SnapsToDevicePixels .  J� InputBindings 5      $   KeyBindingsWPF.RelayKeyBinding  5      
 �� Key$ Escape��6    �� Command 6)   ��  �� ElementName$ canvas�� �� Path$ EscapeCommand�  5      $ Z��6   	 �� 	Modifiers$	 Controlf�6!    65   �� $ canvas��$ UndoCommand�
 � 	Resources
 5       �� MergedDictionaries �� Source)   #DrawLibrary.Tools.ToolTypeConverter   convToolType,   &DrawLibrary.Brushes.BrushTypeConverter  convBrushType  DreamToolBar �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius a�Name K� IsItemsHost  DreamButton �� ContentStringFormat �� ContentStringFormat �� Triggers P� Property P� Value  myToggleButtonTemplate �Name � Points �� ScaleX �� ScaleY �� 
TargetName  DreamRadioButton �� Padding  ]� 
Conditions! �� Property" �� Value�� 5       5      �� 5      $ ../Styles.xaml?�5      %h  &	        &	      &	 &     &	 �    &	 j    &	 x      5      6     5      6   �� 5      " c�6!   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � �� 5       �� 5!      " c�6"   .R�5"      �� $ Border��6   $ 2��5#      $��1q�5$      ���#8F8F8F
���   �._�5'      ]� .+�5(      a� $ PART_ToolBarPanel��6)    . $��0q�5)      5*      5+      5,      5.      5/      �� 52      " ��6    .�53      ��  � �� 54      �� 55   	   " ��6   .R�56   
   �� $ 5��57      ���#737373$��0q�59      ._�5;      �� #��z��6   #��z��6P   # z 6t   #��z��6�   # z 6�   #��z��6/  5<       5=   
   P� 5>       � {� . . �5?      ��  � �� �	#FF3399FF6*   �� 5@       � ��
 �   �6)   5A      5C      5E      5F      �� 5H      " d�65   .R�5I   
   �� $��31��6   $��31��6   .+�5J      � $ polygon��6   
���   �
��� � �E �   ��� ���  Zb����,��� �J]��� �� �� -1�,z�5L      V� 5M      .~�5N      �� $
 0.30Q�6)   $
 0.30Q�67   5O      5Q      5R       5S   
   P� 5T       � {� .  . �5U      �� $ polygon��6   # � ��6>    � ��5V      P� 5W       �@J�IsMouseOver . . �5X      �� $ polygon��6    � ��
 �� ��6>   5Y      5[      �� 5^      " d�6%   .�5_      ��  � ��# � ��6$   �� 5`       � ��$ 2q�6!   �� 5a       � ��$ 4q�6   �� 5b       � ��$ 40��6   �� 5c       � ��$ 40��6   �� 5d       � ��$ 0q�6)   �� 5e       � �� �#b0b0b06$   �� 5f       � ��
 ���� 6%   �� 5g       � ��$ Center=�6-   �� 5h       � ��$ Center=�6+   �� 5i       � ��$ Center=�64   �� 5j       � ��$ Center=�62   �� 5k       � �� 5l      �� 5m   	   " d�6   .R�5n   
   �� $ 5��5o      ���#737373$��1q�5q      #��z��5r      # z��61    . ._�5s      �� #��z��6   #��z��6P   # z 6t   #��z��6�   # z 6�   #��z��6/  5t       5u   
   P� 5z       �@J�IsMouseOver . . �5{      ��  � �� �	#FF3399FF6*   �� 5|       � �� �	#FFC2E0FF6)   5}      P� 5~       �@J�IsKeyboardFocused . . �5      ��  � �� �	#FF3399FF6*   �� 5�       � �� �	#FFC2E0FF6)   5�      ]� 5�        5�      �� 5�      ! �@J�IsMouseOver" . 6.   �� 5�      ! �@d�	IsChecked$
" TrueY�6,   ./�5�      ��  � �� �	#FF3399FF6*   �� 5�       � �� �	#FF99CCFF6)   5�      ]� 5�        5�      �� 5�      ! �@J�IsKeyboardFocused" . 64   �� 5�      ! �@d�	IsChecked$
" TrueY�6,   ./�5�      ��  � �� �	#FF3399FF6*   �� 5�       � �� �	#FF99CCFF6)   5�      P� 5�       � �� . . �5�      ��  � �� �	#FF3399FF6*   �� 5�       � �� �	#FF99CCFF6)   5�      P� 5�       � {� .  . �5�      �� # � ��6)    � ��5�      5�   
   5�      5�      5�      .��5�      ]� ��. .+�5�      o� $	��Top=�6	   .@�5�      m� $��Файл��6   .@�5�      m� $��Сохранить��6   # m� Command# 6"   �� $ canvas��$ SaveCommand�6]   5�      5�      �� 5�      $��Bottom=�6   .@�5�      �� .��5�      �� -   5        $��	statusBar��5�      5�      5�      `� 5�      $ `� Orientation$$ Vertical=�6   $	��Top=�6'   % `� 
Background% �	#FF8F8F8F$
��Left=�6V   .�5�      c� -   5        $��tray1��5�      #��[ 5�      ���	#FF8F8F8F.@�5�      d� -   5        $��buttonToolPointer��5�   F   & � ToolTip$!& Выбор объектов��6   
' � Tag$' Pointer��68   ( d� 	IsChecked( 5�      �� $ canvas��$
 Tool�) �� Mode$) OneWay=�* �� 	Converter#* [  + �� ConverterParameter$+ Pointer��.��5�      �� $��31��6   $��31��6%   , J� Effect, 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�      -   Opacity$	- 0.3Q�6&   .+�5�      � 
���   �
���   �e �   ��{�(�o�����9�@�.�!��U0�lw��<�0L��.�!��U0��, (�o��, �s���{��s���{�(�o�z�5�   	   V� 5�   
   .~�5�      �� . �� Angle$	. 155Q�6   �� 5�      $
 0.30Q�6   $
 0.30Q�6(   T� 5�      / T� X$/ 13Q�6   0 T� Y$0 15Q�6%   5�      5�   
   5�      5�      d� 5�      -   5        $��buttonToolEditor��5�   9   $!& Редактор точек��6   $' Editor��6,   ( 5�      �� $ canvas��$
 Tool�$) OneWay=�#* [  $+ Editor��.��5�      �� $��31��6   $��31��6%   , 5�        5�      $	- 0.3Q�6'   .+�5�      � 
���   �$��5��6    1 � Points?1 �   �� h�,@��xjV�5F�����n^�D����B� +��ݓ�@�2 �� Left$2 0.499��5�   `   
3 �� Top$3 -1.334��6t   z�5�   	   V� 5�   
   .~�5�      �� $	 0.3Q�6   $	 0.3Q�6'   �� 5�      �� 5�      $. 25.182Q�6   T� 5�      $/ 15Q�6   $0 8Q�6%   5�      5�   	   � 5�      
���   �
�������$��3��6,   ; �   �v���Al�@ �Al�@ (O��v��(O��v���Alz�5�   	   V� 5�   
   .~�5�      �� $. 0Q�6   �� 5�      $	 0.4Q�6   $	 0.4Q�6'   T� 5�      $/ 12Q�6   $0 -8Q�6%   5�      5�   	   � 5�      
���   �
���   �1 �   ��W�a�� l�#�-�9����W�az�5�   	   V� 5�   
   .~�5�      �� $. 90Q�6   �� 5�      $	 0.4Q�6   $	 0.4Q�6'   T� 5�      $/ 25Q�6   $0 6Q�6%   5�      5�   	   5�      5�      d� 5�      -   5        $��buttonToolLine��5�   C   $!& Рисовать линии��6   $
' Line��68   ( 5�      �� $ canvas��$
 Tool�$) OneWay=�#* [  $
+ Line��.��5�      �� $��31��6   $��31��6%   , 5�        5�      $	- 0.3Q�6'   .+�5�      � 
���   �$��5��6    %1 �   ��� Zb ���J]�J]$2 0.499��5      $3 -1.334��6%   z�5  	   V� 5  
   .~�5     �� $	 0.3Q�6   $	 0.3Q�6'   �� 5     T� 5     $/ 0Q�6   $0 3Q�6$   5     5	  	   5
     5     d� 5     -   5        $��buttonToolPolygone��5  >   $'& !Рисовать полигоны��6   $' Polygone��6/   ( 5     �� $ canvas��$
 Tool�$) OneWay=�#* [  $+ Polygone��.��5     �� $��31��6   $��31��6%   , 5       5     $	- 0.3Q�6&   .+�5     � 
���   �
��� � �E �   ��� ���  Zb����,��� �J]��� �� �� -1�,z�5  	   V� 5  
   .~�5     �� $
 0.30Q�6   $
 0.30Q�6(   5     5  
   5     5      d� 5"     #��[ 5#     ��5"     �� $ canvas��$ ToClayCommand�.��5$     �� $��31��6   $��31��6%   , 5%       5&     $	- 0.3Q�6&   .+�5(     � 
���   �
��� � �E �   ��� ���  Zb����,��� �J]��� �� �� -1�,z�5*  	   V� 5+  
   .~�5,     �� $
 0.30Q�6   $
 0.30Q�6(   5-     5/  	   50     51     d� 53     -   5        $��buttonToolBrush��53  0   $& Лепить��6   $' Brush��6$   $��Лепить��56     ( 54     �� $ canvas��$
 Tool�$) OneWay=�#* [  $+ Brush��58     �� 5;     $��В SVG��6G   ��6   �� $ canvas��$ ToSVGCommand�6G   �� 5<     $
��Undo��6F   ��6   �� $ canvas��$ UndoCommand�6F   5=     5>     �� 5A     $��Right=�6   $	��150��6&   .+�5B     `� $��Right=�6   $$ Vertical=�6(   $	��Top=�6?   % �	#FF8F8F8F.�5C     c� #��[ 6   4 c� IsOverflowOpen4 . ���	#FF8F8F8F$	��eee��5D  %   .@�5E     d� $'& !Отталкивает линии��6    $��OutMover��5G  "   $��Отодвигалка��5J     ��5F      �� $ canvas��$ SetBrushCommand�( 5H      �� $ canvas��$ Brush�$) OneWay=�#* [ $+ OutMover��5J     d� 5L     $%& Выпрямляет линии��6    $��Smoother��5N      $��Сглаживалка��5Q     ��5M      �� $ canvas��$ SetBrushCommand�( 5O      �� $ canvas��$ Brush�$) OneWay=�#* [ $+ Smoother��5Q     d� 5S     $1& +Делает щипки на контуре��6    $��Pincher��5U      $��Щипалка��5X     ��5T      �� $ canvas��$ SetBrushCommand�( 5V      �� $ canvas��$ Brush�$) OneWay=�#* [ $+ Pincher��5X     d� 5Z     $y�Hidden=�6    $s& mСтарается приблизить очертания контура к форме мазка кисти��5[     $��Smoother��5]      $��Повторялка��5`     ��5\      �� $ canvas��$ SetBrushCommand�( 5^      �� $ canvas��$ Brush�$) OneWay=�#* [ $
+ None��5`     d� 5b     $y�Hidden=�6    $<& 6Стягивает все контуры к кисти��5c     $��InMover��5e      $��Сужалка��5h     ��5d      �� $ canvas��$ SetBrushCommand�( 5f      �� $ canvas��$ Brush�$) OneWay=�#* [ $+ InMover��5h     d� 5j     $6& 0Передвигает кусок контура��5k     $��Mover��5m      $��Перемещалка��5p     ��5l      �� $ canvas��$ SetBrushCommand�( 5n      �� $ canvas��$ Brush�$) OneWay=�#* [ $
+ None��5p     �� 5r     $#��Параметры кисти��6   .��5s     �� 5 �� Orientation$5 Vertical=�6   .+�5u  	   �� $5 
Horizontal=�6   .+�5v  
   �� $��	Сила:��6   $��
0, 0, 5, 0q�6!   �� 5w  
   ��6   �� $ Power��$ Value�6 �� StringFormat$6 N0��5x  
   �� 5y  	   -   5        $��Power��5y  6   $	��100��6   7 � Maximum$	7 100Q�6   8 � Minimum$8 1Q�6*   9 � Value9 5z     �� $ canvas��$ Brush.Power�$) TwoWay=�{�5{  	   �� $ canvas��$
 Tool�$) OneWay=�#* [  $+ Brush���� 5�  	   $5 
Horizontal=�6   .+�5�  
   �� $��Размер:��6   $��
0, 0, 5, 0q�6#   �� 5�  
   ��6   �� $
 Size��$ Value�$6 N0��5�  
   �� 5�  	   -	   5        $
��Size��5�  6   $	��100��6   $	7 100Q�6   $8 1Q�6*   9 5�     �� $ canvas��$ 
Brush.Size�$) TwoWay=�{�5�  	   �� $ canvas��$
 Tool�$) OneWay=�#* [  $+ Brush��5�  	   5�     5�     5�     �� 5�     $��Слои��6   $	��150��6   .��5�     ]� ��. .+�5�     c� $	��Top=�6   .@�5�     �� -
   5        $��+��5�     $& Добавить слой��6   63   �� 5�     $��-��6   $& Удалить слой��6   �� 5�     $��
Вверх��6   �� 5�     $��Вниз��6   5�     ]� 5�     ��. $��Bottom=�6&   .+�5�     �� $��%��6   $
��Left=�6   �� 5�     $	7 255Q�6   9 5�     �� $ 	layerGrid��$ SelectedItem.Alpha�$) TwoWay=�: �� UpdateSourceTrigger$: PropertyChanged=�5�     &   System.Windows.Controls.DataGrid  5�     -   5        $��	layerGrid��5�  d   ;   AutoGenerateColumns; .  <   HeadersVisibility$
< None=�5�     =   CanUserAddRows= .    DataGridStyle2#��[ 67   ��5�     �� $ canvas��$ Layers�>   Columns> 5�     4  .System.Windows.Controls.DataGridCheckBoxColumn  5�  	   ,  &System.Windows.Controls.DataGridColumn?   Width5  /System.Windows.Controls.DataGridLengthConverter$? 10 6p   1	  +System.Windows.Controls.DataGridBoundColumn@ 	  Binding@ 6    �� *	IsVisible+$) TwoWay=�$: PropertyChanged=�6p   0
  *System.Windows.Controls.DataGridTextColumn
  5�  	   $? * 65   A   
IsReadOnlyA . @ 6   �� *Name+6?   5�     5�     5�     5�     �� 5�     $��Bottom=�6   .+�5�     �� -   5        B ��Name$B slider��5�     C �� Orientation$C 
Horizontal=�5�     $
��Left=�6%   $8 1Q�6@   $7 30Q�6L   $	��300��5�     6   5�     �� 5�     -   5        $��scrollViewer��5�  _   $��Visible=�6   $��Visible=�67   .��5�     � -   5        $
��grid��5�     $	��400��6   $	��400��6+   D J� RenderTransformOrigin$D 0.5,0.5&�68   E � LayoutTransformE 5�     V� 5�     .~�5�     �� -   5        scaleTransform��5�  )   5�     .+�5�     (� $��0��6   $��0��6*   .��5�        DrawLibrary.DrawingCanvas  -   5        F  Name$F canvas��5�  (   ���	#00000000$
��2000��6O   $
��2000��6\   5�     5�     5�     5�     5�     