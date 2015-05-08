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

                    Image smallImage = new Image();
                    smallImage.Source = loadImage;
                    smallImage.Height = 80;
                    ListBox.Items.Add(smallImage);

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void listBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                BitmapImage loadImage = new BitmapImage();
                loadImage.BeginInit();
                loadImage.UriSource = new Uri(((System.Windows.Controls.Image)(item.Content)).Source.ToString());
                loadImage.EndInit();

                bigImage.Source = loadImage;
            }
        }
    }
}
