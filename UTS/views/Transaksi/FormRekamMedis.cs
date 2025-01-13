using System;
using System.Data;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class Rekam_Medis : Form
    {
        private readonly RekamMedisController rekamMedisController;
        private int? selectedId;
        private int? selectedDokter;

        public Rekam_Medis()
        {
            InitializeComponent();
            rekamMedisController = new RekamMedisController();

            // Setup DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Setup event handlers
            //BtnTambah.Click += BtnTambah_Click;
            //BtnUpdate.Click += BtnUpdate_Click;
            //BtnEdit.Click += BtnEdit_Click;
            //BtnHapus.Click += BtnHapus_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
            NamaPendaftarComboBox.SelectedIndexChanged += NamaPendaftarComboBox_SelectedIndexChanged;

            // Set current date
            TanggalTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TanggalTextBox.ReadOnly = true;
        }

        private void Rekam_Medis_Load(object sender, EventArgs e)
        {
            LoadInitialData();
            SetInitialState();
        }

        private void LoadInitialData()
        {
            try
            {
                // Load ComboBox Pendaftaran (hanya yang belum memiliki rekam medis)
                var pendaftaranData = rekamMedisController.GetPendaftaranForComboBox();

                // Reset ComboBox
                NamaPendaftarComboBox.DataSource = null;
                NamaPendaftarComboBox.Items.Clear();

                // Set new data
                NamaPendaftarComboBox.DataSource = pendaftaranData;
                NamaPendaftarComboBox.DisplayMember = "info_pendaftaran";
                NamaPendaftarComboBox.ValueMember = "id_pendaftaran";

                if (pendaftaranData.Rows.Count > 0 &&
                    pendaftaranData.Columns.Contains("id_dokter"))
                {
                    selectedDokter = Convert.ToInt32(pendaftaranData.Rows[0]["id_dokter"]);
                }

                NamaPendaftarComboBox.SelectedIndex = -1;

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
                dataGridView1.DataSource = rekamMedisController.GetAllRekamMedis();
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
                dataGridView1.Columns["id_rekam_medis"].HeaderText = "ID";
                dataGridView1.Columns["id_pendaftaran"].Visible = false;
                dataGridView1.Columns["id_dokter"].Visible = false;
                dataGridView1.Columns["nama_pasien"].HeaderText = "Nama Pasien";
                dataGridView1.Columns["nama_dokter"].HeaderText = "Dokter";
                dataGridView1.Columns["nomor_antrian"].HeaderText = "No Antrian";
                dataGridView1.Columns["jadwal_periksa"].HeaderText = "Tanggal Periksa";
                dataGridView1.Columns["keluhan"].HeaderText = "Keluhan";
                dataGridView1.Columns["diagnosa"].HeaderText = "Diagnosa";
                dataGridView1.Columns["tindakan"].HeaderText = "Tindakan";
                dataGridView1.Columns["resep"].HeaderText = "Resep";
                dataGridView1.Columns["tanggal_rekam"].HeaderText = "Tanggal Rekam";

            }
        }

        private void SetInitialState()
        {
            NamaPendaftarComboBox.SelectedIndex = -1;
            KeluhanTextBox.Clear();
            DiagnosaTextBox.Clear();
            TindakanTextBox.Clear();
            ResepTextBox.Clear();

            selectedId = null;
            selectedDokter = null;

            BtnTambah.Enabled = true;
            BtnUpdate.Enabled = false;
            BtnEdit.Enabled = false;
            BtnHapus.Enabled = false;

            NamaPendaftarComboBox.Enabled = true;
            KeluhanTextBox.ReadOnly = false;
            DiagnosaTextBox.ReadOnly = false;
            TindakanTextBox.ReadOnly = false;
            ResepTextBox.ReadOnly = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["id_rekam_medis"].Value);

                BtnEdit.Enabled = true;
                BtnHapus.Enabled = true;
            }
        }

        private bool ValidateInput()
        {
            if (NamaPendaftarComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih pendaftaran!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(KeluhanTextBox.Text))
            {
                MessageBox.Show("Keluhan harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(DiagnosaTextBox.Text))
            {
                MessageBox.Show("Diagnosa harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var rekamMedis = new RekamMedis
                {
                    IdPendaftaran = Convert.ToInt32(NamaPendaftarComboBox.SelectedValue),
                    IdDokter = selectedDokter.Value,
                    Keluhan = KeluhanTextBox.Text,
                    Diagnosa = DiagnosaTextBox.Text,
                    Resep = string.IsNullOrWhiteSpace(ResepTextBox.Text) ?
                        null : ResepTextBox.Text,
                    Tindakan = string.IsNullOrWhiteSpace(TindakanTextBox.Text) ?
                        null : TindakanTextBox.Text,
                    TanggalRekam = DateTime.Now
                };

                rekamMedisController.AddRekamMedis(rekamMedis);
                MessageBox.Show("Data rekam medis berhasil ditambahkan!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh data setelah penambahan
                LoadInitialData();
                SetInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding rekam medis: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var row = dataGridView1.SelectedRows[0];

                // Set nilai ComboBox Pendaftaran
                foreach (DataRowView item in NamaPendaftarComboBox.Items)
                {
                    if (Convert.ToInt32(item.Row["id_pendaftaran"]) ==
                        Convert.ToInt32(row.Cells["id_pendaftaran"].Value))
                    {
                        NamaPendaftarComboBox.SelectedItem = item;
                        break;
                    }
                }

                KeluhanTextBox.Text = row.Cells["keluhan"].Value?.ToString();
                DiagnosaTextBox.Text = row.Cells["diagnosa"].Value?.ToString();
                TindakanTextBox.Text = row.Cells["tindakan"].Value?.ToString();
                ResepTextBox.Text = row.Cells["resep"].Value?.ToString();

                // Disable ComboBox
                NamaPendaftarComboBox.Enabled = false;

                // Set button states
                BtnTambah.Enabled = false;
                BtnUpdate.Enabled = true;
                BtnEdit.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing rekam medis: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue || !ValidateInput()) return;

            try
            {
                var rekamMedis = new RekamMedis
                {
                    IdRekamMedis = selectedId.Value,
                    Keluhan = KeluhanTextBox.Text,
                    Diagnosa = DiagnosaTextBox.Text,
                    Tindakan = string.IsNullOrWhiteSpace(TindakanTextBox.Text) ?
                        null : TindakanTextBox.Text,
                    Resep = string.IsNullOrWhiteSpace(ResepTextBox.Text) ?
                        null : ResepTextBox.Text
                };

                rekamMedisController.UpdateRekamMedis(rekamMedis);
                MessageBox.Show("Data rekam medis berhasil diupdate!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshDataGridView();
                SetInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating rekam medis: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus data rekam medis ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    rekamMedisController.DeleteRekamMedis(selectedId.Value);
                    MessageBox.Show("Data rekam medis berhasil dihapus!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshDataGridView();
                    SetInitialState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting rekam medis: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NamaPendaftarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NamaPendaftarComboBox.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)NamaPendaftarComboBox.SelectedItem;
                selectedDokter = Convert.ToInt32(selectedRow["id_dokter"]);
            }
        }

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
    }
}