using System.Collections.ObjectModel;
using System.Windows.Input;
using Captura.Hotkeys;
using Reactive.Bindings;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HotkeysViewModel
    {
        public ReadOnlyObservableCollection<Hotkey> Hotkeys { get; }

        public HotkeysViewModel(HotKeyManager HotKeyManager)
        {
            Hotkeys = HotKeyManager.Hotkeys;

        }

    }
}