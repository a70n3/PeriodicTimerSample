using System;
using System.Windows;

namespace PeriodicTimerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(TimeSpan.FromMilliseconds(1000));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.Start();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                try
                {
                    await vm.StopTimer();
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Operation was cancelled by the user");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }
    }
}
