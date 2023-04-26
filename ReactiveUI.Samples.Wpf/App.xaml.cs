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
            this.AddExceptionHandle();//添加异常处理
            this.AddSerialLog();//添加日志
            this.AddInteractions();//添加对话框交互
            this.AddDataPersistence();//添加数据持久化
            this.AddSqlite();//添加Sqlite
            this.AddMapper();
            this.AddServices();//添加依赖注入
        }
    }
}