   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      &     Tickets_Bosquejos.EditTicketForm   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5        N� Title$  EditTicketForm��5	      .��5      � ���#f5f5eb � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   ]� $��0��6   $��35��6!   $��Center=�6-   .+�5      �� -   5         ��Name$ 	btnVolver��5      $��20��6(   $��20��63   $��Right=�6?   $��Right=�6[   $��0,6,25,6q�6r   $��1,1,1,1q�6�   ���#333.��5      �� $3��-C:\Users\SISTEMAS\Documents\Imagenes\back.png��6   $��25��6O   6]   5      5      �� 5   
   $��1��6   .��5      �� $��Center=�6   $	��696��66   $��0,20,0,0q�6B   $	��Top=�6T   .+�5      �� $��Editar Ticket��6   $��16(�61   $
��Bold"�6?   $��Center=�6Q   ���#333$��0,0,0,10q�6�   �� 5      $D��>Solo puede cambiar la prioridad y las observaciones del ticket��6   $��12(�6b   $��Center=�6p   6�   �� 5!      $��
Incidencia��6   $��14(�6.   $
��Bold"�6<   �� 5"      $1��+Describa brevemente la incidencia que tiene��6   $��12(�6O   $
��Left=�6]   6z   �� 5#      -   5        $��
txtUsuario��5#      $��20��6,   $��14(�68   $��	0,10,0,20q�6F   $
��Left=�6Y   $��2,2,2,2q�6t   
����������#333$	��286��6�    �� 
IsReadOnly . 5$      �� 5'      $��Sistema��6   $��14(�6+   $
��Bold"�69   ���#3336K   �� 5(      $5��/Seleccione el sistema con el que tiene problema��6   $��12(�6S   $
��Left=�6a   6~   �� 5)      -   5         ��Name$ 
cmbSistema��5)      $��20��6/   $��	0,10,0,20q�6<   $��14(�6O   $	��174��6]   $
��Left=�6i   
������  �� 
IsReadOnly . .@�5*      �� 6%   �� 5+      6%   �� 5,      6%   5-      �� 50      $��	Prioridad��6   $��14(�6-   $
��Bold"�6;   ���#3336M   �� 51      $&�� Que tan urgente es su incidencia��6   $��12(�6D   $
��Left=�6R   6o   �� 52      -   5        $ cmbPrioridad��52      $��20��61   $��	0,10,0,20q�6>   $��14(�6Q   $	��178��6_   $
��Left=�6k   
������ .@�54      �� $��Selecciona la prioridad��6#   {�.  6Y   �� 55      $
��Baja��6#   64   �� 56      $��Media��6#   65   �� 57      $
��Alta��6#   64   �� 58      $��Critica��6#   67   59      �� 5<      $��Observaciones��6   $��14(�61   $
��Bold"�6?   �� 5=      $L��FAgregue todas las observaciones que tenga con respecto a su incidencia��6   $��12(�6j   $
��Left=�6x   6�   �� 5>      -   5        $��txtObservaciones��5>      $��50��62   $��14(�6>   $��	0,10,0,20q�6L   $
��Left=�6_   $��2,2,2,2q�6z   
����������#333$	��286��6�   �� 5B      $	��PDF��6   $��14(�6'   $
��Bold"�65   �� 5C      $M��GAgregue un archivo PDF con sus observacion más detalle si es necesario��6   $��12(�6j   $
��Left=�6x   6�   �� 5D      $��Subir PDF...��6   $��50��60   $��20��6;   $��	0,10,0,20q�6G   $
��Left=�6Z   
������ 6�   �� 5G      $��Correo��6   $��14(�6*   $
��Bold"�68   �� 5H      $?��9Agregue su correo para notificarle el estado de su ticket��6   $��12(�6]   $
��Left=�6k   6�   �� 5I      -   5        $��	txtCorreo��5I      $��20��6+   $��14(�67   $��	0,10,0,20q�6E   $
��Left=�6X   $��2,2,2,2q�6s   
����������#333$	��286��6�   �� 5N      -   5        $��btnEnviarEdit��5N      $��Editar��6.   $	��150��6?   $��40��6K   ���#8B3F3C
�������$��16(�5O   	   $
��Bold"�6   
������ $��10,20,10,10q�6C   $��Center=�6X   5P      5Q      5S      5T      