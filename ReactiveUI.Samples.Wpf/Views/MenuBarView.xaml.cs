using ReactiveUI.Samples.Wpf.ViewModels;
using System.Reactive.Disposables;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// MenuView.xaml 的交互逻辑
    /// </summary>
    public partial class MenuBarView : IViewFor<MenuBarViewModel>
    {
        public MenuBarView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.PageName,
                    v => v.RoutedButton.Content)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.RoutedCommand,
                    v => v.RoutedButton)
                    .DisposeWith(disposable);
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
           typeof(MenuBarViewModel),
           typeof(MenuBarView),
           new PropertyMetadata(null));

        public MenuBarViewModel ViewModel
        {
            get
            {
                return (MenuBarViewModel)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }

        object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (MenuBarViewModel)value;
            }
        }
    }
}