using AutoMapper;
using ReactiveUI.Samples.Wpf.Dtos;
using ReactiveUI.Samples.Wpf.Models;
using Splat;
using System;
using System.Reactive;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class PeopleViewModel : ReactiveObject, IRoutableViewModel
    {
        private PeopleDto people = new();

        private readonly IMapper mapper;

        public string UrlPathSegment => "AddPeopleView";

        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> OkCommand { get; internal set; }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; internal set; }

        public PeopleDto People
        {
            get => people;
            set => this.RaiseAndSetIfChanged(ref people, value);
        }

        public PeopleViewModel()
        {
            mapper = Locator.Current.GetService<IMapper>();
            var host = Locator.Current.GetService<MessageBoxBaseViewModel>();
            HostScreen = host;
            if (host.Input.Parameter is PeopleModel model)
                People = mapper.Map<PeopleDto>(model);

            OkCommand = ReactiveCommand.Create(() =>
            {
                var peopleModel = mapper.Map<PeopleModel>(People);
                if (host.Input.Parameter is PeopleModel model)
                    peopleModel.Id = model.Id;
                host.Output.Result = peopleModel;
                host.Output.DialogResult = "confirm";
            }, 
            this.WhenAnyValue(x => x.People.Name,
                x => x.People.Age,
                x => x.People.Sex,
                x => x.People.Phone,
                (name, age, sex, phone) =>
                !string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(age.ToString()) &&
                !string.IsNullOrEmpty(sex) &&
                !string.IsNullOrEmpty(phone)));

            CancelCommand = ReactiveCommand.Create(() =>
            {
                host.Output.Result = null;
                host.Output.DialogResult = "cancel";
            });
        }
    }
}