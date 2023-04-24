using ReactiveUI.Samples.Wpf.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveUI.Samples.Wpf.Common
{
    public class RoutedPageNames
    {
        public static string NavigateViewName => "NavigatePage";
        public static string DataContractViewName => "DataContractView";
        public static string ExceptionViewName => "ExceptionView";

        public static List<string> PageNames => PageNameViewModels.Keys.ToList();

        public static readonly Dictionary<string, Func<IRoutableViewModel>> PageNameViewModels = new()
        {
             { NavigateViewName, () => Locator.Current.GetService(typeof(NavigateViewModel)) as IRoutableViewModel },
             { DataContractViewName, () => Locator.Current.GetService(typeof(DataContractViewModel)) as IRoutableViewModel },
             { ExceptionViewName, () => Locator.Current.GetService(typeof(ExceptionViewModel)) as IRoutableViewModel },
        };
    }
}