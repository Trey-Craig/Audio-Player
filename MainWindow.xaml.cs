using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Media_Player {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        
        Music music = new Music();
		private bool userIsDraggingSlider = false;
        string playlist_name = "";
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        
        public MainWindow() {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
        }//end constructor

        

        private void timer_Tick(object sender, EventArgs e) {
			if((music.music_player.Source != null) && (music.music_player.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
			{
                //align slider to each duration of song
				slider.Minimum = 0;
				slider.Maximum = music.music_player.NaturalDuration.TimeSpan.TotalSeconds;
				slider.Value = music.music_player.Position.TotalSeconds;
			}
            //when a song reaches the end it will play the next song
            if(slider.Value == slider.Maximum) {
                music.next_Song();
            }
		}
        
        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
		{
			userIsDraggingSlider = true;
		}

		private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			userIsDraggingSlider = false;
            //duration is set to slider
			music.music_player.Position = TimeSpan.FromSeconds(slider.Value);
		}

		private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
            //displays duration
			duration_txt.Content = TimeSpan.FromSeconds(slider.Value).ToString(@"hh\:mm\:ss");
		}

        private void Play_bttn_Click(object sender, RoutedEventArgs e) {    
          music.Play_Song();     
          picture.Source = new Uri(music.Picture);
          Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
            //displays current song title, artist, and album
            title_lbl.Content = song_detail.TagHandler.Title;
            artist_lbl.Content = song_detail.TagHandler.Artist;
            album_lbl.Content = song_detail.TagHandler.Album;
        }

        private void pause_bttn_Click(object sender, RoutedEventArgs e) {       
            music.Pause_Song(); 
            //start recording
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
        }

        private void stop_bttn_Click(object sender, RoutedEventArgs e) {
            music.Stop_Song();
            picture.Source = null;
            title_lbl.Content = "";
            artist_lbl.Content = "";
            album_lbl.Content = "";
        }
        
        private void AddFile_Click(object sender, RoutedEventArgs e) {
            //saves filename path to variable
            music.FileName = LaunchSingleFileDialog();
            if(music.FileName != "") {
                //allows access to mp3 properties i.e.(title, artist, album)
                Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
                //adds song to queue
                music.Picture = song_detail.TagHandler.Genre;
                music.songlist.Add(music.FileName);
                music.img_list.Add(music.Picture);
                music.LoadFile();
                //displays title artist and album of each song
                music_list.Items.Add($"{song_detail.TagHandler.Title} - {song_detail.TagHandler.Artist} - " +
                    $"{song_detail.TagHandler.Album} ");// adds song name to list          
            }//end if
            else {
                duration_txt.Content = "File Not Selected...";
            }
        }
        private void Add_Files_Click(object sender, RoutedEventArgs e) {
           Microsoft.Win32.OpenFileDialog mfd_temp = new Microsoft.Win32.OpenFileDialog();
            mfd_temp.DefaultExt = ".mp3";
            mfd_temp.Filter     = "Audio Files (*.mp3)|*.mp3|Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            mfd_temp.Title = "Song Select";
            mfd_temp.Multiselect = true;
            
            if(mfd_temp.ShowDialog() == true) {
                for (int index = 0; index < mfd_temp.FileNames.Length; index++) {
                    Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(mfd_temp.FileNames[index]);
                    //adds song to queue
                    music.Picture = song_detail.TagHandler.Genre;
                    music.songlist.Add(mfd_temp.FileNames[index]);
                    music.img_list.Add(music.Picture);
                    music.LoadFile();
                    //displays title artist and album of each song
                    music_list.Items.Add($"{song_detail.TagHandler.Title} - {song_detail.TagHandler.Artist} - " +
                        $"{song_detail.TagHandler.Album} ");// adds song name to list 
                    }//end for loop
            }//end if
            else {
                duration_txt.Content = "No files selected...";
            }
            
        }

        //open file
        private string LaunchSingleFileDialog() {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog ofd_temp = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension       
            ofd_temp.DefaultExt = ".mp3";
            ofd_temp.Filter     = "Audio Files (*.mp3)|*.mp3|Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            ofd_temp.Title = "Song Select";
            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = ofd_temp.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true) {
                return ofd_temp.FileName;
            }//end if
            return "";
        }//end method;

        private string SaveFileDialog() {
            Microsoft.Win32.SaveFileDialog save_file = new Microsoft.Win32.SaveFileDialog();
            //save file
            save_file.DefaultExt = ".txt";
            save_file.Filter = "Text File (*.plst)|*.plst|All Files (*.*)|*.*";

            bool? result = save_file.ShowDialog();

            if (result == true) {
                return save_file.FileName;
            }
            return "";
        }
        private void next_bttn_Click(object sender, RoutedEventArgs e) {
            music.next_Song();
            picture.Source = new Uri(music.Picture);
            Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
            title_lbl.Content = song_detail.TagHandler.Title;
            artist_lbl.Content = song_detail.TagHandler.Artist;
            album_lbl.Content = song_detail.TagHandler.Album;
        }

        private void previous_bttn_Click(object sender, RoutedEventArgs e) {
            music.previous_Song();
            picture.Source = new Uri(music.Picture);
            Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
            title_lbl.Content = song_detail.TagHandler.Title;
            artist_lbl.Content = song_detail.TagHandler.Artist;
            album_lbl.Content = song_detail.TagHandler.Album;
        }

        private void repeat_playlist_Click(object sender, RoutedEventArgs e) {
           music.repeat_playlist = true;
           repeat_loop.BorderBrush = Brushes.Red;
        }

        private void repeat_playlist_rightclick(object sender, MouseButtonEventArgs e) {
            music.repeat_playlist = false;
            repeat_loop.BorderBrush = Brushes.Transparent;
        }

        private void repeat_song_bttn_Click(object sender, RoutedEventArgs e) {
            music.repeat_song = true;
            music.repeat_Song();
            repeat_song_bttn.BorderBrush = Brushes.Red;
        }
        private void repeat_song_rightclick(object sender, MouseButtonEventArgs e) {
            music.repeat_song = false;
            repeat_song_bttn.BorderBrush = Brushes.Black;
        }
        private void shuffle_bttn_Click(object sender, RoutedEventArgs e) {
            music.Shuffle();
            //end and save record
            mciSendString("save recsound c:\\Users\\MCA\\Downloads\\test.wav", "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);
            picture.Source = new Uri(music.Picture);
            Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
            title_lbl.Content = song_detail.TagHandler.Title;
            artist_lbl.Content = song_detail.TagHandler.Artist;
            album_lbl.Content = song_detail.TagHandler.Album;
        }

        private void Dark_Mode_Click(object sender, RoutedEventArgs e) {
            window.Background = Brushes.MidnightBlue;
            music_list.Foreground = Brushes.LightCyan;
            music_list.Background = Brushes.Blue;
            duration_txt.Background = Brushes.MidnightBlue;
            duration_txt.Foreground = Brushes.White;
            Play_bttn.Background = Brushes.DarkOrange;
            pause_bttn.Background = Brushes.DarkOrange;
            stop_bttn.Background = Brushes.DarkOrange;
            repeat_loop.Background = Brushes.DarkOrange;
            repeat_song_bttn.Background = Brushes.DarkOrange;
            shuffle_bttn.Background = Brushes.DarkOrange;
            next_bttn.Background = Brushes.DarkOrange;
            previous_bttn.Background = Brushes.DarkOrange;
            title_lbl.Background = Brushes.MidnightBlue;
            artist_lbl.Background = Brushes.MidnightBlue;
            album_lbl.Background = Brushes.MidnightBlue;
            title_lbl.Foreground = Brushes.White;
            artist_lbl.Foreground = Brushes.White;
            album_lbl.Foreground = Brushes.White;
        }

        private void Spotify_Mode_Click(object sender, RoutedEventArgs e) {
            window.Background = Brushes.Black;
            window.BorderBrush = Brushes.Lime;
            music_list.Foreground = Brushes.Green;
            music_list.Background = Brushes.LightGray;
            duration_txt.Background = Brushes.Black;
            duration_txt.Foreground = Brushes.LimeGreen;
            Play_bttn.Background = Brushes.Lime;
            pause_bttn.Background = Brushes.Lime;
            stop_bttn.Background = Brushes.Lime;
            repeat_loop.Background = Brushes.Lime;
            repeat_song_bttn.Background = Brushes.Lime;
            shuffle_bttn.Background = Brushes.Lime;
            next_bttn.Background = Brushes.Lime;
            previous_bttn.Background = Brushes.Lime;
            title_lbl.Background = Brushes.Black;
            artist_lbl.Background = Brushes.Black;
            album_lbl.Background = Brushes.Black;
            title_lbl.Foreground = Brushes.LimeGreen;
            artist_lbl.Foreground = Brushes.LimeGreen;
            album_lbl.Foreground = Brushes.LimeGreen;
        }

        private void Default_Skin_Click(object sender, RoutedEventArgs e) {
            window.Background = Brushes.White;
            music_list.Foreground = Brushes.Black;
            music_list.Background = Brushes.White;
            duration_txt.Background = Brushes.White;
            duration_txt.Foreground = Brushes.Black;
            Play_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            pause_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            stop_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            repeat_loop.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            repeat_song_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            shuffle_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            next_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            previous_bttn.Background = (Brush)new BrushConverter().ConvertFrom("#FFDDDDDD");
            title_lbl.Background = Brushes.White;
            title_lbl.Foreground = Brushes.Black;
            artist_lbl.Background = Brushes.White;
            artist_lbl.Foreground = Brushes.Black;
            album_lbl.Background = Brushes.White;
            album_lbl.Foreground = Brushes.Black; 
        }

        private void Selected_Song(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
           music.song_number = music_list.Items.IndexOf(music_list.SelectedItem);
           music.Pic_num = music_list.Items.IndexOf(music_list.SelectedItem);
           music.LoadFile();
           music.Play_Song();
           picture.Source = new Uri(music.Picture);
            Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(music.FileName);
            title_lbl.Content = song_detail.TagHandler.Title;
            artist_lbl.Content = song_detail.TagHandler.Artist;
            album_lbl.Content = song_detail.TagHandler.Album;
        }

        private void Save_Playlist_Click(object sender, RoutedEventArgs e) {
            //name playlist when saving
            playlist_name = SaveFileDialog();
            string file_paths = "";
            for (int i = 0; i < music.songlist.GetSize; i++) {
                //adds each file name to a string
                file_paths += $"{music.songlist.Get(i).Data.ToString()}{(char)13}";
            }

            string[] playlist_songs = file_paths.Split((char)13);
            if(playlist_name != "") {
                //writes each filename to a txt.file and names the playlist
                File.WriteAllLines(playlist_name, playlist_songs);
            }
            else {
                duration_txt.Content = "Playlist Not Saved";
            }
        }

        private void Load_Playlist_Click(object sender, RoutedEventArgs e) {
            //sets file_path for reading
            string file_path = LaunchSingleFileDialog();
            string[] songs = File.ReadAllLines(file_path);
            //clear songlist and listbox
            music_list.Items.Clear();
            music.songlist.Clear();
            //adds each song to playlist, listbox
            for (int i = 0; i < songs.Length - 1; i++) {
                Mp3Lib.Mp3File song_detail = new Mp3Lib.Mp3File(songs[i]);
                music_list.Items.Add($"{song_detail.TagHandler.Title} - {song_detail.TagHandler.Artist} - " +
                $"{song_detail.TagHandler.Album}");
                music.songlist.Add(songs[i]);
                music.img_list.Add(song_detail.TagHandler.Genre);
            }            
            music.song_number = 0;
            music.LoadFile();
        }

    }
    }
