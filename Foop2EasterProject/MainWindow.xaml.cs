using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
using HtmlAgilityPack;

namespace Foop2EasterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 

    public partial class MainWindow : Window
    {
        private List<Product> ProductsList = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsList = await GetData();
            lstbx1.ItemsSource = ProductsList;
        }

        //Method to pull information from the website, build Product objects and add them to the Products List
        private async Task<List<Product>> GetData()
        {
            List<Product> TempList = new List<Product>();

            HttpClient client = new HttpClient();
            HtmlDocument doc = new HtmlDocument();
            string url = "https://www.tesco.ie/groceries/product/browse/default.aspx?N=4294794608&Nao=0";
            var html = await client.GetStringAsync(url);
            doc.LoadHtml(html);
            var products = doc.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Equals("cf products line")).ToList();
            var productDetails = products[0].Descendants("li").Where(node => node.GetAttributeValue("id", "").Contains("p-")).ToList();
            foreach (var product in productDetails)
                {
                    Product temp = new Product();
                    temp.Price = decimal.Parse(Regex.Match(product.LastChild.FirstChild.FirstChild.FirstChild.InnerText, @"\d+(\.\d+)?").Value);
                    temp.Name = product.FirstChild.FirstChild.FirstChild.InnerText;
                    TempList.Add(temp);
                }
            return TempList;
        }
        //Method to display the price of the selected product
        private void Lstbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selected = lstbx1.SelectedItem as Product;
            txtblkPrice.Text = "€" + selected.Price;
        }
    }
}
