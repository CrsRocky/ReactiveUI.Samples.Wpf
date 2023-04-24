using ReactiveUI.Samples.Wpf.Common;

namespace ReactiveUI.Samples.Wpf.Services
{
    public class RoutableViewModelServices
    {
        public IRoutableViewModel GetRouteableViewModelByName(string pageName)
        {
            if (RoutedPageNames.PageNameViewModels.ContainsKey(pageName))
                return RoutedPageNames.PageNameViewModels[pageName].Invoke();
            else
                return default;
        }
    }
}