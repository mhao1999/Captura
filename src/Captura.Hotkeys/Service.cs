using System.Text;

namespace Captura.Hotkeys
{
    public class Service : NotifyPropertyChanged
    {

        public Service(ServiceName ServiceName)
        {
            this.ServiceName = ServiceName;

        }

        ServiceName _serviceName;

        public ServiceName ServiceName
        {
            get => _serviceName;
            set
            {
                _serviceName = value;

                OnPropertyChanged();

                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Description => GetDescription();

        string GetDescription()
        {
            switch (ServiceName)
            {
                case ServiceName.None:
                    return "没有";

                case ServiceName.Recording:
                    return "开始/停止录制";

                case ServiceName.Pause:
                    return "暂停/继续录制";

                case ServiceName.ScreenShot:
                    return "截图";

                case ServiceName.ActiveScreenShot:
                    return "截取当前活动窗口";

                case ServiceName.DesktopScreenShot:
                    return "截取桌面";

                case ServiceName.ToggleMouseClicks:
                    return "切换鼠标点击";

                case ServiceName.ToggleKeystrokes:
                    return "切换按键";

                case ServiceName.ScreenShotRegion:
                    return "Screenshot (Region)";

                case ServiceName.ScreenShotScreen:
                    return "ScreenShot (Screen)";

                case ServiceName.ScreenShotWindow:
                    return "ScreenShot (Window)";

                default:
                    return SpaceAtCapitals(ServiceName);
            }
        }

        static string SpaceAtCapitals<T>(T Obj)
        {
            var s = Obj.ToString();

            var sb = new StringBuilder();

            for (var i = 0; i < s.Length; ++i)
            {
                if (i != 0 && char.IsUpper(s[i]))
                    sb.Append(" ");

                sb.Append(s[i]);
            }

            return sb.ToString();
        }
    }
}