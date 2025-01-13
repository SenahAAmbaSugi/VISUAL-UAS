using System;
using System.Data;
using System.Windows.Forms;
using UTS.controllers;
using UTS.models;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

//using System.IO;

namespace UTS
{
    public partial class FormLaporanKeuangan : Form
    {
        private readonly LaporanKeuanganController laporanController;

        public FormLaporanKeuangan()
        {
            InitializeComponent();
            laporanController = new LaporanKeuanganController();

            // Setup DateTimePicker
            StartDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            StartDatedateTimePicker.CustomFormat = "dd/MM/yyyy";
            EndDatedateTimePicker.Format = DateTimePickerFormat.Custom;
            EndDatedateTimePicker.CustomFormat = "dd/MM/yyyy";

            // Set default date range (start dari awal bulan ini)
            StartDatedateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDatedateTimePicker.Value = DateTime.Now;

            // Setup ComboBox Status
            StatusComboBox.Items.AddRange(new string[] { "Semua", "Lunas", "Belum Lunas", "Cicilan" });
            StatusComboBox.SelectedIndex = 0;

            // Setup DataGridView
            SetupDataGridView();

            // Attach event handlers
            StartDatedateTimePicker.ValueChanged += DateFilter_ValueChanged;
            EndDatedateTimePicker.ValueChanged += DateFilter_ValueChanged;
            StatusComboBox.SelectedIndexChanged += StatusFilter_SelectedIndexChanged;

            // Load initial data
            LoadData();
        }

        private void SetupDataGridView()
        {

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;

            // Setup columns
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "tanggal_pembayaran",
                    HeaderText = "Tanggal",
                    DataPropertyName = "tanggal_pembayaran",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "no_pembayaran",
                    HeaderText = "No Pembayaran",
                    DataPropertyName = "no_pembayaran",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "nama_pasien",
                    HeaderText = "Nama Pasien",
                    DataPropertyName = "nama_pasien",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "biaya_konsultasi",
                    HeaderText = "Biaya Konsultasi",
                    DataPropertyName = "biaya_konsultasi",
                    Width = 150,
                    DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "biaya_tindakan",
                    HeaderText = "Biaya Tindakan",
                    DataPropertyName = "biaya_tindakan",
                    Width = 150,
                    DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
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
                    Name = "status_pembayaran",
                    HeaderText = "Status",
                    DataPropertyName = "status_pembayaran",
                    Width = 100
                }
            );
        }

        private void LoadData()
        {
            try
            {
                var startDate = StartDatedateTimePicker.Value.Date;
                var endDate = EndDatedateTimePicker.Value.Date.AddDays(1).AddSeconds(-1); // Sampai akhir hari
                var status = StatusComboBox.SelectedItem.ToString();

                // Load data ke grid
                var data = laporanController.GetLaporanKeuangan(startDate, endDate, status);
                dataGridView1.DataSource = data;

                // Update total pendapatan
                decimal totalPendapatan = 0;
                foreach (DataRow row in data.Rows)
                {
                    totalPendapatan += Convert.ToDecimal(row["total_biaya"]);
                }
                LabelTotalPendapatan.Text = $"Rp {totalPendapatan:N0}";
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

        private void StatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Event handler untuk label clicks (kosong karena hanya untuk designer)
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void ButtonPdf_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = $"Laporan_Keuangan_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    doc.Open();

                    // Add Title
                    var titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                    var title = new Paragraph("LAPORAN KEUANGAN", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new Paragraph("\n"));

                    // Add Period Info
                    var periodFont = FontFactory.GetFont("Arial", 11);
                    var period = new Paragraph($"Periode: {StartDatedateTimePicker.Value:dd/MM/yyyy} s/d {EndDatedateTimePicker.Value:dd/MM/yyyy}", periodFont);
                    doc.Add(period);

                    var status = new Paragraph($"Status: {StatusComboBox.Text}", periodFont);
                    doc.Add(status);
                    doc.Add(new Paragraph("\n"));

                    // Create Table
                    PdfPTable table = new PdfPTable(7)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 10f
                    };

                    float[] widths = new float[] { 15f, 20f, 25f, 15f, 15f, 15f, 15f };
                    table.SetWidths(widths);

                    // Add Headers
                    var headerFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                    string[] headers = { "Tanggal", "No Pembayaran", "Nama Pasien", "Biaya Konsultasi", "Biaya Tindakan", "Total Biaya", "Status" };

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

                    var normalFont = FontFactory.GetFont("Arial", 9);
                    decimal totalPendapatan = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["tanggal_pembayaran"].Value.ToString(), normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["no_pembayaran"].Value.ToString(), normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Cells["nama_pasien"].Value.ToString(), normalFont)));

                            decimal biayaKonsultasi = Convert.ToDecimal(row.Cells["biaya_konsultasi"].Value);
                            PdfPCell cellKonsultasi = new PdfPCell(new Phrase(biayaKonsultasi.ToString("N0"), normalFont))
                            {
                                HorizontalAlignment = Element.ALIGN_RIGHT
                            };
                            table.AddCell(cellKonsultasi);

                            decimal biayaTindakan = Convert.ToDecimal(row.Cells["biaya_tindakan"].Value);
                            PdfPCell cellTindakan = new PdfPCell(new Phrase(biayaTindakan.ToString("N0"), normalFont))
                            {
                                HorizontalAlignment = Element.ALIGN_RIGHT
                            };
                            table.AddCell(cellTindakan);

                            decimal totalBiaya = Convert.ToDecimal(row.Cells["total_biaya"].Value);
                            totalPendapatan += totalBiaya;
                            PdfPCell cellTotal = new PdfPCell(new Phrase(totalBiaya.ToString("N0"), normalFont))
                            {
                                HorizontalAlignment = Element.ALIGN_RIGHT
                            };
                            table.AddCell(cellTotal);

                            table.AddCell(new PdfPCell(new Phrase(row.Cells["status_pembayaran"].Value.ToString(), normalFont)));
                        }
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph("\n"));

                    // Add Total
                    var totalFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD);
                    var totalText = new Paragraph($"Total Pendapatan: Rp {totalPendapatan:N0}", totalFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(totalText);

                    var footer = new Paragraph($"\nDicetak pada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", periodFont)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(footer);

                    doc.Close();
                    writer.Close();

                    MessageBox.Show("Laporan berhasil diekspor ke PDF!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

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