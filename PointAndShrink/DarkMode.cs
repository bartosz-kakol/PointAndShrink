using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PointAndShrink
{
    internal static class DarkMode
    {
        public static void AutoDarkMode(Control control)
        {
            if (control is Form)
            {
                int state = 1;
                Dwm.DwmSetWindowAttribute(control.Handle, Dwm.DWMWINDOWATTRIBUTE.ImmersiveDarkMode, ref state, sizeof(int));
                control.BackColor = Color.FromArgb(32, 32, 32);
                control.ForeColor = Color.FromArgb(220, 220, 220);
            }

            try
            {
                control.Controls.OfType<StatusStrip>().First().BackColor = Color.FromArgb(40, 40, 40);
            }
            catch (InvalidOperationException) { }

            foreach (var button in control.Controls.OfType<Button>())
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(70, 70, 70);
                button.BackColor = Color.FromArgb(45, 45, 45);
                ControlDecoration.MakeControlRounded(button);
            }

            foreach (var textBox in control.Controls.OfType<TextBox>())
            {
                textBox.BackColor = Color.FromArgb(55, 55, 55);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.ForeColor = Color.White;
                ControlDecoration.MakeControlRounded(textBox, 1, 1, 1, 3, 2);
            }

            foreach (var comboBox in control.Controls.OfType<ComboBox>())
            {
                if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    comboBox.SelectedIndexChanged += (sender, e) =>
                    {
                        FixComboBox((ComboBox)sender!);
                    };
                }
                comboBox.FlatStyle = FlatStyle.Flat;
                comboBox.BackColor = Color.FromArgb(45, 45, 45);
                comboBox.ForeColor = Color.White;
                ControlDecoration.MakeControlRounded(comboBox, 3, 0, 3, 3);
            }

            foreach (var progressBar in control.Controls.OfType<ProgressBar>())
            {
                progressBar.BackColor = Color.FromArgb(45, 45, 45);
            }

            foreach (var panel in control.Controls.OfType<Panel>())
            {
                AutoDarkMode(panel);
            }

            foreach (var groupBox in control.Controls.OfType<GroupBox>())
            {
                groupBox.ForeColor = Color.FromArgb(220, 220, 220);
                AutoDarkMode(groupBox);
            }

            foreach (var tabControl in control.Controls.OfType<TabControl>())
            {
                foreach (var tabPage in tabControl.Controls.OfType<TabPage>())
                {
                    tabPage.BackColor = Color.Black;
                    AutoDarkMode(tabPage);
                }
            }
        }

        public static void AutoDarkModeMica(Form root)
        {
            Dwm.ApplyMicaBackdropToWindow(root.Handle);
            Dwm.AutoExtendFrameIntoClientArea(root.Handle);
            AutoDarkMode(root);
            root.BackColor = Color.Black;
        }

        public static void MakePrimaryButton(Button button)
        {
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(93, 188, 234);
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(87, 173, 214);
            button.BackColor = Color.FromArgb(98, 204, 255);
            button.ForeColor = Color.Black;
        }

        public static void FixComboBox(ComboBox comboBox)
        {
            comboBox.Enabled = false;
            comboBox.Enabled = true;
        }
    }
}
