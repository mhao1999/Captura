using System;
using System.Drawing;
using System.IO;
using Captura.Video;

namespace Captura.MouseKeyHook
{
    /// <summary>
    /// Draws Mouse Clicks and/or Keystrokes on an Image.
    /// </summary>
    public class MouseKeyOverlay : IOverlay
    {
        #region Fields
        readonly IMouseKeyHook _hook;
        readonly IOverlay _mouseClickOverlay,
            _scrollOverlay;

        readonly KeymapViewModel _keymap;
        #endregion
        
        /// <summary>
        /// Creates a new instance of <see cref="MouseKeyHook"/>.
        /// </summary>
        public MouseKeyOverlay(IMouseKeyHook Hook,
            MouseClickSettings MouseClickSettings,
            KeymapViewModel Keymap,
            string FileName,
            Func<TimeSpan> Elapsed)
        {
            _keymap = Keymap;

            _hook = Hook;
            _mouseClickOverlay = new MouseClickOverlay(_hook, MouseClickSettings);
            _scrollOverlay = new ScrollOverlay(_hook, MouseClickSettings);
        }

        TextWriter InitKeysToTextFile(string FileName, Func<TimeSpan> Elapsed)
        {
            var dir = Path.GetDirectoryName(FileName);
            var fileNameWoExt = Path.GetFileNameWithoutExtension(FileName);

            var targetName = $"{fileNameWoExt}.keys.txt";

            var path = dir == null ? targetName : Path.Combine(dir, targetName);

            var keystrokeFileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            var textWriter = new StreamWriter(keystrokeFileStream);

            return textWriter;
        }
        
        /// <summary>
        /// Draws overlay.
        /// </summary>
        public void Draw(IEditableFrame Editor, Func<Point, Point> Transform = null)
        {
            _mouseClickOverlay?.Draw(Editor, Transform);
            _scrollOverlay?.Draw(Editor, Transform);

        }

        /// <summary>
        /// Frees all resources used by this object.
        /// </summary>
        public void Dispose()
        {
            _hook?.Dispose();

            _mouseClickOverlay?.Dispose();
            _scrollOverlay?.Dispose();
        }
    }
}