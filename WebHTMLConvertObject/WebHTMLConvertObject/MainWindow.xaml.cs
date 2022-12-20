using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WebHTMLConvertObject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new WebClient();
            client.OpenReadCompleted += OpenReadCompletedHandler;
            client.DownloadDataCompleted += DownloadDataCompletedHandler;

        }

        private void DownloadDataCompletedHandler(object sender, DownloadDataCompletedEventArgs e)
        {
            var byteData = e.Result; // <- here is your stream
            MemoryStream memoryStream = new MemoryStream(byteData);
            retrieveHTML(memoryStream);
        }

        private void OpenReadCompletedHandler(object sender, OpenReadCompletedEventArgs e)
        {
            var stream = e.Result; // <- here is your stream
            retrieveHTML(stream);
        }    

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            Content.Text = String.Empty;
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " + "Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.DownloadDataAsync(new Uri(URLText.Text));

            //client.OpenReadAsync(new Uri(URLText.Text));
        }

        private void retrieveHTML(Stream _data)
        {
            // WebClient object
           

            // Retrieve resource as a stream
           // Stream data = await client.OpenReadAsync(new Uri(URLText.Text));

            // Retrive the text
            StreamReader reader = new StreamReader(_data);
            try
            {
                string htmlContent = reader.ReadToEnd();


                if (processHTML(htmlContent))
                    Content.Text = htmlContent;
                else
                {
                    client.DownloadDataAsync(new Uri(URLText.Text));
                }

                // Call function to process HTML Content


                // Cleanup
                _data.Close();
                reader.Close();
            }
            catch (Exception e)
            {

            }
        }

        private bool processHTML(String htmlContent)
        {
            // Obtain the document interface
            IHTMLDocument2 htmlDocument = (IHTMLDocument2)new mshtml.HTMLDocument();

            // Construct the document
            try
            {
                htmlDocument.write(htmlContent);
            }
            catch (Exception e) { }
                

            List<String> listData = new List<string>();
            
            IHTMLElementCollection allElements = htmlDocument.all;

            // Iterate all the elements and display tag names
            foreach (IHTMLElement element in allElements)
            {
               if (element.className == "alert alert-info clearfix")
               {
                    return false;
               }    
               listData.Add(element.tagName);
            }
            var res = listData.GroupBy(x => x).Select(x => x.First()).ToList();

            this.ElementList.ItemsSource = res;
            return true;
        }

        private void SearchHTML(String htmlContent)
        {
            Content1.Text = String.Empty;
            // Obtain the document interface
            IHTMLDocument2 htmlDocument = (IHTMLDocument2)new mshtml.HTMLDocument();

            // Construct the document
            htmlDocument.write(htmlContent);

            List<String> listData = new List<string>();
            //listBox1.Items.Clear();

            //// Extract all image elements
            //IHTMLElementCollection imgElements = htmlDocument.images;

            //// Iterate through each image element
            //foreach (IHTMLImgElement img in imgElements)
            //{
            //    listBox1.Items.Add(img.src);
            //}
            // Extract all elements
            IHTMLElementCollection allElements = htmlDocument.all;

            // Iterate all the elements and display tag names
            foreach (IHTMLElement element in allElements)
            {
                if (!String.IsNullOrEmpty(ClassID.Text))
                {
                    if (element.className == ClassID.Text)
                    {
                        Content1.Text = element.innerText;
                    }
                }
                if (!String.IsNullOrEmpty(ElementList.Text))
                {
                   
                    if (element.tagName == ElementList.Text)
                    {
                        Content1.Text += element.innerText == null ? Environment.NewLine + element.outerHTML : Environment.NewLine + element.innerText;
                        //String temp1 = element.;
                        var temp = element.document;
                        if (element.outerHTML == "twitter:card")
                        {

                        }
                    }
                }

                if (!String.IsNullOrEmpty(ClassID.Text) && !String.IsNullOrEmpty(ElementList.Text))
                {
                    if (element.tagName == ElementList.Text && element.className == ClassID.Text)
                    {
                        Content1.Text = element.innerText;
                    }
                }
               // listData.Add(element.tagName);
            }

            this.ElementList.ItemsSource = listData;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Content1.Text = String.Empty;
            SearchHTML(Content.Text);
        }
    }
}
