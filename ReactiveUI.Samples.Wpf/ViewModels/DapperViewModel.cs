using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class DapperViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => RoutableViewModelServices.DapperViewName;

        public IScreen HostScreen { get; private set; }

        public DapperViewModel()
        {
            HostScreen = Locator.Current.GetService<MainViewModel>();
        }
    }
}