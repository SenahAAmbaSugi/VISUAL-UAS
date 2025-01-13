using System;
using System.Data;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class FormJadwalPraktek : Form
    {
        private readonly JadwalPraktekController jadwalController;
        private int? selectedId;  // Hanya menyimpan ini sebagai state

        public FormJadwalPraktek()
        {
            InitializeComponent();
            jadwalController = new JadwalPraktekController();

            // Set up DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;

            InitializeControls();
            SetupEventHandlers();
            LoadInitialData();
        }

        private void InitializeControls()
        {
            // Setup ComboBoxes
            StatusJadwalComboBox.Items.AddRange(new string[] { "Aktif", "Non Aktif" });
            StatusJadwalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            var timeSlots = jadwalController.GetAvailableTimeSlots();
            JamMulaicomboBox.Items.AddRange(timeSlots.ToArray());
            JamSelesaicomboBox.Items.AddRange(timeSlots.ToArray());
            JamMulaicomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            JamSelesaicomboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set default values
            HariTanggaldateTimePicker.Value = DateTime.Today;
            ClearForm();
        }

        private void SetupEventHandlers()
        {
            // Button Events
            button1.Click += BtnTambah_Click;
            button2.Click += BtnUpdate_Click;
            button3.Click += BtnEdit_Click;
            button4.Click += BtnHapus_Click;

            // Grid Events
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Input Validation Events
            KoutaPasientextBox.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        private void LoadInitialData()
        {
            try
            {
                // Load Dokter ComboBox
                var dokterData = jadwalController.GetDokterForComboBox();
                NamaDoktercomboBox.DataSource = dokterData;
                NamaDoktercomboBox.DisplayMember = "nama_dokter";
                NamaDoktercomboBox.ValueMember = "id_dokter";
                NamaDoktercomboBox.SelectedIndex = -1;

                // Load GridView
                RefreshGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGridData()
        {
            try
            {
                dataGridView1.DataSource = jadwalController.GetAllJadwalPraktek();
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
                dataGridView1.Columns["id_jadwal"].HeaderText = "ID";
                dataGridView1.Columns["id_jadwal"].Width = 50;
                dataGridView1.Columns["id_dokter"].Visible = false;
                dataGridView1.Columns["nama_dokter"].HeaderText = "Nama Dokter";
                dataGridView1.Columns["nama_dokter"].Width = 150;
                dataGridView1.Columns["hari_tanggal"].HeaderText = "Tanggal";
                dataGridView1.Columns["hari_tanggal"].Width = 100;
                dataGridView1.Columns["jam_mulai"].HeaderText = "Jam Mulai";
                dataGridView1.Columns["jam_mulai"].Width = 80;
                dataGridView1.Columns["jam_selesai"].HeaderText = "Jam Selesai";
                dataGridView1.Columns["jam_selesai"].Width = 80;
                dataGridView1.Columns["status_jadwal"].HeaderText = "Status";
                dataGridView1.Columns["status_jadwal"].Width = 80;
                dataGridView1.Columns["kuota_pasien"].HeaderText = "Kuota";
                dataGridView1.Columns["kuota_pasien"].Width = 60;
                dataGridView1.Columns["keterangan"].HeaderText = "Keterangan";
                dataGridView1.Columns["keterangan"].Width = 200;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["id_jadwal"].Value != DBNull.Value)
                {
                    selectedId = Convert.ToInt32(row.Cells["id_jadwal"].Value);
                    button3.Enabled = true;  // Enable Edit button
                    button4.Enabled = true;  // Enable Delete button
                }
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var jadwal = new JadwalPraktek
                {
                    IdDokter = Convert.ToInt32(NamaDoktercomboBox.SelectedValue),
                    HariTanggal = HariTanggaldateTimePicker.Value.Date,
                    JamMulai = TimeSpan.Parse(JamMulaicomboBox.Text),
                    JamSelesai = TimeSpan.Parse(JamSelesaicomboBox.Text),
                    StatusJadwal = StatusJadwalComboBox.Text,
                    KuotaPasien = Convert.ToInt32(KoutaPasientextBox.Text),
                    Keterangan = KeterangantextBox.Text
                };

                if (jadwalController.IsJadwalExists(jadwal.IdDokter, jadwal.HariTanggal,
                    jadwal.JamMulai, jadwal.JamSelesai))
                {
                    MessageBox.Show("Jadwal bentrok dengan jadwal yang sudah ada!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jadwalController.AddJadwalPraktek(jadwal);
                MessageBox.Show("Jadwal berhasil ditambahkan!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                RefreshGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding jadwal: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if(!selectedId.HasValue) return;

            try
            {
                var row = dataGridView1.SelectedRows[0];

                // Set form controls
                if (row.Cells["id_dokter"].Value != DBNull.Value)
                    NamaDoktercomboBox.SelectedValue = row.Cells["id_dokter"].Value;

                if (row.Cells["hari_tanggal"].Value != DBNull.Value)
                    HariTanggaldateTimePicker.Value = Convert.ToDateTime(row.Cells["hari_tanggal"].Value);

                if (row.Cells["jam_mulai"].Value != DBNull.Value)
                {
                    var jamMulai = row.Cells["jam_mulai"].Value.ToString();
                    JamMulaicomboBox.Text = DateTime.Parse(jamMulai).ToString("HH:mm");
                }

                if (row.Cells["jam_selesai"].Value != DBNull.Value)
                {
                    var jamSelesai = row.Cells["jam_selesai"].Value.ToString();
                    JamSelesaicomboBox.Text = DateTime.Parse(jamSelesai).ToString("HH:mm");
                }

                if (row.Cells["status_jadwal"].Value != DBNull.Value)
                    StatusJadwalComboBox.Text = row.Cells["status_jadwal"].Value.ToString();

                if (row.Cells["kuota_pasien"].Value != DBNull.Value)
                    KoutaPasientextBox.Text = row.Cells["kuota_pasien"].Value.ToString();

                KeterangantextBox.Text = row.Cells["keterangan"].Value?.ToString() ?? "";

                // Disable and enable buttons
                button1.Enabled = false;  // Disable Tambah
                button2.Enabled = true;   // Enable Update
                button3.Enabled = false;  // Disable Edit
            }
            catch (Exception ex)

            {
                MessageBox.Show($"Error editing jadwal: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue || !ValidateInput()) return;

            try
            {
                var jadwal = new JadwalPraktek
                {
                    IdJadwal = selectedId.Value,
                    IdDokter = Convert.ToInt32(NamaDoktercomboBox.SelectedValue),
                    HariTanggal = HariTanggaldateTimePicker.Value.Date,
                    JamMulai = TimeSpan.Parse(JamMulaicomboBox.Text),
                    JamSelesai = TimeSpan.Parse(JamSelesaicomboBox.Text),
                    StatusJadwal = StatusJadwalComboBox.Text,
                    KuotaPasien = Convert.ToInt32(KoutaPasientextBox.Text),
                    Keterangan = KeterangantextBox.Text
                };

                if (jadwalController.IsJadwalExists(jadwal.IdDokter, jadwal.HariTanggal,
                    jadwal.JamMulai, jadwal.JamSelesai, jadwal.IdJadwal))
                {
                    MessageBox.Show("Jadwal bentrok dengan jadwal yang sudah ada!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jadwalController.UpdateJadwalPraktek(jadwal);
                MessageBox.Show("Jadwal berhasil diupdate!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                RefreshGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating jadwal: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var result = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus jadwal ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    jadwalController.DeleteJadwalPraktek(selectedId.Value);
                    MessageBox.Show("Jadwal berhasil dihapus!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    RefreshGridData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting jadwal: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            NamaDoktercomboBox.SelectedIndex = -1;
            HariTanggaldateTimePicker.Value = DateTime.Today;
            JamMulaicomboBox.SelectedIndex = -1;
            JamSelesaicomboBox.SelectedIndex = -1;
            StatusJadwalComboBox.SelectedIndex = -1;
            KoutaPasientextBox.Clear();
            KeterangantextBox.Clear();

            selectedId = null;
            // Removed isEditMode reference since it's not needed
            button1.Enabled = true;   // Enable Tambah
            button2.Enabled = false;  // Disable Update
            button3.Enabled = false;  // Disable Edit
            button4.Enabled = false;  // Disable Delete
        }

        private bool ValidateInput()
        {
            if (NamaDoktercomboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih dokter!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (JamMulaicomboBox.SelectedIndex == -1 || JamSelesaicomboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih jam mulai dan jam selesai!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(StatusJadwalComboBox.Text))
            {
                MessageBox.Show("Status jadwal harus dipilih!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(KoutaPasientextBox.Text))
            {
                MessageBox.Show("Kuota pasien harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(KoutaPasientextBox.Text, out int kuota) || kuota <= 0)
            {
                MessageBox.Show("Kuota pasien harus berupa angka positif!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            TimeSpan jamMulai = TimeSpan.Parse(JamMulaicomboBox.Text);
            TimeSpan jamSelesai = TimeSpan.Parse(JamSelesaicomboBox.Text);
            if (jamSelesai <= jamMulai)
            {
                MessageBox.Show("Jam selesai harus lebih besar dari jam mulai!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void FormJadwalPraktek_Load(object sender, EventArgs e)
        {
            LoadInitialData();
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