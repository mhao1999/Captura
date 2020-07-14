﻿using System.Reactive.Linq;
using System.Windows;
using Captura.Models;
using Captura.Video;
using Captura.Webcam;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ViewConditionsModel
    {
        public ViewConditionsModel(VideoSourcesViewModel VideoSourcesViewModel,
            VideoWritersViewModel VideoWritersViewModel,
            Settings Settings,
            RecordingModel RecordingModel,
            AudioSourceViewModel AudioSourceViewModel)
        {
            IsRegionMode = VideoSourcesViewModel
                .ObserveProperty(M => M.SelectedVideoSourceKind)
                .Select(M => M is RegionSourceProvider)
                .ToReadOnlyReactivePropertySlim();

            MultipleVideoWriters = VideoWritersViewModel.AvailableVideoWriters
                .ObserveProperty(M => M.Count)
                .Select(M => M > 1)
                .ToReadOnlyReactivePropertySlim();

            IsVideoQuality = VideoWritersViewModel
                .ObserveProperty(M => M.SelectedVideoWriterKind)
                .Select(M => M is DiscardWriterProvider)
                .Select(M => !M)
                .ToReadOnlyReactivePropertySlim();

            IsEnabled = RecordingModel
                .ObserveProperty(M => M.RecorderState)
                .Select(M => M == RecorderState.NotRecording)
                .ToReadOnlyReactivePropertySlim();

            ShowSourceNameBox = VideoSourcesViewModel
                .ObserveProperty(M => M.SelectedVideoSourceKind)
                .Select(M => M is RegionSourceProvider)
                .Select(M => !M)
                .ToReadOnlyReactivePropertySlim();

        }

        public IReadOnlyReactiveProperty<bool> StepsBtnEnabled { get; }

        public IReadOnlyReactiveProperty<bool> IsNotAudioOrStepsMode { get; }

        public IReadOnlyReactiveProperty<bool> IsRegionMode { get; }

        public IReadOnlyReactiveProperty<bool> IsAudioMode { get; }

        public IReadOnlyReactiveProperty<bool> MultipleVideoWriters { get; }

        public IReadOnlyReactiveProperty<bool> IsVideoQuality { get; }

        public IReadOnlyReactiveProperty<bool> IsEnabled { get; }

        public IReadOnlyReactiveProperty<bool> ShowSourceNameBox { get; }

        public IReadOnlyReactiveProperty<Visibility> FpsVisibility { get; }
    }
}