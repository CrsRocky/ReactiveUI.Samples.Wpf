using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    [DataContract]
    public class DataContractViewModel : ReactiveObject, IRoutableViewModel
    {
        private string name;

        private string password;

        private readonly ISuspensionDriver driver;

        public string UrlPathSegment => RoutableViewModelServices.DataContractViewName;

        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> SendMessage { get; }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public ReactiveCommand<string, Unit> AppStateCommand { get; }

        [DataMember]
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        [DataMember]
        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }

        public DataContractViewModel()
        {
            HostScreen = Locator.Current.GetService<MainViewModel>();
            driver = Locator.Current.GetService<ISuspensionDriver>();

            SendMessage = ReactiveCommand.Create(() =>
            {
                MessageBus.Current.SendMessage("This message send form DataContractView!");
            });

            LoginCommand = ReactiveCommand.CreateFromTask(() => Task.Delay(1000),
                this.WhenAnyValue(x => x.Name,
                        x => x.Password,
                        (n, p) => !string.IsNullOrWhiteSpace(n) && !string.IsNullOrWhiteSpace(p)));

            AppStateCommand = ReactiveCommand.CreateFromTask<string>(async x =>
            {
                switch (x)
                {
                    case "LoadState":
                        var result = await driver.LoadState() as AppState;
                        break;

                    case "SaveState":
                        await driver.SaveState(Locator.Current.GetService<AppState>());
                        break;

                    case "InvalidateState":
                        await driver.InvalidateState();
                        break;
                }
            });
        }
    }
}