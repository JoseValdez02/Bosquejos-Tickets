   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      &     Tickets_Bosquejos.EditTicketForm   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5      -   5          N� Title$  Editar Ticket��5	      .��5      � ���#f5f5eb � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   ]� $��0��6   $��58��6!   $��Center=�6-   .+�5      �� -   5         ��Name$ 	btnVolver��5      $��35��6(   $��35��63   $��Right=�6?   $��Right=�6[   $��0,6,25,6q�6r   $��1,1,1,1q�6�   ���#333.��5      �� $X��RC:\Users\SISTEMAS\source\repos\Tickets-Bosquejos\Tickets-Bosquejos\Images\back.png��6   $��25��6t   6�   5      5      �� 5   
   $��1��6   .��5      �� $
��1050��6   $��	0,20,0,10q�6"   $��1q�65   ���#8B3F3C._�5      �� $��Center=�6   $
��1000��6:   $��0,20,0,0q�6G   $	��Top=�6Y   .+�5      �� $��Editar Ticket��6    $��24(�65   $
��Bold"�6C   $��Center=�6U   ���#333$��0,0,0,10q�6�   �� 5      $+��%Solo puede añadir más observaciones��6    $��16(�6K   $��Center=�6Y   6x   �� 5#      $��
Incidencia��6    $��20(�62   $
��Bold"�6@   �� 5$      $1��+Describa brevemente la incidencia que tiene��6    $��14(�6S   $
��Left=�6a   6~   �� 5%      -   5        $��txtIncidencia��5%      $��24��63   $��14(�6?   $��	0,10,0,20q�6M   $
��Left=�6`   $��2,2,2,2q�6{   
����������#333$	��286��6�    �� 
IsReadOnly . {�.  5&   ,   �� 5)      $��Sistema��6    $��20(�6/   $
��Bold"�6=   ���#3336O   �� 5*      $5��/Seleccione el sistema con el que tiene problema��6    $��14(�6W   $
��Left=�6e   6�   �� 5+      -   5        $��
txtSistema��5+      $��24��60   $��14(�6<   $��	0,10,0,20q�6J   $
��Left=�6]   $��2,2,2,2q�6x   
����������#333$	��286��6�    . {�.  5,   0   �� 5/      $��	Prioridad��6    $��20(�61   $
��Bold"�6?   ���#3336Q   �� 50      $&�� Que tan urgente es su incidencia��6    $��16(�6H   $
��Left=�6V   6s   �� 51      -   5        $��txtPrioridad��51      $��24��62   $��14(�6>   $��	0,10,0,20q�6L   $
��Left=�6_   $��2,2,2,2q�6z   
����������#333$	��286��6�    . {�.  52   +   �� 55      $��Observaciones��6    $��20(�65   $
��Bold"�6C   �� 56      $L��FAgregue todas las observaciones que tenga con respecto a su incidencia��6    $��16(�6n   $
��Left=�6|   6�   �� 57      -   5        $��txtObservaciones��57      $��50��66   $��14(�6B    �� TextWrapping$
 Wrap=�6P   $��	0,10,0,20q�6d   $
��Left=�6w   $��2,2,2,2q�6�   
����������#333$	��286��58   1   �� 5<      $	��PDF��6    $��20(�6+   $
��Bold"�69   �� 5=      $Q��KAgregue un archivo PDF con sus observaciones más a detalle si es necesario��6    $��14(�6r   $
��Left=�6�   6�   �� 5>      -   5        $��txtPdf��5>      $��20��6,   $��14(�68   $��	0,10,0,20q�6F   $
��Left=�6Y   $��2,2,2,2q�6t   
����������#333$	��286��5?   1    . 6=   �� 5@      -   5        $��btnSubirPdf��5@   4   $��Subir PDF...��6   $��65��6H   $��24��6S   $��	0,10,0,20q�6_   $
��Left=�6r   
������ 6�   �� 5C      $��Correo��6    $��20(�6.   $
��Bold"�6<   �� 5D      $?��9Agregue su correo para notificarle el estado de su ticket��6    $��14(�6a   $
��Left=�6o   6�   �� 5E      -	   5        $��	txtCorreo��5E      $��20��6/   $��14(�6;   $��	0,10,0,20q�6I   $
��Left=�6\   $��2,2,2,2q�6w   
����������#333$	��286��5F   1    . {�.  6O   �� 5K      -
   5        $��btnEnviarEdit��5K      $��Editar��62   $	��160��6C   $��50��6O   ���#8B3F3C
�������$��16(�5L      $
��Bold"�6$   
������ $��10,20,10,10q�6P   $��Center=�6e   6�   5M      5N      5O      5Q      5R      