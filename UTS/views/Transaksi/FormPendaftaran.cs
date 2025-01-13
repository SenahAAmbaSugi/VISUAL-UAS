using System;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class FormPendaftaran : Form
    {
        private readonly PendaftaranController pendaftaranController;
        private int? selectedId;

        public FormPendaftaran()
        {
            InitializeComponent();
            pendaftaranController = new PendaftaranController();

            // Set up DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            // Set up event handlers
            dataGridView1.CellClick += DataGridView1_CellClick;
            BtnTambah.Click += BtnTambah_Click;
            BtnHapus.Click += BtnHapus_Click;
            BtnBack.Click += BtnBack_Click;

            // Set initial state
            LoadInitialData();
            SetInitialState();
        }

        private void RefreshJadwalComboBox()
        {
            try
            {
                // Simpan nilai yang dipilih sebelumnya
                var selectedValue = PilihJadwalComboBox.SelectedValue;

                // Refresh data jadwal
                var jadwalData = pendaftaranController.GetJadwalForComboBox();
                PilihJadwalComboBox.DataSource = jadwalData;
                PilihJadwalComboBox.DisplayMember = "jadwal_info";
                PilihJadwalComboBox.ValueMember = "id_jadwal";

                // Kembalikan nilai yang dipilih jika masih ada dalam daftar
                if (selectedValue != null && jadwalData.Select($"id_jadwal = {selectedValue}").Length > 0)
                {
                    PilihJadwalComboBox.SelectedValue = selectedValue;
                }
                else
                {
                    PilihJadwalComboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing jadwal: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                // Load ComboBox Pasien
                var pasienData = pendaftaranController.GetPasienForComboBox();
                NamaPasienComboBox.DataSource = pasienData;
                NamaPasienComboBox.DisplayMember = "nama_pasien";
                NamaPasienComboBox.ValueMember = "id_pasien";
                NamaPasienComboBox.SelectedIndex = -1;

                // Load ComboBox Jadwal
                var jadwalData = pendaftaranController.GetJadwalForComboBox();
                PilihJadwalComboBox.DataSource = jadwalData;
                PilihJadwalComboBox.DisplayMember = "jadwal_info";
                PilihJadwalComboBox.ValueMember = "id_jadwal";
                PilihJadwalComboBox.SelectedIndex = -1;

                // Set tanggal pendaftaran ke hari ini
                TanggalPendaftaranTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TanggalPendaftaranTextBox.ReadOnly = true;

                // Load DataGridView
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            try
            {
                dataGridView1.DataSource = pendaftaranController.GetAllPendaftaran();
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id_pendaftaran"].HeaderText = "ID";
                dataGridView1.Columns["id_pasien"].Visible = false;
                dataGridView1.Columns["id_jadwal"].Visible = false;
                dataGridView1.Columns["nama_pasien"].HeaderText = "Nama Pasien";
                dataGridView1.Columns["nama_dokter"].HeaderText = "Dokter";
                dataGridView1.Columns["hari_tanggal"].HeaderText = "Jadwal";
                dataGridView1.Columns["tanggal_pendaftaran"].HeaderText = "Tgl Daftar";
                dataGridView1.Columns["nomor_antrian"].HeaderText = "No Antrian";
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
            }
        }

        private void SetInitialState()
        {
            NamaPasienComboBox.SelectedIndex = -1;
            PilihJadwalComboBox.SelectedIndex = -1;
            NomorAntrianTextBox.Clear();
            NomorAntrianTextBox.ReadOnly = true;

            selectedId = null;
            BtnHapus.Enabled = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["id_pendaftaran"].Value);
                BtnHapus.Enabled = true;
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                int idPasien = Convert.ToInt32(NamaPasienComboBox.SelectedValue);
                int idJadwal = Convert.ToInt32(PilihJadwalComboBox.SelectedValue);
                DateTime tanggalPendaftaran = DateTime.Now.Date;

                // Check kuota
                if (!pendaftaranController.IsKuotaAvailable(idJadwal, tanggalPendaftaran))
                {
                    MessageBox.Show("Kuota pendaftaran untuk jadwal ini sudah penuh!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get nomor antrian berikutnya
                int nomorAntrian = pendaftaranController.GetNextNomorAntrian(idJadwal, tanggalPendaftaran);

                var pendaftaran = new Pendaftaran
                {
                    IdPasien = idPasien,
                    IdJadwal = idJadwal,
                    TanggalPendaftaran = tanggalPendaftaran,
                    NomorAntrian = nomorAntrian
                };

                pendaftaranController.AddPendaftaran(pendaftaran);
                MessageBox.Show($"Pendaftaran berhasil! Nomor antrian Anda: {nomorAntrian}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshDataGridView();
                RefreshJadwalComboBox();
                SetInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pendaftaran: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus pendaftaran ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    pendaftaranController.DeletePendaftaran(selectedId.Value);
                    MessageBox.Show("Pendaftaran berhasil dihapus!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshDataGridView();
                    SetInitialState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting pendaftaran: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (NamaPasienComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih pasien!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (PilihJadwalComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih jadwal praktek!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to dashboard: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPendaftaran_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }
    }
}