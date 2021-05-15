using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using WebFlower.Entities;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        private EFContext _context;
        public long _Id { get; set; }

        public DeleteWindow(long Id, EFContext context)
        {
            InitializeComponent();
            _Id = Id;
            _context = context;           
            
        }
        private  void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var flower = _context.Flowers.FirstOrDefault(y => y.Id == _Id);
            tbox_name.Text = flower.Name.ToString();
            tbox_family.Text = flower.Family;
            tbox_weight.Text = flower.Weight.ToString();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _ = DeleteRequest();
        }

        public async Task<bool> DeleteRequest()
        {
            WebRequest request = WebRequest.Create($"http://localhost:5000/api/Flowers/{_Id}");
            {
                request.Method = "DELETE";

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

            };
        }






     }
}
