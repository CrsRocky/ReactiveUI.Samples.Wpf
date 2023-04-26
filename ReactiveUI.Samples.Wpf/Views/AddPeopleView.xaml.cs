using ReactiveUI.Samples.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// AddPeopleView.xaml 的交互逻辑
    /// </summary>
    public partial class AddPeopleView : UserControl, IViewFor<AddPeopleViewModel>
    {
        public AddPeopleView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, vm => vm.OkCommand, v => v.OkButton).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.CancelButton).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.People.Name, v => v.NameTextBox.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.People.Age, v => v.AgeTextBox.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.People.Sex, v => v.SexTextBox.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.People.Phone, v => v.PhoneTextBox.Text).DisposeWith(d);
            });
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(AddPeopleViewModel),
                typeof(AddPeopleView),
                new PropertyMetadata(default(AddPeopleViewModel)));

        public AddPeopleViewModel ViewModel
        {
            get => (AddPeopleViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AddPeopleViewModel)value;
        }
    }
}
