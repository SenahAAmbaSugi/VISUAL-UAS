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
    public partial class FormLaporanRekamMedis : Form
    {
        private readonly LaporanRekamMedisController laporanController;

        public FormLaporanRekamMedis()
        {
            InitializeComponent();
            laporanController = new LaporanRekamMedisController();

            // Setup DateTimePicker
            StartDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            StartDatedateTimePicker.CustomFormat = "dd/MM/yyyy";
            EndDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            EndDatedateTimePicker.CustomFormat = "dd/MM/yyyy";

            // Set default date range
            StartDatedateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDatedateTimePicker.Value = DateTime.Now;

            // Setup ComboBox Status
            StatusComboBox.Items.AddRange(new string[] { "-- Pilih Status --", "Aktif", "Non Aktif" });
            StatusComboBox.SelectedIndex = 0;

            // Setup ComboBox
            SetupComboBoxes();
            SetupDataGridView();

            // Setup Event Handlers
            StartDatedateTimePicker.ValueChanged += Filter_Changed;
            EndDatedateTimePicker.ValueChanged += Filter_Changed;
            StatusComboBox.SelectedIndexChanged += Filter_Changed;
            DokterComboBox.SelectedIndexChanged += Filter_Changed;
            PasienComboBox.SelectedIndexChanged += Filter_Changed;

            // Load initial data
            LoadData();
        }

        private void SetupComboBoxes()
        {
            DokterComboBox.DataSource = laporanController.GetDokterForComboBox();
            DokterComboBox.DisplayMember = "nama_dokter";
            DokterComboBox.ValueMember = "id_dokter";

            PasienComboBox.DataSource = laporanController.GetPasienForComboBox();
            PasienComboBox.DisplayMember = "nama_pasien";
            PasienComboBox.ValueMember = "id_pasien";
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "tanggal_rekam",
                    HeaderText = "Tanggal",
                    DataPropertyName = "tanggal_rekam",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "nama_pasien",
                    HeaderText = "Pasien",
                    DataPropertyName = "nama_pasien",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "nama_dokter",
                    HeaderText = "Dokter",
                    DataPropertyName = "nama_dokter",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "keluhan",
                    HeaderText = "Keluhan",
                    DataPropertyName = "keluhan",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "diagnosa",
                    HeaderText = "Diagnosa",
                    DataPropertyName = "diagnosa",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "tindakan",
                    HeaderText = "Tindakan",
                    DataPropertyName = "tindakan",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "resep",
                    HeaderText = "Resep",
                    DataPropertyName = "resep",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "status_jadwal",
                    HeaderText = "Status",
                    DataPropertyName = "status_jadwal",
                    Width = 80
                }
            );
        }

        private void LoadData()
        {
            try
            {
                var startDate = StartDatedateTimePicker.Value.Date;
                var endDate = EndDatedateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);

                int? dokterID = DokterComboBox.SelectedValue is DBNull ? null :
                    (int?)Convert.ToInt32(DokterComboBox.SelectedValue);
                int? pasienID = PasienComboBox.SelectedValue is DBNull ? null :
                    (int?)Convert.ToInt32(PasienComboBox.SelectedValue);
                string status = StatusComboBox.SelectedIndex == 0 ? null :
                    StatusComboBox.SelectedItem.ToString();

                var data = laporanController.GetLaporanRekamMedis(
                    startDate, endDate, dokterID, pasienID, status);
                dataGridView1.DataSource = data;

                LabelTotalRekamMedis.Text = data.Rows.Count.ToString();
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
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Laporan_RekamMedis_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Create Document
                    Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    // Open document for writing
                    doc.Open();

                    // Add Title
                    var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                    var title = new Paragraph("LAPORAN REKAM MEDIS", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));

                    // Add Period Info
                    var infoFont = FontFactory.GetFont("Arial", 11);
                    var period = new Paragraph($"Periode: {StartDatedateTimePicker.Value:dd/MM/yyyy} s/d {EndDatedateTimePicker.Value:dd/MM/yyyy}", infoFont);
                    doc.Add(period);

                    // Add Filter Info
                    if (DokterComboBox.SelectedValue != null && !(DokterComboBox.SelectedValue is DBNull))
                        doc.Add(new Paragraph($"Dokter: {DokterComboBox.Text}", infoFont));
                    if (PasienComboBox.SelectedValue != null && !(PasienComboBox.SelectedValue is DBNull))
                        doc.Add(new Paragraph($"Pasien: {PasienComboBox.Text}", infoFont));
                    if (StatusComboBox.SelectedIndex > 0)
                        doc.Add(new Paragraph($"Status: {StatusComboBox.Text}", infoFont));

                    doc.Add(new Paragraph("\n"));

                    // Create Table
                    PdfPTable table = new PdfPTable(8) // Jumlah kolom
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 10f
                    };

                    // Set Column Widths
                    float[] widths = new float[] { 10f, 15f, 15f, 20f, 20f, 15f, 15f, 10f };
                    table.SetWidths(widths);

                    // Add Headers
                    var headerFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                    string[] headers = { "Tanggal", "Pasien", "Dokter", "Keluhan", "Diagnosa", "Tindakan", "Resep", "Status" };

                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            Padding = 5
                        };
                        table.AddCell(cell);
                    }

                    // Add Data
                    var normalFont = FontFactory.GetFont("Arial", 9);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null) // Skip empty rows
                        {
                            // Tanggal
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["tanggal_rekam"].Value.ToString(), normalFont)));

                            // Pasien
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["nama_pasien"].Value.ToString(), normalFont)));

                            // Dokter
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["nama_dokter"].Value.ToString(), normalFont)));

                            // Keluhan
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["keluhan"].Value?.ToString() ?? "", normalFont)));

                            // Diagnosa
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["diagnosa"].Value?.ToString() ?? "", normalFont)));

                            // Tindakan
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["tindakan"].Value?.ToString() ?? "", normalFont)));

                            // Resep
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["resep"].Value?.ToString() ?? "", normalFont)));

                            // Status
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["status_jadwal"].Value?.ToString() ?? "", normalFont)));
                        }
                    }

                    // Add table to document
                    doc.Add(table);
                    doc.Add(new Paragraph("\n"));

                    // Add Total
                    var totalFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD);
                    var totalText = new Paragraph($"Total Rekam Medis: {LabelTotalRekamMedis.Text}", totalFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(totalText);

                    // Add Footer
                    var footer = new Paragraph($"\nDicetak pada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", infoFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(footer);

                    // Close document
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
    }
}
