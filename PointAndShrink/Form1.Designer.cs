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
            displaySelector = new ComboBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            sizeRadioExtraLarge = new RadioButton();
            sizeRadioLarge = new RadioButton();
            sizeRadioDefault = new RadioButton();
            trayIcon = new NotifyIcon(components);
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // displaySelector
            // 
            displaySelector.DropDownStyle = ComboBoxStyle.DropDownList;
            displaySelector.FormattingEnabled = true;
            displaySelector.Location = new Point(12, 27);
            displaySelector.Name = "displaySelector";
            displaySelector.Size = new Size(212, 23);
            displaySelector.TabIndex = 0;
            displaySelector.SelectedIndexChanged += displaySelector_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 1;
            label1.Text = "Configuration for:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(sizeRadioExtraLarge);
            groupBox1.Controls.Add(sizeRadioLarge);
            groupBox1.Controls.Add(sizeRadioDefault);
            groupBox1.Location = new Point(12, 56);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(212, 110);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cursor size";
            // 
            // sizeRadioExtraLarge
            // 
            sizeRadioExtraLarge.AutoSize = true;
            sizeRadioExtraLarge.Location = new Point(15, 72);
            sizeRadioExtraLarge.Name = "sizeRadioExtraLarge";
            sizeRadioExtraLarge.Size = new Size(80, 19);
            sizeRadioExtraLarge.TabIndex = 2;
            sizeRadioExtraLarge.Tag = "xl";
            sizeRadioExtraLarge.Text = "Extra large";
            sizeRadioExtraLarge.UseVisualStyleBackColor = true;
            // 
            // sizeRadioLarge
            // 
            sizeRadioLarge.AutoSize = true;
            sizeRadioLarge.Location = new Point(15, 47);
            sizeRadioLarge.Name = "sizeRadioLarge";
            sizeRadioLarge.Size = new Size(54, 19);
            sizeRadioLarge.TabIndex = 1;
            sizeRadioLarge.Tag = "l";
            sizeRadioLarge.Text = "Large";
            sizeRadioLarge.UseVisualStyleBackColor = true;
            // 
            // sizeRadioDefault
            // 
            sizeRadioDefault.AutoSize = true;
            sizeRadioDefault.Checked = true;
            sizeRadioDefault.Location = new Point(15, 22);
            sizeRadioDefault.Name = "sizeRadioDefault";
            sizeRadioDefault.Size = new Size(63, 19);
            sizeRadioDefault.TabIndex = 0;
            sizeRadioDefault.TabStop = true;
            sizeRadioDefault.Tag = "";
            sizeRadioDefault.Text = "Default";
            sizeRadioDefault.UseVisualStyleBackColor = true;
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
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(displaySelector);
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
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox displaySelector;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton sizeRadioDefault;
        private RadioButton sizeRadioExtraLarge;
        private RadioButton sizeRadioLarge;
        private NotifyIcon trayIcon;
    }
}
