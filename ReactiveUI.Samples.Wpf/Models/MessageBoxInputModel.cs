namespace ReactiveUI.Samples.Wpf.Models
{
    public class MessageBoxInputModel : ReactiveObject
    {
        private string title = string.Empty;
        private string pageName = string.Empty;
        private object parameter = new();

        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        public string PageName
        {
            get => pageName;
            set => this.RaiseAndSetIfChanged(ref pageName, value);
        }

        public object Parameter
        {
            get => parameter;
            set => this.RaiseAndSetIfChanged(ref parameter, value);
        }
    }
}