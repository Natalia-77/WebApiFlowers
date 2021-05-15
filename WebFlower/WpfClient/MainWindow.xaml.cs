using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using WebFlower.Entities;
using WebFlower.Entities.Domain;



namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFContext _context = new EFContext();
        public long _id { get; set; }

        public MainWindow()
        {
            Thread.Sleep(2000);
            InitializeComponent();        
            
        }


        private void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadDataCompleted += asyncWeb_DownloadDataCompleted;
                Uri url = new Uri("https://nat77.ga/api/Flowers/Search");
                client.DownloadDataAsync(url);
            }

        }

        public void asyncWeb_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string res = Encoding.Default.GetString(e.Result);
                var list = JsonConvert.DeserializeObject<List<Flower>>(res);
                dgFlowers.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        
        //===========Те,що вчора зробила=========
        //public async Task PostRequest()
        //{
        //    Flower model = new Flower
        //    {
        //        Name = "test3",
        //        Family = "family_test_3",
        //        Weight = 1000,
        //        Image = "satin.jpg"
        //    };

        //    WebRequest request = WebRequest.Create("http://localhost:5000/api/Flowers/add");
        //    {
        //        request.Method = "POST";
        //        request.ContentType = "application/json";

        //    };
        //    string json = JsonConvert.SerializeObject(model);
        //    byte[] bytes = Encoding.UTF8.GetBytes(json);

        //    using (Stream stream = await request.GetRequestStreamAsync())
        //    {
        //        stream.Write(bytes, 0, bytes.Length);
        //    }

        //    try
        //    {
        //        await request.GetResponseAsync();
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //        //return false;
        //    }
        //}

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            //Task.Run(() => PostRequest());
           
            new PostWindow().ShowDialog();
        }

        private void btnPut_Click(object sender, RoutedEventArgs e)
        {
            if (dgFlowers.SelectedItem != null)
            {
                if (dgFlowers.SelectedItem is Flower)
                {
                    var flowView = dgFlowers.SelectedItem as Flower;
                    long id = flowView.Id;
                    _id = id;
                    MessageBox.Show(_id.ToString());
                }
            }

            PutWindow edit = new PutWindow(_id,_context);
            edit.ShowDialog();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgFlowers.SelectedItem != null)
            {
                if (dgFlowers.SelectedItem is Flower)
                {
                    var flowerView = dgFlowers.SelectedItem as Flower;
                    long id = flowerView.Id;
                    _id = id;
                    MessageBox.Show(_id.ToString());
                }
            }

            DeleteWindow delete = new DeleteWindow(_id, _context);
            delete.ShowDialog();

        }
      

        










    }
}
