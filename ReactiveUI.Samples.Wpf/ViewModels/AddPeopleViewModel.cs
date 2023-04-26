using AutoMapper;
using ReactiveUI.Samples.Wpf.Dtos;
using ReactiveUI.Samples.Wpf.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class AddPeopleViewModel : ReactiveObject, IRoutableViewModel
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

        public AddPeopleViewModel()
        {
            var host = Locator.Current.GetService<MessageBoxBaseViewModel>();
            HostScreen = host;
            mapper = Locator.Current.GetService<IMapper>();
            var canExecute = this.WhenAnyValue(x => x.People.Name,
                x => x.People.Age,
                x => x.People.Sex,
                x => x.People.Phone,
                (name, age, sex, phone) =>
                !string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(age.ToString()) &&
                !string.IsNullOrEmpty(sex) &&
                !string.IsNullOrEmpty(phone));
            OkCommand = ReactiveCommand.Create(() =>
            {
                host.Output.Result = mapper.Map<PeopleModel>(People);
                host.DialogResult = "complete";
            }, canExecute);
            CancelCommand = ReactiveCommand.Create(() => {
                host.DialogResult = "complete"; });
        }
    }
}
