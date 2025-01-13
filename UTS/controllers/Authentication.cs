using System;
using MySql.Data.MySqlClient;
using UTS.config;
using UTS.models;
using UTS.session;

namespace UTS.controllers
{
    internal class Authentication
    {
        // Deklarasi objek koneksi database
        private readonly Connection dbConnection;

        // Constructor untuk inisialisasi koneksi database
        public Authentication()
        {
            dbConnection = new Connection();
        }

        // Method untuk melakukan proses login
        public models.Admin Login(string username, string password)
        {
            try
            {
                // Buka koneksi database
                dbConnection.OpenConnection();

                // Query SQL untuk mencari admin berdasarkan username dan password
                string query = "SELECT * FROM admin WHERE username=@username AND password=@password";

                // Membuat command SQL dengan parameter
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    // Menambahkan parameter untuk mencegah SQL injection
                    cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                    // Eksekusi query dan baca hasilnya
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Jika ditemukan data admin
                        if (reader.Read())
                        {
                            // Buat objek Admin baru dengan data dari database
                            var admin = new Admin
                            {
                                Id_admin = reader.GetInt32("id_admin"),
                                Username = reader.GetString("username"),
                                Password = reader.GetString("password"),
                                Nama_admin = reader.GetString("nama_admin")
                            };

                            // Set data session untuk admin yang login
                            Session.Id = admin.Id_admin.ToString();
                            Session.Username = admin.Nama_admin;

                            // Kembalikan objek admin
                            return admin;
                        }
                        else
                        {
                            // Jika tidak ditemukan data, kembalikan null
                            return null;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Tangani error database
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                // Pastikan koneksi ditutup
                dbConnection.CloseConnection();
            }
        }
    }
}