using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        string current;
        string[] playList;
        
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

         

        }

        

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((me.Source != null) && (me.NaturalDuration.HasTimeSpan) )
            {
                progBar.Minimum = 0;
                progBar.Maximum = me.NaturalDuration.TimeSpan.TotalSeconds;
                progBar.Value = me.Position.TotalSeconds;
               
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
           
            foreach (string item in playList)
            {
                me.Source = new Uri(item);
                me.Play();
                preparations();
                current = item.ToString().Substring(item.ToString().LastIndexOf("\\") + 1);
                window.Title = current;
               string extention= me.Source.ToString().Substring(me.Source.ToString().IndexOf('.')+1).ToLower();
                if (extention != "mp3")
                {
                    window.Icon = new BitmapImage(new Uri("G:\\ITI\\WPF\\project\\MediaPlayer\\MediaPlayer\\mp4.png"));
                }
                if (me.NaturalDuration.HasTimeSpan)
                {
                    double end = me.NaturalDuration.TimeSpan.TotalSeconds;
                    do
                    {


                    } while (me.Position.TotalSeconds != end);
                    
                }
               
            }
        }
        void preparations()
        {

            me.Volume = soundSlidr.Value;
           
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            me.Pause();

        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                
                 playList = openFileDialog.FileNames;
                foreach (var item in playList)
                {
                     list.Items.Add(item.ToString().Substring(item.ToString().LastIndexOf("\\") + 1));
                }
                
            }
        }
        private void soundSlidr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            me.Volume = soundSlidr.Value;
        }
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            double totalSeconds = me.NaturalDuration.TimeSpan.TotalSeconds;
            progBar.Maximum = totalSeconds;
            
        }
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            me.Stop();
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            me.SpeedRatio += 1;
        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            me.SpeedRatio -= 1;
        }

        private void progBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            timelbl.Text = TimeSpan.FromSeconds(progBar.Value).ToString(@"hh\:mm\:ss")+"/"+me.NaturalDuration.TimeSpan.ToString();

        }
    }
}
