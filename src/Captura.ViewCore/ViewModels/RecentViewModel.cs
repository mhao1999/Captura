using Captura.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Reactive.Bindings;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RecentViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<IRecentItem> Items { get; }

        public RecentViewModel(Settings Settings,
            IRecentList Recent)
            : base(Settings)
        {
            Items = Recent.Items;

        }
    }
}