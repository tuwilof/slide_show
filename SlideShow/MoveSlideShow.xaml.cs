using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SlideShow
{
    /// <summary>
    /// Interaction logic for MoveSlideShow.xaml
    /// </summary>
    public partial class MoveSlideShow : Window
    {
        public MoveSlideShow()
        {
            InitializeComponent();
        }

        public void theImage(string urlImg)
        {
            BitmapImage loadImage = new BitmapImage();
            loadImage.BeginInit();
            loadImage.UriSource = new Uri(urlImg.Replace("%20", " "));
            loadImage.EndInit();

            veryBigImage.Source = loadImage;
        }

        private DispatcherTimer timer = null;
        private int x;
        private string[] pic;
        private int[] t;
        int j;

        public void timerStart(List<Slide> pictures)
        {
            x = 0;
            int n = pictures.Count;
            pic = new string[n];
            t = new int[n];
            int i = 0;
            foreach (var s in pictures)
            {
                pic[i] = s.FileInfo;
                t[i] = s.Time;
                i++;
            }

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            x += 100;
            try
            {
                if (t[j] > x)
                {
                    theImage(pic[j]);
                }
                else
                {
                    x = 0;
                    j++;
                }
            }
            catch
            {
                veryBigImage.Source = null;
            }
        }
    }
}
