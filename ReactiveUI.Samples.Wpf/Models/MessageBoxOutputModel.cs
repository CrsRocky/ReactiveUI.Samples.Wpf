namespace ReactiveUI.Samples.Wpf.Models
{
    public class MessageBoxOutputModel : ReactiveObject
    {
        private string dialogResult = string.Empty;

        public object Result { get; set; }

        public string DialogResult
        {
            get => dialogResult;
            set => this.RaiseAndSetIfChanged(ref dialogResult, value);
        }
    }
}