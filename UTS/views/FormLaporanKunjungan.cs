using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTS.views
{
    public partial class FormLaporanKunjungan : Form
    {
        private readonly LaporanKunjunganController laporanController;

        public FormLaporanKunjungan()
        {
            InitializeComponent();
            laporanController = new LaporanKunjunganController();

            // Setup DateTimePicker
            StartDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            StartDatedateTimePicker.CustomFormat = "dd/MM/yyyy";
            EndDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            EndDatedateTimePicker.CustomFormat = "dd/MM/yyyy";

            // Set default date range
            StartDatedateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDatedateTimePicker.Value = DateTime.Now;

            // Setup ComboBox
            SetupComboBoxes();
            SetupDataGridView();

            // Event handlers
            StartDatedateTimePicker.ValueChanged += Filter_Changed;
            EndDatedateTimePicker.ValueChanged += Filter_Changed;
            DokterComboBox.SelectedIndexChanged += Filter_Changed;
            ButtonPdf.Click += ButtonPdf_Click;
            BtnBack.Click += BtnBack_Click;

            // Load initial data
            LoadData();
        }

        private void SetupComboBoxes()
        {
            DokterComboBox.DataSource = laporanController.GetDokterForComboBox();
            DokterComboBox.DisplayMember = "nama_dokter";
            DokterComboBox.ValueMember = "id_dokter";
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
            {
        new DataGridViewTextBoxColumn { Name = "tanggal", HeaderText = "Tanggal", DataPropertyName = "tanggal", Width = 100 },
        new DataGridViewTextBoxColumn { Name = "nomor_antrian", HeaderText = "No Antrian", DataPropertyName = "nomor_antrian", Width = 80 },
        new DataGridViewTextBoxColumn { Name = "nama_pasien", HeaderText = "Nama Pasien", DataPropertyName = "nama_pasien", Width = 200 },
        new DataGridViewTextBoxColumn { Name = "jam_mulai", HeaderText = "Jam Mulai", DataPropertyName = "jam_mulai", Width = 100 },
        new DataGridViewTextBoxColumn { Name = "jam_selesai", HeaderText = "Jam Selesai", DataPropertyName = "jam_selesai", Width = 100 },
        new DataGridViewTextBoxColumn { Name = "nama_dokter", HeaderText = "Dokter", DataPropertyName = "nama_dokter", Width = 200 },
        new DataGridViewTextBoxColumn { Name = "status_jadwal", HeaderText = "Status", DataPropertyName = "status_jadwal", Width = 100 }
            });
        }

        private void LoadData()
        {
            try
            {
                var startDate = StartDatedateTimePicker.Value.Date;
                var endDate = EndDatedateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);

                int? dokterID = DokterComboBox.SelectedValue is DBNull ? null :
                    (int?)Convert.ToInt32(DokterComboBox.SelectedValue);

                var data = laporanController.GetLaporanKunjungan(startDate, endDate, dokterID);
                dataGridView1.DataSource = data;

                // Update statistik menggunakan data dari controller
                int totalKunjungan = laporanController.GetTotalKunjungan(startDate, endDate, dokterID);
                LabelTotalKunjungan.Text = totalKunjungan.ToString();

                //double avgVisits = laporanController.GetAverageVisitsPerDay(startDate, endDate, dokterID);
                //LabelRataKunjunganPerHari.Text = avgVisits.ToString("N1");

                // Format grid setelah data di-load
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    switch (col.Name.ToLower())
                    {
                        case "nomor_antrian":
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "jam_daftar":
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "status":
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Filter_Changed(object sender, EventArgs e)
        {
            if (EndDatedateTimePicker.Value < StartDatedateTimePicker.Value)
            {
                MessageBox.Show("Tanggal akhir tidak boleh lebih kecil dari tanggal awal",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EndDatedateTimePicker.Value = StartDatedateTimePicker.Value;
                return;
            }
            LoadData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ButtonPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"Laporan_Kunjungan_{DateTime.Now:yyyyMMdd}.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    var doc = new Document(PageSize.A4.Rotate());
                    PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Title
                    var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                    var title = new Paragraph("LAPORAN KUNJUNGAN PASIEN", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));

                    // Period & Filter Info
                    var infoFont = FontFactory.GetFont("Arial", 11);
                    doc.Add(new Paragraph($"Periode: {StartDatedateTimePicker.Value:dd/MM/yyyy} s/d {EndDatedateTimePicker.Value:dd/MM/yyyy}", infoFont));

                    if (DokterComboBox.SelectedIndex > 0)
                    {
                        doc.Add(new Paragraph($"Dokter: {DokterComboBox.Text}", infoFont));
                    }

                    doc.Add(new Paragraph("\n"));

                    // Statistics
                    var statsFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD);


                    // Get the values directly from the controller to ensure consistency
                    var startDate = StartDatedateTimePicker.Value.Date;
                    var endDate = EndDatedateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);
                    int? dokterID = DokterComboBox.SelectedValue is DBNull ? null :
                        (int?)Convert.ToInt32(DokterComboBox.SelectedValue);

                    int totalKunjungan = laporanController.GetTotalKunjungan(startDate, endDate, dokterID);
                    double avgVisits = laporanController.GetAverageVisitsPerDay(startDate, endDate, dokterID);

                    doc.Add(new Paragraph($"Total Kunjungan: {totalKunjungan}", statsFont));
                    doc.Add(new Paragraph($"Rata-rata Kunjungan per Hari: {avgVisits.ToString("F1").Replace(".", ",")}", statsFont));
                    doc.Add(new Paragraph("\n"));

                    // Data Table
                    var table = new PdfPTable(7) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 15f, 10f, 25f, 12f, 12f, 20f, 15f });

                    string[] headers = { "Tanggal", "No Antrian", "Nama Pasien", "Jam Mulai", "Jam Selesai", "Dokter", "Status" };
                    foreach (string header in headers)
                    {
                        table.AddCell(new PdfPCell(new Phrase(header, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            table.AddCell(row.Cells["tanggal"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["nomor_antrian"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["nama_pasien"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["jam_mulai"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["jam_selesai"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["nama_dokter"].Value?.ToString() ?? "");
                            table.AddCell(row.Cells["status_jadwal"].Value?.ToString() ?? "");
                        }
                    }

                    doc.Add(table);

                    // Footer
                    var footer = new Paragraph($"\nDicetak pada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", infoFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(footer);

                    doc.Close();
                }

                System.Diagnostics.Process.Start(sfd.FileName);
                MessageBox.Show("PDF berhasil dibuat!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LabelRataKunjunganPerHari_Click(object sender, EventArgs e)
        {

        }

        private void ButtonPdf_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
