using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Aspose;

using winForms = System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;

namespace Sklad_v1_001.GlobalVariable
{
    public class FileWork
    {        
        List<BitmapImage> listImage;
        public ImageSource Source { get; set; }
        public String PuthString { get; set; }
        public List<BitmapImage> ListImage { get => listImage; set => listImage = value; }
        public byte[] BufferDocument { get; set; }

        public FileWork()
        {
            listImage = new List<BitmapImage>();          
        }
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            // Открываем окно диалога с пользователем.
            if (openFileDialog.ShowDialog() == true)
            {

                // Получаем расширение файла, выбранного пользователем.
                var extension = openFileDialog.FileName;

                foreach (String file in openFileDialog.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        BitmapImage loadedImage = new BitmapImage(new Uri(file));
                        listImage.Add(loadedImage);
                    }
                    catch (Exception ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                }
                // Проверяем есть ли в ОС программа, которая может открыть
                // файл с указанным расширением.
                if (HasRegisteredFileExstension(extension))
                {
                    // Открываем файл. 
                    Process.Start(openFileDialog.FileName);
                }
            }
        }

        // Проверяем есть ли в ОС программа, которая может открыть
        // файл с указанным расширением.
        private bool HasRegisteredFileExstension(string fileExstension)
        {
            RegistryKey rkRoot = Registry.ClassesRoot;
            RegistryKey rkFileType = rkRoot.OpenSubKey(fileExstension);

            return rkFileType != null;
        }

        public bool SaveFileDialogImage(List<BitmapImage> _listImage)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".jpg";
            saveFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.Filter.Split('|', '*')[saveFileDialog.FilterIndex * 3 - 1] == Path.GetExtension(saveFileDialog.FileName))
                {
                    Save(saveFileDialog.FileName, _listImage);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public void Save(string filePuth, List<BitmapImage> _ist)
        {
            string filename = "";
            Int32 temp = 0;
            String[] str = filePuth.Split('\\');
            filePuth = "";
            for (int i = 0; i < str.Length - 1; i++)
            {
                filePuth += str[i] + "\\";
            }
            string format = str.Last().Split('.')[1];

            foreach (BitmapImage image in _ist)
            {
                temp++;
                filename = temp + "." + format;
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                using (FileStream fs = new FileStream(filePuth + filename, FileMode.Create))
                {
                    encoder.Save(fs);
                }
            }
        }
        public void LoadImage()
        {
            using (winForms.OpenFileDialog dlg = new winForms.OpenFileDialog())
            {
                dlg.DefaultExt = ".jpg";
                dlg.Filter = "Jpg files|*.jpg|Png files|*.png|Bitmap files|*.bmp";

                winForms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlg.Filter.Split('|', '*')[dlg.FilterIndex * 3 - 1] == Path.GetExtension(dlg.FileName))
                        Load(dlg.FileName);
                }
            }
        }

        private void Load(string fileName)
        {
            BitmapImage image = new BitmapImage();
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            Source = image;
        }    

        #region PDF
        public String OpenPDF( string _filterPuth)
        {
            using (winForms.OpenFileDialog dlg = new winForms.OpenFileDialog())
            {
                dlg.DefaultExt = ".pdf";
                dlg.Filter = _filterPuth;

                winForms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlg.Filter.Split('|', '*')[dlg.FilterIndex * 3 - 1] == Path.GetExtension(dlg.FileName))
                    {
                        return PuthString = dlg.FileName;
                    }
                }
            }
            return string.Empty;
        }


        public void PDFToByte()
        {
            try
            {
                // Initialize FileStream object
                FileStream fs = new FileStream(PuthString, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(PuthString).Length;

                // Load the file contents in the byte array
                BufferDocument = br.ReadBytes((int)numBytes);
                fs.Close();
            }
            catch(Exception e)
            {
                BufferDocument = null;
            }

            //Document document = new Document(PuthString);
            //document.Save("output.xps", Aspose.Pdf.SaveFormat.Xps);
        }  
        
        public String ByteToXPS(byte[] _documentByte, String _nameFile)
        {
            using (MemoryStream InputStream = new MemoryStream(_documentByte))
            {
                Document document = new Document(InputStream);
                document.Save(_nameFile, SaveFormat.Xps);
                using (MemoryStream InputStreamOutput = new MemoryStream())
                {
                    document.Save(InputStreamOutput, SaveFormat.Xps);
                    Package package = Package.Open(InputStreamOutput);
                    string inMemoryStream = string.Format("memorystream://{0}.xps", Guid.NewGuid());
                    Uri packageUri = new Uri(inMemoryStream);
                    PackageStore.AddPackage(packageUri, package);
                    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Maximum, inMemoryStream);
                }
                return _nameFile;
            }
            return String.Empty;
        }

        public String ByteToPDF(byte[] _documentByte)
        {
            using (MemoryStream InputStream = new MemoryStream(_documentByte))
            {
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(InputStream);
                pdfDocument.Save("output.pdf", SaveFormat.Pdf);
                return "output.pdf";
            }
            return String.Empty;
        }

        public static void ConvertPDFToJPEG(Byte[] PDFBlob, int resolution, string dataDir)
        {
            // Open document
            using (MemoryStream InputStream = new MemoryStream(PDFBlob))
            {
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(InputStream);

                for (int pageCount = 1; pageCount <= pdfDocument.Pages.Count; pageCount++)
                {

                    using (FileStream imageStream = new FileStream(dataDir + "image" + pageCount + "_out" + ".jpg", FileMode.Create))
                    {
                        // Create JPEG device with specified attributes
                        // Width, Height, Resolution, Quality
                        // Quality [0-100], 100 is Maximum
                        // Create Resolution object

                        Aspose.Pdf.Devices.Resolution res = new Aspose.Pdf.Devices.Resolution(resolution);
                        // JpegDevice jpegDevice = new JpegDevice(500, 700, resolution, 100);

                        // added the following to determine if landscape or not
                        Int32 height, width = 0;

                        PdfFileInfo info = new PdfFileInfo(pdfDocument);
                        width = Convert.ToInt32(info.GetPageWidth(pdfDocument.Pages[pageCount].Number));
                        height = Convert.ToInt32(info.GetPageHeight(pdfDocument.Pages[pageCount].Number));


                        Aspose.Pdf.Devices.JpegDevice jpegDevice =
                        //new Aspose.Pdf.Devices.JpegDevice(Aspose.Pdf.PageSize.A4, res, 100);
                        new Aspose.Pdf.Devices.JpegDevice(width, height, res, 100);
                        // Convert a particular page and save the image to stream

                        //Aspose.Pdf.PageSize.A4.IsLandscape = true;
                        jpegDevice.Process(pdfDocument.Pages[pageCount], imageStream);
                        // Close stream
                        imageStream.Close();
                    }
                }
            }
        }

            #endregion

        #region Image
            //Image --> byte[]
            public void LoadImage(string _filterPuth)
        {
            using (winForms.OpenFileDialog dlg = new winForms.OpenFileDialog())
            {
                dlg.DefaultExt = ".jpg";
                dlg.Filter = _filterPuth;

                winForms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlg.Filter.Split('|', '*')[dlg.FilterIndex * 3 - 1] == Path.GetExtension(dlg.FileName))
                        BufferDocument = ImageToByteArray(Image(dlg.FileName));
                }
            }
        }
        private BitmapImage Image(string fileName)
        {
            BitmapImage image = new BitmapImage();
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }

       
        public byte[] ImageToByteArray(BitmapImage imageIn)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageIn));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        //byte[] --> Image
       
        #endregion
    }
}
