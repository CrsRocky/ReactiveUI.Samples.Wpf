using ReactiveUI.Samples.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
