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
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public MainWindow()
        {


            InitializeComponent();

            //var videoPath = Directory.GetCurrentDirectory();
            //video.Source = new Uri(videoPath + @"film.flv", UriKind.Relative);
            

        }

        private void videoClipPlayHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Play();
            if (timer != null)
                timer.Start();
        }
        private void videoClipPauseHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Pause();
            if (timer != null)
                timer.Stop();
        }
        private void videoClipStopHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void videoClip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            videoClip.ScrubbingEnabled = true;
            videoClip.Stop();
        }

        private void videoClip_MediaOpened(object sender, RoutedEventArgs e)
        {
            timerSlider.Maximum = videoClip.NaturalDuration.TimeSpan.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerSlider.Value = videoClip.Position.TotalSeconds;
        }

        private void timerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoClip.Position = TimeSpan.FromSeconds(timerSlider.Value);
        }

        private void timerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            videoClipPauseHandler(sender, e);

        }

        private void timerSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            videoClipPlayHandler(sender, e);
        }
    }
}
