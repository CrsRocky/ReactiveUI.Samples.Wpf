using System.Reactive;
using System.Reactive.Concurrency;
using System.Windows;

namespace ReactiveUI.Samples.Wpf.Interactions
{
    public class MessageInteractions
    {
        public Interaction<string, Unit> ShowMessage { get; private set; }

        public Interaction<string, MessageBoxResult> ShowDialog { get; private set; }

        public MessageInteractions()
        {
            ShowMessage = new Interaction<string, Unit>(Scheduler.Default);
            ShowDialog = new Interaction<string, MessageBoxResult>();
        }
    }
}