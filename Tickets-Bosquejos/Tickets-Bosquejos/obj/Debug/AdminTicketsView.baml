   M S B A M L   `   `   `  ����L  HTickets-Bosquejos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Tickets_BosquejosTickets_Bosquejos  5      (    "Tickets_Bosquejos.AdminTicketsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Tickets_Bosquejos     5        N� Title$  AdminTicketsView��5	      .��5      � ���#f5f5e5 � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   ]� $��0��6   $��57��6!   $��Center=�6-   ���#f5f5eb.+�5      �� -   5         ��Name$ cmbFiltrarBusqueda��5      $��22��6�   $��
0,15,25,20q�6�   $��14(�6�   $	��130��6�   $��Right=�5      $��Right=�63   ���#333.@�5      �� $��Filtrar tickets��6   {�.  6M   �� 5      $��Filtrar por Status��6   {�.  $
��Bold"�6N   6b   �� 5      $��Nuevo��6   61   �� 5      $��Abierto��6   63   �� 5      $��Solucionado��6   67   �� 5      $��Filtrar por Prioridad��6   {�.  $
��Bold"�6Q   6e   �� 5      $
��Baja��6   60   �� 5      $��Media��6   61   �� 5       $
��Alta��6   60   �� 5!      $��Critico��6   63   5"      �� 5%      -   5         ��Name$ 	btnBuscar��5%      $��20��6(   $��22��63   $��Right=�6?   $��Right=�6[   $��0,0,16,6q�6r   $��1,1,1,1q�6�   ���#333.��5&      �� $5��/C:\Users\SISTEMAS\Documents\Imagenes\search.png��6   $��25��6Q   6_   5'      �� 5)      -   5         ��Name$ txtSearchBar��5)      $��22��6,   $	��250��68   $��Right=�6D   $��Right=�6`   $��0,0,0,6q�6w   $��2,2,2,2q�6�   ���#3336�   �� 5,      -   5        $ btnActualizar��5,      $��20��6,   $��22��67   $��Right=�6C   $��Right=�6_   $��0,0,16,6q�6v   $��1,1,1,1q�6�   ���#333.��5-      �� $5��/C:\Users\SISTEMAS\Documents\Imagenes\update.png��6   $��25��6Q   6_   5.      �� 50      $��Tickets��6   $��80��6'   $��25��62   $��20(�6>   $
��Bold"�6L   
���   �$
��Left=�6q   $
��Left=�6�   $��12,0,0,8q�51      6$   52      �� 54   
   $��1��6   .+�55      �� $	��440��6   .��56      &   System.Windows.Controls.DataGrid  -   5          Name$ tableTickets��56      $��Stretch=�61      AutoGenerateColumns .  $	��600��6i   $	��833��6v   ��.    Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-   6;     59      $ Titulo del Ticket��6-   6J     5:      $ Usuario��6-   6@     5;      $ Sistema��6-   6@     5<      $ Empresa��6-   6@     5=      $ Status��6-   6?     5>      $ 	Prioridad��6-   6B     5?      $ Observaciones��6-   6F     5@      $	 PDF��6-   6<     5A      $ Correo��6-   6?     5B      $ Responsable��6-   6D     5C      $ Fecha de creación��6-   6J     5D      $ Fecha de finalización��6-   6N   5F      5G      5H      � 5J   
   $��2��6   $��54��6   .+�5K      �� -   5        $ btnAgregarResponsable��5K      $��Agregar Responsable��64   $	��130��6R   $��25��6^   $
��Left=�6j   ���#8B3F3C
�������$��12(�5L      $
��Bold"�6   
������ $��132,27,55,2q�6;   $��1,1,1,1q�6P   6�   �� 5M      -   5        $ btnSolucionado��5M      $��Marcar como solucionado��6-   $	��161��6O   $��25��6[   $��Center=�6g   ���#8B3F3C
�������$��12(�5N      $
��Bold"�6   
������ $��3,27,0,2q�6;   $��1,1,1,1q�6M   6i   �� 5O      -   5        $ btnEliminar��5O      $��Eliminar Ticket��6*   $��95��6D   $��25��6O   $��Right=�6[   ���#8B3F3C
�������$��12(�5P      $
��Bold"�6   
������ $��
0,27,132,2q�6;   $��1,1,1,1q�6O   6k   5Q      5R      5S      