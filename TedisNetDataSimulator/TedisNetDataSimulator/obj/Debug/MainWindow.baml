   M S B A M L   `   `   `  ����P  LTedisNetDataSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TedisNetDataSimulatorTedisNetDataSimulator  5   	   =#clr-namespace:TedisNetDataSimulatorTedisNetDataSimulator  5   	   &     TedisNetDataSimulator.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:TedisNetDataSimulator     5   	   2vm#clr-namespace:TedisNetDataSimulator     5   	     � Title$  
MainWindow��5	   	   $��275.233��6   $��335.349��6-    � DataContext 5
      )   #TedisNetDataSimulator.MainViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��132* �6   �� 5      $
��197* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   .+�5   
   �� $
��Left=�6   $��	30,10,0,0q�6/   $
��Wrap=�6B   $��Eventos y TagValueChanges��6V   $	��Top=�6w   $��2��6�   $��1��6�   �� 5   
   $
��Left=�6   $��23��6-   $��
32,0.4,0,0q�69    �� TextWrapping$
 Wrap=�6M   $	��Top=�5      $��73��6*   $��Center=�65   $��2��6Y   ��5      �� *FrecuencyEvents+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� FallbackValue$
 5000��5   Y   �� 5   
   $
��Left=�6   $��110,0.4,0,0q�6/   $
��Wrap=�6D   $��ms��6X   $	��Top=�6b   $��2��6z   �� 5   
   $��Generar��6   $
��Left=�6#   $��25.462,0.4,0,0q�6>   $	��Top=�6V   $��75��6n   $��2��5    ;   $��1��6H   ��6   �� *GenerateEventsCommand+6H   �� 5!   
   $��Parar��6   $
��Left=�6!   $��105.462,0.4,0,0q�6<   $	��Top=�6U   $��75��6m   $��2��5"   7   $��1��6D   ��6   �� *StopEventsCommand+6D   �� 5$   
   $
��Left=�6   $��	30,10,0,0q�6/   $
��Wrap=�6B   $��Medidas��6V   $	��Top=�6e   $��2��6}   $��3��6�   �� 5%   
   $
��Left=�6   $��23��6-   $��
32,0.4,0,0q�69   $
 Wrap=�6M   $	��Top=�5'      $��73��6*   $��Center=�65   $��4��6Y   ��5&      �� *FrecuencyMeasures+$ PropertyChanged=�$ TwoWay=�$
 5000��5'   Y   �� 5(   
   $
��Left=�6   $��110,0.4,0,0q�6/   $
��Wrap=�6D   $��ms��6X   $	��Top=�6b   $��4��6z   �� 5)   
   $��Generar��6   $
��Left=�6#   $��25.462,0.4,0,0q�6>   $	��Top=�6V   $��75��6n   $��4��5*   =   $��1��6J   ��6   �� *GenerateMeasuresCommand+6J   �� 5+   
   $��Parar��6   $
��Left=�6!   $��105.462,0.4,0,0q�6<   $	��Top=�6U   $��75��6m   $��4��5,   9   $��1��6F   ��6   �� *StopMeasuresCommand+6F   5-      5.      