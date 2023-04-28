using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.ViewModels;
using ReactiveUI.Samples.Wpf.Views;
using Splat;
using System;
using System.Reactive;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Services.Interactions
{
    public class MessageInteractionServices
    {
        public Interaction<string, Unit> ShowMessageDialog { get; private set; }

        public Interaction<string, MessageBoxResult> ShowDialog { get; private set; }

        public Interaction<Unit, PeopleModel> AddPeopleDialog { get; private set; }

        public Interaction<PeopleModel, PeopleModel> UpdatePeopleDialog { get; private set; }

        public MessageInteractionServices()
        {
            Init();
            Register();
        }

        private void Init()
        {
            ShowMessageDialog = new Interaction<string, Unit>();
            ShowDialog = new Interaction<string, MessageBoxResult>();
            AddPeopleDialog = new Interaction<Unit, PeopleModel>();
            UpdatePeopleDialog = new Interaction<PeopleModel, PeopleModel>();
        }

        private void Register()
        {
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
                vm.Input.Parameter = x.Input;
                var addView = new MessageBoxBaseView
                {
                    ViewModel = vm
                };
                addView.ShowDialog();
                x.SetOutput(vm.Output.Result as PeopleModel);
            });
            UpdatePeopleDialog.RegisterHandler(x =>
            {
                var vm = Locator.Current.GetService<MessageBoxBaseViewModel>();
                vm.Input.Title = "修改";
                vm.Input.PageName = MessageBoxServices.UpdatePeopleViewName;
                vm.Input.Parameter = x.Input;
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