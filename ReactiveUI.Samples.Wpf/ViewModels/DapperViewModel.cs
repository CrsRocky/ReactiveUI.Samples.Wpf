using DynamicData;
using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services;
using ReactiveUI.Samples.Wpf.Services.Interactions;
using ReactiveUI.Samples.Wpf.Services.Sqlite;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class DapperViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IPeopleServices peopleServices;

        private ReadOnlyObservableCollection<PeopleModel> peopleModels;

        private PeopleModel selectPoeple;

        private string searchId;

        public ReadOnlyObservableCollection<PeopleModel> PeopleModels => peopleModels;

        public SourceList<PeopleModel> PeopleModelSource { get; private set; }

        public string UrlPathSegment => MainRoutableServices.DapperViewName;

        public IScreen HostScreen { get; private set; }

        public ReactiveCommand<Unit, Unit> AddButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> DelectButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> SearchAllButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> SearchByIdButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> UpDateButtonCommand { get; private set; }

        public PeopleModel SelectPeople
        {
            get => selectPoeple;
            set => this.RaiseAndSetIfChanged(ref selectPoeple, value);
        }

        public string SearchId
        {
            get => searchId;
            set => this.RaiseAndSetIfChanged(ref searchId, value);
        }

        public DapperViewModel()
        {
            HostScreen = Locator.Current.GetService<MainViewModel>();
            peopleServices = Locator.Current.GetService<IPeopleServices>();

            PeopleModelSource = new SourceList<PeopleModel>();
            PeopleModelSource.Connect()
                .Bind(out peopleModels)
                .Subscribe();

            AddButtonCommand = ReactiveCommand.Create(() =>
            {
                var ms = Locator.Current.GetService<MessageInteractionServices>();
                ms.AddPeopleDialog.Handle(Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(x => x != null)
                .Subscribe(async x =>
                {
                    await peopleServices.AddAsync(x);
                    await GetAll();
                });
            });

            SearchAllButtonCommand = ReactiveCommand.CreateFromTask(async () => await GetAll());

            DelectButtonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectPeople != null)
                {
                    await peopleServices.RemoveAsync(SelectPeople.Id);
                    await GetAll();
                }
            });

            SearchByIdButtonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await Search(SearchId);
                return Unit.Default;
            });

            this.WhenAnyValue(x => x.SearchId).Subscribe(async id =>
            {
                if (string.IsNullOrEmpty(id))
                    PeopleModelSource.Clear();
                await Search(id);
            });

            UpDateButtonCommand = ReactiveCommand.Create(() =>
            {
                if (SelectPeople != null)
                {
                    var ms = Locator.Current.GetService<MessageInteractionServices>();
                    ms.UpdatePeopleDialog.Handle(SelectPeople)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Where(x => x != null)
                    .Subscribe(async x =>
                    {
                        await peopleServices.UpDateAsync(x);
                        await GetAll();
                    });
                }
            });
        }

        private async Task GetAll()
        {
            var result = await peopleServices.GetAllAsync();
            PeopleModelSource.Clear();
            PeopleModelSource.AddRange(result);
        }

        private async Task Search(string id)
        {
            var cvt = int.TryParse(id, out int idInt32);
            if (cvt)
            {
                var result = await peopleServices.SearchAsync(idInt32);
                if (result != null)
                {
                    PeopleModelSource.Clear();
                    PeopleModelSource.Add(result);
                }
            }
        }
    }
}