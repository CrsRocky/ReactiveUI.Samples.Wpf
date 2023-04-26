using DynamicData;
using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services;
using ReactiveUI.Samples.Wpf.Services.Interactions;
using ReactiveUI.Samples.Wpf.Services.Sqlite;
using ReactiveUI.Samples.Wpf.Views;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class DapperViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IPeopleServices peopleServices;

        private ReadOnlyObservableCollection<PeopleModel> peopleModels;

        public ReadOnlyObservableCollection<PeopleModel> PeopleModels => peopleModels;

        public SourceList<PeopleModel> PeopleModelSource { get; private set; }

        public string UrlPathSegment => MainRoutableServices.DapperViewName;

        public IScreen HostScreen { get; private set; }

        public ReactiveCommand<Unit, Unit> AddButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> DelectButtonCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> SearchAllButtonCommand { get; private set; }

        public ReactiveCommand<string, Unit> SearchByIdButtonCommand { get; private set; }

        public ReactiveCommand<string, Unit> UpDateButtonCommand { get; private set; }

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
                var ms = Locator.Current.GetService<MessageServices>();
                ms.AddPeopleDialog.Handle(Unit.Default)
                .Where(x => x != null)
                .Subscribe(async x =>
                {
                    await peopleServices.AddAsync(x);
                });
            });

            SearchAllButtonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await peopleServices.GetAllAsync();
                PeopleModelSource.Clear();
                PeopleModelSource.AddRange(result);
            });
        }
    }
}