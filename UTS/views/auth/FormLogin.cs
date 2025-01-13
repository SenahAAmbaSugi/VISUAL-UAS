using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTS.controllers;
using UTS.config;
using UTS.models;
using UTS.views;
using UTS.session;

using System.Drawing.Drawing2D;

namespace UTS.views
{
    public partial class FormLogin : Form
    {
        // Inisialisasi objek autentikasi untuk menangani proses login
        private readonly Authentication auth;

        // Constructor Form Login
        public FormLogin()
        {
            // Inisialisasi komponen form
            InitializeComponent();
            // Menerapkan desain kustom pada form
            CustomizeDesign();

            // Cek apakah user sudah login sebelumnya melalui session
            if (!string.IsNullOrEmpty(Session.Id))
            {
                // Jika sudah login, buat dan tampilkan form dashboard
                Dashboard dashboardForm = new Dashboard();
                dashboardForm.Show();
                // Sembunyikan form login
                this.Hide();
            }
            // Inisialisasi objek autentikasi
            auth = new Authentication();
        }

        // Method untuk mengkustomisasi desain form
        private void CustomizeDesign()
        {
            // Set warna background panel utama
            BackgroundPanel.BackColor = Color.FromArgb(240, 244, 248);
            // Membuat sudut panel menjadi rounded/melengkung
            SetRoundedRegion(BackgroundPanel, 20);

            // Kustomisasi panel1
            panel1.BackColor = Color.White;
            SetRoundedRegion(panel1, 15);

            // Kustomisasi panel kanan
            RightCardPanel.BackColor = Color.White;
            SetRoundedRegion(RightCardPanel, 15);

            // Terapkan style pada kontrol-kontrol
            StyleControls();
            // Konfigurasi textbox
            ConfigureTextBoxes();
        }

        // Method untuk mengatur style kontrol-kontrol dalam form
        private void StyleControls()
        {
            // Set font dan warna untuk label KLINIK
            KLINIK_label.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            KLINIK_label.ForeColor = Color.FromArgb(33, 150, 243);

            // Set font dan warna untuk label2
            label2.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(33, 150, 243);

            // Set font dan warna untuk label3
            label3.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(33, 150, 243);

            // Kustomisasi tombol login
            Button_Login.FlatStyle = FlatStyle.Flat;
            Button_Login.FlatAppearance.BorderSize = 0;
            Button_Login.BackColor = Color.FromArgb(33, 150, 243);
            Button_Login.ForeColor = Color.White;
            Button_Login.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            SetRoundedRegion(Button_Login, 10);

            // Set font dan warna untuk checkbox show password
            ShowPasswordCheckBox.Font = new Font("Segoe UI", 10);
            ShowPasswordCheckBox.ForeColor = Color.FromArgb(100, 100, 100);
        }

        // Method untuk mengkonfigurasi textbox
        private void ConfigureTextBoxes()
        {
            // Konfigurasi textbox username dan password
            ConfigureSingleTextBox(UsernameTextBox, "Username");
            ConfigureSingleTextBox(PasswordTextBox, "Password");

            // Set karakter password dan system password char
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.UseSystemPasswordChar = false;
        }

        // Method untuk mengkonfigurasi satu textbox
        private void ConfigureSingleTextBox(TextBox textBox, string placeholder)
        {
            // Hapus border textbox
            textBox.BorderStyle = BorderStyle.None;
            // Set font textbox
            textBox.Font = new Font("Segoe UI", 11);
            // Set warna background
            textBox.BackColor = Color.FromArgb(245, 247, 250);
            // Set teks placeholder
            textBox.Text = placeholder;
            // Set warna teks placeholder
            textBox.ForeColor = Color.Gray;

            // Buat container panel untuk textbox
            Panel container = new Panel
            {
                Location = new Point(textBox.Left - 5, textBox.Top - 5),
                Size = new Size(textBox.Width + 10, textBox.Height + 10),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Set sudut rounded untuk container
            SetRoundedRegion(container, 20);
            // Tambahkan container ke parent control
            textBox.Parent.Controls.Add(container);
            // Tambahkan textbox ke container
            container.Controls.Add(textBox);
            // Set posisi textbox dalam container
            textBox.Location = new Point(5, 5);

            // Event handler saat textbox mendapat fokus
            textBox.Enter += (s, e) => {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            // Event handler saat textbox kehilangan fokus
            textBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        // Method untuk membuat sudut rounded pada control
        private void SetRoundedRegion(Control control, int radius)
        {
            // Buat path untuk bentuk rounded
            using (var path = new GraphicsPath())
            {
                // Tambahkan arc untuk setiap sudut
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Terapkan region rounded ke control
                control.Region = new Region(path);
            }
        }

        // Event handler untuk tombol login
        private void Button_Login_Click(object sender, EventArgs e)
        {
            // Ambil username dan password dari textbox
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Text;

            // Validasi input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan password tidak boleh kosong!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Set cursor wait
                Cursor = Cursors.WaitCursor;

                // Coba login
                Admin loggedInAdmin = auth.Login(username, password);

                // Jika login berhasil
                if (loggedInAdmin != null)
                {
                    MessageBox.Show($"Selamat datang, {Session.Username}!", "Sukses", MessageBoxButtons.OK);
                    // Buat dan tampilkan form dashboard
                    Dashboard dashboardForm = new Dashboard();
                    dashboardForm.Show();
                    this.Hide();
                }
                else
                {
                    // Jika login gagal
                    MessageBox.Show("Username atau password salah!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTextBox.Clear();
                    PasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                // Tangani error
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Kembalikan cursor normal
                Cursor = Cursors.Default;
            }
        }

        // Event handler untuk checkbox show password
        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle tampilan karakter password
            PasswordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\u0000' : '*';
        }
    }
}