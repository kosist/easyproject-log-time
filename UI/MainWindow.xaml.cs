using System.Windows;
using System.Windows.Input;
using BaseLayer.Model;
using Prism.Events;
using UI.Event;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private IEventAggregator _eventAggregator;

        public MainWindow(MainViewModel viewModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DataUpdatedEvent>().Subscribe(OnDataUpdatedEvent);
            //Loaded += MainWindow_Loaded;
            //TabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        private void OnDataUpdatedEvent(bool updateStarted)
        {
            Application.Current.Dispatcher.Invoke(() => { Mouse.OverrideCursor = updateStarted ? null : Cursors.Wait; });
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
