namespace UTS.views
{
    partial class FormLaporanKunjungan
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
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonPdf = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.LabelTotalKunjungan = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DokterComboBox = new System.Windows.Forms.ComboBox();
            this.EndDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Swis721 Hv BT", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "LAPORAN KUNJUNGAN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ButtonPdf
            // 
            this.ButtonPdf.BackColor = System.Drawing.Color.Snow;
            this.ButtonPdf.FlatAppearance.BorderSize = 0;
            this.ButtonPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPdf.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ButtonPdf.Location = new System.Drawing.Point(1200, 117);
            this.ButtonPdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonPdf.Name = "ButtonPdf";
            this.ButtonPdf.Size = new System.Drawing.Size(117, 49);
            this.ButtonPdf.TabIndex = 104;
            this.ButtonPdf.Text = "Export";
            this.ButtonPdf.UseVisualStyleBackColor = false;
            this.ButtonPdf.Click += new System.EventHandler(this.ButtonPdf_Click_1);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Snow;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnBack.Location = new System.Drawing.Point(84, 116);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(124, 50);
            this.BtnBack.TabIndex = 103;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = false;
            // 
            // LabelTotalKunjungan
            // 
            this.LabelTotalKunjungan.AutoSize = true;
            this.LabelTotalKunjungan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LabelTotalKunjungan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTotalKunjungan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelTotalKunjungan.Location = new System.Drawing.Point(189, 20);
            this.LabelTotalKunjungan.Name = "LabelTotalKunjungan";
            this.LabelTotalKunjungan.Size = new System.Drawing.Size(38, 17);
            this.LabelTotalKunjungan.TabIndex = 102;
            this.LabelTotalKunjungan.Text = "XXX";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 237);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1409, 409);
            this.dataGridView1.TabIndex = 101;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(326, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 96;
            this.label6.Text = "DOKTER";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(732, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 98;
            this.label4.Text = "s/d";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 17);
            this.label5.TabIndex = 99;
            this.label5.Text = "TOTAL KUNJUNGAN :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 100;
            this.label3.Text = "PERIODE";
            // 
            // DokterComboBox
            // 
            this.DokterComboBox.FormattingEnabled = true;
            this.DokterComboBox.Location = new System.Drawing.Point(424, 168);
            this.DokterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.DokterComboBox.Name = "DokterComboBox";
            this.DokterComboBox.Size = new System.Drawing.Size(288, 24);
            this.DokterComboBox.TabIndex = 93;
            // 
            // EndDatedateTimePicker
            // 
            this.EndDatedateTimePicker.Location = new System.Drawing.Point(776, 127);
            this.EndDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.EndDatedateTimePicker.Name = "EndDatedateTimePicker";
            this.EndDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.EndDatedateTimePicker.TabIndex = 90;
            // 
            // StartDatedateTimePicker
            // 
            this.StartDatedateTimePicker.Location = new System.Drawing.Point(425, 126);
            this.StartDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.StartDatedateTimePicker.Name = "StartDatedateTimePicker";
            this.StartDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.StartDatedateTimePicker.TabIndex = 91;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1441, 48);
            this.panel1.TabIndex = 89;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.LabelTotalKunjungan);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(23, 685);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(254, 54);
            this.panel2.TabIndex = 105;
            // 
            // FormLaporanKunjungan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1435, 743);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ButtonPdf);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DokterComboBox);
            this.Controls.Add(this.EndDatedateTimePicker);
            this.Controls.Add(this.StartDatedateTimePicker);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLaporanKunjungan";
            this.Text = "FormLaporanKunjungan";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonPdf;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Label LabelTotalKunjungan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DokterComboBox;
        private System.Windows.Forms.DateTimePicker EndDatedateTimePicker;
        private System.Windows.Forms.DateTimePicker StartDatedateTimePicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}