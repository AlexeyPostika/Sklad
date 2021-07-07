using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
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

namespace Sklad_v1_001.Control.SimpleControl
{
    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelNumeric.xaml
    /// </summary>
    public partial class EditBoxWithLabelTime : UserControl, INotifyPropertyChanged
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(String),
                        typeof(EditBoxWithLabelTime), new UIPropertyMetadata("00:00:00"));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Value
        {
            get
            {
                return (String)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                if (value.Length == 8)
                {
                    TextHours.Text = value.Substring(0, 2);
                    TextMinutes.Text = value.Substring(3, 2);
                    TextSeconds.Text = value.Substring(6, 2);
                }
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextStringProperty = DependencyProperty.Register(
                        "LabelTextString",
                        typeof(String),
                        typeof(EditBoxWithLabelTime), new UIPropertyMetadata());

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelTextString
        {
            get { return (string)GetValue(LabelTextStringProperty); }
            set { SetValue(LabelTextStringProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.label.Width;
            }

            set
            {
                this.label.Width = value;
            }
        }

        String lastFocus = "TextSeconds";
        Boolean needRefresh;

        public event Action ValueChanged;

        public EditBoxWithLabelTime()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(TextHours, OnPaste);
            DataObject.AddPastingHandler(TextMinutes, OnPaste);
            DataObject.AddPastingHandler(TextSeconds, OnPaste);
        }

        private void control_Loaded(object sender, RoutedEventArgs e)
        {
            TextHours.Text = Value.Substring(0, 2);
            TextMinutes.Text = Value.Substring(3, 2);
            TextSeconds.Text = Value.Substring(6, 2);
            needRefresh = true;
        }

        private bool IsValidTime(string timeString)
        {
            Regex checktime = new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$");
            return checktime.IsMatch(timeString);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true);
            if (!isText)
            {
                e.CancelCommand();
            }
            else
            {
                var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
                if (!String.IsNullOrEmpty(text))
                {
                    Int32 tempInt = 0;
                    if (Int32.TryParse(text, out tempInt) && text.Length == 2)
                    {
                        if ((textBox == TextHours && Int32.Parse(text) <= 23 && Int32.Parse(text) >= 0) ||
                            (textBox == TextMinutes && Int32.Parse(text) <= 59 && Int32.Parse(text) >= 0) ||
                            (textBox == TextSeconds && Int32.Parse(text) <= 59 && Int32.Parse(text) >= 0)
                            )
                        {
                            textBox.Text = text;
                            textBox.CaretIndex = 2;
                            e.CancelCommand();
                        }
                        else
                        {
                            e.CancelCommand();
                        }
                    }
                    else
                    {
                        if (IsValidTime(text))
                        {
                            TextHours.Text = text.Substring(0, 2);
                            TextMinutes.Text = text.Substring(3, 2);
                            TextSeconds.Text = text.Substring(6, 2);
                            e.CancelCommand();
                        }
                        else
                        {
                            e.CancelCommand();
                        }
                    }
                }
                else
                {
                    e.CancelCommand();
                }
            }
        }

        private Boolean CheckAllKey(KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.D0
                || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.PageUp || e.Key == Key.PageDown || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftShift || e.Key == Key.RightShift
                )
                return true;
            else
                return false;
        }

        private Boolean CheckDigitalKey(KeyEventArgs e)
        {
            if (e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.D0
                || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                )
                return true;
            else
                return false;
        }

        private Boolean CheckOtherKey(KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.PageUp || e.Key == Key.PageDown
                )
                return true;
            else
                return false;
        }

        private void CheckCopyPaste(KeyEventArgs e)
        {
            if ((e.Key == Key.C && Keyboard.IsKeyDown(Key.LeftCtrl)) || (e.Key == Key.C && Keyboard.IsKeyDown(Key.RightCtrl)) ||
                 (e.Key == Key.Insert && Keyboard.IsKeyDown(Key.LeftCtrl)) || (e.Key == Key.Insert && Keyboard.IsKeyDown(Key.RightCtrl))
                )
            {
                if (TextHours.SelectionLength == 0 && TextMinutes.SelectionLength == 0 && TextSeconds.SelectionLength == 0)
                {
                    Clipboard.SetText(String.Concat(TextHours.Text, ":", TextMinutes.Text, ":", TextSeconds.Text));
                }
                e.Handled = false;
            }

            if ((e.Key == Key.V && Keyboard.IsKeyDown(Key.LeftCtrl)) || (e.Key == Key.V && Keyboard.IsKeyDown(Key.RightCtrl)) ||
                 (e.Key == Key.Insert && Keyboard.IsKeyDown(Key.RightShift)) || (e.Key == Key.Insert && Keyboard.IsKeyDown(Key.RightShift))
                )
                e.Handled = false;
        }

        private Int32 ReturnSymbolNumeric(KeyEventArgs e)
        {
            Int32 symbolNumeric = 0;
            switch (e.Key)
            {
                case Key.D0:
                    symbolNumeric = 0;
                    break;
                case Key.D1:
                    symbolNumeric = 1;
                    break;
                case Key.D2:
                    symbolNumeric = 2;
                    break;
                case Key.D3:
                    symbolNumeric = 3;
                    break;
                case Key.D4:
                    symbolNumeric = 4;
                    break;
                case Key.D5:
                    symbolNumeric = 5;
                    break;
                case Key.D6:
                    symbolNumeric = 6;
                    break;
                case Key.D7:
                    symbolNumeric = 7;
                    break;
                case Key.D8:
                    symbolNumeric = 8;
                    break;
                case Key.D9:
                    symbolNumeric = 9;
                    break;
                case Key.NumPad0:
                    symbolNumeric = 0;
                    break;
                case Key.NumPad1:
                    symbolNumeric = 1;
                    break;
                case Key.NumPad2:
                    symbolNumeric = 2;
                    break;
                case Key.NumPad3:
                    symbolNumeric = 3;
                    break;
                case Key.NumPad4:
                    symbolNumeric = 4;
                    break;
                case Key.NumPad5:
                    symbolNumeric = 5;
                    break;
                case Key.NumPad6:
                    symbolNumeric = 6;
                    break;
                case Key.NumPad7:
                    symbolNumeric = 7;
                    break;
                case Key.NumPad8:
                    symbolNumeric = 8;
                    break;
                case Key.NumPad9:
                    symbolNumeric = 9;
                    break;
                default:
                    break;
            }
            return symbolNumeric;
        }

        private void InsertNextSymbol0(KeyEventArgs e, TextBox textBox, Int32 symbolNumeric)
        {
            String twoChar = textBox.Text.Substring(1, 1);
            int caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(caretIndex, symbolNumeric.ToString());
            textBox.Text = textBox.Text.Remove(caretIndex);
            textBox.Text = textBox.Text.Insert(caretIndex, twoChar);
            textBox.CaretIndex = caretIndex;
            e.Handled = false;
        }

        private void InsertNextSymbol1(KeyEventArgs e, TextBox textBox, Int32 symbolNumeric)
        {
            Int32 caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(caretIndex, symbolNumeric.ToString());
            textBox.Text = textBox.Text.Remove(caretIndex);
            textBox.CaretIndex = caretIndex + 1;
            e.Handled = false;
        }

        private void InsertNextSymbol2(KeyEventArgs e, TextBox textBox, Int32 symbolNumeric)
        {
            textBox.Focus();
            textBox.CaretIndex = 0;
            String twoChar = textBox.Text.Substring(1, 1);
            int caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.Insert(caretIndex, symbolNumeric.ToString());
            textBox.Text = textBox.Text.Remove(caretIndex);
            textBox.Text = textBox.Text.Insert(caretIndex, twoChar);
            textBox.CaretIndex = caretIndex;
            e.Handled = false;
        }

        private void KeyRightPosition(KeyEventArgs e, TextBox textBox)
        {
            e.Handled = false;
            textBox.CaretIndex = 0;
            e.Handled = true;
            textBox.Focus();
            textBox.CaretIndex = 0;
        }

        private void KeyLeftPosition(KeyEventArgs e, TextBox textBox)
        {
            e.Handled = false;
            textBox.CaretIndex = 2;
            e.Handled = true;
            textBox.Focus();
            textBox.CaretIndex = 2;
        }

        private void KeyDelete1(KeyEventArgs e, TextBox textBox, Int32 caretIndex)
        {
            e.Handled = false;
            textBox.Text = textBox.Text.Insert(caretIndex + 1, "0");
            textBox.Text = textBox.Text.Remove(caretIndex, 1);
            textBox.CaretIndex = caretIndex + 1;
            e.Handled = true;
        }

        private void KeyDelete2(KeyEventArgs e, TextBox textBox)
        {
            textBox.Focus();
            textBox.CaretIndex = 0;
            textBox.Text = textBox.Text.Insert(1, "0");
            textBox.Text = textBox.Text.Remove(0, 1);
            textBox.Text = textBox.Text.Insert(0, "0");
            textBox.CaretIndex = 1;
            e.Handled = false;
        }

        private void KeyBack0(KeyEventArgs e, TextBox textBox)
        {
            textBox.CaretIndex = 2;
            textBox.Focus();
            String twoChar = textBox.Text.Substring(0, 1);
            textBox.Text = textBox.Text.Remove(1, 1);
            textBox.Text = textBox.Text.Remove(0, 1);
            textBox.Text = textBox.Text.Insert(0, twoChar);
            textBox.Text = textBox.Text.Insert(1, "0");
            textBox.CaretIndex = 1;
            e.Handled = true;
        }

        private void KeyBack1(KeyEventArgs e, TextBox textBox)
        {
            e.Handled = false;
            textBox.Text = textBox.Text.Remove(0, 1);
            textBox.Text = textBox.Text.Insert(0, "0");
            textBox.CaretIndex = 0;
            e.Handled = true;
        }

        private void KeyBack2(KeyEventArgs e, TextBox textBox)
        {
            String twoChar = textBox.Text.Substring(0, 1);
            textBox.Text = textBox.Text.Remove(1, 1);
            textBox.Text = textBox.Text.Remove(0, 1);
            textBox.Text = textBox.Text.Insert(0, twoChar);
            textBox.Text = textBox.Text.Insert(1, "0");
            textBox.CaretIndex = 1;
            e.Handled = true;
        }

        private void KeyHome(KeyEventArgs e)
        {
            TextHours.Focus();
            TextHours.CaretIndex = 0;
            e.Handled = true;
        }

        private void KeyEnd(KeyEventArgs e)
        {
            TextSeconds.Focus();
            TextSeconds.CaretIndex = 2;
            e.Handled = true;
        }

        private void TextHours_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (CheckAllKey(e))
            {
                e.Handled = true;
                Int32 symbolNumeric = ReturnSymbolNumeric(e);
                if (CheckDigitalKey(e))
                {
                    if (TextHours.CaretIndex == 0)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 2)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            if (Int32.Parse(TextHours.Text.Substring(1, 1)) <= 3 || symbolNumeric <= 1)
                                InsertNextSymbol0(e, TextHours, symbolNumeric);
                            else
                                e.Handled = true;
                        }
                    }
                    else if (TextHours.CaretIndex == 1)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 3)
                        {
                            if (Int32.Parse(TextHours.Text.Substring(0, 1)) > 1)
                                e.Handled = true;
                            else
                                InsertNextSymbol1(e, TextHours, symbolNumeric);
                        }
                        else
                        {
                            InsertNextSymbol1(e, TextHours, symbolNumeric);
                        }
                    }
                    else if (TextHours.CaretIndex == 2)
                    {
                        if (symbolNumeric > 5)
                            e.Handled = true;
                        else
                            InsertNextSymbol2(e, TextMinutes, symbolNumeric);
                    }
                }
                if (CheckOtherKey(e))
                {
                    e.Handled = false;

                    if (e.Key == Key.Right && TextHours.CaretIndex == 2)
                        KeyRightPosition(e, TextMinutes);

                    if (e.Key == Key.PageUp)
                    {
                        TextHours.Text = "23";
                        TextHours.CaretIndex = 0;
                    }

                    if (e.Key == Key.PageDown)
                    {
                        TextHours.Text = "00";
                        TextHours.CaretIndex = 0;
                    }

                    if (e.Key == Key.Up)
                        DigitalUp();

                    if (e.Key == Key.Down)
                        DigitalDown();

                    if (e.Key == Key.Delete)
                    {
                        Int32 caretIndex = TextHours.CaretIndex;
                        if (caretIndex == 0 || caretIndex == 1)
                            KeyDelete1(e, TextHours, caretIndex);
                        else if (caretIndex == 2)
                            KeyDelete2(e, TextMinutes);
                        else
                            e.Handled = true;
                    }

                    if (e.Key == Key.Back && TextHours.CaretIndex == 0)
                    {
                        if (TextHours.SelectionLength == 2)
                            TextHours.Text = "00";

                        e.Handled = true;
                    }
                    else if (e.Key == Key.Back && TextHours.CaretIndex == 1)
                    {
                        if (TextHours.SelectionLength == 2)
                        {
                            TextHours.Text = "00";
                            e.Handled = true;
                        }
                        else
                        {
                            KeyBack1(e, TextHours);
                        }
                    }
                    else if (e.Key == Key.Back && TextHours.CaretIndex == 2)
                    {
                        if (TextHours.SelectionLength == 2)
                        {
                            TextHours.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack2(e, TextHours);
                    }

                    if (e.Key == Key.Home)
                        KeyHome(e);

                    if (e.Key == Key.End)
                        KeyEnd(e);
                }
            }
            else
            {
                e.Handled = true;
            }

            CheckCopyPaste(e);
        }

        private void TextMinutes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (CheckAllKey(e))
            {
                e.Handled = true;
                Int32 symbolNumeric = ReturnSymbolNumeric(e);

                if (CheckDigitalKey(e))
                {
                    if (TextMinutes.CaretIndex == 0)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 5)
                            e.Handled = true;
                        else
                            InsertNextSymbol0(e, TextMinutes, symbolNumeric);
                    }
                    else if (TextMinutes.CaretIndex == 1)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 0)
                        {
                            if (Int32.Parse(TextMinutes.Text.Substring(0, 1)) > 5)
                                e.Handled = true;
                            else
                                InsertNextSymbol1(e, TextMinutes, symbolNumeric);
                        }
                        else
                        {
                            InsertNextSymbol1(e, TextMinutes, symbolNumeric);
                        }
                    }
                    else if (TextMinutes.CaretIndex == 2)
                    {
                        if (symbolNumeric > 5)
                            e.Handled = true;
                        else
                            InsertNextSymbol2(e, TextSeconds, symbolNumeric);
                    }
                }
                if (CheckOtherKey(e))
                {
                    e.Handled = false;

                    if (e.Key == Key.Right && TextMinutes.CaretIndex == 2)
                        KeyRightPosition(e, TextSeconds);

                    if (e.Key == Key.Left && TextMinutes.CaretIndex == 0)
                        KeyLeftPosition(e, TextHours);

                    if (e.Key == Key.PageUp)
                    {
                        TextMinutes.Text = "59";
                        TextMinutes.CaretIndex = 0;
                    }

                    if (e.Key == Key.PageDown)
                    {
                        TextMinutes.Text = "00";
                        TextMinutes.CaretIndex = 0;
                    }

                    if (e.Key == Key.Up)
                        DigitalUp();

                    if (e.Key == Key.Down)
                        DigitalDown();

                    if (e.Key == Key.Delete)
                    {
                        Int32 caretIndex = TextMinutes.CaretIndex;
                        if (caretIndex == 0 || caretIndex == 1)
                            KeyDelete1(e, TextMinutes, caretIndex);
                        else if (caretIndex == 2)
                            KeyDelete2(e, TextSeconds);
                        else
                            e.Handled = true;
                    }

                    if (e.Key == Key.Back && TextMinutes.CaretIndex == 0)
                    {
                        if (TextMinutes.SelectionLength == 2)
                        {
                            TextMinutes.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack0(e, TextHours);
                    }
                    else if (e.Key == Key.Back && TextMinutes.CaretIndex == 1)
                    {
                        if (TextMinutes.SelectionLength == 2)
                        {
                            TextMinutes.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack1(e, TextMinutes);
                    }
                    else if (e.Key == Key.Back && TextMinutes.CaretIndex == 2)
                    {
                        if (TextMinutes.SelectionLength == 2)
                        {
                            TextMinutes.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack2(e, TextMinutes);
                    }

                    if (e.Key == Key.Home)
                        KeyHome(e);

                    if (e.Key == Key.End)
                        KeyEnd(e);
                }
            }
            else
            {
                e.Handled = true;
            }

            CheckCopyPaste(e);
        }

        private void TextSeconds_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (CheckAllKey(e))
            {
                e.Handled = true;
                Int32 symbolNumeric = ReturnSymbolNumeric(e);

                if (CheckDigitalKey(e))
                {
                    if (TextSeconds.CaretIndex == 0)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 5)
                            e.Handled = true;
                        else
                            InsertNextSymbol0(e, TextSeconds, symbolNumeric);
                    }
                    else if (TextSeconds.CaretIndex == 1)
                    {
                        e.Handled = true;
                        if (symbolNumeric > 0)
                        {
                            if (Int32.Parse(TextSeconds.Text.Substring(0, 1)) > 5)
                                e.Handled = true;
                            else
                                InsertNextSymbol1(e, TextSeconds, symbolNumeric);
                        }
                        else
                            InsertNextSymbol1(e, TextSeconds, symbolNumeric);
                    }
                    else if (TextSeconds.CaretIndex == 2)
                    {
                        e.Handled = true;
                    }
                }
                if (CheckOtherKey(e))
                {
                    e.Handled = false;

                    if (e.Key == Key.Left && TextSeconds.CaretIndex == 0)
                        KeyLeftPosition(e, TextMinutes);

                    if (e.Key == Key.PageUp)
                    {
                        TextSeconds.Text = "59";
                        TextSeconds.CaretIndex = 0;
                    }

                    if (e.Key == Key.PageDown)
                    {
                        TextSeconds.Text = "00";
                        TextSeconds.CaretIndex = 0;
                    }

                    if (e.Key == Key.Up)
                        DigitalUp();

                    if (e.Key == Key.Down)
                        DigitalDown();

                    if (e.Key == Key.Delete)
                    {
                        Int32 caretIndex = TextSeconds.CaretIndex;
                        if (caretIndex == 0 || caretIndex == 1)
                            KeyDelete1(e, TextSeconds, caretIndex);
                        else
                            e.Handled = true;
                    }

                    if (e.Key == Key.Back && TextSeconds.CaretIndex == 0)
                    {
                        if (TextSeconds.SelectionLength == 2)
                        {
                            TextSeconds.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack0(e, TextMinutes);
                    }
                    else if (e.Key == Key.Back && TextSeconds.CaretIndex == 1)
                    {
                        if (TextSeconds.SelectionLength == 2)
                        {
                            TextSeconds.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack1(e, TextSeconds);
                    }
                    else if (e.Key == Key.Back && TextSeconds.CaretIndex == 2)
                    {
                        if (TextSeconds.SelectionLength == 2)
                        {
                            TextSeconds.Text = "00";
                            e.Handled = true;
                        }
                        else
                            KeyBack2(e, TextSeconds);
                    }

                    if (e.Key == Key.Home)
                        KeyHome(e);

                    if (e.Key == Key.End)
                        KeyEnd(e);
                }
            }
            else
            {
                e.Handled = true;
            }

            CheckCopyPaste(e);
        }

        private void DigitalUp()
        {
            if (lastFocus == "TextHours")
            {
                if (Int32.Parse(TextHours.Text) < 23)
                    TextHours.Text = (Int32.Parse(TextHours.Text) + 1).ToString("00");
                else if (Int32.Parse(TextHours.Text) == 23)
                    TextHours.Text = "00";
                TextHours.CaretIndex = 2;
            }
            if (lastFocus == "TextMinutes")
            {
                if (Int32.Parse(TextMinutes.Text) < 59)
                {
                    TextMinutes.Text = (Int32.Parse(TextMinutes.Text) + 1).ToString("00");
                }
                else if (Int32.Parse(TextMinutes.Text) == 59 && Int32.Parse(TextHours.Text) == 23)
                {
                    TextHours.Text = "00";
                    TextMinutes.Text = "00";
                }
                else
                {
                    if (Int32.Parse(TextHours.Text) < 23)
                    {
                        TextMinutes.Text = "00";
                        TextHours.Text = (Int32.Parse(TextHours.Text) + 1).ToString("00");
                    }
                }
                TextMinutes.CaretIndex = 2;
            }
            if (lastFocus == "TextSeconds")
            {
                if (Int32.Parse(TextSeconds.Text) < 59)
                {
                    TextSeconds.Text = (Int32.Parse(TextSeconds.Text) + 1).ToString("00");
                }
                else
                {
                    if (Int32.Parse(TextMinutes.Text) == 59)
                    {
                        if (Int32.Parse(TextHours.Text) < 23)
                        {
                            TextSeconds.Text = "00";
                            TextMinutes.Text = "00";
                            TextHours.Text = (Int32.Parse(TextHours.Text) + 1).ToString("00");
                        }
                        else
                        {
                            TextSeconds.Text = "00";
                            TextMinutes.Text = "00";
                            TextHours.Text = "00";
                        }
                    }
                    else
                    {
                        TextSeconds.Text = "00";
                        TextMinutes.Text = (Int32.Parse(TextMinutes.Text) + 1).ToString("00");
                    }
                }
                TextSeconds.CaretIndex = 2;
            }
        }

        private void DigitalDown()
        {
            if (lastFocus == "TextHours")
            {
                if (Int32.Parse(TextHours.Text) > 0)
                {
                    TextHours.Text = (Int32.Parse(TextHours.Text) - 1).ToString("00");
                }
                if (Int32.Parse(TextHours.Text) == 0)
                {
                    TextHours.Text = "23";
                }
                TextHours.CaretIndex = 2;
            }
            if (lastFocus == "TextMinutes")
            {
                if (Int32.Parse(TextMinutes.Text) == 0)
                {
                    if (Int32.Parse(TextHours.Text) != 0)
                    {
                        TextHours.Text = (Int32.Parse(TextHours.Text) - 1).ToString("00");
                        TextMinutes.Text = "59";
                    }
                    else
                    {
                        TextHours.Text = "23";
                        TextMinutes.Text = "59";
                    }
                }
                else
                {
                    TextMinutes.Text = (Int32.Parse(TextMinutes.Text) - 1).ToString("00");
                }
                TextMinutes.CaretIndex = 2;
            }
            if (lastFocus == "TextSeconds")
            {
                if (Int32.Parse(TextSeconds.Text) == 0)
                {
                    if (Int32.Parse(TextMinutes.Text) == 0)
                    {
                        if (Int32.Parse(TextHours.Text) != 0)
                        {
                            TextHours.Text = (Int32.Parse(TextHours.Text) - 1).ToString("00");
                            TextMinutes.Text = "59";
                            TextSeconds.Text = "59";
                        }
                        else
                        {
                            TextHours.Text = "23";
                            TextMinutes.Text = "59";
                            TextSeconds.Text = "59";
                        }

                    }
                    else
                    {
                        TextMinutes.Text = (Int32.Parse(TextMinutes.Text) - 1).ToString("00");
                        TextSeconds.Text = "59";
                    }
                }
                else
                {
                    TextSeconds.Text = (Int32.Parse(TextSeconds.Text) - 1).ToString("00");
                }
                TextSeconds.CaretIndex = 2;
            }
        }

        private void Button_Click_Down(object sender, RoutedEventArgs e)
        {
            DigitalDown();
        }

        private void Button_Click_Up(object sender, RoutedEventArgs e)
        {
            DigitalUp();
        }

        private void TextHours_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocus = "TextHours";
            TextHours.SelectAll();
        }

        private void TextMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocus = "TextMinutes";
            TextMinutes.SelectAll();
        }

        private void TextSeconds_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocus = "TextSeconds";
            TextSeconds.SelectAll();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (needRefresh)
            {
                if (TextHours.Text.Length == 2 && TextMinutes.Text.Length == 2 && TextSeconds.Text.Length == 2)
                {
                    Value = String.Concat(TextHours.Text, ":", TextMinutes.Text, ":", TextSeconds.Text);
                    ValueChanged?.Invoke();
                }
            }
        }
    }
}
