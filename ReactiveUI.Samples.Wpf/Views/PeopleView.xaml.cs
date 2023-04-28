using ReactiveUI.Samples.Wpf.ViewModels;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// AddPeopleView.xaml 的交互逻辑
    /// </summary>
    public partial class PeopleView : UserControl, IViewFor<PeopleViewModel>
    {
        public PeopleView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel, 
                    vm => vm.OkCommand,
                    v => v.OkButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel, 
                    vm => vm.CancelCommand, 
                    v => v.CancelButton)
                    .DisposeWith(d);

                this.Bind(ViewModel, 
                    vm => vm.People.Name, 
                    v => v.NameTextBox.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel, 
                    vm => vm.People.Age, 
                    v => v.AgeTextBox.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel, 
                    vm => vm.People.Sex, 
                    v => v.SexTextBox.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel, 
                    vm => vm.People.Phone, 
                    v => v.PhoneTextBox.Text)
                    .DisposeWith(d);
            });
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(PeopleViewModel),
                typeof(PeopleView),
                new PropertyMetadata(default(PeopleViewModel)));

        public PeopleViewModel ViewModel
        {
            get => (PeopleViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (PeopleViewModel)value;
        }
    }
}