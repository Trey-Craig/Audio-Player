   M S B A M L   `   `   `  ����G  CMedia Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Media_PlayerMedia_Player  5   	       Media_Player.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Media_Player     5   	   -   5          �Name$  window��5   	    � Title$ Music Player��5   	   $��809.2��6   $	��585��6-    J� SnapsToDevicePixels . 
�������.��5
      � $��	0,0,0,1.5q�6   .+�5   
   �� -   5         ��Name$ 	next_bttn��5      $
��Left=�6$   $��431,566,0,0q�6?   $	��Top=�6T   $��70��6l   $��66��6w   .��5      �� $U��OC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\next.png��6    J� RenderTransformOrigin$ 0.5,0.5&�6m   6�   5      �� 5   
   -   5        $ 	stop_bttn��5      $
��Left=�6$   $��335,566,0,0q�6?   $	��Top=�6T   $��65��6l   $��66��6w   .��5      �� $U��OC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\stop.png��6   $��55��6m   $��50��6x   6�   5      �� 5   
   -   5        $ 
pause_bttn��5      $
��Left=�6%   $��234,566,0,0q�6@   $	��Top=�6U   $��70��6m   $��66��6x   .��5      �� $V��PC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\pause.png��6   6p   5      �� 5   
   -   5        $ 	Play_bttn��5      $
��Left=�6$   $��139,566,0,0q�6?   $	��Top=�6T   $��60��6l   $��66��6w   .��5      �� $U��OC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\play.png��6   6o   5      �� 5   
   -   5        $ previous_bttn��5      $
��Left=�6(   $��
39,566,0,0q�6C   $	��Top=�6W   $��74��6o   $��66��6z   .��5      �� $Y��SC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\previous.png��6   $��56��6q   6   5      o� 5   
   $
��Left=�6   $��26��6*   $	��Top=�66   $	��795��6N   .@�5      m� $
��File��6   .@�5       m� -   5        $��Add_File��5       $��Add File��6+   6=   m� 5!      -   5        $��	Add_Files��5!      $��	Add Files��6,   6?   m� 5"      -	   5        $��Default_Skin��5"      $��Default Skin��6/   6E   m� 5#      -
   5        $��	Dark_Mode��5#      $��	Dark Mode��6,   6?   m� 5$      -   5        $��Spotify_Mode��5$      $��Spotify Mode��6/   6E   5%      m� 5&      $��Playlist��6   .@�5'      m� -   5        $��Save_Playlist��5'      $
��Save��60   6>   m� 5(      -   5        $��Load_Playlist��5(      $
��Load��60   6>   5*      5,      �� 5-   
   -   5         ��Name$ 
music_list��5-      $
��Left=�6&   $	��296��6A   $	��Top=�6N   $	��262��6f   $��
298,31,0,0q�6r   6�   �� 5/   
   -   5        $ repeat_loop��5/      $
��Left=�6&   $��124,667,0,0q�6A   $	��Top=�6V   $��75��6n   $��69��6y   .��50      �� $W��QC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\repeat.png��6   $��59��6o   $��50��6{   6�   51      �� 52   
   -   5        $ repeat_song_bttn��52      $
��Left=�6+   $��229,667,0,0q�6F   $	��Top=�6[   $��75��6s   $��69��6~   .��53      �� $[��UC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\repeat_one.png��6   $��59��6s   $��50��6   6�   54      �� 55   
   -   5        $ shuffle_bttn��55      $
��Left=�6'   $��335,667,0,0q�6B   $	��Top=�6W   $��85��6o   $��69��6z   .��56      �� $X��RC:\Users\MCA\source\repos\Media_Player\Media_Player\Media_Player_Icons\shuffle.png��6   $��59��6p   $��50��6|   6�   57      �� 59   
   -   5         ��Name$ duration_txt��59      $��Pick A Song!��6&   $
��Left=�6=   $��364,446,0,0q�6X   $	��Top=�6m   $��37��6�   $	��137��6�   $��18(�6�   �� 5:   
   -   5         ��Name$ slider��5:      $
��Left=�6!   $��284,488,0,0q�6<   $	��Top=�6Q   $��27��6i   $	��276��6u   5;   n   r� 5<   
   -   5         r�Name$ picture��5<      $
��Left=�6(   $	��275��6C   $	��Top=�6P   $	��270��6h   $��	10,52,0,0q�6t   �� 5=   
   -   5        $ 	title_lbl��5=      $�� ��6#   $
��Left=�6.   $��
39,332,0,0q�6I   $	��Top=�6]   $��36��6u   $	��462��6�   $��22(�6�   �� 5>   
   -   5        $ 
artist_lbl��5>      $�� ��6$   $
��Left=�6/   $��
39,368,0,0q�6J   $	��Top=�6^   $��40��6v   $	��462��6�   $��22(�6�   �� 5?   
   -   5        $ 	album_lbl��5?      $�� ��6#   $
��Left=�6.   $��
39,408,0,0q�6I   $	��Top=�6]   $��42��6u   $	��462��6�   $��22(�6�   5@      5A      