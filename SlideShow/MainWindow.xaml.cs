using Microsoft.Win32;
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

namespace SlideShow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
 
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    BitmapImage loadImage = new BitmapImage();
                    loadImage.BeginInit();
                    loadImage.UriSource = new Uri(fileInfo.FullName);
                    loadImage.EndInit();

                    Image image = new Image();
                    image.Source = loadImage;

                    MyPanel.Children.Add(image);

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
        }
    }
}
