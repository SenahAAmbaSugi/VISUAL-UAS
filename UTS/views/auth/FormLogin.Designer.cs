namespace UTS.views
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.KLINIK_label = new System.Windows.Forms.Label();
            this.RightCardPanel = new System.Windows.Forms.Panel();
            this.ShowPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.Button_Login = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.BackgroundPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RightCardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BackColor = System.Drawing.Color.SteelBlue;
            this.BackgroundPanel.Controls.Add(this.panel1);
            this.BackgroundPanel.Controls.Add(this.RightCardPanel);
            this.BackgroundPanel.Location = new System.Drawing.Point(26, 23);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(1055, 505);
            this.BackgroundPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.KLINIK_label);
            this.panel1.Location = new System.Drawing.Point(69, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 433);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(129, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "KLINIK ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(40, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(323, 310);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // KLINIK_label
            // 
            this.KLINIK_label.AutoSize = true;
            this.KLINIK_label.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KLINIK_label.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.KLINIK_label.Location = new System.Drawing.Point(32, 8);
            this.KLINIK_label.Name = "KLINIK_label";
            this.KLINIK_label.Size = new System.Drawing.Size(350, 46);
            this.KLINIK_label.TabIndex = 0;
            this.KLINIK_label.Text = "Sistem Informasi ";
            this.KLINIK_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RightCardPanel
            // 
            this.RightCardPanel.BackColor = System.Drawing.Color.White;
            this.RightCardPanel.Controls.Add(this.ShowPasswordCheckBox);
            this.RightCardPanel.Controls.Add(this.Button_Login);
            this.RightCardPanel.Controls.Add(this.PasswordTextBox);
            this.RightCardPanel.Controls.Add(this.label3);
            this.RightCardPanel.Controls.Add(this.UsernameTextBox);
            this.RightCardPanel.Location = new System.Drawing.Point(624, 34);
            this.RightCardPanel.Name = "RightCardPanel";
            this.RightCardPanel.Size = new System.Drawing.Size(399, 433);
            this.RightCardPanel.TabIndex = 1;
            // 
            // ShowPasswordCheckBox
            // 
            this.ShowPasswordCheckBox.AutoSize = true;
            this.ShowPasswordCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ShowPasswordCheckBox.Location = new System.Drawing.Point(116, 256);
            this.ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
            this.ShowPasswordCheckBox.Size = new System.Drawing.Size(180, 29);
            this.ShowPasswordCheckBox.TabIndex = 6;
            this.ShowPasswordCheckBox.Text = "Show Password ";
            this.ShowPasswordCheckBox.UseVisualStyleBackColor = true;
            this.ShowPasswordCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordCheckBox_CheckedChanged);
            // 
            // Button_Login
            // 
            this.Button_Login.BackColor = System.Drawing.Color.SteelBlue;
            this.Button_Login.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Login.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Button_Login.Location = new System.Drawing.Point(64, 301);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(287, 52);
            this.Button_Login.TabIndex = 5;
            this.Button_Login.Text = "LOGIN";
            this.Button_Login.UseVisualStyleBackColor = false;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PasswordTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.PasswordTextBox.Location = new System.Drawing.Point(76, 206);
            this.PasswordTextBox.Multiline = true;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PasswordTextBox.Size = new System.Drawing.Size(254, 44);
            this.PasswordTextBox.TabIndex = 4;
            this.PasswordTextBox.Text = "Password";
            this.PasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(68, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 46);
            this.label3.TabIndex = 3;
            this.label3.Text = "LOGIN ADMIN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UsernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.UsernameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.UsernameTextBox.Location = new System.Drawing.Point(76, 120);
            this.UsernameTextBox.Multiline = true;
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UsernameTextBox.Size = new System.Drawing.Size(254, 44);
            this.UsernameTextBox.TabIndex = 1;
            this.UsernameTextBox.Text = "Username";
            this.UsernameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UsernameTextBox.UseSystemPasswordChar = true;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1106, 545);
            this.Controls.Add(this.BackgroundPanel);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.BackgroundPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.RightCardPanel.ResumeLayout(false);
            this.RightCardPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackgroundPanel;
        private System.Windows.Forms.Panel RightCardPanel;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label KLINIK_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_Login;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.CheckBox ShowPasswordCheckBox;
    }
}