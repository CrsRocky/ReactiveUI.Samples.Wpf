using ReactiveUI.Samples.Wpf.ViewModels;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// NavigateView.xaml 的交互逻辑
    /// </summary>
    public partial class NavigateView : IViewFor<NavigateViewModel>
    {
        public NavigateView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Router,
                    v => v.NavigateHost.Router);

                this.BindCommand(ViewModel,
                    vm => vm.GoBackCommand,
                    v => v.BackButton);

                this.BindCommand(ViewModel,
                    vm => vm.GoNextCommand,
                    v => v.NextButton);

                this.BindCommand(ViewModel,
                    vm => vm.NavigateResetCommand,
                    v => v.ResetButton);
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
            typeof(NavigateViewModel),
            typeof(NavigateView),
            new PropertyMetadata(null));

        public NavigateViewModel ViewModel
        {
            get
            {
                return (NavigateViewModel)GetValue(ViewModelProperty);
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
                ViewModel = (NavigateViewModel)value;
            }
        }
    }
}