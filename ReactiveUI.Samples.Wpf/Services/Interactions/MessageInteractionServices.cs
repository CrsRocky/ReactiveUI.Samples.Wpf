using Microsoft.VisualBasic;
using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.ViewModels;
using ReactiveUI.Samples.Wpf.Views;
using Splat;
using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Services.Interactions
{
    public class MessageInteractionServices
    {
        public Interaction<string, Unit> ShowMessageDialog { get; private set; }

        public Interaction<string, MessageBoxResult> ShowDialog { get; private set; }

        public Interaction<Unit, PeopleModel> AddPeopleDialog { get; private set; }

        public MessageInteractionServices()
        {
            Register();
        }

        private void Register()
        {
            ShowMessageDialog = new Interaction<string, Unit>();
            ShowDialog = new Interaction<string, MessageBoxResult>();
            AddPeopleDialog = new Interaction<Unit, PeopleModel>();
            ShowMessageDialog.RegisterHandler(x =>
            {
                MessageBox.Show(x.Input, "ShowMessage");
                x.SetOutput(Unit.Default);
            });
            ShowDialog.RegisterHandler(x =>
            {
                var result = MessageBox.Show(x.Input,
                    "Confirmation required",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                x.SetOutput(result);
            });
            AddPeopleDialog.RegisterHandler(x =>
            {
                var vm = Locator.Current.GetService<MessageBoxBaseViewModel>();
                vm.Input.Title = "添加";
                vm.Input.PageName = MessageBoxServices.AddPeopleViewName;
                var addView = new MessageBoxBaseView
                {
                    ViewModel = vm
                };
                addView.ShowDialog();
                x.SetOutput(vm.Output.Result as PeopleModel);
            });
        }
    }
}