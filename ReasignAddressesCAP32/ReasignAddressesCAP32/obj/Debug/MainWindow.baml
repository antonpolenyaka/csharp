   M S B A M L   `   `   `  ����  ReasignAddressesCAP32&     ReasignAddressesCAP32.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  
MainWindow��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�5   	   .��5	      � $��
10,0,10,10q�6    � RowDefinitions 5
   
   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   �� 5      $��30 �6   6*   �� 5      $��20 �6   6*   .+�5   
   ��  �� DisplayMemberPath$
 Name��6   $��0��5      $��
10,10,10,0q�5      ��5      �� *	Devices+ �� SelectedItem 5      �� *SelectedDevice+5      �� 5   
   $��1��6   $��Renumerar tags en CAP32��5      $��Center=�5       $��Center=�5!      �� 5"   
   -   5        $��2��5"      $��	10,0,10,0q�5$      $��Start��6$   �� 5%   
   $��3��6   $:��4Comprobar los canales y puertos del CAP32 + esclavos��5&      $��Center=�5'      $��Center=�5(      �� 5)   
   -   5        $��4��5)      $��	10,0,10,0q�5+      $��Start��6$   �� 5,   
   $��5��6   $?��9Renombrar los tags en CAP32 (AI.001, DI.001, DO.001, ...)��5-      $��Center=�5.      $��Center=�5/      �� 50   
   -   5        $��6��50      $��	10,0,10,0q�52      $��Start��6$   �� 53   
   $��7��6   $U��OCT -> transformador. Encima se pone elmento CT Se utiliza el fichero del Calser��54      $��Center=�55      $��Center=�56      �� 57   
   -   5        $��8��57      $��	10,0,10,0q�59      ��6$   �� *NumElementsUpdated+ �� FallbackValue$ Start���� 5:   
   $��9��6   $5��/Alta masiva de concentradores PRIME desde Excel��5;      $��Center=�5<      $��Center=�5=      �� 5>   
   $��10��6   $��	10,0,10,0q�5@      ��5?      �� * AddNewConcentratorPRIMECommand+��5@   $   �� *NumCncCreated+$ Start���� 5A   
   $��11��6   $&�� Alta dispositivo 104 desde Excel��5B      $��Center=�5C      $��Center=�5D      �� 5E   
   $��12��6   $��	10,0,10,0q�5G      $��Start��6$   ��5F      �� *#!AddNewDevice104AndTagClassCommand+5G   $   5H      5I      