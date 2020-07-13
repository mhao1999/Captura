
namespace Captura.Video
{
    public abstract class VideoSourceProviderBase : NotifyPropertyChanged, IVideoSourceProvider
    {

        protected VideoSourceProviderBase()
        {

        }

        public virtual bool SupportsStepsMode => true;

        public abstract IVideoItem Source { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Icon { get; }

        public abstract IBitmapImage Capture(bool IncludeCursor);

        public virtual bool OnSelect() => true;

        public virtual void OnUnselect() { }

        public virtual string Serialize()
        {
            return Source.ToString();
        }

        public abstract bool Deserialize(string Serialized);

        public abstract bool ParseCli(string Arg);
    }
}