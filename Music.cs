using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace Media_Player {
    class Music
    {
        //Variables
        public MediaPlayer music_player = new MediaPlayer();
        public LinkedList<string> songlist = new LinkedList<string>();
        public LinkedList<string> img_list = new LinkedList<string>();
        string _file_name= "";
        string _picture = "";
        string current_song = "";
        string current_pic = "";
        private int song_num = 0;
        private int pic_num = 0;
		public bool userIsDraggingSlider = false;
        public bool repeat_song = false;
        public bool repeat_playlist = false;

        //Properties
        public Music() {
            _file_name = null;
            _picture = null;
        }//end constructor

        public string FileName {
            get{return _file_name;}
            set{_file_name = value;}
        }

        public string Picture {
            get {return _picture;}
            set {_picture = value;}
        }

        public int song_number {
            get{return song_num;}
            set{song_num = value;}
        }

        public int Pic_num {
            get{return pic_num;}
            set{pic_num = value;}
        }

        

        public LinkedList<string> Song_List {
            get{return songlist;}
        }

        public LinkedList<string> Img_List {
            get {return img_list;}
        }
        public void Shuffle() {
            Random index = new Random();
            song_num = index.Next(0,songlist.GetSize);
            pic_num = song_num;
            //randomizes song_number and plays shuffled song
            _file_name = songlist.Get(song_num).Data.ToString();
            _picture = img_list.Get(pic_num).Data.ToString();
            music_player.Open(new Uri(_file_name));
            music_player.Play();
        }

        //sets current filename to current song to be used later

        public void LoadFile() {
            _file_name = songlist.Get(song_num).Data.ToString();
            _picture = img_list.Get(pic_num).Data.ToString();
            music_player.Open(new Uri(_file_name));
            
        }
        public void repeat_Song() {
            current_song = _file_name;
            current_pic = _picture;
        }

        public void Play_Song() {
            //plays current song          
            music_player.Play();
        }

        public void Pause_Song() {
            music_player.Pause();
        }

        public void Stop_Song() {
            music_player.Stop();
        }

        public void previous_Song() {
            //if repeat mode for the song is on previous song will only play current_song
            if(repeat_song == true){
                music_player.Open(new Uri(current_song));
                music_player.Play();
            }
            else {
                //if repeat playlist is on then playlist will loop back to the beginning
                if(song_num == songlist.GetSize - 1 && repeat_playlist) {
                    song_num = 0;
                    pic_num = 0;
                    LoadFile();
                    music_player.Play();
                }
                //playlist will go back to the beginning and wait for further instruction
                else if(song_num == songlist.GetSize - 1) {
                    song_num = 0;
                    pic_num = 0;

                }
                else {
                    //goes back to previous song in list
                    song_num--;
                    pic_num--;
                    LoadFile();
                    music_player.Play();
                }
            }
        }

        public void next_Song() {
            //if repeat mode for the song is on previous song will only play current_song
           if(repeat_song == true){
                music_player.Open(new Uri(current_song));
                music_player.Play();
           }
            else {
                //if repeat playlist is on then playlist will loop back to the beginning
                if(song_num == songlist.GetSize - 1 && repeat_playlist == true) {
                    song_num = 0;
                    pic_num = 0;
                    LoadFile();
                    music_player.Play();
                }
                //playlist will go back to the beginning and wait for further instruction
                else if(song_num == songlist.GetSize - 1) {
                    song_num = 0;
                    pic_num = 0;
                    music_player.Stop();
                    LoadFile();
                }
                //goes to next song in list
                else {
                    song_num++;
                    pic_num++;
                    LoadFile();
                    music_player.Play();
                }

            }
        }


    }
}
