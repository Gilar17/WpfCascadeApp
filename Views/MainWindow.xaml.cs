using System.Windows;
using WpfCascadeApp.ViewModels;

namespace WpfCascadeApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
