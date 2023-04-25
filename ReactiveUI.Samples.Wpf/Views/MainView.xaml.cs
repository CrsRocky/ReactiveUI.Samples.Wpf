using ReactiveUI.Samples.Wpf.ViewModels;
using Splat;
using System.Reactive.Disposables;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : IViewFor<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService(typeof(MainViewModel)) as MainViewModel;
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Title,
                    v => v.Title)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.Router,
                    v => v.RoutedViewHost.Router)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Menus,
                    v => v.PagesListBox.ItemsSource)
                    .DisposeWith(disposable);
            });
        }

        public MainViewModel ViewModel
        {
            get => (MainViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                typeof(MainViewModel),
                typeof(MainView));

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }
    }
}