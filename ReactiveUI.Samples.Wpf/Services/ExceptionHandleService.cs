using Splat;
using System;
using System.Diagnostics;

namespace ReactiveUI.Samples.Wpf.Services
{
    public class ExceptionHandleService : IObserver<Exception>, IEnableLogger
    {
        public void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        public void OnError(Exception error)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            this.Log().Error(error, "error exception.");
        }

        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            this.Log().Fatal(value, "unhandled exception.");
        }
    }
}