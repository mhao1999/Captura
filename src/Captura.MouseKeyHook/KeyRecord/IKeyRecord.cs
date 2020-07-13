using System;

namespace Captura.MouseKeyHook
{
    interface IKeyRecord
    {
        DateTime TimeStamp { get; }

        bool Control { get; }
        bool Shift { get; }
        bool Alt { get; }
    }
}