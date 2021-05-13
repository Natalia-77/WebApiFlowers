using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebFlower.Entities.Domain;
using WebFlower.ModelFlowers;


namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private static readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
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
        public async Task<bool> PostRequest()
        {
            Flower model = new Flower
            {
                Name = "test2",
                Family = "family_test_2",
                Weight = 1000,
                Image = "2021-Ford-Th999underbird-Rebord.jpg"
            };

            WebRequest request = WebRequest.Create("http://localhost:5000/api/Flowers/add");
            {
                request.Method = "POST";
                request.ContentType = "application/json";

            };
            string json = JsonConvert.SerializeObject(model);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                await request.GetResponseAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => PostRequest());

        }

        //================================================


        //public void RequestForPost()
        //{
        //    // Uri url = new Uri("https://nat77.ga/api/Flowers/add");

        //    // Create a request using a URL that can receive a post.
        //    WebRequest request = WebRequest.Create("http://localhost:5000/api/Flowers/add");
        //    // Set the Method property of the request to POST.
        //    request.Method = "POST";

        //    // Create POST data and convert it to a byte array.
        //    string postData = JsonConvert.SerializeObject(new
        //    {
        //        Name = "ytr",
        //    Family = "qqq",
        //    Weight = 55555,
        //    Image = "hyu"
        //});
        //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //// Set the ContentType property of the WebRequest.
        //request.ContentType = "application/json";
        //// Set the ContentLength property of the WebRequest.
        //request.ContentLength = byteArray.Length;

        //// Get the request stream.
        //Stream dataStream = request.GetRequestStream();
        //// Write the data to the request stream.
        //dataStream.Write(byteArray, 0, byteArray.Length);
        //// Close the Stream object.
        //dataStream.Close();

        //try
        //{
        //    // Get the response.
        //    WebResponse response = request.GetResponse();
        //    // Display the status.
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

        //    // Get the stream containing content returned by the server.
        //    // The using block ensures the stream is automatically closed.
        //    using (dataStream = response.GetResponseStream())
        //    {
        //        // Open the stream using a StreamReader for easy access.
        //        StreamReader reader = new StreamReader(dataStream);
        //        // Read the content.
        //        string responseFromServer = reader.ReadToEnd();
        //        // Display the content.
        //        Console.WriteLine(responseFromServer);
        //    }

        //        // Close the response.
        //        response.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}








    }
}
