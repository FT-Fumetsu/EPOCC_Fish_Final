using UnityEngine;

namespace UI
{
    public static class UIState
    {
        public static bool IsUiOpen { get; private set; }

        public static void SetUiOpen(bool open) => IsUiOpen = open;
    }
}