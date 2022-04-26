using Sklad_v1_001.Control.FlexButton;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sklad_v1_001
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public enum LogImageType
        {
            None = 1,
            Error = 2,
            Hand = 3,
            Stop = 4,
            Question = 5,
            Exclamation = 6,
            Warning = 7,
            Asterisk = 8,
            Information = 9,
            Action = 10,
            Filter = 11
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            #region Tooltip
            EventManager.RegisterClassHandler(typeof(LevelButton), UIElement.MouseEnterEvent, new RoutedEventHandler(level1Button_MouseEnterEvent), true);
            EventManager.RegisterClassHandler(typeof(Button), UIElement.MouseEnterEvent, new RoutedEventHandler(button_MouseEnterEvent), true);
            #endregion
        }


        private void level1Button_MouseEnterEvent(object sender, RoutedEventArgs e)
        {
            if (Sklad_v1_001.MainWindow.AppWindow != null)
            {
                LevelButton selected = sender as LevelButton;
                if (selected != null)
                {
                    Label testLabel = new Label();
                    testLabel.Content = selected.Text != String.Empty ? selected.Text : String.Empty;
                    testLabel.Style = selected.Style;
                    testLabel.FontFamily = selected.FontFamily;
                    testLabel.FontSize = selected.FontSize;
                    testLabel.Padding = selected.Padding;
                    testLabel.Margin = selected.Margin;
                    testLabel.Arrange(new Rect(0, 0, 9999, (Int32)selected.ActualHeight));
                    if (selected.ActualHeight == 100)
                    {
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = selected.Text != String.Empty ? selected.Text : String.Empty;
                        selected.ToolTip = toolTip;
                    }
                    else
                    {
                        selected.ToolTip = null;
                    }
                }
            }
        }
        private void button_MouseEnterEvent(object sender, RoutedEventArgs e)
        {
            if (Sklad_v1_001.MainWindow.AppWindow != null)
            {
                Button selected = sender as Button;
                if (selected != null)
                {
                    Label testLabel = new Label();
                    testLabel.Content = selected.Name.ToString() == String.Empty ? selected.Name : String.Empty;
                   // testLabel.Style = selected.Style;
                    testLabel.FontFamily = selected.FontFamily;
                    testLabel.FontSize = selected.FontSize;
                    testLabel.Padding = selected.Padding;
                    testLabel.Margin = selected.Margin;
                    testLabel.Arrange(new Rect(0, 0, 9999, (Int32)selected.ActualHeight));
                    if (selected.Name != "button")
                    {
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = selected.Name.ToString() != String.Empty ? selected.Name : String.Empty;
                        selected.ToolTip = toolTip;
                    }
                    else
                    {
                        selected.ToolTip = null;
                    }
                }
            }
        }

    }
}
