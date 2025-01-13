using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTS.views;
using UTS.session;

namespace UTS
{
    public partial class Dashboard : Form
    {
        // Deklarasi list untuk menyimpan kontrol yang akan di-disable/enable
        private List<Control> controlsToDisable;
        // Konstanta untuk properti transparent window
        private const int WS_EX_TRANSPARENT = 0x20;

        // Constructor dashboard
        public Dashboard()
        {
            // Inisialisasi komponen form
            InitializeComponent();
            // Inisialisasi daftar kontrol
            InitializeControlsList();
        }

        // Method untuk menginisialisasi daftar kontrol yang akan di-disable/enable
        private void InitializeControlsList()
        {
            controlsToDisable = new List<Control>
            {
                BtnDataPasien,      // Tombol menu data pasien
                BtnDataDokter,      // Tombol menu data dokter
                BtnJadwalPraktek,   // Tombol menu jadwal praktek
                BtnPendaftaran,     // Tombol menu pendaftaran
                BtnPembayaran,      // Tombol menu pembayaran
                BtnRekamMedis,      // Tombol menu rekam medis
                LogoutButton,       // Tombol logout
                //pictureBox2,
                //pictureBox3,
                //pictureBox4,
                //pictureBox7,
                //pictureBox8,
                //pictureBox9
            };
        }

        // Method untuk menonaktifkan semua kontrol
        private void DisableControls()
        {
            // Menonaktifkan setiap kontrol dalam daftar
            foreach (var control in controlsToDisable)
            {
                control.Enabled = false;
            }
            // Membuat form tidak interaktif
            this.Enabled = false;
        }

        // Method untuk mengaktifkan kembali semua kontrol
        private void EnableControls()
        {
            // Mengaktifkan kembali setiap kontrol dalam daftar
            foreach (var control in controlsToDisable)
            {
                control.Enabled = true;
            }
            // Membuat form interaktif kembali
            this.Enabled = true;
        }

        // Method generic untuk menampilkan child form
        private void ShowChildForm<T>(T childForm) where T : Form
        {
            // Nonaktifkan kontrol dashboard
            DisableControls();

            // Set properti child form
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.TopMost = true;
            childForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            childForm.MaximizeBox = false;
            childForm.MinimizeBox = false;

            // Tampilkan form sebagai dialog untuk mencegah interaksi dengan dashboard
            childForm.ShowDialog();

            // Aktifkan kembali kontrol setelah form ditutup
            EnableControls();
        }

        // Event handler untuk tombol Data Pasien
        private void BtnDataPasien_Click(object sender, EventArgs e)
        {
            var formPasien = new FormDataPasien();
            ShowChildForm(formPasien);
        }

        // Event handler untuk tombol Data Dokter
        private void BtnDataDokter_Click(object sender, EventArgs e)
        {
            var formDokter = new FormDataDokter();
            ShowChildForm(formDokter);
        }

        // Event handler untuk tombol Jadwal Praktek
        private void BtnJadwalPraktek_Click(object sender, EventArgs e)
        {
            var formJadwal = new FormJadwalPraktek();
            ShowChildForm(formJadwal);
        }

        // Event handler untuk tombol Pendaftaran
        private void BtnPendaftaran_Click(object sender, EventArgs e)
        {
            var formPendaftaran = new FormPendaftaran();
            ShowChildForm(formPendaftaran);
        }

        // Event handler untuk tombol Pembayaran
        private void BtnPembayaran_Click(object sender, EventArgs e)
        {
            var formPembayaran = new FormPembayaran();
            ShowChildForm(formPembayaran);
        }

        // Event handler untuk tombol Rekam Medis
        private void BtnRekamMedis_Click(object sender, EventArgs e)
        {
            var formRekamMedis = new Rekam_Medis();
            ShowChildForm(formRekamMedis);
        }

        // Event handler untuk tombol Logout
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // Tampilkan konfirmasi logout
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin keluar?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear session
                Session.Id = null;
                Session.Username = null;

                // Navigasi ke form login
                FormLogin loginForm = new FormLogin();
                this.Hide();
                loginForm.FormClosed += new FormClosedEventHandler(LoginForm_FormClosed);
                loginForm.Show();
            }
        }

        // Event handler ketika form login ditutup
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        // Event handler untuk tombol Laporan Keuangan
        private void BtnLaporanKeuangan_Click(object sender, EventArgs e)
        {
            var formLaporan = new FormLaporanKeuangan();
            ShowChildForm(formLaporan);
        }

        // Event handler untuk tombol Laporan Rekam Medis
        private void BtnLaporanRekamMedis_Click(object sender, EventArgs e)
        {
            var formLaporan = new FormLaporanRekamMedis();
            ShowChildForm(formLaporan);
        }

        // Event handler untuk tombol Laporan Kunjungan
        private void BtnLaporanKunjungan_Click(object sender, EventArgs e)
        {
            var formLaporan = new FormLaporanKunjungan();
            ShowChildForm(formLaporan);
        }

        // Event handler untuk tombol Laporan Kinerja Dokter
        private void BtnLaporanKinerjaDokter_Click(object sender, EventArgs e)
        {
            var formLaporan = new FormLaporanKinerjaDokter();
            ShowChildForm(formLaporan);
        }

        // Event handler saat form dimuat
        private void Form1_Load(object sender, EventArgs e)
        {
            // Kosong karena tidak ada inisialisasi khusus saat form dimuat
        }
    }
}