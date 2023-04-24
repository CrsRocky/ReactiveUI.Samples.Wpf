using ReactiveUI.Samples.Wpf.Extensions;
using ReactiveUI.Samples.Wpf.Interactions;
using ReactiveUI.Samples.Wpf.Models;
using ReactiveUI.Samples.Wpf.Services.Sqlite;
using Serilog;
using Splat;
using Splat.Serilog;
using System.Reactive;
using System.Reflection;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Services
{
    public static class AppRegisterService
    {
        public static void AddDataPersistence(this Application app)
        {
            var autoSuspendHelper = new AutoSuspendHelper(app);
            Locator.CurrentMutable.RegisterConstant(autoSuspendHelper);

            var driver = new NewtonsoftJsonSuspensionDriver("appstate.json");
            Locator.CurrentMutable.RegisterConstant(driver, typeof(ISuspensionDriver));

            RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(driver);
        }

        public static void AddServices(this Application app)
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.Register(() => new RoutableViewModelServices());

            var appState = RxApp.SuspensionHost.GetAppState<AppState>();
            if (appState == null)
            {
                appState = new AppState();
                RxApp.SuspensionHost.AppState = appState;
            }
            Locator.CurrentMutable.RegisterConstant(appState);
            Locator.CurrentMutable.RegisterConstant(appState.MainViewModel);
            Locator.CurrentMutable.RegisterConstant(appState.NavigateViewModel);
            Locator.CurrentMutable.RegisterConstant(appState.DataContractViewModel);
            Locator.CurrentMutable.RegisterConstant(appState.ExceptionViewModel);
            Locator.CurrentMutable.RegisterConstant(appState.DapperViewModel);
        }

        public static void AddSerialLog(this Application app)
        {
            //.Filter.ByExcluding(Matching.FromSource("ReactiveUI.POCOObservableForProperty"))//过滤
            //.Filter.ByExcluding(x => x.Properties.TryGetValue("SourceContext", out var sc) && sc.ToString().Contains("ReactiveUI.POCOObservableForProperty"))
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.With(new CallerEnricher(includeFileInfo: true, maxDepth: 1))
#if DEBUG
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [ThreadId:{ThreadId}] [{SourceContext}]{NewLine}[at {Caller}]{NewLine}Message:{Message:lj}{NewLine}{Exception}")
#else
                .MinimumLevel.Override("ReactiveUI.POCOObservableForProperty", LogEventLevel.Error)//设置等级Error以下
                .WriteTo.File(path: "Logs\\log-.txt",
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [ThreadId:{ThreadId}] [{SourceContext}]{NewLine}[at {Caller}]{NewLine}Message:{Message:lj}{NewLine}{Exception}",
                            rollingInterval: RollingInterval.Day,
                            restrictedToMinimumLevel: LogEventLevel.Information)
#endif
                .CreateLogger();

            Locator.CurrentMutable.UseSerilogFullLogger(Log.Logger);
        }

        public static void AddExceptionHandle(this Application app)
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ExceptionHandleService());
            RxApp.DefaultExceptionHandler = Locator.Current.GetService<ExceptionHandleService>();
        }

        public static void AddInteractions(this Application app)
        {
            var interactions = new MessageInteractions();
            interactions.ShowMessage.RegisterHandler(x =>
            {
                MessageBox.Show(x.Input, "ShowMessage");
                x.SetOutput(Unit.Default);
            });
            interactions.ShowDialog.RegisterHandler(x =>
            {
                var result = MessageBox.Show(x.Input,
                    "Confirmation required",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);
                x.SetOutput(result);
            });
            Locator.CurrentMutable.RegisterConstant(interactions);
        }

        public static void AddSqlite(this Application app)
        {
            var boot = new DatabaseBootstrap();
            boot.Build();
            Locator.CurrentMutable.RegisterConstant<IDatabaseBootstrap>(boot);
        }
    }
}