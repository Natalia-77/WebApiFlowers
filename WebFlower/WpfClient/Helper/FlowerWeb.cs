using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WebFlower.ModelFlowers;

namespace WpfClient.Helper
{
    public class FlowerWeb
    {
        private string _url = "https://nat77.ga/api/Flowers/Search";
      
        public List<FlowerVM> GetFlowers()     
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8

            };

            string data = client.DownloadString(_url);      
            dynamic list = JsonConvert.DeserializeObject<List<FlowerVM>>(data);
            var res = list.Image;
           
            return list;


        }
        public Image GetPicture()
        {

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(_url);
            MemoryStream ms = new MemoryStream(bytes);
            var image = System.Drawing.Image.FromStream(ms);


            return image;
        }




    }
}
