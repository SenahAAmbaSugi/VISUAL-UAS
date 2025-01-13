using System;
using System.Data;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class FormPembayaran : Form
    {
        private readonly PembayaranController pembayaranController;
        private int? selectedId;
        private int? selectedIdPasien;
        private int? selectedIdRekamMedis;
        private string selectedNama;
        private bool isLoading = false;

        public FormPembayaran()
        {
            InitializeComponent();
            pembayaranController = new PembayaranController();

            // Setup DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;  
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            InitializeComboBoxes();
            SetupEventHandlers();
            LoadInitialData();
            SetInitialState();
        }

        private void InitializeComboBoxes()
        {
            // Status Pembayaran Setup
            StatusPembayaranComboBox.Items.AddRange(new string[] { "Lunas", "Belum Lunas", "Cicilan" });
            StatusPembayaranComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // RekamMedis ComboBox Setup
            RekamMedisComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RekamMedisComboBox.DisplayMember = "info_rekam_medis";
            RekamMedisComboBox.ValueMember = "id_rekam_medis";

            // NamaPasien ComboBox Setup
            NamaPasienComboBox.DisplayMember = "nama_pasien";
            NamaPasienComboBox.ValueMember = "id_pasien";
            NamaPasienComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetupEventHandlers()
        {
            // ComboBox Events
            NamaPasienComboBox.SelectedIndexChanged += NamaPasienComboBox_SelectedIndexChanged;
            RekamMedisComboBox.SelectedIndexChanged += RekamMedisComboBox_SelectedIndexChanged;

            // Numeric TextBox Events
            BiayaKonsultasiTextBox.KeyPress += NumericOnly_KeyPress;
            BiayaTindakanTextBox.KeyPress += NumericOnly_KeyPress;
            BiayaKonsultasiTextBox.TextChanged += BiayaTextBox_TextChanged;
            BiayaTindakanTextBox.TextChanged += BiayaTextBox_TextChanged;

            // Grid Events
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Button Events
            BtnTambah.Click += BtnTambah_Click;
            BtnUpdate.Click += BtnUpdate_Click;
            BtnEdit.Click += BtnEdit_Click;
            BtnHapus.Click += BtnHapus_Click;
        }

        private void LoadInitialData()
        {
            try
            {
                // Load NamaPasien ComboBox
                var pasienData = pembayaranController.GetPasienForComboBox();
                NamaPasienComboBox.DataSource = pasienData;

                // Set current date
                TanggalPembayaranTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // Generate new payment number
                NoPembayaranTextBox.Text = pembayaranController.GenerateNoPembayaran();

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
                var dt = pembayaranController.GetAllPembayaran();
                dataGridView1.DataSource = dt;
                FormatDataGridView();

                // Tambahkan konfigurasi untuk mencegah baris kosong
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NamaPasienComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading || NamaPasienComboBox.SelectedValue == null) return;

            try
            {
                isLoading = true;
                selectedIdPasien = Convert.ToInt32(NamaPasienComboBox.SelectedValue);
                LoadRekamMedisList(selectedIdPasien.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rekam medis: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void LoadRekamMedisList(int idPasien, int? selectedRekamMedisId = null)
        {
            RekamMedisComboBox.DataSource = null;
            RekamMedisComboBox.Items.Clear();

            var rekamMedisData = pembayaranController.GetRekamMedisByPasien(idPasien, selectedRekamMedisId);

            if (rekamMedisData.Rows.Count > 0)
            {
                RekamMedisComboBox.DataSource = rekamMedisData;
                RekamMedisComboBox.DisplayMember = "info_rekam_medis";
                RekamMedisComboBox.ValueMember = "id_rekam_medis";

                if (selectedRekamMedisId.HasValue)
                {
                    // Find and select the specific rekam medis
                    foreach (DataRowView row in RekamMedisComboBox.Items)
                    {
                        if (Convert.ToInt32(row["id_rekam_medis"]) == selectedRekamMedisId.Value)
                        {
                            RekamMedisComboBox.SelectedItem = row;
                            break;
                        }
                    }
                }
                else
                {
                    RekamMedisComboBox.SelectedIndex = -1;
                }

                RekamMedisComboBox.Enabled = true;
            }
            else
            {
                // Buat DataTable kosong dengan struktur yang sama
                DataTable dt = new DataTable();
                dt.Columns.Add("id_rekam_medis", typeof(int));
                dt.Columns.Add("info_rekam_medis", typeof(string));

                // Tambahkan satu baris dengan pesan "Tidak ada rekam medis"
                dt.Rows.Add(-1, "Tidak ada rekam medis");

                RekamMedisComboBox.DataSource = dt;
                RekamMedisComboBox.DisplayMember = "info_rekam_medis";
                RekamMedisComboBox.ValueMember = "id_rekam_medis";
                RekamMedisComboBox.SelectedIndex = 0;
                RekamMedisComboBox.Enabled = false;
            }
        }


        private void RekamMedisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading && RekamMedisComboBox.SelectedValue != null)
            {
                selectedIdRekamMedis = Convert.ToInt32(RekamMedisComboBox.SelectedValue);
            }
        }

        private void BiayaTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal biayaKonsultasi = 0;
            decimal biayaTindakan = 0;

            if (decimal.TryParse(BiayaKonsultasiTextBox.Text, out decimal konsultasi))
            {
                biayaKonsultasi = konsultasi;
            }

            if (decimal.TryParse(BiayaTindakanTextBox.Text, out decimal tindakan))
            {
                biayaTindakan = tindakan;
            }

            decimal total = biayaKonsultasi + biayaTindakan;
            TotalBiayaTextBox.Text = total.ToString("N0");
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormatDataGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id_pembayaran"].HeaderText = "ID";
                dataGridView1.Columns["id_pembayaran"].Visible = false;
                dataGridView1.Columns["id_pasien"].Visible = false;

                dataGridView1.Columns["no_pembayaran"].HeaderText = "No Pembayaran";
                dataGridView1.Columns["nama_pasien"].HeaderText = "Nama Pasien";
                dataGridView1.Columns["id_rekam_medis"].HeaderText = "ID Rekam Medis";
                dataGridView1.Columns["tanggal_pembayaran"].HeaderText = "Tanggal";
                dataGridView1.Columns["biaya_konsultasi"].HeaderText = "Biaya Konsultasi";
                dataGridView1.Columns["biaya_tindakan"].HeaderText = "Biaya Tindakan";
                dataGridView1.Columns["total_biaya"].HeaderText = "Total Biaya";
                dataGridView1.Columns["status_pembayaran"].HeaderText = "Status";

                // Format currency columns
                dataGridView1.Columns["biaya_konsultasi"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["biaya_tindakan"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["total_biaya"].DefaultCellStyle.Format = "N0";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["id_pembayaran"].Value);
                selectedIdPasien = Convert.ToInt32(row.Cells["id_pasien"].Value);
                selectedNama = row.Cells["nama_pasien"].Value.ToString();

                BtnEdit.Enabled = true;
                BtnHapus.Enabled = true;
            }
        }

        private void SetInitialState()
        {
            isLoading = true;
            try
            {
                NoPembayaranTextBox.Text = pembayaranController.GenerateNoPembayaran();
                NamaPasienComboBox.SelectedIndex = -1;

                RekamMedisComboBox.DataSource = null;
                RekamMedisComboBox.Items.Clear();

                BiayaKonsultasiTextBox.Clear();
                BiayaTindakanTextBox.Clear();
                TotalBiayaTextBox.Clear();
                StatusPembayaranComboBox.SelectedIndex = -1;

                TanggalPembayaranTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");

                selectedId = null;
                selectedIdPasien = null;
                selectedIdRekamMedis = null;
                selectedNama = null;

                BtnTambah.Enabled = true;
                BtnUpdate.Enabled = false;
                BtnEdit.Enabled = false;
                BtnHapus.Enabled = false;

                NamaPasienComboBox.Enabled = true;
                RekamMedisComboBox.Enabled = true;
            }
            finally
            {
                isLoading = false;
            }
        }

        private bool ValidateInput()
        {
            if (NamaPasienComboBox.SelectedValue == null)
            {
                MessageBox.Show("Silakan pilih pasien!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Cek jika pasien memiliki rekam medis
            if (RekamMedisComboBox.SelectedValue == null ||
                Convert.ToInt32(RekamMedisComboBox.SelectedValue) == -1)
            {
                MessageBox.Show("Pasien harus memiliki rekam medis untuk melakukan pembayaran!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(BiayaKonsultasiTextBox.Text))
            {
                MessageBox.Show("Biaya konsultasi harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(BiayaTindakanTextBox.Text))
            {
                MessageBox.Show("Biaya tindakan harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(StatusPembayaranComboBox.Text))
            {
                MessageBox.Show("Status pembayaran harus dipilih!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var pembayaran = new Pembayaran
                {
                    NoPembayaran = NoPembayaranTextBox.Text,
                    IdPasien = (int)NamaPasienComboBox.SelectedValue,
                    IdRekamMedis = (int)RekamMedisComboBox.SelectedValue,
                    TanggalPembayaran = DateTime.Now,
                    BiayaKonsultasi = decimal.Parse(BiayaKonsultasiTextBox.Text),
                    BiayaTindakan = decimal.Parse(BiayaTindakanTextBox.Text),
                    TotalBiaya = decimal.Parse(TotalBiayaTextBox.Text.Replace(",", "")),
                    StatusPembayaran = StatusPembayaranComboBox.Text
                };

                pembayaranController.AddPembayaran(pembayaran);
                MessageBox.Show("Pembayaran berhasil ditambahkan!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshDataGridView();
                SetInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                isLoading = true;
                var row = dataGridView1.SelectedRows[0];

                NoPembayaranTextBox.Text = row.Cells["no_pembayaran"].Value?.ToString();

                // Set Nama Pasien
                NamaPasienComboBox.SelectedValue = row.Cells["id_pasien"].Value;

                // Load and set Rekam Medis
                int rekamMedisId = Convert.ToInt32(row.Cells["id_rekam_medis"].Value);
                LoadRekamMedisList((int)NamaPasienComboBox.SelectedValue, rekamMedisId);

                BiayaKonsultasiTextBox.Text = Convert.ToDecimal(row.Cells["biaya_konsultasi"].Value).ToString("N0");
                BiayaTindakanTextBox.Text = Convert.ToDecimal(row.Cells["biaya_tindakan"].Value).ToString("N0");
                StatusPembayaranComboBox.Text = row.Cells["status_pembayaran"].Value?.ToString();

                CalculateTotal();

                NamaPasienComboBox.Enabled = false;
                RekamMedisComboBox.Enabled = false;

                BtnTambah.Enabled = false;
                BtnUpdate.Enabled = true;
                BtnEdit.Enabled = false;
                BtnHapus.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing payment: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue || !ValidateInput()) return;

            try
            {
                var pembayaran = new Pembayaran
                {
                    IdPembayaran = selectedId.Value,
                    BiayaKonsultasi = decimal.Parse(BiayaKonsultasiTextBox.Text),
                    BiayaTindakan = decimal.Parse(BiayaTindakanTextBox.Text),
                    TotalBiaya = decimal.Parse(TotalBiayaTextBox.Text.Replace(",", "")),
                    StatusPembayaran = StatusPembayaranComboBox.Text
                };

                pembayaranController.UpdatePembayaran(pembayaran);
                MessageBox.Show("Pembayaran berhasil diupdate!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshDataGridView();
                SetInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var result = MessageBox.Show(
                    $"Apakah anda yakin ingin menghapus pembayaran untuk {selectedNama}?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    pembayaranController.DeletePembayaran(selectedId.Value);
                    MessageBox.Show("Pembayaran berhasil dihapus!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshDataGridView();
                    SetInitialState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting payment: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FormPembayaran_Load(object sender, EventArgs e)
        {

        }
    }
}