using System;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class FormDataPasien : Form
    {
        // Deklarasi controller untuk mengelola data pasien
        private readonly PasienController pasienController;
        // Variabel untuk menyimpan ID pasien yang dipilih
        private int? selectedId;
        // Variabel untuk menyimpan nama pasien yang dipilih
        private string selectedNama;

        // Constructor form data pasien
        public FormDataPasien()
        {
            InitializeComponent();
            pasienController = new PasienController();

            // Pengaturan DataGridView untuk seleksi baris penuh
            dataGridViewPasien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPasien.MultiSelect = false;

            // Inisialisasi event handlers
            InitializeEvents();
            // Load data awal
            LoadData();
            // Set status awal form
            SetInitialState();
        }

        // Method untuk menginisialisasi event handlers
        private void InitializeEvents()
        {
            // Event untuk cell click di grid
            dataGridViewPasien.CellClick += DataGridViewPasien_CellClick;
            // Event untuk tombol-tombol CRUD
            btnTambah.Click += BtnTambah_Click;
            btnEdit.Click += BtnEdit_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnHapus.Click += BtnHapus_Click;
        }

        // Method untuk mengatur status awal form
        private void SetInitialState()
        {
            // Reset textbox
            SetTextBoxesReadOnly(false);

            // Set status awal tombol
            btnTambah.Enabled = true;
            btnUpdate.Enabled = false;
            btnHapus.Enabled = false;
            btnEdit.Enabled = false;
        }

        // Method untuk mengatur status readonly textbox
        private void SetTextBoxesReadOnly(bool readOnly)
        {
            txtNamaPasien.ReadOnly = readOnly;
            txtNomorKartu.ReadOnly = readOnly;
            txtAlamat.ReadOnly = readOnly;
            txtUmur.ReadOnly = readOnly;
            NoTeleponTextBox.ReadOnly = readOnly;
            EmailTextBox.ReadOnly = readOnly;
            JenisKelaminComboBox.Enabled = !readOnly;
            TanggalLahirdateTimePicker.Enabled = !readOnly;
        }

        // Method untuk memuat data pasien ke grid
        private void LoadData()
        {
            try
            {
                var pasienList = pasienController.GetAllPasien();
                dataGridViewPasien.DataSource = pasienList;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method untuk memformat tampilan grid
        private void FormatDataGridView()
        {
            if (dataGridViewPasien.ColumnCount > 0)
            {
                // Set header dan format untuk setiap kolom
                dataGridViewPasien.Columns["IdPasien"].HeaderText = "ID";
                dataGridViewPasien.Columns["NamaPasien"].HeaderText = "Nama Pasien";
                dataGridViewPasien.Columns["NomorKartu"].HeaderText = "Nomor Kartu";
                dataGridViewPasien.Columns["JenisKelamin"].HeaderText = "Jenis Kelamin";
                dataGridViewPasien.Columns["TanggalLahir"].HeaderText = "Tanggal Lahir";
                dataGridViewPasien.Columns["NoTelepon"].HeaderText = "No Telepon";
                dataGridViewPasien.Columns["Email"].HeaderText = "Email";
            }
        }

        // Method untuk membersihkan form
        private void ClearForm()
        {
            selectedId = null;
            selectedNama = null;
            txtNamaPasien.Clear();
            txtNomorKartu.Clear();
            txtAlamat.Clear();
            txtUmur.Clear();
            NoTeleponTextBox.Clear();
            EmailTextBox.Clear();
            JenisKelaminComboBox.SelectedIndex = -1;
            TanggalLahirdateTimePicker.Value = DateTime.Now;

            SetTextBoxesReadOnly(false);
            btnTambah.Enabled = true;
            btnUpdate.Enabled = false;
            btnHapus.Enabled = false;
            btnEdit.Enabled = false;
        }

        // Event handler untuk cell click di grid
        private void DataGridViewPasien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewPasien.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["IdPasien"].Value);
                selectedNama = row.Cells["NamaPasien"].Value.ToString();

                btnEdit.Enabled = true;
                btnHapus.Enabled = true;
            }
        }

        // Event handler untuk tombol tambah
        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var pasien = new Pasien
                {
                    NamaPasien = txtNamaPasien.Text,
                    NomorKartu = txtNomorKartu.Text,
                    Alamat = txtAlamat.Text,
                    JenisKelamin = JenisKelaminComboBox.SelectedItem.ToString(),
                    TanggalLahir = TanggalLahirdateTimePicker.Value,
                    NoTelepon = NoTeleponTextBox.Text,
                    Email = EmailTextBox.Text,
                    Umur = !string.IsNullOrEmpty(txtUmur.Text) ?
                           Convert.ToInt32(txtUmur.Text) : (int?)null
                };

                pasienController.AddPasien(pasien);
                MessageBox.Show("Data pasien berhasil ditambahkan!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol edit
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var row = dataGridViewPasien.SelectedRows[0];
                txtNamaPasien.Text = row.Cells["NamaPasien"].Value?.ToString();
                txtNomorKartu.Text = row.Cells["NomorKartu"].Value?.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value?.ToString();
                txtUmur.Text = row.Cells["Umur"].Value?.ToString();
                JenisKelaminComboBox.Text = row.Cells["JenisKelamin"].Value?.ToString();
                NoTeleponTextBox.Text = row.Cells["NoTelepon"].Value?.ToString();
                EmailTextBox.Text = row.Cells["Email"].Value?.ToString();
                if (row.Cells["TanggalLahir"].Value != DBNull.Value)
                    TanggalLahirdateTimePicker.Value = Convert.ToDateTime(row.Cells["TanggalLahir"].Value);

                SetTextBoxesReadOnly(false);
                btnTambah.Enabled = false;
                btnUpdate.Enabled = true;
                btnEdit.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol update
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // Insert pasien melewati Model.pasien
                var pasien = new Pasien
                {
                    IdPasien = selectedId.Value,
                    NamaPasien = txtNamaPasien.Text,
                    NomorKartu = txtNomorKartu.Text,
                    Alamat = txtAlamat.Text,
                    JenisKelamin = JenisKelaminComboBox.SelectedItem.ToString(),
                    TanggalLahir = TanggalLahirdateTimePicker.Value,
                    NoTelepon = NoTeleponTextBox.Text,
                    Email = EmailTextBox.Text,
                    Umur = !string.IsNullOrEmpty(txtUmur.Text) ?
                           Convert.ToInt32(txtUmur.Text) : (int?)null
                };

                pasienController.UpdatePasien(pasien);
                MessageBox.Show("Data pasien berhasil diupdate!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol hapus
        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var result = MessageBox.Show(
                    $"Apakah anda yakin ingin menghapus pasien {selectedNama}?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    pasienController.DeletePasien(selectedId.Value);
                    MessageBox.Show("Data pasien berhasil dihapus!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk tombol back
        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler saat form dimuat
        private void FormDataPasien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Validasi input form
        private bool ValidateInput()
        {
            // Validasi nama pasien
            if (string.IsNullOrWhiteSpace(txtNamaPasien.Text))
            {
                MessageBox.Show("Nama Pasien harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi nomor kartu
            if (string.IsNullOrWhiteSpace(txtNomorKartu.Text))
            {
                MessageBox.Show("Nomor Kartu harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi jenis kelamin
            if (JenisKelaminComboBox.SelectedItem == null)
            {
                MessageBox.Show("Jenis Kelamin harus dipilih!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi nomor telepon
            if (!string.IsNullOrWhiteSpace(NoTeleponTextBox.Text))
            {
                string noTelp = NoTeleponTextBox.Text;
                if (!(noTelp.StartsWith("08") || noTelp.StartsWith("62")))
                {
                    MessageBox.Show("Nomor Telepon harus dimulai dengan '08' atau '62'!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (noTelp.Length < 10 || noTelp.Length > 13)
                {
                    MessageBox.Show("Nomor Telepon harus antara 10-13 digit!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Validasi email
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(EmailTextBox.Text);
                    if (addr.Address != EmailTextBox.Text)
                    {
                        MessageBox.Show("Format Email tidak valid!", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Format Email tidak valid!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}