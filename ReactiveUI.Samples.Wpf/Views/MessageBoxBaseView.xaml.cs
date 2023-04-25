using ReactiveUI.Samples.Wpf.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                    vm => vm.Title, 
                    v => v.Title)
                    .DisposeWith(d);

                this.BindCommand(ViewModel, 
                    vm => vm.OkCommand, 
                    v => v.OkButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel, 
                    vm => vm.CancelCommand,
                    v => v.CancelButton)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                    vm => vm.Router,
                    v => v.DialogViewHost.Router)
                    .DisposeWith(d);

                this.WhenAnyValue(v => v.ViewModel.DialogResult)
                    .Where(x => x == "OK" || x == "Cancel")
                    .Subscribe(x => this.Close())
                    .DisposeWith(d);
            });
            ViewModel = Locator.Current.GetService<MessageBoxBaseViewModel>();
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
