using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Models
{
    public class MessageBoxInputModel : ReactiveObject
    {
        private string title = string.Empty;
        private string pageName = string.Empty;

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
    }
}
