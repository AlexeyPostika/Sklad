using Sklad_v1_001.Control;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.FlexMessageBox
{
    /// <summary>
    /// Логика взаимодействия для FlexMessageBox.xaml
    /// </summary>
    public partial class FlexMessageBox : DialogWindow, INotifyPropertyChanged, IAbstractMessageBox
    {
        [DllImport("user32.dll")]
        static extern void MessageBeep(uint uType);


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Переменные
        private string button1Text;
        private string button2Text;
        private string button3Text;
        private string description;

        private Int32 button1value;
        private Int32 button2value;
        private Int32 button3value;
        private Int32 defaultButton;
        private MessageBoxButton buttonType;

        private String fieldNameForHistory;

        private MediaPlayer player = new MediaPlayer();
        MessageBoxResult value;

        Int32 imageType;

        public string Button1Text
        {
            get
            {
                return button1Text;
            }

            set
            {
                button1Text = value;
                OnPropertyChanged("Button1Text");
            }
        }

        public string Button2Text
        {
            get
            {
                return button2Text;
            }

            set
            {
                button2Text = value;
                OnPropertyChanged("Button2Text");
            }
        }

        public string Button3Text
        {
            get
            {
                return button3Text;
            }

            set
            {
                button3Text = value;
                OnPropertyChanged("Button3Text");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public Int32 Button1value
        {
            get
            {
                return button1value;
            }

            set
            {
                button1value = value;
            }
        }

        public Int32 Button2value
        {
            get
            {
                return button2value;
            }

            set
            {
                button2value = value;
            }
        }

        public Int32 Button3value
        {
            get
            {
                return button3value;
            }

            set
            {
                button3value = value;
            }
        }

        public MessageBoxResult Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public Int32 ImageType
        {
            get
            {
                return imageType;
            }

            set
            {
                imageType = value;
            }
        }

        public MessageBoxButton ButtonType
        {
            get
            {
                return buttonType;
            }

            set
            {
                buttonType = value;
            }
        }

        public Int32 DefaultButton
        {
            get
            {
                return defaultButton;
            }

            set
            {
                defaultButton = value;
            }
        }
        #endregion

        public enum MessageBoxIcon
        {
            None = 1,
            Error = 2,
            Hand = 3,
            Stop = 4,
            Question = 5,
            Exclamation = 6,
            Warning = 7,
            Asterisk = 8,
            Information = 9
        }

        public static class MessageBeepSound
        {
            public static uint MB_ICONASTERISK = (uint)0x00000040L;
            public static uint MB_ICONEXCLAMATION = (uint)0x00000030L;
            public static uint MB_ICONERROR = (uint)0x00000010L;
            public static uint MB_ICONHAND = (uint)0x00000010L;
            public static uint MB_ICONINFORMATION = (uint)0x00000040L;
            public static uint MB_ICONQUESTION = (uint)0x00000040L;
            public static uint MB_ICONSTOP = (uint)0x00000010L;
            public static uint MB_ICONWARNING = (uint)0x00000030L;
            public static uint MB_OK = (uint)0x00000000L;
        }

        public FlexMessageBox()
        {
            InitializeComponent();
            // this.Width = 300;
            fieldNameForHistory = "";
            this.Topmost = true;
        }

        private void ChooseIcons(MessageBoxIcon iconType, string sound = null)
        {
            switch (iconType)
            {
                case MessageBoxIcon.Asterisk:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONASTERISK);
                    image.Source = ImageHelper.GenerateImage("Asterisk.png");
                    ImageType = 8;
                    break;
                case MessageBoxIcon.Error:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONERROR);
                    image.Source = ImageHelper.GenerateImage("Error.png");
                    ImageType = 2;
                    break;
                case MessageBoxIcon.Exclamation:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONEXCLAMATION);
                    image.Source = ImageHelper.GenerateImage("Exclamation.png");
                    ImageType = 6;
                    break;
                case MessageBoxIcon.Hand:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONHAND);
                    image.Source = ImageHelper.GenerateImage("Hand.png");
                    ImageType = 3;
                    break;
                case MessageBoxIcon.Information:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONINFORMATION);
                    image.Source = ImageHelper.GenerateImage("Information.png");
                    ImageType = 9;
                    break;
                case MessageBoxIcon.Question:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONQUESTION);
                    image.Source = ImageHelper.GenerateImage("Question.png");
                    ImageType = 5;
                    break;
                case MessageBoxIcon.Stop:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONSTOP);
                    image.Source = ImageHelper.GenerateImage("Stop.png");
                    ImageType = 4;
                    break;
                case MessageBoxIcon.Warning:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONWARNING);
                    image.Source = ImageHelper.GenerateImage("Warning.png");
                    ImageType = 7;
                    break;
                case MessageBoxIcon.None:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONASTERISK);
                    image.Source = null;
                    ImageType = 1;
                    break;
                default:
                    if (sound != null)
                    {
                        player.Open(SoundHelper.GenerateSoundPath(sound));
                        player.Play();
                    }
                    else
                        MessageBeep(MessageBeepSound.MB_ICONASTERISK);
                    image.Source = null;
                    ImageType = 1;
                    break;
            }
        }

        Int32 ChooseButtons(MessageBoxButtons buttonType)
        {
            switch (buttonType)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        Grid3.Visibility = Visibility.Visible;
                        Grid2.Visibility = Visibility.Collapsed;
                        Grid1.Visibility = Visibility.Collapsed;

                        Button31.Text = Properties.Resources.MessageAbort;
                        Button31.Image.Source = ImageHelper.GenerateImage("MessageAbort.png");
                        Button1value = (Int32)System.Windows.Forms.DialogResult.Abort;
                        Button32.Text = Properties.Resources.MessageRetry;
                        Button32.Image.Source = ImageHelper.GenerateImage("MessageRetry.png");
                        Button2value = (Int32)System.Windows.Forms.DialogResult.Retry;
                        Button33.Text = Properties.Resources.MessageIgnore;
                        Button33.Image.Source = ImageHelper.GenerateImage("MessageIgnore.png");
                        Button3value = (Int32)System.Windows.Forms.DialogResult.Ignore;
                        return 3;
                    }
                case MessageBoxButtons.OK:
                    {
                        Grid3.Visibility = Visibility.Collapsed;
                        Grid2.Visibility = Visibility.Collapsed;
                        Grid1.Visibility = Visibility.Visible;

                        Button11.Text = Properties.Resources.MessageOK;
                        Button11.Image.Source = ImageHelper.GenerateImage("MessageOK.png");
                        Button1value = (Int32)System.Windows.Forms.DialogResult.OK;
                        return 1;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        Grid3.Visibility = Visibility.Collapsed;
                        Grid2.Visibility = Visibility.Visible;
                        Grid1.Visibility = Visibility.Collapsed;

                        Button1value = (Int32)System.Windows.Forms.DialogResult.OK;
                        Button21.Text = Properties.Resources.MessageOK;
                        Button21.Image.Source = ImageHelper.GenerateImage("MessageOK.png");
                        Button22.Text = Properties.Resources.MessageCancel;
                        Button22.Image.Source = ImageHelper.GenerateImage("MessageCancel.png");
                        Button2value = (Int32)System.Windows.Forms.DialogResult.Cancel;
                        return 2;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        Grid3.Visibility = Visibility.Collapsed;
                        Grid2.Visibility = Visibility.Visible;
                        Grid1.Visibility = Visibility.Collapsed;

                        Button1value = (Int32)System.Windows.Forms.DialogResult.Retry;
                        Button21.Text = Properties.Resources.MessageRetry;
                        Button21.Image.Source = ImageHelper.GenerateImage("MessageRetry.png");
                        Button2value = (Int32)System.Windows.Forms.DialogResult.Cancel;
                        Button22.Text = Properties.Resources.MessageCancel;
                        Button22.Image.Source = ImageHelper.GenerateImage("MessagCancel.png");
                        return 2;
                    }
                case MessageBoxButtons.YesNo:
                    {
                        Grid3.Visibility = Visibility.Collapsed;
                        Grid2.Visibility = Visibility.Visible;
                        Grid1.Visibility = Visibility.Collapsed;

                        Button1value = (Int32)System.Windows.Forms.DialogResult.Yes;
                        Button21.Text = Properties.Resources.MessageYes;
                        Button21.Image.Source = ImageHelper.GenerateImage("MessageYes.png");

                        Button2value = (Int32)System.Windows.Forms.DialogResult.No;
                        Button22.Text = Properties.Resources.MessageNo;
                        Button22.Image.Source = ImageHelper.GenerateImage("MessageNo.png");
                        return 2;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        Grid3.Visibility = Visibility.Visible;
                        Grid2.Visibility = Visibility.Collapsed;
                        Grid1.Visibility = Visibility.Collapsed;

                        Button1value = (Int32)System.Windows.Forms.DialogResult.Yes;
                        Button31.Text = Properties.Resources.MessageYes;
                        Button31.Image.Source = ImageHelper.GenerateImage("MessageYes.png");
                        Button2value = (Int32)System.Windows.Forms.DialogResult.No;
                        Button32.Text = Properties.Resources.MessageNo;
                        Button32.Image.Source = ImageHelper.GenerateImage("MessageNo.png");
                        Button3value = (Int32)System.Windows.Forms.DialogResult.Cancel;
                        Button33.Text = Properties.Resources.MessageCancel;
                        Button33.Image.Source = ImageHelper.GenerateImage("MessageCancel.png");
                        return 3;
                    }
                default:
                    {
                        Grid3.Visibility = Visibility.Collapsed;
                        Grid2.Visibility = Visibility.Collapsed;
                        Grid1.Visibility = Visibility.Visible;

                        Button11.Text = Properties.Resources.MessageOK;
                        Button11.Image.Source = ImageHelper.GenerateImage("MessageOK.png");
                        Button1value = (Int32)System.Windows.Forms.DialogResult.OK;
                        return 1;
                    }
            }
        }

        #region Ф-ции отображения

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">text inside the message box</param>
        /// <param name="caption">caption on the title bar</param>

        public MessageBoxResult Show(string text)
        {
            Description = text;
            Title = text;
            ChooseButtons(MessageBoxButtons.OK);
            ChooseIcons(MessageBoxIcon.None);
          
            this.ShowDialog();
            return Value;
        }

        public MessageBoxResult Show(string text, string caption)
        {
            Description = text;
            this.Title = caption;

            ChooseButtons(MessageBoxButtons.OK);
            ChooseIcons(MessageBoxIcon.None);

            this.ShowDialog();
            return Value;
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton buttonType)
        {
            Description = text;
            this.Title = caption;

            MessageBoxButtons _buttonType = (MessageBoxButtons)buttonType;
            ChooseButtons(_buttonType);
            ChooseIcons(MessageBoxIcon.None);

            this.ShowDialog();
            return Value;
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton buttonType, MessageBoxImage iconType, string sound = null)
        {
            Description = text;
            this.Title = caption;

            MessageBoxButtons _buttonType = (MessageBoxButtons)buttonType;

            MessageBoxIcon _iconType = MessageBoxIcon.None;

            if (iconType == MessageBoxImage.None)
            {
                _iconType = MessageBoxIcon.None;
            }
            else if (iconType == MessageBoxImage.Error)
            {
                _iconType = MessageBoxIcon.Error;
            }
            else if (iconType == MessageBoxImage.Hand)
            {
                _iconType = MessageBoxIcon.Hand;
            }
            else if (iconType == MessageBoxImage.Stop)
            {
                _iconType = MessageBoxIcon.Stop;
            }
            else if (iconType == MessageBoxImage.Question)
            {
                _iconType = MessageBoxIcon.Question;
            }
            else if (iconType == MessageBoxImage.Exclamation)
            {
                _iconType = MessageBoxIcon.Exclamation;
            }
            else if (iconType == MessageBoxImage.Warning)
            {
                _iconType = MessageBoxIcon.Warning;
            }
            else if (iconType == MessageBoxImage.Asterisk)
            {
                _iconType = MessageBoxIcon.Asterisk;
            }
            else if (iconType == MessageBoxImage.Information)
            {
                _iconType = MessageBoxIcon.Information;
            }

            ChooseButtons(_buttonType);
            ChooseIcons(_iconType, sound);

            this.ShowDialog();
            return Value;
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton buttonType, MessageBoxImage iconType, Int32 defaultButton)
        {
            Description = text;
            this.Title = caption;
            DefaultButton = defaultButton;
            ButtonType = buttonType;

            MessageBoxButtons _buttonType = (MessageBoxButtons)buttonType;
            MessageBoxIcon _iconType = MessageBoxIcon.None;

            if (iconType == MessageBoxImage.None)
            {
                _iconType = MessageBoxIcon.None;
            }
            else if (iconType == MessageBoxImage.Error)
            {
                _iconType = MessageBoxIcon.Error;
            }
            else if (iconType == MessageBoxImage.Hand)
            {
                _iconType = MessageBoxIcon.Hand;
            }
            else if (iconType == MessageBoxImage.Stop)
            {
                _iconType = MessageBoxIcon.Stop;
            }
            else if (iconType == MessageBoxImage.Question)
            {
                _iconType = MessageBoxIcon.Question;
            }
            else if (iconType == MessageBoxImage.Exclamation)
            {
                _iconType = MessageBoxIcon.Exclamation;
            }
            else if (iconType == MessageBoxImage.Warning)
            {
                _iconType = MessageBoxIcon.Warning;
            }
            else if (iconType == MessageBoxImage.Asterisk)
            {
                _iconType = MessageBoxIcon.Asterisk;
            }
            else if (iconType == MessageBoxImage.Information)
            {
                _iconType = MessageBoxIcon.Information;
            }

            Int32 count = ChooseButtons(_buttonType);
            ChooseIcons(_iconType);

            if (count == 1)
            {
                Button11.Focus();
            }

            if (count == 2)
            {
                switch (defaultButton)
                {
                    case 1:
                        {
                            Button21.Focus();
                            break;
                        }
                    case 2:
                        {
                            Button22.Focus();
                            break;
                        }
                    default:
                        {
                            Button21.Focus();
                            break;
                        }
                }
            }

            if (count == 3)
            {
                switch (defaultButton)
                {
                    case 1:
                        {
                            Button31.Focus();
                            break;
                        }
                    case 2:
                        {
                            Button32.Focus();
                            break;
                        }

                    case 3:
                        {
                            Button33.Focus();
                            break;
                        }
                    default:
                        {
                            Button31.Focus();
                            break;
                        }
                }
            }

            this.ShowDialog();
            return Value;
        }

        public MessageBoxResult Show(string text, string caption, List<BitmapImage> ButtonImages, List<string> ButtonText, BitmapImage _image = null)
        {
            this.DescriptionBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Description = text;
            this.Title = caption;
            if (_image != null)
            {
                image.Source = _image;
                image.Stretch = Stretch.UniformToFill;
            }

            if (ButtonImages != null)
            {
                Int32 count = ButtonImages.Count;
                switch (count)
                {
                    case 1:
                        {
                            Grid3.Visibility = Visibility.Collapsed;
                            Grid2.Visibility = Visibility.Collapsed;
                            Grid1.Visibility = Visibility.Visible;

                            Button11.Image.Source = ButtonImages[0];
                            Button11.Text = ButtonText[0];
                            Button1value = (Int32)System.Windows.Forms.DialogResult.Yes;
                            break;
                        }
                    case 2:
                        {
                            Grid3.Visibility = Visibility.Collapsed;
                            Grid2.Visibility = Visibility.Visible;
                            Grid1.Visibility = Visibility.Collapsed;

                            Button21.Image.Source = ButtonImages[0];
                            Button21.Text = ButtonText[0];
                            Button1value = (Int32)System.Windows.Forms.DialogResult.Yes;

                            Button22.Image.Source = ButtonImages[1];
                            Button22.Text = ButtonText[1];
                            Button2value = (Int32)System.Windows.Forms.DialogResult.No;
                            break;
                        }

                    case 3:
                        {
                            Grid3.Visibility = Visibility.Visible;
                            Grid2.Visibility = Visibility.Collapsed;
                            Grid1.Visibility = Visibility.Collapsed;

                            Button31.Image.Source = ButtonImages[0];
                            Button31.Text = ButtonText[0];
                            Button1value = (Int32)System.Windows.Forms.DialogResult.Yes;

                            Button32.Image.Source = ButtonImages[1];
                            Button32.Text = ButtonText[1];
                            Button2value = (Int32)System.Windows.Forms.DialogResult.No;

                            Button33.Image.Source = ButtonImages[2];
                            Button33.Text = ButtonText[2];
                            Button3value = (Int32)System.Windows.Forms.DialogResult.Cancel;
                            break;
                        }
                    default:
                        {
                            Grid3.Visibility = Visibility.Collapsed;
                            Grid2.Visibility = Visibility.Collapsed;
                            Grid1.Visibility = Visibility.Collapsed;
                            break;
                        }
                }
            }
            ImageType = (Int32)App.LogImageType.None;
            this.ShowDialog();
            return Value;
        }
        #endregion

        private void Button1_ButtonClick()
        {
            Value = (MessageBoxResult)button1value;
            this.Close();
        }

        private void Button2_ButtonClick()
        {
            Value = (MessageBoxResult)button2value;
            this.Close();
        }

        private void Button3_ButtonClick()
        {
            Value = (MessageBoxResult)button3value;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)

        {
            Int32 count = 0;
            if (ButtonType == MessageBoxButton.OK)
                count = 1;
            if (ButtonType == MessageBoxButton.OKCancel)
                count = 2;
            if (ButtonType == MessageBoxButton.YesNoCancel)
                count = 3;
            if (ButtonType == MessageBoxButton.YesNo)
                count = 2;

            if (count != 0)
            {
                if (count == 1)
                {
                    Button11.button.Focus();
                }

                if (count == 2)
                {
                    switch (DefaultButton)
                    {
                        case 1:
                            {
                                Button21.button.Focus();
                                break;
                            }
                        case 2:
                            {
                                Button22.button.Focus();
                                break;
                            }
                        default:
                            {
                                Button21.Focus();
                                break;
                            }
                    }
                }

                if (count == 3)
                {
                    switch (DefaultButton)
                    {
                        case 1:
                            {
                                Button31.button.Focus();
                                break;
                            }
                        case 2:
                            {
                                Button32.button.Focus();
                                break;
                            }

                        case 3:
                            {
                                Button33.button.Focus();
                                break;
                            }
                        default:
                            {
                                Button31.button.Focus();
                                break;
                            }
                    }
                }
            }
            DefaultButton = 0;
        }
    }
}
