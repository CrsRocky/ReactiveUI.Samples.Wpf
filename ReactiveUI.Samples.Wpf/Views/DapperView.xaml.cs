using ReactiveUI.Samples.Wpf.ViewModels;
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