using System.Windows;
using System.Windows.Controls;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            //Loaded += MainWindow_Loaded;
            //TabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        //private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    TabControl tabControl = sender as TabControl;
        //    if (tabControl.SelectedIndex == 0)
        //    {
        //        UserName.Focus();
        //    }
        //}

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    UserName.Focus();
        //}
    }
}
