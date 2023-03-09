using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sklad_v1_001.Report.FlexGrid
{
    public class FlexGrid : Grid
    {
        public enum GridLinesVisibilityEnum
        {
            Both,
            Vertical,
            Horizontal,
            None
        }

        public bool ShowFlexGridLines
        {
            get { return (bool)GetValue(ShowFlexGridLinesProperty); }
            set { SetValue(ShowFlexGridLinesProperty, value); }
        }

        public static readonly DependencyProperty ShowFlexGridLinesProperty =
            DependencyProperty.Register("ShowFlexGridLines", typeof(bool), typeof(FlexGrid), new UIPropertyMetadata(false));

        public GridLinesVisibilityEnum GridLinesVisibility
        {
            get { return (GridLinesVisibilityEnum)GetValue(GridLinesVisibilityProperty); }
            set { SetValue(GridLinesVisibilityProperty, value); }
        }

        public static readonly DependencyProperty GridLinesVisibilityProperty =
            DependencyProperty.Register("GridLinesVisibility", typeof(GridLinesVisibilityEnum), typeof(FlexGrid), new UIPropertyMetadata(GridLinesVisibilityEnum.Both));

        public Brush GridLineBrush
        {
            get { return (Brush)GetValue(GridLineBrushProperty); }
            set { SetValue(GridLineBrushProperty, value); }
        }

        public static readonly DependencyProperty GridLineBrushProperty =
            DependencyProperty.Register("GridLineBrush", typeof(Brush), typeof(FlexGrid), new UIPropertyMetadata(Brushes.Black));

        public double GridLineThickness
        {
            get { return (double)GetValue(GridLineThicknessProperty); }
            set { SetValue(GridLineThicknessProperty, value); }
        }

        public static readonly DependencyProperty GridLineThicknessProperty =
            DependencyProperty.Register("GridLineThickness", typeof(double), typeof(FlexGrid), new UIPropertyMetadata(1.0));

        protected override void OnRender(DrawingContext dc)
        {
            if (ShowFlexGridLines)
            {
                if (GridLinesVisibility == GridLinesVisibilityEnum.Both)
                {
                    foreach (var rowDefinition in RowDefinitions)
                    {
                        dc.DrawLine(new Pen(GridLineBrush, GridLineThickness), new Point(0, rowDefinition.Offset), new Point(ActualWidth, rowDefinition.Offset));
                    }

                    foreach (var columnDefinition in ColumnDefinitions)
                    {
                        dc.DrawLine(new Pen(GridLineBrush, GridLineThickness), new Point(columnDefinition.Offset, 0), new Point(columnDefinition.Offset, ActualHeight));
                    }
                    dc.DrawRectangle(Brushes.Transparent, new Pen(GridLineBrush, GridLineThickness), new Rect(0, 0, ActualWidth, ActualHeight));
                }
                else if (GridLinesVisibility == GridLinesVisibilityEnum.Vertical)
                {
                    foreach (var columnDefinition in ColumnDefinitions)
                    {
                        dc.DrawLine(new Pen(GridLineBrush, GridLineThickness), new Point(columnDefinition.Offset, 0), new Point(columnDefinition.Offset, ActualHeight));
                    }
                    dc.DrawRectangle(Brushes.Transparent, new Pen(GridLineBrush, GridLineThickness), new Rect(0, 0, ActualWidth, ActualHeight));
                }
                else if (GridLinesVisibility == GridLinesVisibilityEnum.Horizontal)
                {
                    foreach (var rowDefinition in RowDefinitions)
                    {
                        dc.DrawLine(new Pen(GridLineBrush, GridLineThickness), new Point(0, rowDefinition.Offset), new Point(ActualWidth, rowDefinition.Offset));
                    }
                    dc.DrawRectangle(Brushes.Transparent, new Pen(GridLineBrush, GridLineThickness), new Rect(0, 0, ActualWidth, ActualHeight));
                }
                else if (GridLinesVisibility == GridLinesVisibilityEnum.Horizontal)
                {

                }

            }
            base.OnRender(dc);
        }
        static FlexGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlexGrid), new FrameworkPropertyMetadata(typeof(FlexGrid)));
        }
    }
}
