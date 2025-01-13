using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using UTS.controllers;

namespace UTS.views
{
    public partial class FormLaporanKinerjaDokter : Form
    {
        private readonly LaporanKinerjaDokterController laporanController;

        public FormLaporanKinerjaDokter()
        {
            InitializeComponent();
            laporanController = new LaporanKinerjaDokterController();

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

            // Setup event handlers
            StartDatedateTimePicker.ValueChanged += DateFilter_ValueChanged;
            EndDatedateTimePicker.ValueChanged += DateFilter_ValueChanged;
            DokterComboBox.SelectedIndexChanged += Filter_Changed;
            ButtonPdf.Click += ButtonPdf_Click;
            BtnBack.Click += BtnBack_Click;

            // Load initial data
            LoadData();
        }

        private void SetupComboBoxes()
        {
            try
            {
                DokterComboBox.DataSource = laporanController.GetDokterForComboBox();
                DokterComboBox.DisplayMember = "nama_dokter";
                DokterComboBox.ValueMember = "id_dokter";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up combo boxes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "tanggal",
                    HeaderText = "Tanggal",
                    DataPropertyName = "tanggal",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "jumlah_pasien",
                    HeaderText = "Jumlah Pasien",
                    DataPropertyName = "jumlah_pasien",
                    Width = 100,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "jam_mulai",
                    HeaderText = "Jam Mulai",
                    DataPropertyName = "jam_mulai",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "jam_selesai",
                    HeaderText = "Jam Selesai",
                    DataPropertyName = "jam_selesai",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "jam_praktek",
                    HeaderText = "Jam Praktek",
                    DataPropertyName = "jam_praktek",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "total_biaya",
                    HeaderText = "Total Biaya",
                    DataPropertyName = "total_biaya",
                    Width = 150,
                    DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "status_jadwal",
                    HeaderText = "Status",
                    DataPropertyName = "status_jadwal",
                    Width = 100
                }
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

                // Load data ke grid
                var data = laporanController.GetLaporanKinerjaDokter(startDate, endDate, dokterID);
                dataGridView1.DataSource = data;

                // Update statistik
                var statistik = laporanController.GetStatistik(startDate, endDate, dokterID);
                LabelHasilTotalPasien.Text = statistik.totalPasien.ToString();
                LabelHasilTotalJamPraktek.Text = $"{statistik.totalJamPraktek} Jam";
                LabelHasilTotalPasienPerhari.Text = statistik.rataRataPasienPerHari.ToString("F1");
                LabelHasilTotalPendapatan.Text = $"Rp {statistik.totalPendapatan:N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateFilter_ValueChanged(object sender, EventArgs e)
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

        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonPdf_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Laporan_Kinerja_Dokter_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    doc.Open();

                    // Add Title
                    var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                    var title = new Paragraph("LAPORAN KINERJA DOKTER", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));

                    // Add Period Info
                    var infoFont = FontFactory.GetFont("Arial", 11);
                    doc.Add(new Paragraph($"Periode: {StartDatedateTimePicker.Value:dd/MM/yyyy} s/d {EndDatedateTimePicker.Value:dd/MM/yyyy}", infoFont));
                    if (DokterComboBox.SelectedValue != null && !(DokterComboBox.SelectedValue is DBNull))
                        doc.Add(new Paragraph($"Dokter: {DokterComboBox.Text}", infoFont));
                    doc.Add(new Paragraph("\n"));

                    // Add Statistics
                    var statsFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD);
                    var statistik = laporanController.GetStatistik(
                        StartDatedateTimePicker.Value.Date,
                        EndDatedateTimePicker.Value.Date.AddDays(1).AddSeconds(-1),
                        DokterComboBox.SelectedValue is DBNull ? null : (int?)Convert.ToInt32(DokterComboBox.SelectedValue)
                    );

                    // Create a table for statistics
                    var statsTable = new PdfPTable(4) { WidthPercentage = 100 };
                    statsTable.SetWidths(new float[] { 1f, 1f, 1f, 1f });

                    // Add headers
                    PdfPCell[] headers = new[]
                    {
                        new PdfPCell(new Phrase("Total Pasien", statsFont)),
                        new PdfPCell(new Phrase("Total Jam Praktek", statsFont)),
                        new PdfPCell(new Phrase("Rata-rata Pasien/Hari", statsFont)),
                        new PdfPCell(new Phrase("Total Pendapatan", statsFont))
                    };

                    foreach (var header in headers)
                    {
                        header.BackgroundColor = BaseColor.LIGHT_GRAY;
                        header.HorizontalAlignment = Element.ALIGN_CENTER;
                        header.Padding = 5;
                        statsTable.AddCell(header);
                    }

                    // Add values
                    statsTable.AddCell(new PdfPCell(new Phrase(statistik.totalPasien.ToString(), infoFont))
                    { HorizontalAlignment = Element.ALIGN_CENTER });
                    statsTable.AddCell(new PdfPCell(new Phrase($"{statistik.totalJamPraktek} Jam", infoFont))
                    { HorizontalAlignment = Element.ALIGN_CENTER });
                    statsTable.AddCell(new PdfPCell(new Phrase(statistik.rataRataPasienPerHari.ToString("F1"), infoFont))
                    { HorizontalAlignment = Element.ALIGN_CENTER });
                    statsTable.AddCell(new PdfPCell(new Phrase($"Rp {statistik.totalPendapatan:N0}", infoFont))
                    { HorizontalAlignment = Element.ALIGN_CENTER });

                    doc.Add(statsTable);
                    doc.Add(new Paragraph("\n"));

                    // Create table for details
                    PdfPTable table = new PdfPTable(7) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 15f, 15f, 12f, 12f, 12f, 20f, 14f });

                    // Add headers
                    string[] detailHeaders = {
                        "Tanggal", "Jumlah Pasien", "Jam Mulai", "Jam Selesai",
                        "Jam Praktek", "Total Biaya", "Status"
                    };

                    foreach (string header in detailHeaders)
                    {
                        var cell = new PdfPCell(new Phrase(header, statsFont))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        table.AddCell(cell);
                    }

                    // Add data
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["tanggal"].Value?.ToString() ?? "", infoFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["jumlah_pasien"].Value?.ToString() ?? "", infoFont))
                            { HorizontalAlignment = Element.ALIGN_RIGHT });
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["jam_mulai"].Value?.ToString() ?? "", infoFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["jam_selesai"].Value?.ToString() ?? "", infoFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["jam_praktek"].Value?.ToString() ?? "", infoFont)));
                            table.AddCell(new PdfPCell(new Phrase(
                                Convert.ToDecimal(row.Cells["total_biaya"].Value).ToString("N0"), infoFont))
                            { HorizontalAlignment = Element.ALIGN_RIGHT });
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["status_jadwal"].Value?.ToString() ?? "", infoFont)));
                        }
                    }

                    doc.Add(table);

                    // Add footer with timestamp
                    var footer = new Paragraph($"\nDicetak pada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", infoFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(footer);

                    doc.Close();
                    writer.Close();

                    MessageBox.Show("Laporan berhasil diekspor ke PDF!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open the created PDF file
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}",
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
                MessageBox.Show($"Error closing form: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}