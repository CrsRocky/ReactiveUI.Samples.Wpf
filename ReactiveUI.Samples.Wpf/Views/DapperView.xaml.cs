using ReactiveUI.Samples.Wpf.ViewModels;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ReactiveUI.Samples.Wpf.Views
{
    /// <summary>
    /// DapperView.xaml 的交互逻辑
    /// </summary>
    public partial class DapperView : UserControl, IViewFor<DapperViewModel>
    {
        public DapperView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.BindCommand(ViewModel,
                    vm => vm.AddButtonCommand,
                    v => v.AddButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel,
                    vm => vm.DelectButtonCommand,
                    v => v.DelectButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel,
                    vm => vm.SearchAllButtonCommand,
                    v => v.SearchAllButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel,
                    vm => vm.SearchByIdButtonCommand,
                    v => v.SearchByIdButton)
                    .DisposeWith(d);

                this.BindCommand(ViewModel,
                    vm => vm.UpDateButtonCommand,
                    v => v.UpDateButton)
                    .DisposeWith(d);

                this.OneWayBind(ViewModel,
                    vm => vm.PeopleModels,
                    v => v.DetailsDataGrid.ItemsSource)
                    .DisposeWith(d);

                //one way to source
                this.WhenAnyValue(v => v.DetailsDataGrid.SelectedItem)
                    .Where(x => x != null)
                    .BindTo(this, v => v.ViewModel.SelectPeople)
                    .DisposeWith(d);

                this.WhenAnyValue(v => v.IdTextBox.Text)
                    .BindTo(this, v => v.ViewModel.SearchId)
                    .DisposeWith(d);
            });
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(DapperViewModel),
                typeof(DapperView),
                new PropertyMetadata(default(DapperViewModel)));

        public DapperViewModel ViewModel
        {
            get => (DapperViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DapperViewModel)value;
        }
    }
}