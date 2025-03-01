namespace PointAndShrink
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            trayIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // trayIcon
            // 
            trayIcon.BalloonTipText = "PointAndShrink";
            trayIcon.BalloonTipTitle = "PointAndShrink";
            trayIcon.Text = "PointAndShrink";
            trayIcon.Visible = true;
            trayIcon.Click += trayIcon_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(236, 175);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Opacity = 0D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PointAndShrink";
            Activated += Form1_Activated;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private NotifyIcon trayIcon;
    }
}
