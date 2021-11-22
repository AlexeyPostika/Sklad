using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
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
using winForms = System.Windows.Forms;

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
        public void LoadImaje()
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
        public String OpenPDFtoImage( string _filterPuth)
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


        public void PDFTo()
        {
            if (!String.IsNullOrEmpty(PuthString))
            {
                PdfDocument document = PdfReader.Open(PuthString);

                int imageCount = 0;
                // Iterate pages
                foreach (PdfPage page in document.Pages)
                {
                    // Get resources dictionary
                    PdfDictionary resources = page.Elements.GetDictionary("/Resources");
                    if (resources != null)
                    {
                        // Get external objects dictionary
                        PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");
                        if (xObjects != null)
                        {
                            ICollection<PdfItem> items = xObjects.Elements.Values;
                            // Iterate references to external objects
                            foreach (PdfItem item in items)
                            {
                                PdfReference reference = item as PdfReference;
                                if (reference != null)
                                {
                                    PdfDictionary xObject = reference.Value as PdfDictionary;
                                    // Is external object an image?
                                    if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                                    {
                                        ExportImage(xObject, ref imageCount);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ExportImage(PdfDictionary image, ref int count)
        {
            string filter = image.Elements.GetName("/Filter");
            switch (filter)
            {
                case "/DCTDecode":
                    Source = ExportJpegImage(image, ref count);
                    break;

                case "/FlateDecode":
                    ExportAsPngImage(image, ref count);
                    break;
            }
        }
        public BitmapImage ExportJpegImage(PdfDictionary image, ref int count)
        {
            // Fortunately JPEG has native support in PDF and exporting an image is just writing the stream to a file.                       
            BufferDocument = image.Stream.Value;

            FileStream fs = new FileStream(String.Format("Image{0}.jpeg", count++), FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(BufferDocument);
            bw.Close();

            BitmapImage image1 = new BitmapImage();
            image1.BeginInit();
            image1.CacheOption = BitmapCacheOption.OnLoad;
            image1.StreamSource = fs;
            image1.EndInit();


            return image1;
        }
        static void ExportAsPngImage(PdfDictionary image, ref int count)
        {
            int width = image.Elements.GetInteger(PdfImage.Keys.Width);
            int height = image.Elements.GetInteger(PdfImage.Keys.Height);
            int bitsPerComponent = image.Elements.GetInteger(PdfImage.Keys.BitsPerComponent);
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
