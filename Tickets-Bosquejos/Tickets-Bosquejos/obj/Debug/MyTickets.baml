   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      !    Tickets_Bosquejos.MyTickets   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5      -   5          N� Title$  Mis Tickets��5
      .��5      � ���#f5f5eb � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   ]� $��0��6   $��58��6!   $��Center=�6-   .+�5      �� -   5         ��Name$ cmbFiltrarStatus��5       �� Text$ Filtrar por Status��6�   $��24��6�   $��
0,14,25,20q�5      $��14(�6+   $	��130��69   $��Right=�6E   $��Right=�6a   ���#333.@�5      �� $��Filtrar por Status��6   {�.  6P   �� 5      $��Nuevo��6   61   �� 5      $��Abierto��6   63   �� 5      $��Resuelto��6   64   5      �� 5!      -   5         ��Name$ 	btnBuscar��5!      $��20��6(   $��22��63   $��Right=�6?   $��Right=�6[   $��0,0,16,6q�6r   $��1,1,1,1q�6�   ���#333.��5"      �� $Z��TC:\Users\SISTEMAS\source\repos\Tickets-Bosquejos\Tickets-Bosquejos\Images\search.png��6   $��25��6v   6�   5#      �� 5%      -   5         ��Name$ txtSearchBar��5%      $��22��6,   $	��250��68   $��Right=�6D   $��Right=�6`   $��0,0,0,6q�6w   $��2,2,2,2q�6�   ���#3336�   �� 5(      -   5        $ btnActualizar��5(      $��20��6,   $��22��67   $��Right=�6C   $��Right=�6_   $��0,0,16,6q�6v   $��1,1,1,1q�6�   ���#333.��5*      �� $Z��TC:\Users\SISTEMAS\source\repos\Tickets-Bosquejos\Tickets-Bosquejos\Images\update.png��6   $��25��6v   6�   5+      �� 5-      $��Tickets��6   $��80��6'   $��25��62   $��20(�6>   $
��Bold"�6L   
���   �$
��Left=�6q   $
��Left=�6�   $��12,0,0,8q�5.      6   5/      �� 51   
   $��1��6   $��0,10,0,0q�6"   .+�52      �� $	��540��6   $
��Auto=�6(   .��53      &   System.Windows.Controls.DataGrid  -   5          Name$ tableTickets��53      $��Stretch=�61      AutoGenerateColumns .  $	��600��6i   $
��1050��6v   $��18(�6�      
IsReadOnly . ��. 	   Columns	 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	tic_clave+6Y     57      $
 Ticket��6-    6=   �� *
tic_nombre+6^     58      $
 Sistema��6-    6>   �� *
sis_nombre+6_     59      $
 Status��6-    6=   �� *
tic_status+6^     5:      $
 	Prioridad��6-    6@   �� *tic_prioridad+6d     5;      $
 Observaciones��6-    6D   �� *tic_observaciones+6l     5<      $	
 PDF��6-    6:   �� *	tic_pdf+6X     5=      $
 Correo��6-    6=   �� *
tic_correo+6^     5>      $
 Responsable��6-    6B   �� *
pro_nombre+6c     5?      $
 Fecha de creación��6-    6H   �� *tic_fechacreacion+6p     5@      $
 Fecha de finalización��6-    6L   �� *tic_fechafin+6o   5B      5C      5D      ]� 5F   
   $��2��6   $��70��6!   .+�5G      �� -   5        $ 	btnEditar��5G      $��Editar Ticket��6(   $��95��6@   $��35��6K   $
��Left=�6W   ���#8B3F3C
�������$��12(�5H      $
��Bold"�6   
������ $��
500,30,0,0q�6@   $��1,1,1,1q�6T   6�   �� 5I      -   5        $ btnEliminar��5I      $��Eliminar Ticket��6*   $��95��6D   $��35��6O   $��Right=�6[   ���#8B3F3C
�������$��12(�5J      $
��Bold"�6   
������ $��
0,30,500,0q�6@   $��1,1,1,1q�6T   6�   5K      5O      5P      