using ReactiveUI.Samples.Wpf.Extensions;
using ReactiveUI.Samples.Wpf.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveUI.Samples.Wpf.Services
{
    public class MainRoutableServices
    {
        private readonly Dictionary<string, Func<IRoutableViewModel>> pageNameViewModels = new()
        {
             { NavigateViewName, () => Locator.Current.GetService(typeof(NavigateViewModel)) as IRoutableViewModel },
             { DataContractViewName, () => Locator.Current.GetService(typeof(DataContractViewModel)) as IRoutableViewModel },
             { ExceptionViewName, () => Locator.Current.GetService(typeof(ExceptionViewModel)) as IRoutableViewModel },
             { DapperViewName, () => Locator.Current.GetService(typeof(DapperViewModel)) as IRoutableViewModel },
        };

        public const string NavigateViewName = "NavigatePage";
        public const string DataContractViewName = "DataContractView";
        public const string ExceptionViewName = "ExceptionView";
        public const string DapperViewName = "DapperView";

        public List<string> PageNames => pageNameViewModels.Keys.ToList();

        public IRoutableViewModel GetRouteableViewModel(string pageName)
        {
            if (pageNameViewModels.ContainsKey(pageName))
                return pageNameViewModels[pageName].Invoke();
            else
                return default;
        }

        public string GetNextPageName(string pageName) => pageNameViewModels.NextKey(pageName);

        public IRoutableViewModel GetNextRoutableViewModel(string pageName) => GetRouteableViewModel(GetNextPageName(pageName));
    }
}