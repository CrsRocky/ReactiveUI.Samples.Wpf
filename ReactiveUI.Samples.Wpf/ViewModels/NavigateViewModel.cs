using ReactiveUI.Fody.Helpers;
using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    [DataContract]
    public class NavigateViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public ReactiveCommand<Unit, IRoutableViewModel> GoBackCommand => Router.NavigateBack;

        public ReactiveCommand<Unit, Unit> GoNextCommand { get; }

        public ReactiveCommand<Unit, Unit> NavigateResetCommand { get; }

        public string UrlPathSegment => MainRoutableServices.NavigateViewName;

        public IScreen HostScreen { get; }

        public RoutingState Router { get; }

        [Reactive]
        [DataMember]
        public bool CanGoNext { get; set; }

        public NavigateViewModel()
        {
            CanGoNext = true;
            Router = new RoutingState();
            HostScreen = Locator.Current.GetService<MainViewModel>();

            GoNextCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var rs = Locator.Current.GetService<MainRoutableServices>();
                if (Router.NavigationStack.Count > 0)
                {
                    var pageName = Router.GetCurrentViewModel().UrlPathSegment;
                    var nextPageName = rs.GetNextPageName(pageName);
                    if (!string.IsNullOrEmpty(nextPageName))
                        await Router.Navigate.Execute(rs.GetRouteableViewModel(nextPageName));
                    var LastPageName = rs.PageNames.Last();
                    CanGoNext = LastPageName != nextPageName;
                }
                else
                {
                    await Router.Navigate.Execute(rs.GetRouteableViewModel(MainRoutableServices.DataContractViewName));
                    CanGoNext = true;
                }
                this.Log().Info("Navigate Go Next!");
            }, this.WhenAnyValue(x => x.CanGoNext));

            GoBackCommand.Subscribe(x =>
            {
                CanGoNext = true;
                this.Log().Info("Navigate Go Back!");
            });

            NavigateResetCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                // 导航到 NextPage，并重置整个堆栈
                var rs = Locator.Current.GetService<MainRoutableServices>();
                await Router.NavigateAndReset.Execute(rs.GetRouteableViewModel(MainRoutableServices.DataContractViewName));
                CanGoNext = true;
                this.Log().Info("Navigate Stack Reset!");
            });
        }
    }
}