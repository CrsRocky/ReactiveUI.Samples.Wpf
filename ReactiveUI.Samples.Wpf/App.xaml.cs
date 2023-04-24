namespace ReactiveUI.Samples.Wpf
{
    using ReactiveUI.Samples.Wpf.Services;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppRegisterService.AddExceptionHandle();//添加异常处理
            AppRegisterService.AddSerialLog();//添加日志
            AppRegisterService.AddInteractions();//添加对话框交互
            AppRegisterService.AddDataPersistence(this);//添加数据持久化
            AppRegisterService.AddServices();//添加依赖注入
        }
    }
}