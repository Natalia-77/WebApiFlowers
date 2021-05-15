using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebFlower.Entities;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for PutWindow.xaml
    /// </summary>
    public partial class PutWindow : Window
    {
        private EFContext _context;
        public long _res { get; set; }
        public PutWindow(long res,EFContext context)
        {
            InitializeComponent();
            _res = res;
            _context = context;
        }

     
        private void btn_savechanges(object sender, RoutedEventArgs e)
        {
            //Task.Run(() => PutRequest());      

            _ = PutRequest();
        }

        public async Task<bool> PutRequest()
        {      
             var n = _context.Flowers.FirstOrDefault(x => x.Id == _res);


            if (!string.IsNullOrEmpty(tbname.Text))
                n.Name = tbname.Text.ToString();

            if (!string.IsNullOrEmpty(tbfamily.Text))
                n.Family = tbfamily.Text.ToString();

            if (!string.IsNullOrEmpty(tbweight.Text))
                n.Weight = int.Parse(tbweight.Text);

            WebRequest request = WebRequest.Create($"http://localhost:5000/api/Flowers/{_res}");
                {
                    request.Method = "PUT";
                    request.ContentType = "application/json";

                };
            
            string json = JsonConvert.SerializeObject(new
            {
                n.Id,
                n.Name,
                n.Family,
                n.Weight,
                Image = "fgfhf"

            });

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
    }
}
