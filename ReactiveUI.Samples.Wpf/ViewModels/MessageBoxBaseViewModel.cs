using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class MessageBoxBaseViewModel : ReactiveObject, IScreen, IEnableLogger
    {
        public RoutingState Router { get; }

        public MessageBoxInputModel Input { get; } = new();

        public MessageBoxOutputModel Output { get; }= new();

        public MessageBoxBaseViewModel()
        {
            Router = new RoutingState();

            this.WhenAnyValue(x => x.Input.PageName)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Subscribe(async x => await Naviagation(x));
        }

        async Task Naviagation(string name)
        {
            var ms = Locator.Current.GetService<MessageBoxServices>();
            switch (name)
            {
                case MessageBoxServices.AddPeopleViewName:
                case MessageBoxServices.UpdatePeopleViewName:
                    await Router.Navigate.Execute(ms.GetRouteableViewModel(name));
                    break;
            }
        }

        internal void Reset()
        {
            Input.Title = string.Empty;
            Input.PageName = string.Empty;
            Output.DialogResult = string.Empty;
        }
    }
}