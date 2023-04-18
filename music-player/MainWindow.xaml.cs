using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace music_player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> musicFiles = new List<string>();
        private int currentTrackIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void LoadMusicFiles(string folderPath)
        {
            // Load a list of music files from the selected folder
            musicFiles.Clear();
            string[] files = Directory.GetFiles(folderPath, "*.mp3");
            foreach (string file in files)
            {
                musicFiles.Add(file);
            }
            currentTrackIndex = -1;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex >= 0)
            {
                mediaPlayer.Play();
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex >= 0)
            {
                mediaPlayer.Pause();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex >= 0)
            {
                mediaPlayer.Stop();
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex > 0)
            {
                currentTrackIndex--;
                mediaPlayer.Source = new Uri(musicFiles[currentTrackIndex]);
                mediaPlayer.Play();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrackIndex < musicFiles.Count - 1)
            {
                currentTrackIndex++;
                mediaPlayer.Source = new Uri(musicFiles[currentTrackIndex]);
                mediaPlayer.Play();
            }
        }

        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Move to the next track when the current track has finished playing
            if (currentTrackIndex < musicFiles.Count - 1)
            {
                currentTrackIndex++;
                mediaPlayer.Source = new Uri(musicFiles[currentTrackIndex]);
                mediaPlayer.Play();
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = volumeSlider.Value;
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a folder selection dialog box and load the music files from the selected folder
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Select folder containing music files";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "Folder Selection.";
            dialog.Filter = "Folders|no.files";
            dialog.ValidateNames = false;
            if (dialog.ShowDialog() == true)
            {
                string folderPath = System.IO.Path.GetDirectoryName(dialog.FileName);
                LoadMusicFiles(folderPath);
            }
        }
    }
}