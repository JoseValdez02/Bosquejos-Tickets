   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      #    Tickets_Bosquejos.TicketsForm   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5      -   5          N� Title$  TicketsForm��5	      .��5   
   � ���#f5f5eb.+�5   
   �� .��5      �� $
��1050��6   $��	0,20,0,10q�6"   $��1q�65   ���#8B3F3C._�5      �� $��Center=�6   $
��1000��6:   $��0,20,0,0q�6G   $	��Top=�6Y   .+�5      �� $��Nuevo Ticket��6   $��24(�60   $
��Bold"�6>   $��Center=�6P   ���#333$��0,0,0,10q�6   �� 5      $H��BRellene el formulario con su incidencia para convertirla en ticket��6   $��16(�6f   $��Center=�6t   6�   �� 5      $��
Incidencia��6   $��20(�6.   $
��Bold"�6<   $��0,20,0,0q�6N   �� 5      $1��+Describa brevemente la incidencia que tiene��6   $��14(�6O   $
��Left=�6]   6z   �� 5      -   5        $��txtIncidencia��5      $��24��6/   $��14(�6;   $��	0,10,0,20q�6I   $
��Left=�6\   $��2,2,2,2q�6w   
����������#333$	��286��6�   �� 5      $��Sistema��6   $��20(�6+   $
��Bold"�69   ���#3336K   �� 5      $5��/Seleccione el sistema con el que tiene problema��6   $��14(�6S   $
��Left=�6a   6~   �� 5      -   5         ��Name$ 
cmbSistema��5      $��24��6y   $��	0,10,0,20q�6�   $��14(�6�   $	��286��6�   $
��Left=�6�   
������  �� DisplayMemberPath$ Value��5   5    �� SelectedValuePath$	 Key��6O   .@�5       �� $��Seleccione un sistema��6#   {�.  6W   �� 5!      6%   �� 5"      6%   5#      �� 5&      $��	Prioridad��6   $��20(�6-   $
��Bold"�6;   ���#3336M   �� 5'      $'��!Qué tan urgente es su incidencia��6   $��14(�6D   $
��Left=�6R   6o   �� 5(      -   5        $ cmbPrioridad��5(      $��24��6   $��	0,10,0,20q�6�   $��14(�6�   $	��286��6�   $
��Left=�5)      
������ .@�5*      �� $��Selecciona la prioridad��6#   {�.  6Y   �� 5+      .��6#   Baja6)   �� 5,      .��6#   Media6*   �� 5-      .��6#   Alta6)   �� 5.      .��6#   
Crítica6,   5/      �� 52      $��Observaciones��6   $��20(�61   $
��Bold"�6?   �� 53      $L��FAgregue todas las observaciones que tenga con respecto a su incidencia��6   $��14(�6j   $
��Left=�6x   6�   �� 54      -   5        $��txtObservaciones��54      $��50��62   $��14(�6>   $��	0,10,0,20q�6L   $
��Left=�6_   $��2,2,2,2q�6z   
������� �� TextWrapping$
 Wrap=�55      ���#333$	��286��6A   �� 59      $	��PDF��6   $��20(�6'   $
��Bold"�65   �� 5:      $R��LAgregue un archivo PDF con sus observaciones más a detalle sí es necesario��6   $��14(�6n   $
��Left=�6|   6�   �� 5;      -   5        $��txtPdf��5;      $��20��6,   $��14(�68   $��	0,10,0,20q�6F   $
��Left=�6Y   $��2,2,2,2q�6t   
����������#333$	��286��5<   D    �� 
IsReadOnly . 6P   �� 5=      -   5         ��Name$ btnSubirPdf��5=      $��Subir PDF...��62   $��65��6I   $��24��6T   $��	0,10,0,20q�6`   $
��Left=�6s   
������ 6�   �� 5@      $��Correo��6   $��20(�6*   $
��Bold"�68   �� 5A      $?��9Agregue su correo para notificarle el estado de su ticket��6   $��14(�6]   $
��Left=�6k   6�   �� 5B      -   5        $��	txtCorreo��5B      $��20��6+   $��14(�67   $��	0,10,0,20q�6E   $
��Left=�6X   $��2,2,2,2q�6s   
����������#333$	��286��6�   �� 5G      -	   5        $��	btnEnviar��5G      $��Enviar��6*   $	��160��6;   $��50��6G   ���#8B3F3C
�������$��16(�5H      $
��Bold"�6   
������ $��10,50,10,10q�6K   $��Center=�6`   6}   5I      5J      5K      5M      5N      