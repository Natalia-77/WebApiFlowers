using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        private string file_selected = string.Empty;
        public string file_name { get; set; }

        ObservableCollection<string> _errors;
        public PostWindow()
        {            
            InitializeComponent();
            
        }

        //Кнопка для вибору фото
        private void btn_select_photo(object sender, RoutedEventArgs e)
        {
            Bitmap image;
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;" +
                "*.PNG|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    image = new Bitmap(dlg.FileName);
                    file_selected = dlg.FileName;
                }
                catch
                {
                    MessageBox.Show("Неможливо відкрити!");
                }
            }
        }

        //Кнопка зберігання.
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            _ = PostRequest();
        }


        public async Task PostRequest()
        {
            if (!string.IsNullOrEmpty(file_selected))
            {

                var extension = System.IO.Path.GetExtension(file_selected);
                var imageName = System.IO.Path.GetFileNameWithoutExtension(file_selected) + extension;
                var dir = Directory.GetCurrentDirectory();
                var saveDir = System.IO.Path.Combine(dir, "Photos");
                // if (!Directory.Exists(saveDir))
                // Directory.CreateDirectory(saveDir);
                 var fileSave = System.IO.Path.Combine(saveDir, imageName);              
               

                var bmp = ResizeImage.ResizeOrigImg(
                    new Bitmap(System.Drawing.Image.FromFile(file_selected)), 75, 75);

                bmp.Save(fileSave, ImageFormat.Jpeg);                
                file_name = fileSave;

            }


             WebRequest request = WebRequest.Create("http://localhost:5000/api/Flowers/add");
            {
                request.Method = "POST";
                request.ContentType = "application/json";

            };
            string json = JsonConvert.SerializeObject(new
            {
                Name = tbname.Text.ToString(),
                Family = tbfamily.Text.ToString(),
                Weight = int.Parse(tbweight.Text),
                Image = file_name

            }) ;
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            string responseBody = string.Empty;
            try
            {
                await request.GetResponseAsync();
               
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                   
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                   
                }
                
            }
            catch (WebException e)
            {
                

                using var stream = e.Response.GetResponseStream();
                using var reader = new StreamReader(stream);
                var responce = reader.ReadToEnd();
                MessageBox.Show(responce);
                var errors = JsonConvert.DeserializeObject<ErrorValid>(responce);
                _errors = new ObservableCollection<string>();

                
                if (errors.Errors.Name.Count != 0)
                {
                    for (int i = 0; i < errors.Errors.Name.Count; i++)
                    {
                         //MessageBox.Show(errors.Errors.Name[i]);
                        _errors.Add(errors.Errors.Name[i]);
                    }
                }

                if (errors.Errors.Family.Count != 0)
                {
                    for (int j = 0; j < errors.Errors.Family.Count; j++)
                    {
                        //MessageBox.Show(errors.Errors.Family[j]);
                        _errors.Add(errors.Errors.Family[j]);
                    }
                }

                if (errors.Errors.Weight.Count != 0)
                {
                    for (int k = 0; k < errors.Errors.Weight.Count; k++)
                    {
                        //MessageBox.Show(errors.Errors.Weight[k].ToString());
                        _errors.Add(errors.Errors.Weight[k].ToString());
                    }
                }

                if (errors.Errors.Image.Count != 0)
                {
                    for (int m = 0; m < errors.Errors.Image.Count; m++)
                    {
                        //MessageBox.Show(errors.Errors.Image[m]);
                        _errors.Add(errors.Errors.Image[m]);
                    }
                }


                List<ErrorsList> eror_list = new List<ErrorsList>();

                eror_list = _errors.Select(item => new ErrorsList
                {
                    Errorlist = item
                }).ToList();

                dg_errors.ItemsSource = eror_list;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
               
            }
        }

        
    }
}
