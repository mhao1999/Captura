﻿using Captura.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Reactive.Bindings;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RecentViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<IRecentItem> Items { get; }

        public ICommand ClearCommand { get; }

        public RecentViewModel(Settings Settings,
            IRecentList Recent)
            : base(Settings)
        {
            Items = Recent.Items;

            ClearCommand = new ReactiveCommand()
                .WithSubscribe(Recent.Clear);
        }
    }
}