using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Sklad_v1_001.Control.SimpleControl
{
    class TextBoxTimeBehavior: Behavior<TextBox>
    {
        public static readonly DependencyProperty InputMaskProperty =
         DependencyProperty.Register("InputMask", typeof(string), typeof(TextBoxTimeBehavior), null);
        public string InputMask
        {
            get { return (string)GetValue(InputMaskProperty); }
            set { SetValue(InputMaskProperty, value); }
        }

        public static readonly DependencyProperty ResultTimeStringProperty =
          DependencyProperty.Register("ResultTimeString", typeof(string), typeof(TextBoxTimeBehavior), null);
        public string ResultTimeString
        {
            get { return (string)GetValue(ResultTimeStringProperty); }
            set { SetValue(ResultTimeStringProperty, value); }
        }

        public static readonly DependencyProperty IsErrorProperty =
          DependencyProperty.Register("IsError", typeof(Boolean), typeof(TextBoxTimeBehavior));
        public Boolean IsError
        {
            get { return (Boolean)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        public static readonly DependencyProperty PromptCharProperty =
           DependencyProperty.Register("PromptChar", typeof(char), typeof(TextBoxTimeBehavior),
                                        new PropertyMetadata('_'));
        public char PromptChar
        {
            get { return (char)GetValue(PromptCharProperty); }
            set { SetValue(PromptCharProperty, value); }
        }

        public MaskedTextProvider Provider { get; private set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput += AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown += AssociatedObjectPreviewKeyDown;
            DataObject.AddPastingHandler(AssociatedObject, Pasting);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.PreviewTextInput -= AssociatedObjectPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= AssociatedObjectPreviewKeyDown;
            DataObject.RemovePastingHandler(AssociatedObject, Pasting);
        }

        void AssociatedObjectLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Provider = new MaskedTextProvider(InputMask, CultureInfo.CurrentCulture);
            Provider.Set(AssociatedObject.Text);
            ValidateText();
            Provider.PromptChar = PromptChar;
            AssociatedObject.Text = Provider.ToDisplayString();
            var textProp = DependencyPropertyDescriptor.FromProperty(TextBox.TextProperty, typeof(TextBox));
            if (textProp != null)
            {
                textProp.AddValueChanged(AssociatedObject, (s, args) => UpdateText());
            }
        }

        void AssociatedObjectPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TreatSelectedText();
            var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);
            if (Keyboard.IsKeyToggled(Key.Insert))
            {
                if (Provider.Replace(e.Text, position))
                {
                    position++;
                }
            }
            else
            {
                if (Provider.InsertAt(e.Text, position))
                {
                    position++;
                }
            }
            position = GetNextCharacterPosition(position);
            RefreshText(position);
            e.Handled = true;
        }

        void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (
                e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 ||
                e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                )
            {
                if (!AssociatedObject.Text.Contains("_") && String.IsNullOrEmpty(AssociatedObject.SelectedText))
                {
                    if (Provider.RemoveAt(AssociatedObject.SelectionStart))
                    {
                        RefreshText(AssociatedObject.SelectionStart);
                    }
                }
            }

            if (e.Key == Key.Space)
            {
                TreatSelectedText();
                var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);
                if (Provider.InsertAt(" ", position))
                {
                    RefreshText(position);
                }
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                if (TreatSelectedText())
                {
                    RefreshText(AssociatedObject.SelectionStart);
                }
                else
                {
                    if (AssociatedObject.SelectionStart != 0)
                    {
                        if (Provider.RemoveAt(AssociatedObject.SelectionStart - 1))
                        {
                            RefreshText(AssociatedObject.SelectionStart - 1);
                        }
                    }
                }
                e.Handled = true;
            }

            if (e.Key == Key.Delete)
            {
                if (TreatSelectedText())
                {
                    RefreshText(AssociatedObject.SelectionStart);
                }
                else
                {
                    if (Provider.RemoveAt(AssociatedObject.SelectionStart))
                    {
                        RefreshText(AssociatedObject.SelectionStart);
                    }
                }
                e.Handled = true;
            }
        }

        private void Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));
                TreatSelectedText();
                var position = GetNextCharacterPosition(AssociatedObject.SelectionStart);
                if (Provider.InsertAt(pastedText, position))
                {
                    RefreshText(position);
                }
            }
            e.CancelCommand();
        }

        private void ValidateText()
        {
            Boolean tryResult;

            if (AssociatedObject.Text.Length > 5)
            {
                tryResult = DateTime.TryParse("01.01.1990 " + AssociatedObject.Text, out DateTime temp);
            }
            else
            {
                tryResult = DateTime.TryParse("01.01.1990 " + AssociatedObject.Text + ":00", out DateTime temp);
            }

            Border borderAssociatedObject = null;
            Grid gridAssociatedObject = AssociatedObject.Parent as Grid;
            if (gridAssociatedObject != null)
            {
                borderAssociatedObject = gridAssociatedObject.Parent as Border;
            }

            if (tryResult)
            {
                ResultTimeString = AssociatedObject.Text;
                if (borderAssociatedObject != null)
                {
                    borderAssociatedObject.Style = (Style)MainWindow.AppWindow.TryFindResource("FilterBorderStyle");
                }
                IsError = false;
            }
            else
            {
                if (borderAssociatedObject != null)
                {
                    borderAssociatedObject.Style = (Style)MainWindow.AppWindow.TryFindResource("FilterBorderErrorStyle");
                }
                IsError = true;
            }
        }

        private void UpdateText()
        {
            ValidateText();
            if (Provider.ToDisplayString().Equals(AssociatedObject.Text))
            {
                return;
            }
            var success = Provider.Set(AssociatedObject.Text);
            SetText(success ? Provider.ToDisplayString() : AssociatedObject.Text);
        }

        private bool TreatSelectedText()
        {
            if (AssociatedObject.SelectionLength > 0)
            {
                return Provider.RemoveAt(AssociatedObject.SelectionStart, AssociatedObject.SelectionStart + AssociatedObject.SelectionLength - 1);
            }
            return false;
        }

        private void RefreshText(int position)
        {
            SetText(Provider.ToDisplayString());
            AssociatedObject.SelectionStart = position;
        }

        private void SetText(string text)
        {
            AssociatedObject.Text = String.IsNullOrWhiteSpace(text) ? String.Empty : text;
        }

        private int GetNextCharacterPosition(int startPosition)
        {
            var position = Provider.FindEditPositionFrom(startPosition, true);
            if (position == -1)
            {
                return startPosition;
            }
            else
            {
                return position;
            }
        }
    }
}
