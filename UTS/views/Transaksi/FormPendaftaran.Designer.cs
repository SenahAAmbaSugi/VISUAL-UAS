namespace UTS
{
    partial class FormPendaftaran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPendaftaran));
            this.label1 = new System.Windows.Forms.Label();
            this.TanggalPendaftaranTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnHapus = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.BtnTambah = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.NomorAntrianTextBox = new System.Windows.Forms.TextBox();
            this.PilihJadwalComboBox = new System.Windows.Forms.ComboBox();
            this.NamaPasienComboBox = new System.Windows.Forms.ComboBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Swis721 Hv BT", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(413, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "PENDAFTARAN PASIEN";
            // 
            // TanggalPendaftaranTextBox
            // 
            this.TanggalPendaftaranTextBox.Location = new System.Drawing.Point(612, 157);
            this.TanggalPendaftaranTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TanggalPendaftaranTextBox.Multiline = true;
            this.TanggalPendaftaranTextBox.Name = "TanggalPendaftaranTextBox";
            this.TanggalPendaftaranTextBox.Size = new System.Drawing.Size(287, 29);
            this.TanggalPendaftaranTextBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(406, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "PILIH JADWAL";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(406, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "TANGGAL PENDAFTARAN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(406, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "NAMA PASIEN";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(351, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(987, 26);
            this.panel1.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(407, 289);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(914, 261);
            this.dataGridView1.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, -30);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(375, 597);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // BtnHapus
            // 
            this.BtnHapus.BackColor = System.Drawing.Color.Red;
            this.BtnHapus.FlatAppearance.BorderSize = 0;
            this.BtnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHapus.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.BtnHapus.Location = new System.Drawing.Point(1202, 241);
            this.BtnHapus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnHapus.Name = "BtnHapus";
            this.BtnHapus.Size = new System.Drawing.Size(93, 30);
            this.BtnHapus.TabIndex = 27;
            this.BtnHapus.Text = "HAPUS";
            this.BtnHapus.UseVisualStyleBackColor = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.BackColor = System.Drawing.Color.Yellow;
            this.BtnEdit.FlatAppearance.BorderSize = 0;
            this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnEdit.Location = new System.Drawing.Point(1087, 237);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(93, 38);
            this.BtnEdit.TabIndex = 26;
            this.BtnEdit.Text = "EDIT";
            this.BtnEdit.UseVisualStyleBackColor = false;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.Color.YellowGreen;
            this.BtnUpdate.FlatAppearance.BorderSize = 0;
            this.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnUpdate.Location = new System.Drawing.Point(1202, 174);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(93, 31);
            this.BtnUpdate.TabIndex = 25;
            this.BtnUpdate.Text = "UPDATE";
            this.BtnUpdate.UseVisualStyleBackColor = false;
            // 
            // BtnTambah
            // 
            this.BtnTambah.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnTambah.FlatAppearance.BorderSize = 0;
            this.BtnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnTambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTambah.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnTambah.Location = new System.Drawing.Point(1087, 174);
            this.BtnTambah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTambah.Name = "BtnTambah";
            this.BtnTambah.Size = new System.Drawing.Size(93, 31);
            this.BtnTambah.TabIndex = 24;
            this.BtnTambah.Text = "TAMBAH";
            this.BtnTambah.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(406, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "NOMOR ANTRIAN";
            // 
            // NomorAntrianTextBox
            // 
            this.NomorAntrianTextBox.Location = new System.Drawing.Point(612, 202);
            this.NomorAntrianTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NomorAntrianTextBox.Multiline = true;
            this.NomorAntrianTextBox.Name = "NomorAntrianTextBox";
            this.NomorAntrianTextBox.Size = new System.Drawing.Size(287, 29);
            this.NomorAntrianTextBox.TabIndex = 29;
            // 
            // PilihJadwalComboBox
            // 
            this.PilihJadwalComboBox.FormattingEnabled = true;
            this.PilihJadwalComboBox.Location = new System.Drawing.Point(614, 125);
            this.PilihJadwalComboBox.Name = "PilihJadwalComboBox";
            this.PilihJadwalComboBox.Size = new System.Drawing.Size(285, 24);
            this.PilihJadwalComboBox.TabIndex = 30;
            // 
            // NamaPasienComboBox
            // 
            this.NamaPasienComboBox.FormattingEnabled = true;
            this.NamaPasienComboBox.Location = new System.Drawing.Point(614, 82);
            this.NamaPasienComboBox.Name = "NamaPasienComboBox";
            this.NamaPasienComboBox.Size = new System.Drawing.Size(285, 24);
            this.NamaPasienComboBox.TabIndex = 31;
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Snow;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnBack.Location = new System.Drawing.Point(12, 7);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(124, 50);
            this.BtnBack.TabIndex = 43;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // FormPendaftaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1333, 565);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.NamaPasienComboBox);
            this.Controls.Add(this.PilihJadwalComboBox);
            this.Controls.Add(this.NomorAntrianTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtnHapus);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.BtnTambah);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TanggalPendaftaranTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormPendaftaran";
            this.Text = "FormPendaftaran";
            this.Load += new System.EventHandler(this.FormPendaftaran_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TanggalPendaftaranTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnHapus;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button BtnTambah;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox NomorAntrianTextBox;
        private System.Windows.Forms.ComboBox PilihJadwalComboBox;
        private System.Windows.Forms.ComboBox NamaPasienComboBox;
        private System.Windows.Forms.Button BtnBack;
    }
}