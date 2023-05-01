using ReactiveUI.Samples.Wpf.ViewModels;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI.Samples.Wpf.Extensions;
using System;
using System.Reactive.Subjects;
using System.Net;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// PageAView.xaml 的交互逻辑
    /// </summary>
    public partial class DataContractView : UserControl, IViewFor<DataContractViewModel>
    {
        public DataContractView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(ViewModel,
                    vm => vm.SendMessage,
                    v => v.SendMessageButton)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.Name,
                    v => v.NameTextBox.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.LoginCommand,
                    v => v.LoginButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.AppStateCommand,
                    v => v.LoadStateButton,
                    Observable.Return(LoadStateButton.Content.ToString()))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.AppStateCommand,
                    v => v.SaveStateButton,
                    Observable.Return(SaveStateButton.Content.ToString()))
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    vm => vm.AppStateCommand,
                    v => v.InvalidateStateButton,
                    Observable.Return(InvalidateStateButton.Content.ToString()))
                    .DisposeWith(disposable);
            });
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
                typeof(DataContractViewModel),
                typeof(DataContractView),
                new PropertyMetadata(null));

        public DataContractViewModel ViewModel
        {
            get { return (DataContractViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DataContractViewModel)value;
        }
    }
}