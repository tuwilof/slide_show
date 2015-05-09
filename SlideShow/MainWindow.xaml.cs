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
using System.Xml.Serialization;

namespace SlideShow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> pictures;
        public MainWindow()
        {
            InitializeComponent();
            pictures = new List<string>();
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

                    pictures.Add(loadImage.UriSource.AbsolutePath);

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

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);

                    MySlideShow slideShow = new MySlideShow();
                    slideShow.Head = new Head();
                    slideShow.Head.Title = "SlideShow1";

                    slideShow.Body = new List<Slide>();

                    int count = ((System.Windows.Controls.ItemsControl)(ListBox)).Items.Count;

                    foreach(var p in pictures) 
                    {
                        slideShow.Body.Add(new Slide() { FileInfo = p, Time = 1000, Type = "picture" });
                    }

                    XmlSerializer formatter = new XmlSerializer(typeof(MySlideShow));
                    FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate);
                    formatter.Serialize(fs, slideShow);

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            pictures = new List<string>();
            ListBox.Items.Clear();

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    XmlSerializer formatter = new XmlSerializer(typeof(MySlideShow));
                    MySlideShow slideShow;
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        slideShow = (MySlideShow)formatter.Deserialize(fs);
                    }

                    foreach (var slide in slideShow.Body) 
                    {
                        FileInfo fileInfo2 = new FileInfo(slide.FileInfo.Replace("%20", " "));

                        BitmapImage loadImage = new BitmapImage();
                        loadImage.BeginInit();
                        loadImage.UriSource = new Uri(fileInfo2.FullName);
                        loadImage.EndInit();

                        Image smallImage = new Image();
                        smallImage.Source = loadImage;
                        smallImage.Height = 80;
                        ListBox.Items.Add(smallImage);

                        pictures.Add(loadImage.UriSource.AbsolutePath);
                    }

                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
