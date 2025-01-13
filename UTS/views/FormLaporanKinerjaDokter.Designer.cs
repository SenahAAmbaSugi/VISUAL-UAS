namespace UTS.views
{
    partial class FormLaporanKinerjaDokter
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
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DokterComboBox = new System.Windows.Forms.ComboBox();
            this.EndDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelHasilTotalPasien = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelHasilTotalJamPraktek = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LabelHasilTotalPasienPerhari = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LabelHasilTotalPendapatan = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Swis721 Hv BT", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(605, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "LAPORAN KINERJA DOKTER";
            // 
            // ButtonPdf
            // 
            this.ButtonPdf.BackColor = System.Drawing.Color.Snow;
            this.ButtonPdf.FlatAppearance.BorderSize = 0;
            this.ButtonPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPdf.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ButtonPdf.Location = new System.Drawing.Point(1209, 134);
            this.ButtonPdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonPdf.Name = "ButtonPdf";
            this.ButtonPdf.Size = new System.Drawing.Size(117, 49);
            this.ButtonPdf.TabIndex = 131;
            this.ButtonPdf.Text = "Export";
            this.ButtonPdf.UseVisualStyleBackColor = false;
            this.ButtonPdf.Click += new System.EventHandler(this.ButtonPdf_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Snow;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnBack.Location = new System.Drawing.Point(93, 133);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(124, 50);
            this.BtnBack.TabIndex = 130;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(335, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 126;
            this.label6.Text = "DOKTER";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(741, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 127;
            this.label4.Text = "s/d";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(335, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 128;
            this.label3.Text = "PERIODE";
            // 
            // DokterComboBox
            // 
            this.DokterComboBox.FormattingEnabled = true;
            this.DokterComboBox.Location = new System.Drawing.Point(433, 185);
            this.DokterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.DokterComboBox.Name = "DokterComboBox";
            this.DokterComboBox.Size = new System.Drawing.Size(288, 24);
            this.DokterComboBox.TabIndex = 125;
            // 
            // EndDatedateTimePicker
            // 
            this.EndDatedateTimePicker.Location = new System.Drawing.Point(785, 144);
            this.EndDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.EndDatedateTimePicker.Name = "EndDatedateTimePicker";
            this.EndDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.EndDatedateTimePicker.TabIndex = 123;
            // 
            // StartDatedateTimePicker
            // 
            this.StartDatedateTimePicker.Location = new System.Drawing.Point(434, 143);
            this.StartDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.StartDatedateTimePicker.Name = "StartDatedateTimePicker";
            this.StartDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.StartDatedateTimePicker.TabIndex = 124;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-4, 41);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1465, 48);
            this.panel1.TabIndex = 122;
            // 
            // LabelHasilTotalPasien
            // 
            this.LabelHasilTotalPasien.AutoSize = true;
            this.LabelHasilTotalPasien.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHasilTotalPasien.Location = new System.Drawing.Point(113, 48);
            this.LabelHasilTotalPasien.Name = "LabelHasilTotalPasien";
            this.LabelHasilTotalPasien.Size = new System.Drawing.Size(38, 17);
            this.LabelHasilTotalPasien.TabIndex = 104;
            this.LabelHasilTotalPasien.Text = "XXX";
            this.LabelHasilTotalPasien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(50, 12);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(170, 17);
            this.label7.TabIndex = 103;
            this.label7.Text = "TOTAL JAM PRAKTEK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 103;
            this.label2.Text = "TOTAL PASIEN";
            // 
            // LabelHasilTotalJamPraktek
            // 
            this.LabelHasilTotalJamPraktek.AutoSize = true;
            this.LabelHasilTotalJamPraktek.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHasilTotalJamPraktek.Location = new System.Drawing.Point(112, 48);
            this.LabelHasilTotalJamPraktek.Name = "LabelHasilTotalJamPraktek";
            this.LabelHasilTotalJamPraktek.Size = new System.Drawing.Size(38, 17);
            this.LabelHasilTotalJamPraktek.TabIndex = 104;
            this.LabelHasilTotalJamPraktek.Text = "XXX";
            this.LabelHasilTotalJamPraktek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.Controls.Add(this.LabelHasilTotalJamPraktek);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(420, 282);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(267, 92);
            this.panel4.TabIndex = 134;
            // 
            // LabelHasilTotalPasienPerhari
            // 
            this.LabelHasilTotalPasienPerhari.AutoSize = true;
            this.LabelHasilTotalPasienPerhari.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHasilTotalPasienPerhari.Location = new System.Drawing.Point(116, 48);
            this.LabelHasilTotalPasienPerhari.Name = "LabelHasilTotalPasienPerhari";
            this.LabelHasilTotalPasienPerhari.Size = new System.Drawing.Size(38, 17);
            this.LabelHasilTotalPasienPerhari.TabIndex = 104;
            this.LabelHasilTotalPasienPerhari.Text = "XXX";
            this.LabelHasilTotalPasienPerhari.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 17);
            this.label8.TabIndex = 103;
            this.label8.Text = "RATA - RATA PASIEN/HARI ";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel5.Controls.Add(this.LabelHasilTotalPasienPerhari);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(772, 282);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(267, 92);
            this.panel5.TabIndex = 133;
            // 
            // LabelHasilTotalPendapatan
            // 
            this.LabelHasilTotalPendapatan.AutoSize = true;
            this.LabelHasilTotalPendapatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHasilTotalPendapatan.Location = new System.Drawing.Point(121, 48);
            this.LabelHasilTotalPendapatan.Name = "LabelHasilTotalPendapatan";
            this.LabelHasilTotalPendapatan.Size = new System.Drawing.Size(38, 17);
            this.LabelHasilTotalPendapatan.TabIndex = 104;
            this.LabelHasilTotalPendapatan.Text = "XXX";
            this.LabelHasilTotalPendapatan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(82, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 17);
            this.label9.TabIndex = 103;
            this.label9.Text = "TOTAL PASIEN";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel6.Controls.Add(this.LabelHasilTotalPendapatan);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(1125, 282);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(267, 92);
            this.panel6.TabIndex = 132;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.LabelHasilTotalPasien);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(66, 282);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 92);
            this.panel3.TabIndex = 135;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 423);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1409, 300);
            this.dataGridView1.TabIndex = 129;
            // 
            // FormLaporanKinerjaDokter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1457, 736);
            this.Controls.Add(this.ButtonPdf);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DokterComboBox);
            this.Controls.Add(this.EndDatedateTimePicker);
            this.Controls.Add(this.StartDatedateTimePicker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormLaporanKinerjaDokter";
            this.Text = "FormLaporanKinerjaDokter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonPdf;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DokterComboBox;
        private System.Windows.Forms.DateTimePicker EndDatedateTimePicker;
        private System.Windows.Forms.DateTimePicker StartDatedateTimePicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelHasilTotalPasien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabelHasilTotalJamPraktek;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label LabelHasilTotalPasienPerhari;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label LabelHasilTotalPendapatan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}