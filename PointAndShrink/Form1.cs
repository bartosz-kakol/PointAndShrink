using Microsoft.Win32;
using System.Diagnostics;
using System.Text.Json;
using WinFormsDarkMode;

namespace PointAndShrink
{
    public partial class Form1 : Form
    {
        private readonly PointerSettings pointerSettings;

        private Thread? backgroundThread;

        private ManualResetEventSlim backgroundThreadPauseEvent = new(true);

        private string currentScreen = "";

        private Dictionary<string, Monitors.ScalingFactor> scalingFactors = [];

        public Form1()
        {
            pointerSettings = new PointerSettings();

            InitializeComponent();

            Icon = Assets.appicon;
            trayIcon.Icon = Assets.appicon;
        }

        private void DoEverythingToFocusTheWindow()
        {
            WindowState = FormWindowState.Normal;
            BringToFront();
            TopMost = true;
            Focus();
            TopMost = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DarkMode.AutoDarkModeMica(this);

            if (!pointerSettings.RegistryKeyOpened)
            {
                MessageBox.Show("Could not find Cursors registry key.", "Failed to initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            UpdateMonitors();
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

            backgroundThread = new Thread(BackgroundLoop)
            {
                IsBackground = true
            };
            backgroundThread.Start();
        }

        private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs e)
        {
            Debug.WriteLine("SystemEvents_DisplaySettingsChanged");
            UpdateMonitors();

            if (scalingFactors.Count <= 1)
            {
                backgroundThreadPauseEvent.Reset();
            }
            else
            {
                backgroundThreadPauseEvent.Set();
            }
        }

        private void UpdateMonitors()
        {
            scalingFactors = [];

            foreach (var monitor in Monitors.GetMonitors())
            {
                scalingFactors.Add(monitor.Screen.DeviceName, monitor.ScalingFactor);
            }

            currentScreen = "";
        }

        private void BackgroundLoop()
        {
            while (true)
            {
                backgroundThreadPauseEvent.Wait();

                var screen = Screen.FromPoint(Cursor.Position);

                if (screen.DeviceName != currentScreen)
                {
                    currentScreen = screen.DeviceName;

                    if (!scalingFactors.ContainsKey(currentScreen))
                    {
                        Debug.WriteLine($"[WARNING] Asked for the scaling factor of a screen which hasn't registered yet! ({currentScreen})");
                        currentScreen = "";
                        Thread.Sleep(500);
                        continue;
                    }

                    var scalingFactor = scalingFactors[currentScreen];
                    var cursorSize = scalingFactor switch
                    {
                        Monitors.ScalingFactor.Scale125 => PointerSettings.SizeVariant.Large,
                        Monitors.ScalingFactor.Scale175 => PointerSettings.SizeVariant.Large,
                        _ => PointerSettings.SizeVariant.Default,
                    };

                    pointerSettings.SetPointerSize(cursorSize);
                }

                Thread.Sleep(10);
            }
        }

        private void Form1_Activated(object? sender, EventArgs e)
        {
            Hide();
            Opacity = 1;
            Activated -= Form1_Activated;
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            //Show();
            //DoEverythingToFocusTheWindow();

            var result = MessageBox.Show("Do you want to stop PointAndShrink?", "Quit PointAndShrink", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Hide();

            SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
            pointerSettings.SetPointerSize(PointerSettings.SizeVariant.Default);
        }
    }
}
