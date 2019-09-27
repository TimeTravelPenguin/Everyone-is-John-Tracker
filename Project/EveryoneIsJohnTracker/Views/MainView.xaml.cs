using System.Windows;
using EveryoneIsJohnTracker.ViewModels;

namespace EveryoneIsJohnTracker.Views
{
    /// <summary>
    ///     Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}