using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BuyScan_UW.Charts
{
    public sealed partial class PieChart : UserControl
    {
        public PieChart()
        {
            this.InitializeComponent();

            double radius = 20.0;
            Point center = new Point(100, 100);

            Slice slice1 = new Slice(5, Colors.Blue, radius, center);
            //Slice slice2 = new Slice(22.5, Colors.Red, radius, center, slice1.EndAngle);
            //Slice slice3 = new Slice(62.0, Colors.Green, radius, center, slice2.EndAngle);

            PieChartCanvas.Children.Add(slice1.SlicePath);
            //PieChartCanvas.Children.Add(slice2.SlicePath);
            //PieChartCanvas.Children.Add(slice3.SlicePath);
        }

        public class Slice
        {
            public Path SlicePath { get { return GetSlicePath(); } }

            public double Value { get; set; }
            public Color SliceColor { get; set; }
            public double Radius { get; set; }
            public Point StartVector { get; set; }
            public Point StartPoint { get; set; }
            public Point Center { get; set; }
            public double StartAngle { get; set; }
            public double EndAngle { get { return GetEndAngle(); } }

            public Slice(double value, Color color, double radius, Point center, double startAngle = 0.0)
            {
                StartAngle = startAngle;
                Value = value;
                SliceColor = color;
                Radius = radius;
                Center = center;

                StartVector = new Point(center.X, -radius);
                StartPoint = new Point(center.X, center.Y - radius);
            }

            private Path GetSlicePath()
            {
                Path path = new Path();
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                PathSegmentCollection segments = new PathSegmentCollection();

                figure.StartPoint = StartPoint;
                //figure.StartPoint = new Point(0, 0);

                ArcSegment arc = new ArcSegment();
                arc.Point = EndPoint();
                //arc.Point = new Point(50.0, 50.0);
                arc.Size = new Size(1.0, 1.0);
                //arc.RotationAngle = 90;
                arc.IsLargeArc = EndAngle > 180;
                //arc.IsLargeArc = false;
                arc.SweepDirection = SweepDirection.Counterclockwise;

                segments.Add(arc);

                figure.Segments = segments;
                geometry.Figures.Add(figure);
                path.Data = geometry;
                path.Stroke = new SolidColorBrush(SliceColor);

                return path;
            }

            private double GetEndAngle()
            {
                return StartAngle + (Value / 100.0 * 360.0);
            }

            private Point EndPoint()
            {
                double endPointX = (Math.Cos(Theta()) * StartVector.X) + (Math.Sin(Theta()) * StartVector.Y);
                double endPointY = (-Math.Sin(Theta()) * StartVector.X) + (Math.Cos(Theta()) * StartVector.Y);
                return new Point(endPointX, endPointY);
            }

            private double Theta()
            {
                //return (2 * Math.PI) / 360.0 * EndAngle;
                return EndAngle * (Math.PI / 180.0);
            }
        }
    }
}
