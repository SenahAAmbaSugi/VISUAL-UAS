namespace UTS
{
    partial class FormDataPasien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataPasien));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewPasien = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamaPasien = new System.Windows.Forms.TextBox();
            this.txtNomorKartu = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.txtUmur = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.NoTeleponTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.JenisKelaminComboBox = new System.Windows.Forms.ComboBox();
            this.TanggalLahirdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPasien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(377, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 26);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Swis721 Hv BT", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(482, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "DATA PASIEN";
            // 
            // dataGridViewPasien
            // 
            this.dataGridViewPasien.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewPasien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPasien.Location = new System.Drawing.Point(396, 276);
            this.dataGridViewPasien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewPasien.Name = "dataGridViewPasien";
            this.dataGridViewPasien.RowHeadersWidth = 82;
            this.dataGridViewPasien.RowTemplate.Height = 33;
            this.dataGridViewPasien.Size = new System.Drawing.Size(1012, 258);
            this.dataGridViewPasien.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(424, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "NAMA PASIEN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(424, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "NOMOR KARTU";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(424, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "UMUR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(424, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "ALAMAT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNamaPasien
            // 
            this.txtNamaPasien.Location = new System.Drawing.Point(555, 66);
            this.txtNamaPasien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNamaPasien.Multiline = true;
            this.txtNamaPasien.Name = "txtNamaPasien";
            this.txtNamaPasien.Size = new System.Drawing.Size(287, 29);
            this.txtNamaPasien.TabIndex = 9;
            // 
            // txtNomorKartu
            // 
            this.txtNomorKartu.Location = new System.Drawing.Point(555, 103);
            this.txtNomorKartu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNomorKartu.Multiline = true;
            this.txtNomorKartu.Name = "txtNomorKartu";
            this.txtNomorKartu.Size = new System.Drawing.Size(287, 29);
            this.txtNomorKartu.TabIndex = 10;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(555, 139);
            this.txtAlamat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(287, 29);
            this.txtAlamat.TabIndex = 11;
            // 
            // txtUmur
            // 
            this.txtUmur.Location = new System.Drawing.Point(555, 178);
            this.txtUmur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUmur.Multiline = true;
            this.txtUmur.Name = "txtUmur";
            this.txtUmur.Size = new System.Drawing.Size(287, 29);
            this.txtUmur.TabIndex = 12;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnTambah.Location = new System.Drawing.Point(944, 228);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(93, 31);
            this.btnTambah.TabIndex = 13;
            this.btnTambah.Text = "TAMBAH";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.YellowGreen;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnUpdate.Location = new System.Drawing.Point(1163, 228);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 31);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Yellow;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnEdit.Location = new System.Drawing.Point(1055, 225);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 34);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.Red;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnHapus.Location = new System.Drawing.Point(1283, 225);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(93, 38);
            this.btnHapus.TabIndex = 16;
            this.btnHapus.Text = "HAPUS";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(375, 538);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Snow;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.SystemColors.InfoText;
            this.BtnBack.Location = new System.Drawing.Point(12, 11);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(124, 50);
            this.BtnBack.TabIndex = 17;
            this.BtnBack.Text = "BACK";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(1079, 177);
            this.EmailTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EmailTextBox.Multiline = true;
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(287, 29);
            this.EmailTextBox.TabIndex = 25;
            // 
            // NoTeleponTextBox
            // 
            this.NoTeleponTextBox.Location = new System.Drawing.Point(1079, 139);
            this.NoTeleponTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NoTeleponTextBox.Multiline = true;
            this.NoTeleponTextBox.Name = "NoTeleponTextBox";
            this.NoTeleponTextBox.Size = new System.Drawing.Size(287, 29);
            this.NoTeleponTextBox.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(941, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "NO TELEPON";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(941, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "EMAIL (opsi)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(941, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "TANGGAL LAHIR";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(941, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "JENIS KELAMIN";
            // 
            // JenisKelaminComboBox
            // 
            this.JenisKelaminComboBox.FormattingEnabled = true;
            this.JenisKelaminComboBox.Items.AddRange(new object[] {
            "Laki-Laki",
            "Perempuan"});
            this.JenisKelaminComboBox.Location = new System.Drawing.Point(1079, 66);
            this.JenisKelaminComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.JenisKelaminComboBox.Name = "JenisKelaminComboBox";
            this.JenisKelaminComboBox.Size = new System.Drawing.Size(286, 24);
            this.JenisKelaminComboBox.TabIndex = 41;
            // 
            // TanggalLahirdateTimePicker
            // 
            this.TanggalLahirdateTimePicker.Location = new System.Drawing.Point(1079, 102);
            this.TanggalLahirdateTimePicker.Name = "TanggalLahirdateTimePicker";
            this.TanggalLahirdateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.TanggalLahirdateTimePicker.TabIndex = 43;
            // 
            // FormDataPasien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1424, 542);
            this.Controls.Add(this.TanggalLahirdateTimePicker);
            this.Controls.Add(this.JenisKelaminComboBox);
            this.Controls.Add(this.EmailTextBox);
            this.Controls.Add(this.NoTeleponTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtUmur);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.txtNomorKartu);
            this.Controls.Add(this.txtNamaPasien);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewPasien);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormDataPasien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Pasien";
            this.Load += new System.EventHandler(this.FormDataPasien_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPasien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewPasien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamaPasien;
        private System.Windows.Forms.TextBox txtNomorKartu;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.TextBox txtUmur;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.TextBox NoTeleponTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox JenisKelaminComboBox;
        private System.Windows.Forms.DateTimePicker TanggalLahirdateTimePicker;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}