using Microsoft.Win32;
using System.Diagnostics;

namespace PointAndShrink
{
    public partial class Form1 : Form
    {
        private readonly PointerSettings pointerSettings;

        private readonly Configurations configurations;

        private Thread? backgroundThread;

        private Configurations.Configuration? selectedConfiguration;

        private string currentScreen = "";

        private string? SelectedDisplayDeviceName => displaySelector.SelectedItem?.ToString();

        public Form1()
        {
            pointerSettings = new PointerSettings();
            configurations = new Configurations();

            InitializeComponent();

            sizeRadioDefault.CheckedChanged += SizeRadioCheckedChanged;
            sizeRadioLarge.CheckedChanged += SizeRadioCheckedChanged;
            sizeRadioExtraLarge.CheckedChanged += SizeRadioCheckedChanged;

            Icon = Assets.appicon;
            trayIcon.Icon = Assets.appicon;
        }

        private void ModifyConfigurationForDisplay(string display, Action<Configurations.Configuration> modifier)
        {
            var configuration = configurations.GetForDisplay(display);
            modifier(configuration);
            configurations.SetForDisplay(display, configuration);
        }

        private void SizeRadioCheckedChanged(object? sender, EventArgs e)
        {
            if (selectedConfiguration == null || SelectedDisplayDeviceName == null)
            {
                return;
            }

            var radio = (RadioButton)sender!;
            
            if (radio.Checked)
            {
                var tag = (radio.Tag ?? "").ToString();
                var sizeVariant = tag switch
                {
                    "l" => PointerSettings.SizeVariant.Large,
                    "xl" => PointerSettings.SizeVariant.ExtraLarge,
                    _ => PointerSettings.SizeVariant.Default,
                };

                ModifyConfigurationForDisplay(SelectedDisplayDeviceName, configuration =>
                {
                    configuration.Size = sizeVariant;
                });
            }
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
            }

            foreach (var screen in Screen.AllScreens)
            {
                displaySelector.Items.Add(screen.DeviceName);
            }

            if (displaySelector.Items.Count == 0)
            {
                MessageBox.Show("Could not find any screen.", "Failed to initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            displaySelector.SelectedIndex = 0;

            backgroundThread = new Thread(BackgroundLoop)
            {
                IsBackground = true
            };
            backgroundThread.Start();
        }

        private void RefreshConfigurationDisplay()
        {
            if (selectedConfiguration == null)
            {
                return;
            }

            // Size radio buttons
            switch (selectedConfiguration.Size)
            {
                case PointerSettings.SizeVariant.Default:
                    sizeRadioDefault.Checked = true;
                    break;
                case PointerSettings.SizeVariant.Large:
                    sizeRadioLarge.Checked = true;
                    break;
                case PointerSettings.SizeVariant.ExtraLarge:
                    sizeRadioExtraLarge.Checked = true;
                    break;
            }
        }

        private void BackgroundLoop()
        {
            while (true)
            {
                var screen = Screen.FromPoint(Cursor.Position);

                if (screen.DeviceName != currentScreen)
                {
                    currentScreen = screen.DeviceName;
                    var configuration = configurations.GetForDisplay(currentScreen);

                    pointerSettings.SetPointerSize(configuration.Size);
                }

                Thread.Sleep(5);
            }
        }

        private void Form1_Activated(object? sender, EventArgs e)
        {
            DarkMode.FixComboBox(displaySelector);
            Hide();
            Opacity = 1;
            Activated -= Form1_Activated;
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            Show();
            DoEverythingToFocusTheWindow();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void displaySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedDisplayDeviceName != null)
            {
                selectedConfiguration = configurations.GetForDisplay(SelectedDisplayDeviceName);
                RefreshConfigurationDisplay();
            }
        }
    }
}
