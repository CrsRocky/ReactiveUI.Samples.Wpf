using ReactiveUI.Samples.Wpf.ViewModels;
using System.Runtime.Serialization;

namespace ReactiveUI.Samples.Wpf.Models
{
    [DataContract]
    public class AppState
    {
        private MainViewModel mainViewModel;
        private NavigateViewModel navigateViewModel;
        private DataContractViewModel dataContractViewModel;
        private ExceptionViewModel exceptionViewModel;

        [DataMember]
        public MainViewModel MainViewModel
        {
            get => mainViewModel ??= new MainViewModel();
            set => mainViewModel = value;
        }

        [DataMember]
        public NavigateViewModel NavigateViewModel
        {
            get => navigateViewModel ??= new NavigateViewModel();
            set => navigateViewModel = value;
        }

        [DataMember]
        public DataContractViewModel DataContractViewModel
        {
            get => dataContractViewModel ??= new DataContractViewModel();
            set => dataContractViewModel = value;
        }

        [DataMember]
        public ExceptionViewModel ExceptionViewModel
        {
            get => exceptionViewModel ??= new ExceptionViewModel();
            set => exceptionViewModel = value;
        }
    }
}