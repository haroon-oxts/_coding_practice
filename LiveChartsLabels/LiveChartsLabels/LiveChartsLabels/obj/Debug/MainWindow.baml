   M S B A M L   `   `   `  ����K  GLiveChartsLabels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:LiveChartsLabelsLiveChartsLabels  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   !    LiveChartsLabels.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:LiveChartsLabels     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$  
MainWindow��5	   	   $	��350��6   $	��525��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� Labels6   �� 5   
   -   5        $��1��5      $��30��6   .��6C   
Move All5      #  LiveCharts.Wpf.CartesianChart  5   
   -   5        $��2��5      &   LiveCharts.Wpf.Charts.Base.Chart   Series 6*   �� *SeriesCollection+   AxisX 5        LiveCharts.Wpf.Axis     LabelsRotation$ 20Q�6      Position$ 
LeftBottom=�6I      Labels 6/   �� *Labels+   	Separator 5        LiveCharts.Wpf.Separator  5         Step$ 1Q�6(   63   5      	   AxisY	 5        
   Title$
 
Sold Items��6   $ RightTop=�6S      LabelFormatter 6.   �� *	Formatter+6i   5       5!      5"      