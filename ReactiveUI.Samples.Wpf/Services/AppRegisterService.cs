using ReactiveUI.Samples.Wpf.Extensions;
using ReactiveUI.Samples.Wpf.Interactions;
using ReactiveUI.Samples.Wpf.Models;
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
        public static void AddDataPersistence(Application app)
        {
            var autoSuspendHelper = new AutoSuspendHelper(app);
            Locator.CurrentMutable.RegisterConstant(autoSuspendHelper);

            var driver = new NewtonsoftJsonSuspensionDriver("appstate.json");
            Locator.CurrentMutable.RegisterConstant(driver, typeof(ISuspensionDriver));

            RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(driver);
        }

        public static void AddServices()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.Register(() => new RoutableViewModelServices());


            var app = RxApp.SuspensionHost.GetAppState<AppState>();
            if (app == null)
            {
                app = new AppState();
                RxApp.SuspensionHost.AppState = app;
            }
            Locator.CurrentMutable.RegisterConstant(app);
            Locator.CurrentMutable.RegisterConstant(app.MainViewModel);
            Locator.CurrentMutable.RegisterConstant(app.NavigateViewModel);
            Locator.CurrentMutable.RegisterConstant(app.DataContractViewModel);
            Locator.CurrentMutable.RegisterConstant(app.ExceptionViewModel);
            Locator.CurrentMutable.RegisterConstant(app.DapperViewModel);
        }

        public static void AddSerialLog()
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

        public static void AddExceptionHandle()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ExceptionHandleService());
            RxApp.DefaultExceptionHandler = Locator.Current.GetService<ExceptionHandleService>();
        }

        public static void AddInteractions()
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
    }
}