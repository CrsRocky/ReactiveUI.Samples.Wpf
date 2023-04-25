using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    [DataContract]
    public class MenuBarViewModel : ReactiveObject
    {
        private readonly IScreen _screen;
        public string PageName { get; set; }

        public ReactiveCommand<Unit, Unit> RoutedCommand { get; }

        public MenuBarViewModel(IScreen screen)
        {
            _screen = screen;
            var rs = Locator.Current.GetService<MainRoutableServices>();
            RoutedCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (rs.PageNames.Contains(PageName))
                    await _screen.Router.Navigate.Execute(rs.GetRouteableViewModel(PageName));
            });
        }
    }
}