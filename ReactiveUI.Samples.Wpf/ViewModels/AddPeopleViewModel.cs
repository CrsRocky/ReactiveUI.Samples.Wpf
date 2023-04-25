using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class AddPeopleViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "AddPeopleView";

        public IScreen HostScreen { get; }

        public AddPeopleViewModel()
        {
            HostScreen = Locator.Current.GetService<MessageBoxBaseViewModel>();
        }
    }
}
