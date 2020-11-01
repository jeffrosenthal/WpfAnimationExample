using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfAnimationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EasingFunctionDisplayWindow : Window
    {
        public EasingFunctionDisplayWindow()
        {
            InitializeComponent();
            Canvas.SetLeft(MainEllipse, 0);
            Canvas.SetTop(MainEllipse, 40);

            EasingCombo.Items.Add("None");
            EasingCombo.Items.Add("BackEase");
            EasingCombo.Items.Add("BounceEase");
            EasingCombo.Items.Add("CircleEase");
            EasingCombo.Items.Add("CubicEase");
            EasingCombo.Items.Add("ElasticEase");
            EasingCombo.Items.Add("ExponentialEase");
            EasingCombo.Items.Add("PowerEase");
            EasingCombo.Items.Add("QuadraticEase");
            EasingCombo.Items.Add("QuarticEase");
            EasingCombo.Items.Add("QuinticEase");
            EasingCombo.Items.Add("SineEase");
            EasingCombo.SelectedIndex = 0;

            ColorCombo.Items.Add("Blue");
            ColorCombo.Items.Add("Green");
            ColorCombo.Items.Add("Red");
            ColorCombo.SelectedIndex = 0;

        }

        
        public object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            var color = ColorCombo.SelectedValue;
            var ef = EasingCombo.SelectedValue;
            if (ef == null) return;
            if (color == null) return;

            EasingFunctionBase easing = null;
            if ((string) ef != "None")
            {
                easing = (EasingFunctionBase) GetInstance($"System.Windows.Media.Animation.{(string) ef}");
                easing.EasingMode = EasingMode.EaseOut;
            }

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 330;
            doubleAnimation.EasingFunction = easing;
            doubleAnimation.Duration = TimeSpan.FromSeconds(6);
            doubleAnimation.FillBehavior = FillBehavior.Stop;
            MainEllipse.BeginAnimation(Canvas.TopProperty, doubleAnimation);

           
            Dispatcher.BeginInvoke(
                new ThreadStart((async () =>
            {
                for (int t = 0; t < 50; t++)
                {
                    try
                    {
                        var x = Canvas.GetLeft(MainEllipse);
                        var y = Canvas.GetTop(MainEllipse);
                        Color cc = (Color)ColorConverter.ConvertFromString((string)color);
                        Ellipse freezeFrameControl = new Ellipse { Height = 2, Width = 2, Fill = new SolidColorBrush(cc) };
                        freezeFrameControl.ToolTip = new ToolTip().Content = (string) ef;
                        Canvas.SetLeft(freezeFrameControl, x);
                        Canvas.SetTop(freezeFrameControl, y);
                        Canvas.Children.Add(freezeFrameControl);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                    await Task.Delay(100);
                }
            })));
        }

        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var ellipse = Canvas.Children[0];
            Canvas.Children.Clear();
            Canvas.Children.Add(ellipse);
        }
    }
}
