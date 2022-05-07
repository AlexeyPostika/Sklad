using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.FlexButton
{
    /// <summary>
    /// Логика взаимодействия для LevelButtonBasket.xaml
    /// </summary>
    public partial class LevelButtonBasket : UserControl, IAbstractButton
    {
        Boolean isCollapse;
        Boolean isCollapseVisible;

        // свойство зависимостей
        public static readonly DependencyProperty VisibilityEditBoxProperty = DependencyProperty.Register(
                        "VisibilityEditBox",
                        typeof(Visibility),
                        typeof(LevelButtonBasket), new UIPropertyMetadata(Visibility.Visible));     

        // свойство зависимостей
        public static readonly DependencyProperty VisibilityImageWarningProperty = DependencyProperty.Register(
                        "VisibilityImageWarning",
                        typeof(Boolean),
                        typeof(LevelButtonBasket), new UIPropertyMetadata(false, new PropertyChangedCallback(ChangeVisibilityImageWarning)));

        // свойство зависимостей
        public static readonly DependencyProperty VisibilityEllipsProperty = DependencyProperty.Register(
                      "VisibilityEllips",
                      typeof(Visibility),
                      typeof(LevelButtonBasket), new UIPropertyMetadata(Visibility.Collapsed));
        
        // свойство зависимостей
        public static readonly DependencyProperty QuantityBasketProperty = DependencyProperty.Register(
                     "QuantityBasket",
                     typeof(Int32),
                     typeof(LevelButtonBasket), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilityEditBox
        {
            get { return (Visibility)GetValue(VisibilityEditBoxProperty); }
            set { SetValue(VisibilityEditBoxProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean VisibilityImageWarning
        {
            get
            {
                return (Boolean)GetValue(VisibilityImageWarningProperty);
            }
            set
            {
                SetValue(VisibilityImageWarningProperty, value);
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilityEllips
        {
            get { return (Visibility)GetValue(VisibilityEllipsProperty); }
            set { SetValue(VisibilityEllipsProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 QuantityBasket
        {
            get { return (Int32)GetValue(QuantityBasketProperty); }
            set { SetValue(QuantityBasketProperty, value); }
        }


        public String ImageSource
        {
            set
            {
                this.Image.Source = new BitmapImage(new Uri(value, UriKind.Relative));
            }
            get
            {
                return null;
            }
        }

        public Boolean IsCollapse
        {
            get
            {
                return isCollapse;
            }

            set
            {
                isCollapse = value;
            }
        }

        public Boolean IsCollapseVisible
        {
            get
            {
                return isCollapseVisible;
            }

            set
            {
                isCollapseVisible = value;
                if (!isCollapseVisible)
                {
                    this.ImageUp.Visibility = Visibility.Hidden;
                    this.ImageDown.Visibility = Visibility.Hidden;
                }

                if (isCollapseVisible)
                {
                    this.ImageUp.Visibility = Visibility.Hidden;
                    this.ImageDown.Visibility = Visibility.Visible;
                }
            }
        }

        public string Text
        {
            get
            {
                return this.TextField.Content.ToString();
            }

            set
            {
                this.TextField.Content = value;
            }
        }

        public LevelButtonBasket()
        {
            InitializeComponent();

            this.ImageUp.Visibility = Visibility.Hidden;
            this.ImageDown.Visibility = Visibility.Visible;
            this.ImageWarning.Source = ImageHelper.GenerateImage("IconWarningMenu.png");
            IsCollapse = true;
            IsCollapseVisible = false;
        }

        public event Action ButtonClick; //получаем название события
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)
            {
                Collapsed_Uncollapsed();
                ButtonClick();
            }
        }
        public void Collapsed_Uncollapsed()
        {
            if (isCollapseVisible)
            {
                if (isCollapse)
                {
                    IsCollapse = false;
                    this.ImageUp.Visibility = Visibility.Visible;
                    this.ImageDown.Visibility = Visibility.Hidden;
                }
                else
                {
                    IsCollapse = true;
                    this.ImageUp.Visibility = Visibility.Hidden;
                    this.ImageDown.Visibility = Visibility.Visible;
                }
            }
        }
        public static void ChangeVisibilityImageWarning(DependencyObject depObject, DependencyPropertyChangedEventArgs args)
        {
            LevelButton lb = (LevelButton)depObject;

            if (lb.VisibilityImageWarning == false)
            {
                lb.ImageWarning.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (lb.ImageUp.Visibility == Visibility.Hidden && lb.ImageDown.Visibility == Visibility.Hidden)
                {
                    lb.ImageWarning.Margin = new Thickness(0, 0, 5, 0);
                    lb.ImageWarning.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(lb.ImageWarning, 3);
                }
                else
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.From = 1;
                    doubleAnimation.To = 0;
                    doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.8));
                    doubleAnimation.AutoReverse = true;
                    doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    lb.ImageWarning.BeginAnimation(OpacityProperty, doubleAnimation);

                    lb.ImageWarning.Margin = new Thickness(0, 3, -10, 0);
                    lb.ImageWarning.VerticalAlignment = VerticalAlignment.Top;
                }
                lb.ImageWarning.Visibility = Visibility.Visible;
            }
        }
    }
}
