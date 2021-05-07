using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using WebFlower.ModelFlowers;


namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {     
        
        public MainWindow()
        {
            InitializeComponent();
            //dgFlowers.ItemsSource = flowers;
        }
       

        private  void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadDataCompleted += asyncWeb_DownloadDataCompleted;
                Uri url = new Uri("https://nat77.ga/api/Flowers/Search");
                client.DownloadDataAsync(url);
            }

        }

        void asyncWeb_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {           
            try
            {
                string res = Encoding.Default.GetString(e.Result);
                var list = JsonConvert.DeserializeObject<List<FlowerVM>>(res);
                dgFlowers.ItemsSource = list;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());               
            }
            
        }
    }
}
