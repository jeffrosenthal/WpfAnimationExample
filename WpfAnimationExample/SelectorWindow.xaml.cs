using System.Windows;

namespace WpfAnimationExample
{
    /// <summary>
    /// Interaction logic for SelectorWindow.xaml
    /// </summary>
    public partial class SelectorWindow : Window
    {
        public SelectorWindow()
        {
            InitializeComponent();
        }
        
        private void EasingSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EasingFunctionDisplayWindow();
            window.Show();
        }
        private void TestingSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new TestWindow();
            window.Show();
        }
    }
}
