using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Tar;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PointAndShrink
{
    internal partial class PointerSettings
    {
        private const int SPI_SETCURSORS = 0x0057;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        private const string GLOBAL_CURSOR_FILE_PREFIX = @"%SystemRoot%\cursors\";

        private static readonly Dictionary<string, string> aeroCursors = new()
        {
            { "AppStarting", GLOBAL_CURSOR_FILE_PREFIX + "aero_working{0}.ani" },
            { "Arrow", GLOBAL_CURSOR_FILE_PREFIX + "aero_arrow{0}.cur" },
            { "Hand", GLOBAL_CURSOR_FILE_PREFIX + "aero_link{0}.cur" },
            { "Help", GLOBAL_CURSOR_FILE_PREFIX + "aero_helpsel{0}.cur" },
            { "No", GLOBAL_CURSOR_FILE_PREFIX + "aero_unavail{0}.cur" },
            { "NWPen", GLOBAL_CURSOR_FILE_PREFIX + "aero_pen{0}.cur" },
            { "Person", GLOBAL_CURSOR_FILE_PREFIX + "aero_person{0}.cur" },
            { "Pin", GLOBAL_CURSOR_FILE_PREFIX + "aero_pin{0}.cur" },
            { "SizeAll", GLOBAL_CURSOR_FILE_PREFIX + "aero_move{0}.cur" },
            { "SizeNESW", GLOBAL_CURSOR_FILE_PREFIX + "aero_nesw{0}.cur" },
            { "SizeNS", GLOBAL_CURSOR_FILE_PREFIX + "aero_ns{0}.cur" },
            { "SizeNWSE", GLOBAL_CURSOR_FILE_PREFIX + "aero_nwse{0}.cur" },
            { "SizeWE", GLOBAL_CURSOR_FILE_PREFIX + "aero_ew{0}.cur" },
            { "UpArrow", GLOBAL_CURSOR_FILE_PREFIX + "aero_up{0}.cur" },
            { "Wait", GLOBAL_CURSOR_FILE_PREFIX + "aero_busy{0}.ani" }
        };

        public enum SizeVariant
        {
            Default,
            Large,
            ExtraLarge
        }

        [LibraryImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

        private readonly RegistryKey? _cursorsRegistryKey;

        private RegistryKey cursorsRegistryKey => _cursorsRegistryKey ?? throw new InvalidOperationException("Cursors registry key has not been opened.");

        public bool RegistryKeyOpened => _cursorsRegistryKey != null;

        public PointerSettings()
        {
            _cursorsRegistryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\\Cursors", true);
        }

        private static void UpdateCursorSPI()
        {
            _ = SystemParametersInfo(SPI_SETCURSORS, 0, 0, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
        }

        private static string GetSuffixForSizeVariant(SizeVariant variant)
        {
            return variant switch
            {
                SizeVariant.Large => "_l",
                SizeVariant.ExtraLarge => "_xl",
                _ => ""
            };
        }

        private void SetSingleCursorFile(string cursorName, string filePath)
        {
            cursorsRegistryKey.SetValue(cursorName, filePath);
        }

        public void SetPointerSize(SizeVariant variant)
        {
            var suffix = GetSuffixForSizeVariant(variant);

            foreach (var entry in aeroCursors)
            {
                SetSingleCursorFile(entry.Key, string.Format(entry.Value, suffix));
            }

            UpdateCursorSPI();
        }
    }
}
