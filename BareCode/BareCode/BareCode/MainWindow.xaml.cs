using BareCode.DataSet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using MessagingToolkit.Barcode;
using System.Windows.Xps;
using System.Printing;
using System.Windows.Markup;
using System.Windows.Documents;

namespace BareCode
{
    public class BarCode
    {
        public String IDProduct { get; set; }
        public WriteableBitmap ImageSource { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BarCode> listBarCode;
        BarCodeData barCodeData;
        public MainWindow()
        {
            InitializeComponent();
            listBarCode = new List<BarCode>();
            barCodeData = new BarCodeData();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Column7.Children.Clear();
            if (!String.IsNullOrEmpty(textBlock.Text))
            {
                List<String> listNumber = textBlock.Text.Split(',').ToList();
                BarCode barCode;
                foreach (String row in listNumber)
                {
                    barCode = new BarCode();
                    barCode.ImageSource = GenerateBarCode(row);
                    if (barCode.ImageSource != null)
                    {
                        Image image1 = new Image();
                        image1.Width = 100;
                        image1.Height = 50;
                        image1.Source = barCode.ImageSource;
                        barCode.IDProduct = row;
                        listBarCode.Add(barCode);
                        AddVisibilityControl(Column7, true, image1);                     
                    }
                }
            }
        }
        private void AddVisibilityControl(Panel PanelToAdd, Object VisibilityRow, UIElement ElementValue)
        {
            Boolean visibility = false;
            Boolean.TryParse(VisibilityRow.ToString(), out visibility);
            if (visibility == true)
                PanelToAdd.Children.Add(ElementValue);
        }
        //FixedPage
        private void AddVisibilityControlf(FixedPage PanelToAdd, Object VisibilityRow, UIElement ElementValue)
        {
            Boolean visibility = false;
            Boolean.TryParse(VisibilityRow.ToString(), out visibility);
            if (visibility == true)
                PanelToAdd.Children.Add(ElementValue);
        }

        public WriteableBitmap GenerateBarCode(String documentNumber)//System.Windows.Controls.Image barcodeImage
        {
            Int64 temp;
            if (Int64.TryParse(documentNumber.ToString(), out temp))
            {
                string barcodeString = documentNumber;
                Int32 QualityPrint = 10;
                BarcodeEncoder barcodeEncoder = new BarcodeEncoder();
                barcodeEncoder.CharacterSet = "UTF-8";
                barcodeEncoder.Width =100;
                barcodeEncoder.Height =50;
                barcodeEncoder.IncludeLabel = false;
                //barcodeEncoder.LabelFont = new Font("Arial", QualityPrint * 10);
                WriteableBitmap img = barcodeEncoder.Encode(BarcodeFormat.Code128, barcodeString);
                //barcodeImage.Source = img;
                return img;
            }

            return null;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            //if (pd.ShowDialog() == true)
            //{         
            FixedDocument fd = new FixedDocument();
            FixedPage fp = new FixedPage();
            fp.Width = fd.DocumentPaginator.PageSize.Width;
            fp.Height = fd.DocumentPaginator.PageSize.Height;
            PageContent pc = new PageContent();
            StackPanel stackPanel;
            TextBlock tb;
            Image image1;
            Int32 tempW = 30;
            Int32 tempH = 30;
            int flag = 0;
            foreach (BarCode writeableBitmap in listBarCode)
            {
                stackPanel = new StackPanel();
                tb = new TextBlock();
                image1 = new Image();
                image1.Width = 100;
                image1.Height = 50;
                image1.Source = writeableBitmap.ImageSource;
                image1.Margin = new Thickness(tempW, tempH, 0, 0);
                stackPanel.Children.Add(image1);
                //AddVisibilityControl(stackPanel, true, image1);
                tb.Text = writeableBitmap.IDProduct;
                tb.Margin = new Thickness(tempW + 25, 0, 0, 0);
                AddVisibilityControl(stackPanel, true, tb);

                AddVisibilityControlf(fp, true, stackPanel);
                tempW += 100;
                flag++;
                if (flag > 6)
                {
                    tempW = 30;
                    tempH += 70;
                    flag = 0;
                }

               // fp.Children.Add(image1);
               //add some text to a TextBox object
               //tb.Text = "This is some test text";
               ////add the text box to the FixedPage
               //fp.Children.Add(tb);
            }
           
            //add the FixedPage to the PageContent 
            pc.Child = fp;
            //add the PageContent to the FixedDocument
            fd.Pages.Add(pc);

            documentViewer.Document = fd;
            // dataGrid.ItemsSource = listBarCode;
            //Label label = new Label();
            //label.Content = "lksdjjkldjfklsadjjklsdfklsdl;kjsd";
            //dataGrid.Items.Add(label);
            

            //}
            //dataGrid.ItemsSource = listBarCode;


            //fp.Children.Add(dataGrid);
            //PageContent pc = new PageContent();
            //((IAddChild)pc).AddChild(fp);
            //doc.Pages.Add(pc);
            //
            //pd.PrintDocument(doc.DocumentPaginator, "Print Job Name");
            //}
        }
    }
}
