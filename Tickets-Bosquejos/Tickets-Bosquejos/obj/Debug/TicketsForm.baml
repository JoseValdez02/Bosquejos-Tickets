   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      #    Tickets_Bosquejos.TicketsForm   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5        N� Title$  TicketsForm��5	      .��5      � ���#f5f5eb.+�5   
   �� .��5      �� $��Center=�6   $	��696��66   $��0,20,0,0q�6B   $	��Top=�6T   .+�5      �� $��Nuevo Ticket��6   $��16(�60   $
��Bold"�6>   $��Center=�6P   ���#333$��0,0,0,10q�6   �� 5      $H��BRellene el formulario con su incidencia para convertirla en ticket��6   $��12(�6f   $��Center=�6t   6�   �� 5      $��
Incidencia��6   $��14(�6.   $
��Bold"�6<   �� 5      $1��+Describa brevemente la incidencia que tiene��6   $��12(�6O   $
��Left=�6]   6z   �� 5      -   5        $��txtIncidencia��5      $��20��6/   $��14(�6;   $��	0,10,0,20q�6I   $
��Left=�6\   $��2,2,2,2q�6w   
����������#333$	��286��6�   �� 5      $��Sistema��6   $��14(�6+   $
��Bold"�69   ���#3336K   �� 5      $5��/Seleccione el sistema con el que tiene problema��6   $��12(�6S   $
��Left=�6a   6~   �� 5      -   5         ��Name$ 
cmbSistema��5      $��20��6/   $��	0,10,0,20q�6<   $��14(�6O   $	��174��6]   $
��Left=�6i   
������ .@�5      �� $��Seleccione un sistema��6#   {�.  6W   �� 5      6%   �� 5      6%   5       �� 5#      $��	Prioridad��6   $��14(�6-   $
��Bold"�6;   ���#3336M   �� 5$      $&�� Que tan urgente es su incidencia��6   $��12(�6D   $
��Left=�6R   6o   �� 5%      -   5        $ cmbPrioridad��5%      $��20��6   $��	0,10,0,20q�6�   $��14(�6�   $	��178��6�   $
��Left=�6�   
������ .@�5&      �� $��Selecciona la prioridad��6#   {�.  6Y   �� 5'      .��6#   Baja6)   �� 5(      .��6#   Media6*   �� 5)      .��6#   Alta6)   �� 5*      .��6#   	Critica6,   5+      �� 5.      $��Observaciones��6   $��14(�61   $
��Bold"�6?   �� 5/      $L��FAgregue todas las observaciones que tenga con respecto a su incidencia��6   $��12(�6j   $
��Left=�6x   6�   �� 50      -   5        $��txtObservaciones��50      $��50��62   $��14(�6>   $��	0,10,0,20q�6L   $
��Left=�6_   $��2,2,2,2q�6z   
����������#333$	��286��6�   �� 54      $	��PDF��6   $��14(�6'   $
��Bold"�65   �� 55      $M��GAgregue un archivo PDF con sus observacion más detalle si es necesario��6   $��12(�6j   $
��Left=�6x   6�   �� 56      -   5         ��Name$ btnSubirPdf��56      $��Subir PDF...��6.   $��50��6E   $��20��6P   $��	0,10,0,20q�6\   $
��Left=�6o   
������ 6�   �� 59      $��Correo��6   $��14(�6*   $
��Bold"�68   �� 5:      $?��9Agregue su correo para notificarle el estado de su ticket��6   $��12(�6]   $
��Left=�6k   6�   �� 5;      -   5        $��	txtCorreo��5;      $��20��6+   $��14(�67   $��	0,10,0,20q�6E   $
��Left=�6X   $��2,2,2,2q�6s   
����������#333$	��286��6�   �� 5@      -   5        $��	btnEnviar��5@      $��Enviar��6*   $	��150��6;   $��40��6G   ���#8B3F3C
�������$��16(�5A      $
��Bold"�6   
������ $��10,20,10,10q�6K   $��Center=�6`   5B      5C      5E      5F      