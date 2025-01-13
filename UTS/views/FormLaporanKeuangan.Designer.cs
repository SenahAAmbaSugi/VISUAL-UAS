namespace UTS
{
    partial class FormLaporanKeuangan
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StatusComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EndDatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.LabelTotalPendapatan = new System.Windows.Forms.Label();
            this.BtnBack = new System.Windows.Forms.Button();
            this.ButtonPdf = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Swis721 Hv BT", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(548, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "LAPORAN KEUANGAN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 26);
            this.panel1.TabIndex = 29;
            // 
            // StartDatedateTimePicker
            // 
            this.StartDatedateTimePicker.Location = new System.Drawing.Point(383, 55);
            this.StartDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartDatedateTimePicker.Name = "StartDatedateTimePicker";
            this.StartDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.StartDatedateTimePicker.TabIndex = 30;
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.FormattingEnabled = true;
            this.StatusComboBox.Location = new System.Drawing.Point(383, 100);
            this.StatusComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(283, 24);
            this.StatusComboBox.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(284, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 32;
            this.label3.Text = "PERIODE";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "STATUS";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // EndDatedateTimePicker
            // 
            this.EndDatedateTimePicker.Location = new System.Drawing.Point(733, 57);
            this.EndDatedateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndDatedateTimePicker.Name = "EndDatedateTimePicker";
            this.EndDatedateTimePicker.Size = new System.Drawing.Size(291, 22);
            this.EndDatedateTimePicker.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(689, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "s/d";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 154);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1271, 394);
            this.dataGridView1.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 591);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "TOTAL PENDAPATAN : ";
            // 
            // LabelTotalPendapatan
            // 
            this.LabelTotalPendapatan.AutoSize = true;
            this.LabelTotalPendapatan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTotalPendapatan.Location = new System.Drawing.Point(224, 591);
            this.LabelTotalPendapatan.Name = "LabelTotalPendapatan";
            this.LabelTotalPendapatan.Size = new System.Drawing.Size(34, 17);
            this.LabelTotalPendapatan.TabIndex = 34;
            this.LabelTotalPendapatan.Text = "RP.";
            this.LabelTotalPendapatan.Click += new System.EventHandler(this.label6_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Snow;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnBack.Location = new System.Drawing.Point(24, 47);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(124, 50);
            this.BtnBack.TabIndex = 59;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // ButtonPdf
            // 
            this.ButtonPdf.BackColor = System.Drawing.Color.Snow;
            this.ButtonPdf.FlatAppearance.BorderSize = 0;
            this.ButtonPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPdf.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ButtonPdf.Location = new System.Drawing.Point(1065, 52);
            this.ButtonPdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonPdf.Name = "ButtonPdf";
            this.ButtonPdf.Size = new System.Drawing.Size(112, 41);
            this.ButtonPdf.TabIndex = 60;
            this.ButtonPdf.Text = "Export";
            this.ButtonPdf.UseVisualStyleBackColor = false;
            this.ButtonPdf.Click += new System.EventHandler(this.ButtonPdf_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Location = new System.Drawing.Point(16, 575);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(254, 54);
            this.panel2.TabIndex = 106;
            // 
            // FormLaporanKeuangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1303, 652);
            this.Controls.Add(this.ButtonPdf);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.LabelTotalPendapatan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StatusComboBox);
            this.Controls.Add(this.EndDatedateTimePicker);
            this.Controls.Add(this.StartDatedateTimePicker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormLaporanKeuangan";
            this.Text = "FormLaporanKeuangan";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker StartDatedateTimePicker;
        private System.Windows.Forms.ComboBox StatusComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker EndDatedateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LabelTotalPendapatan;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button ButtonPdf;
        private System.Windows.Forms.Panel panel2;
    }
}