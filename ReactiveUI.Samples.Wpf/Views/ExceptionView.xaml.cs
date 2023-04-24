using ReactiveUI.Samples.Wpf.ViewModels;
using System.Reactive.Disposables;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// PageBView.xaml 的交互逻辑
    /// </summary>
    public partial class ExceptionView : IViewFor<ExceptionViewModel>
    {
        public ExceptionView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.ExceptionCommand,
                    v => v.ThrowExButton.Command)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.IsHandleException,
                    v => v.IsHandleCheckBox.IsChecked)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.DialogResult,
                    v => v.DialogResultTxb.Text)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.ShowMessageCommand,
                    v => v.ShowMessageButton.Command)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.ShowDialogCommand,
                    v => v.ShowDialogButton.Command)
                    .DisposeWith(disposable);
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
             typeof(ExceptionViewModel),
             typeof(ExceptionView),
             new PropertyMetadata());

        public ExceptionViewModel ViewModel
        {
            get { return (ExceptionViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ExceptionViewModel)value;
        }
    }
}