using HelperLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Helper;
using WpfClient.Model;
using WpfClient.Validation;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
       // private string file_selected = string.Empty;
        public string File_name { get; set; }
        public string File_base { get; set; }

        public PostWindow()
        {            
            InitializeComponent();
                        
        }

        //Кнопка для вибору фото
        private void btn_select_photo(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;" +
                "*.PNG|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    File_base = dlg.FileName;
                }
                catch
                {
                    MessageBox.Show("Неможливо відкрити!");
                }
            }
        }

        //Кнопка зберігання.
        private  void btn_save_Click(object sender, RoutedEventArgs e)
        {
             _ = PostRequest();
            //await Task.Run(() => PostRequest());
        }


        public async Task<bool> PostRequest()
        {
            string base64 = ImageConverterBase.ConvertToBase(File_base);
            //WebRequest request = WebRequest.Create("http://localhost:5000/api/Flowers/add");
            var applic = Application.Current as IGetConfiguration;
            var serv_url=applic.Configuration.GetSection("Url").Value;
             WebRequest request = WebRequest.Create($"{serv_url}api/Flowers/add");
            {
                request.Method = "POST";
                request.ContentType = "application/json";

            };
            int.TryParse(tbweight.Text, out int resparse);
            string json = JsonConvert.SerializeObject(new
            {
                
                Name = tbname.Text.ToString(),
                Family = tbfamily.Text.ToString(),               
                Weight=resparse,
                Image = base64

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
            catch (WebException e)
            {

               
                using (WebResponse response = e.Response)
                {
                    
                    HttpWebResponse web= (HttpWebResponse)response;
                  
                    MessageBox.Show("Error-->>>" + web.StatusCode);
                    using var stream = e.Response.GetResponseStream();
                    using var reader = new StreamReader(stream);
                    var responces = reader.ReadToEnd();                   
                    var errors = JsonConvert.DeserializeObject<ErrorValid>(responces);                   


                    List<ErrorsList> eror_list = new List<ErrorsList>();
                    eror_list = errors.Errors.ErrorRes().Select(item => new ErrorsList
                    {  Errorlist = item
                    }).ToList();

                    dg_errors.ItemsSource = eror_list;
                }
                return false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            
        }
        
        
        

        
    }
}
