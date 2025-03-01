using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PointAndShrink
{
    internal partial class Monitors
    {
        private const int MONITOR_DEFAULTTONEAREST = 2;
        private const int MDT_EFFECTIVE_DPI = 0;

        public enum ScalingFactor : uint
        {
            Other = 0,
            Scale100 = 96,
            Scale125 = 120,
            Scale150 = 144,
            Scale175 = 168,
            Scale200 = 192
        }

        public static ScalingFactor ConvertDPIToScalingFactor(uint value) =>
            Enum.IsDefined(typeof(ScalingFactor), value) ? (ScalingFactor)value : ScalingFactor.Other;

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromRect([In] ref Rectangle lprc, uint dwFlags);

        [DllImport("shcore.dll")]
        private static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

        public static List<CompleteMonitorInfo> GetMonitors()
        {
            var monitors = new List<CompleteMonitorInfo>();

            foreach (var screen in Screen.AllScreens)
            {
                var bounds = screen.Bounds;
                var monitorHandle = MonitorFromRect(ref bounds, MONITOR_DEFAULTTONEAREST);

                _ = GetDpiForMonitor(monitorHandle, MDT_EFFECTIVE_DPI, out uint dpi, out _);

                Debug.WriteLine($"Screen: {screen.DeviceName} | DPI: {dpi}");

                var monitorObj = new CompleteMonitorInfo
                {
                    Screen = screen,
                    ScalingFactor = ConvertDPIToScalingFactor(dpi)
                };
                monitors.Add(monitorObj);
            }

            return monitors;
        }

        public record CompleteMonitorInfo
        {
            public required ScalingFactor ScalingFactor { get; init; }

            public required Screen Screen { get; init; }
        }
    }
}
