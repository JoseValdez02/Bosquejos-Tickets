   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      (    "Tickets_Bosquejos.AdminTicketsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5      -   5          N� Title$  Administrar Tickets��5	      .��5      � ���#f5f5e5 � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   ]� $��0��6   $��58��6!   $��Center=�6-   .+�5      �� -   5         ��Name$ cmbFiltrarBusqueda��5       �� Text$ Filtrar por Status��6�   $��24��6�   $��
0,14,25,20q�5      $��14(�6'   $	��130��65   $��Right=�6A   $��Right=�6]   ���#333.@�5      �� $��Filtrar tickets��6   {�.  6M   �� 5      $��Filtrar por status��6   {�.  6P   �� 5      $��Nuevo��6   61   �� 5      $��Abierto��6   63   �� 5      $��Resuelto��6   64   �� 5      $��Filtrar por prioridad��6   {�.  6S   �� 5       $
��Baja��6   60   �� 5!      $��Media��6   61   �� 5"      $
��Alta��6   60   �� 5#      $��Crítica��6   63   5$      �� 5'      -   5         ��Name$ 	btnBuscar��5'      $��20��6(   $��22��63   $��Right=�6?   $��Right=�6[   $��0,0,16,6q�6r   $��1,1,1,1q�6�   ���#333.��5)      �� $Z��TC:\Users\SISTEMAS\source\repos\Tickets-Bosquejos\Tickets-Bosquejos\Images\search.png��6   $��25��6v   6�   5*      �� 5,      -   5         ��Name$ txtSearchBar��5,      $��22��6,   $	��250��68   $��Right=�6D   $��Right=�6`   $��0,0,0,6q�6w   $��2,2,2,2q�6�   ���#3336�   �� 5/      -   5        $ btnActualizar��5/      $��20��6,   $��22��67   $��Right=�6C   $��Right=�6_   $��0,0,16,6q�6v   $��1,1,1,1q�6�   ���#333.��51      �� $Z��TC:\Users\SISTEMAS\source\repos\Tickets-Bosquejos\Tickets-Bosquejos\Images\update.png��6   $��25��6v   6�   52      �� 54      $��Tickets��6   $��80��6'   $��25��62   $��20(�6>   $
��Bold"�6L   
���   �$
��Left=�6q   $
��Left=�6�   $��12,0,0,8q�55      6   56      �� 58   
   $��1��6   $��0,10,0,0q�6"   .+�59      �� $	��540��6   .��5:      &   System.Windows.Controls.DataGrid  -   5          Name$ tableTickets��5:      $��Stretch=�61      AutoGenerateColumns .  $	��600��6i   $
��1050��6v   $��18(�6�   ��.    
IsReadOnly . 	   Columns	 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	tic_clave+6Y     5>      $
 Ticket��6-    6=   �� *
tic_nombre+6^     5?      $
 Usuario��6-    6>   �� *tic_usuario+6a     5@      $
 Sistema��6-    6>   �� *
sis_nombre+6_     5A      $
 Empresa��6-    6>   �� *
emp_nombre+6_     5B      $
 Status��6-    6=   �� *
tic_status+6^     5C      $
 	Prioridad��6-    6@   �� *tic_prioridad+6d     5D      $
 Observaciones��6-    6D   �� *tic_observaciones+6l     5E      $	
 PDF��6-    6:   �� *	tic_pdf+6X     5F      $
 Correo��6-    6=   �� *
tic_correo+6^     5G      $
 Responsable��6-    6B   �� *
pro_nombre+6c     5H      $
 Fecha de creación��6-    6H   �� *tic_fechacreacion+6p     5I      $
 Fecha de finalización��6-    6L   �� *tic_fechafin+6o   5K      5L      5M      ]� 5O   
   $��2��6   $��70��6!   .+�5P      �� -   5        $ btnAgregarResponsable��5P      $'��!Agregar Responsable/Asignar Fecha��64   $	��230��6`   $��35��6l   $
��Left=�6x   ���#8B3F3C
�������$��12(�5Q      $
��Bold"�6   
������ $��123,35,55,2q�6;   $��1,1,1,1q�6P   6�   �� 5R      -   5        $ btnSolucionado��5R      $��Marcar como resuelto��6-   $	��161��6L   $��35��6X   $��Center=�6d   ���#8B3F3C
�������$��12(�5S      $
��Bold"�6   
������ $��
225,35,0,2q�6;   $��1,1,1,1q�6O   6�   �� 5T      -	   5        $ btnEliminar��5T      $��Eliminar Ticket��6*   $��95��6D   $��35��6O   $��Right=�6[   ���#8B3F3C
�������$��12(�5U      $
��Bold"�6   
������ $��
0,35,132,2q�6;   $��1,1,1,1q�6O   6�   5V      5W      5X      