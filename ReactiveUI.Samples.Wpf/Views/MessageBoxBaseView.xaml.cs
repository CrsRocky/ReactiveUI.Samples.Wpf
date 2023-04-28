using ReactiveUI.Samples.Wpf.ViewModels;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// AddPeopleView.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxBaseView : Window, IViewFor<MessageBoxBaseViewModel>
    {
        public MessageBoxBaseView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel,
                    vm => vm.Input.Title,
                    v => v.Title)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                    vm => vm.Router,
                    v => v.DialogViewHost.Router)
                    .DisposeWith(d);

                // Subscribe to Cancel, and close the Window when it happens
                //this.WhenAnyObservable(x => x.ViewModel.Cancel)
                //    .Subscribe(_ => this.Close());

                this.WhenAnyValue(v => v.ViewModel.Output.DialogResult)
                    .Where(x => x == "confirm" || x == "cancel")
                    .Subscribe(x => Close())
                    .DisposeWith(d);

                this.Events().Closed
                    .Subscribe(x => ViewModel.Reset())
                    .DisposeWith(d);
            });
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(MessageBoxBaseViewModel),
                typeof(MessageBoxBaseView),
                new PropertyMetadata(default(MessageBoxBaseViewModel)));

        public MessageBoxBaseViewModel ViewModel
        {
            get => (MessageBoxBaseViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MessageBoxBaseViewModel)value;
        }
    }
}