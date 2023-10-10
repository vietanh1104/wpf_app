using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for CircleProgressBarUserControl.xaml
    /// </summary>
    public partial class CircleProgressBarUserControl : UserControl
    {
        DispatcherTimer progressTimer;
        double currentAngle = 0;

        public CircleProgressBarUserControl()
        {
            InitializeComponent();
            Angle = (Percentage * 360) / 100;
            Loaded += (o, e) =>
            {
                RenderTrail();
                RenderProgress();
            };
            LayoutUpdated += (o, e) =>
            {
                RenderTrail();
                RenderProgress();
            };
            Unloaded += (o, e) =>
            {
                if (progressTimer != null && progressTimer.IsEnabled)
                {
                    progressTimer.Stop();
                }
            };
        }

        #region Properties

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public Brush TrailColor
        {
            get { return (Brush)GetValue(TrailColorProperty); }
            set { SetValue(TrailColorProperty, value); }
        }

        public Brush ProgressColor
        {
            get { return (Brush)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        public int StrokeThickness
        {
            get { return (int)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(CircleProgressBarUserControl), new PropertyMetadata(65d, new PropertyChangedCallback(OnPercentageChanged)));

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(int), typeof(CircleProgressBarUserControl), new PropertyMetadata(5, new PropertyChangedCallback(OnThicknessChanged)));

        // Using a DependencyProperty as the backing store for TrailColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrailColorProperty =
            DependencyProperty.Register("TrailColor", typeof(Brush), typeof(CircleProgressBarUserControl), new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnTrailColorChanged)));

        // Using a DependencyProperty as the backing store for ProgressColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.Register("ProgressColor", typeof(Brush), typeof(CircleProgressBarUserControl), new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnProgressColorChanged)));

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(CircleProgressBarUserControl), new PropertyMetadata(25, new PropertyChangedCallback(OnRadiusChanged)));

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CircleProgressBarUserControl), new PropertyMetadata(120d, new PropertyChangedCallback(OnAngleChanged)));

        #endregion

        #region Methods

        private static void OnTrailColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            //CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            //circle.SetTrailColor((SolidColorBrush)args.NewValue);
        }

        private static void OnProgressColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            //CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            //circle.SetProgressColor((SolidColorBrush)args.NewValue);
        }

        private static void OnThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            //CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            //circle.SetStrokeThickness((int)args.NewValue);
        }

        private static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            if (circle.Percentage > 100) circle.Percentage = 100;
            circle.Angle = (circle.Percentage * 360) / 100;
        }

        private static void OnRadiusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            circle.RenderTrail();
            circle.RenderProgress();
        }

        private static void OnAngleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CircleProgressBarUserControl circle = sender as CircleProgressBarUserControl;
            circle.RenderProgress();
        }

        public void SetStrokeThickness(int strokeThickness)
        {
            trailPathRoot.StrokeThickness = strokeThickness;
            progressPathRoot.StrokeThickness = strokeThickness;
        }

        public void SetTrailColor(SolidColorBrush trailColor)
        {
            trailPathRoot.Stroke = trailColor;
        }

        public void SetProgressColor(SolidColorBrush progressColor)
        {
            progressPathRoot.Stroke = progressColor;
        }

        public void RenderTrail()
        {
            Point startPoint = new Point(Radius, 0);
            Point endPoint = ComputeCartesianCoordinate(360, Radius);
            endPoint.X += Radius;
            endPoint.Y += Radius;

            trailPathRoot.Width = Radius * 2 + StrokeThickness;
            trailPathRoot.Height = Radius * 2 + StrokeThickness;
            trailPathRoot.Margin = new Thickness(StrokeThickness, StrokeThickness, 0, 0);

            Size outerArcSize = new Size(Radius, Radius);

            trailPathFigure.StartPoint = startPoint;

            if (startPoint.X == Math.Round(endPoint.X) && startPoint.Y == Math.Round(endPoint.Y))
                endPoint.X -= 0.01;

            trailArcSegment.Point = endPoint;
            trailArcSegment.Size = outerArcSize;
            trailArcSegment.IsLargeArc = true;
        }

        public void RenderProgress()
        {
            Point startPoint = new Point(Radius, 0);

            progressPathRoot.Width = Radius * 2 + StrokeThickness;
            progressPathRoot.Height = Radius * 2 + StrokeThickness;
            progressPathRoot.Margin = new Thickness(StrokeThickness, StrokeThickness, 0, 0);

            Size outerArcSize = new Size(Radius, Radius);

            progressPathFigure.StartPoint = new Point(Radius, 0);
            progressArcSegment.Size = outerArcSize;

            // draw current angle
            RenderProgressAnimate(startPoint);

            // check if current angle = target angle -> return
            if (currentAngle == Angle)
            {
                if (progressTimer != null && progressTimer.IsEnabled)
                {
                    progressTimer.Stop();
                }
                return;
            }

            if (progressTimer == null || !progressTimer.IsEnabled)
            {
                progressTimer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher)
                {
                    Interval = TimeSpan.FromMilliseconds(10)
                };
                double intervalAngle = 5;
                progressTimer.Tick += (o, e) =>
                {
                    if (Math.Abs(currentAngle - Angle) < intervalAngle)
                    {
                        currentAngle = Angle;
                    }
                    else
                    {
                        if (currentAngle > Angle)
                        {
                            currentAngle -= intervalAngle;
                        }
                        else
                        {
                            currentAngle += intervalAngle;
                        }
                    }

                    RenderProgressAnimate(startPoint);

                    if (currentAngle == Angle)
                    {
                        progressTimer.Stop();
                    }
                };

                progressTimer.Start();
            }
        }

        private void RenderProgressAnimate(Point startPoint)
        {
            Point endPoint = ComputeCartesianCoordinate(currentAngle, Radius);
            endPoint.X += Radius;
            endPoint.Y += Radius;
            bool largeArc = currentAngle > 180.0;

            if (startPoint.X == Math.Round(endPoint.X) && startPoint.Y == Math.Round(endPoint.Y))
                endPoint.X -= 0.01;

            progressArcSegment.Point = endPoint;
            progressArcSegment.IsLargeArc = largeArc;
        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            // convert to radians
            double angleRad = (Math.PI / 180.0) * (angle - 90);

            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);

            return new Point(x, y);
        }

        #endregion
    }
}
