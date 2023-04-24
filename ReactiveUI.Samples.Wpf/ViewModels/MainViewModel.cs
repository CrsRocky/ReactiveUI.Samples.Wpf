﻿using DynamicData;
using ReactiveUI.Samples.Wpf.Common;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.Samples.Wpf.ViewModels
{
    [DataContract]
    public class MainViewModel : ReactiveObject, IScreen, IEnableLogger
    {
        private string title = "Reactive UI Samples Of Wpf!";
        private readonly ReadOnlyObservableCollection<MenuBarViewModel> menus;
        private RoutingState _router = new RoutingState();

        [DataMember]
        public string Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        public ReadOnlyObservableCollection<MenuBarViewModel> Menus => menus;
        public SourceList<MenuBarViewModel> MenuSource { get; private set; }

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public MainViewModel()
        {
            MenuSource = new SourceList<MenuBarViewModel>();
            MenuSource.Add(new MenuBarViewModel(this) { PageName = RoutedPageNames.NavigateViewName });
            MenuSource.Add(new MenuBarViewModel(this) { PageName = RoutedPageNames.DataContractViewName });
            MenuSource.Add(new MenuBarViewModel(this) { PageName = RoutedPageNames.ExceptionViewName });
            MenuSource.Connect()
                .Bind(out menus)
                .Subscribe();
        }
    }
}