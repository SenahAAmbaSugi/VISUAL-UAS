using System;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;

namespace UTS
{
    public partial class FormDataDokter : Form
    {
        private readonly DokterController dokterController;
        private int? selectedId;
        private string selectedNama;

        public FormDataDokter()
        {
            InitializeComponent();
            dokterController = new DokterController();

            // Pengaturan DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Event handler untuk NoTeleponTextBox
            NoTeleponTextBox.KeyPress += NoTeleponTextBox_KeyPress;
            NoTeleponTextBox.MaxLength = 13;

            InitializeEvents();
            LoadData();
            SetInitialState();
        }

        private void InitializeEvents()
        {
            dataGridView1.CellClick += DataGridView1_CellClick;
            button1.Click += BtnTambah_Click;
            button3.Click += BtnEdit_Click;
            button2.Click += BtnUpdate_Click;
            button4.Click += BtnHapus_Click;
        }

        private void NoTeleponTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Hanya terima angka dan tombol kontrol (seperti backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SetInitialState()
        {
            SetTextBoxesReadOnly(false);

            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
        }

        private void SetTextBoxesReadOnly(bool readOnly)
        {
            NamaDokterTextbox.ReadOnly = readOnly;
            NoTeleponTextBox.ReadOnly = readOnly;
            StatusComboBox.Enabled = !readOnly;
        }

        private void LoadData()
        {
            try
            {
                var dokterList = dokterController.GetAllDokter();
                dataGridView1.DataSource = dokterList;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dataGridView1.ColumnCount > 0)
            {
                dataGridView1.Columns["IdDokter"].HeaderText = "ID";
                dataGridView1.Columns["NamaDokter"].HeaderText = "Nama Dokter";
                dataGridView1.Columns["StatusDokter"].HeaderText = "Status";
                dataGridView1.Columns["NomorTelepon"].HeaderText = "No Telepon";
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Mengambil seluruh row dan memilihnya
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                    dataGridView1.Rows[e.RowIndex].Selected = true;

                    // Hanya simpan ID dan nama untuk keperluan edit/hapus
                    selectedId = Convert.ToInt32(row.Cells["IdDokter"].Value);
                    selectedNama = row.Cells["NamaDokter"].Value.ToString();

                    // Aktifkan tombol edit dan hapus
                    button3.Enabled = true; // Edit button
                    button4.Enabled = true; // Delete button
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var dokter = new Dokter
                {
                    NamaDokter = NamaDokterTextbox.Text,
                    StatusDokter = StatusComboBox.Text,
                    NomorTelepon = NoTeleponTextBox.Text
                };

                dokterController.AddDokter(dokter);
                MessageBox.Show("Data dokter berhasil ditambahkan!", "Success",
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

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!selectedId.HasValue) return;

            try
            {
                var row = dataGridView1.SelectedRows[0];

                // Isi textbox dengan data dari grid menggunakan null checking
                NamaDokterTextbox.Text = row.Cells["NamaDokter"].Value?.ToString() ?? "";
                StatusComboBox.Text = row.Cells["StatusDokter"].Value?.ToString() ?? "";
                NoTeleponTextBox.Text = row.Cells["NomorTelepon"].Value?.ToString() ?? "";

                SetTextBoxesReadOnly(false);
                button1.Enabled = false;  // Tambah button
                button2.Enabled = true;   // Update button
                button3.Enabled = false;  // Edit button
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;
                if (!selectedId.HasValue) return;

                var dokter = new Dokter
                {
                    IdDokter = selectedId.Value,
                    NamaDokter = NamaDokterTextbox.Text,
                    StatusDokter = StatusComboBox.Text,
                    NomorTelepon = NoTeleponTextBox.Text
                };

                dokterController.UpdateDokter(dokter);
                MessageBox.Show("Data dokter berhasil diupdate!", "Success",
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

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (!selectedId.HasValue) return;

                var result = MessageBox.Show(
                    $"Apakah anda yakin ingin menghapus dokter {selectedNama}?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dokterController.DeleteDokter(selectedId.Value);
                    MessageBox.Show("Data dokter berhasil dihapus!", "Success",
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

        private bool ValidateInput()
        {
            // Validasi Nama Dokter
            if (string.IsNullOrWhiteSpace(NamaDokterTextbox.Text))
            {
                MessageBox.Show("Nama Dokter harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi Status Dokter
            if (string.IsNullOrWhiteSpace(StatusComboBox.Text))
            {
                MessageBox.Show("Status Dokter harus dipilih!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi Nomor Telepon - Menggunakan pattern yang sama dengan FormDataPasien
            if (!string.IsNullOrWhiteSpace(NoTeleponTextBox.Text))
            {
                string noTelp = NoTeleponTextBox.Text.Trim();

                // Validasi panjang nomor telepon
                if (noTelp.Length < 10 || noTelp.Length > 13)
                {
                    MessageBox.Show("Nomor Telepon harus antara 10-13 digit!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Validasi format nomor telepon (harus dimulai dengan 08 atau 62)
                if (!(noTelp.StartsWith("08") || noTelp.StartsWith("62")))
                {
                    MessageBox.Show("Nomor Telepon harus dimulai dengan '08' atau '62'!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Validasi khusus untuk nomor yang dimulai dengan 62
                if (noTelp.StartsWith("62") && noTelp.Length < 11)
                {
                    MessageBox.Show("Nomor Telepon dengan awalan '62' minimal 11 digit!",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Nomor Telepon harus diisi!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

   

        private void ClearForm()
        {
            selectedId = null;
            selectedNama = null;
            NamaDokterTextbox.Clear();
            StatusComboBox.SelectedIndex = -1;
            NoTeleponTextBox.Clear();

            SetTextBoxesReadOnly(false);
            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
        }

        private void FormDataDokter_Load(object sender, EventArgs e)
        {
            LoadData();
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