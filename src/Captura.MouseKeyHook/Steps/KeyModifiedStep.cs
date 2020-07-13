using System;
using System.Drawing;

namespace Captura.MouseKeyHook.Steps
{
    abstract class KeyModifiedStep : IRecordStep
    {
        readonly ModifierStates _modifierStates;
        readonly KeymapViewModel _keymap;

        public KeyModifiedStep(
            KeymapViewModel Keymap)
        {
            _keymap = Keymap;

            _modifierStates = ModifierStates.GetCurrent();
        }

        public virtual void Draw(IEditableFrame Editor, Func<Point, Point> PointTransform)
        {

        }

        public virtual bool Merge(IRecordStep NextStep)
        {
            return false;
        }
    }
}
