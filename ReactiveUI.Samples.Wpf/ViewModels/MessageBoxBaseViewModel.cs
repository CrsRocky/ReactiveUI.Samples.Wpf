using ReactiveUI;
using ReactiveUI.Samples.Wpf.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    public class MessageBoxBaseViewModel : ReactiveObject, IScreen, IEnableLogger
    {
        private string title;

        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        private string dialogResult;
        public string DialogResult
        {
            get => dialogResult;
            set => this.RaiseAndSetIfChanged(ref dialogResult, value);
        }


        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> OkCommand { get; }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public object OutputResult { get; private set; } = new ();

        public MessageBoxBaseViewModel()
        {
            Router = new RoutingState();
            OkCommand = ReactiveCommand.Create(() => 
            {
                DialogResult = "Ok";
            });
            CancelCommand = ReactiveCommand.Create(() => 
            {
                DialogResult = "Cancel";
            });
            Naviagation();
        }

        void Naviagation()
        {
            var ms = Locator.Current.GetService<MessageBoxServices>();
            switch (Title)
            {
                case MessageBoxServices.AddPeopleViewName:
                    Router.Navigate.Execute(ms.GetRouteableViewModel(MessageBoxServices.AddPeopleViewName));
                    break;
            }

            //RoutedCommand = ReactiveCommand.CreateFromTask(async () =>
            //{
            //    if (rs.PageNames.Contains(PageName))
            //        await _screen.Router.Navigate.Execute(rs.GetRouteableViewModel(PageName));
            //});
        }

    }
}
