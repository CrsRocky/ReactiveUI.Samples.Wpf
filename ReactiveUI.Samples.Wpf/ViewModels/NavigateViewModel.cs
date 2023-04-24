using ReactiveUI.Fody.Helpers;
using ReactiveUI.Samples.Wpf.Common;
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

        public string UrlPathSegment => RoutedPageNames.NavigateViewName;

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
                if (Router.NavigationStack.Count > 0)
                {
                    var pageName = Router.GetCurrentViewModel().UrlPathSegment;
                    var nextPageName = RoutedPageNames.PageNameViewModels.NextKey(pageName);
                    if (!string.IsNullOrEmpty(nextPageName))
                        await Router.Navigate.Execute(Locator.Current.GetService<RoutableViewModelServices>()
                            .GetRouteableViewModelByName(nextPageName));

                    var LastPageName = RoutedPageNames.PageNameViewModels.Last().Key;
                    CanGoNext = LastPageName != nextPageName;
                }
                else
                {
                    await Router.Navigate.Execute(Locator.Current.GetService<RoutableViewModelServices>()
                        .GetRouteableViewModelByName(RoutedPageNames.DataContractViewName));
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
                await Router.NavigateAndReset.Execute(Locator.Current.GetService<RoutableViewModelServices>()
                        .GetRouteableViewModelByName(RoutedPageNames.DataContractViewName));
                CanGoNext = true;
                this.Log().Info("Navigate Stack Reset!");
            });
        }
    }
}