using ReactiveUI.Samples.Wpf.Services;
using ReactiveUI.Samples.Wpf.Services.Interactions;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    [DataContract]
    public class ExceptionViewModel : ReactiveObject, IRoutableViewModel
    {
        private bool isHandleException;
        private IDisposable exceptionSub;
        private readonly MessageInteractionServices messageInteractions;
        private string dialogResult;

        public string UrlPathSegment => MainRoutableServices.ExceptionViewName;

        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> ExceptionCommand { get; private set; }

        [DataMember]
        public bool IsHandleException
        {
            get => isHandleException;
            set => this.RaiseAndSetIfChanged(ref isHandleException, value);
        }

        public ReactiveCommand<Unit, Unit> ShowMessageCommand { get; }

        public ReactiveCommand<Unit, Unit> ShowDialogCommand { get; }

        public string DialogResult
        {
            get => dialogResult;
            set => this.RaiseAndSetIfChanged(ref dialogResult, value);
        }

        public ExceptionViewModel()
        {
            HostScreen = Locator.Current.GetService<MainViewModel>();
            messageInteractions = Locator.Current.GetService<MessageInteractionServices>();

            MessageBus.Current.Listen<string>()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => MessageBox.Show($"MessageBus Test.Receive by ExceptionViewModel:{x}"));

            this.WhenAnyValue(x => x.IsHandleException).Subscribe(x =>
            {
                if (x)
                {
                    exceptionSub = ExceptionCommand.ThrownExceptions.Subscribe(error =>
                    {
                        MessageBox.Show($"异常已处理,不会被记录在日志中.Messages:{error.Message}");
                    });
                }
                else
                {
                    exceptionSub?.Dispose();
                }
            });

            ExceptionCommand = ReactiveCommand.Create(() =>
            {
                throw new ArgumentException("cat unhappy.happy happy happy!");
            });

            ShowMessageCommand = ReactiveCommand.Create(() =>
            {
                //Interactions.Errors.Handle() returns a cold observable, i.e.
                //it doesn't actually do anything until you subscribe to it.
                messageInteractions.ShowMessageDialog.Handle($"{Guid.NewGuid()}").Subscribe();
            });

            ShowDialogCommand = ReactiveCommand.Create(() =>
            {
                messageInteractions.ShowDialog.Handle("Are you sure?")
                    .Subscribe(r => DialogResult = r.ToString());
            });
        }
    }
}