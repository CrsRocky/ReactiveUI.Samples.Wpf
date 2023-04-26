using AutoMapper;
using ReactiveUI;
using ReactiveUI.Samples.Wpf.Dtos;
using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class MessageBoxBaseViewModel : ReactiveObject, IScreen, IEnableLogger
    {
        private string dialogResult = string.Empty;
        private string title = string.Empty;

        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        public string DialogResult
        {
            get => dialogResult;
            set => this.RaiseAndSetIfChanged(ref dialogResult, value);
        }

        public RoutingState Router { get; }

        public MessageBoxInputModel Input { get; } = new();

        public MessageBoxOutputModel Output { get; } = new();

        public MessageBoxBaseViewModel()
        {
            Router = new RoutingState();

            this.WhenAnyValue(x => x.Input.Title)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Subscribe(x => Title = x);

            this.WhenAnyValue(x => x.Input.PageName)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Subscribe(x => Naviagation(x));
        }

        void Naviagation(string name)
        {
            var ms = Locator.Current.GetService<MessageBoxServices>();
            switch (name)
            {
                case MessageBoxServices.AddPeopleViewName:
                    Router.Navigate.Execute(ms.GetRouteableViewModel(MessageBoxServices.AddPeopleViewName));
                    break;
            }
        }
    }
}
