using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AboutViewModel : ViewModelBase
    {
        public static Version Version { get; }

        public string AppVersion { get; }

        static AboutViewModel()
        {
            Version = ServiceProvider.AppVersion;
        }

        public AboutViewModel(Settings Settings) : base(Settings)
        {
            AppVersion = "v" + Version.ToString(3);

        }
    }
}
