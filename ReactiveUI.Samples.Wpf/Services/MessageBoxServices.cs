using ReactiveUI.Samples.Wpf.Extensions;
using ReactiveUI.Samples.Wpf.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services
{
    public class MessageBoxServices
    {
        private readonly Dictionary<string, Func<IRoutableViewModel>> pageNameViewModels = new()
        {
            {
                AddPeopleViewName,
                () => Locator.Current.GetService(typeof(AddPeopleViewModel)) as IRoutableViewModel
            },
        };

        public const string AddPeopleViewName = "AddPeopelView";

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
