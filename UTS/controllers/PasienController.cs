using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UTS.models;
using UTS.config;

namespace UTS.controllers
{
    public class PasienController
    {
        // Deklarasi objek koneksi database
        private readonly Connection dbConnection;

        // Constructor untuk inisialisasi koneksi database
        public PasienController()
        {
            dbConnection = new Connection();
        }

        // Method untuk mendapatkan semua data pasien
        public List<Pasien> GetAllPasien()
        {
            // Inisialisasi list untuk menampung data pasien
            List<Pasien> pasienList = new List<Pasien>();

            try
            {
                // Buka koneksi database
                dbConnection.OpenConnection();
                // Query untuk mengambil semua data pasien diurutkan berdasarkan ID descending
                string query = "SELECT * FROM pasien ORDER BY id_pasien DESC";

                // Eksekusi query dan baca hasilnya
                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                using (var reader = command.ExecuteReader())
                {
                    // Loop untuk setiap baris data
                    while (reader.Read())
                    {
                        // Buat objek Pasien baru
                        var pasien = new Pasien
                        {
                            IdPasien = reader.GetInt32("id_pasien"),
                            NamaPasien = reader.GetString("nama_pasien"),
                            NomorKartu = reader.GetString("nomor_kartu"),
                            JenisKelamin = reader.GetString("jenis_kelamin"),
                            TanggalLahir = reader.IsDBNull(reader.GetOrdinal("tanggal_lahir")) ? (DateTime?)null : reader.GetDateTime("tanggal_lahir"),
                            NoTelepon = reader.IsDBNull(reader.GetOrdinal("no_telepon")) ? null : reader.GetString("no_telepon"),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email")
                        };

                        // Set alamat jika tidak null
                        if (!reader.IsDBNull(reader.GetOrdinal("alamat")))
                        {
                            pasien.Alamat = reader.GetString("alamat");
                        }

                        // Set umur jika tidak null
                        if (!reader.IsDBNull(reader.GetOrdinal("umur")))
                        {
                            pasien.Umur = reader.GetInt32("umur");
                        }

                        // Tambahkan pasien ke list
                        pasienList.Add(pasien);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving pasien data: " + ex.Message);
            }
            finally
            {
                // Tutup koneksi database
                dbConnection.CloseConnection();
            }

            return pasienList;
        }

        // Method untuk menambah data pasien baru
        public void AddPasien(Pasien pasien)
        {
            // Validasi parameter
            if (pasien == null)
            {
                throw new ArgumentNullException(nameof(pasien));
            }

            try
            {
                // Buka koneksi database
                dbConnection.OpenConnection();
                // Query untuk insert data pasien
                string query = @"INSERT INTO pasien (nama_pasien, nomor_kartu, alamat, umur, jenis_kelamin, tanggal_lahir, no_telepon, email) 
                       VALUES (@nama, @nomor, @alamat, @umur, @jenis_kelamin, @tanggal_lahir, @no_telepon, @email)";

                // Eksekusi query dengan parameter
                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@nama", pasien.NamaPasien);
                    command.Parameters.AddWithValue("@nomor", pasien.NomorKartu);
                    command.Parameters.AddWithValue("@jenis_kelamin", pasien.JenisKelamin);
                    command.Parameters.AddWithValue("@tanggal_lahir", pasien.TanggalLahir ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@no_telepon", pasien.NoTelepon ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", pasien.Email ?? (object)DBNull.Value);

                    // Handling null value untuk alamat
                    if (string.IsNullOrEmpty(pasien.Alamat))
                    {
                        command.Parameters.AddWithValue("@alamat", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@alamat", pasien.Alamat);
                    }

                    // Handling null value untuk umur
                    if (pasien.Umur.HasValue)
                    {
                        command.Parameters.AddWithValue("@umur", pasien.Umur.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@umur", DBNull.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                // Handling error duplicate nomor kartu
                if (ex.Number == 1062)
                {
                    throw new Exception("Nomor kartu sudah terdaftar, silakan gunakan nomor lain.");
                }
                throw new Exception("Error adding pasien: " + ex.Message);
            }
            finally
            {
                // Tutup koneksi database
                dbConnection.CloseConnection();
            }
        }

        // Method untuk update data pasien
        public void UpdatePasien(Pasien pasien)
        {
            // Validasi parameter
            if (pasien == null)
            {
                throw new ArgumentNullException(nameof(pasien));
            }

            try
            {
                // Buka koneksi database
                dbConnection.OpenConnection();
                // Query untuk update data pasien
                string query = @"UPDATE pasien 
                       SET nama_pasien = @nama, 
                           nomor_kartu = @nomor, 
                           alamat = @alamat, 
                           umur = @umur,
                           jenis_kelamin = @jenis_kelamin,
                           tanggal_lahir = @tanggal_lahir,
                           no_telepon = @no_telepon,
                           email = @email
                       WHERE id_pasien = @id";

                // Eksekusi query dengan parameter
                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", pasien.IdPasien);
                    command.Parameters.AddWithValue("@nama", pasien.NamaPasien);
                    command.Parameters.AddWithValue("@nomor", pasien.NomorKartu);
                    command.Parameters.AddWithValue("@jenis_kelamin", pasien.JenisKelamin);
                    command.Parameters.AddWithValue("@tanggal_lahir", pasien.TanggalLahir ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@no_telepon", pasien.NoTelepon ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", pasien.Email ?? (object)DBNull.Value);

                    // Handling null value untuk alamat
                    if (string.IsNullOrEmpty(pasien.Alamat))
                    {
                        command.Parameters.AddWithValue("@alamat", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@alamat", pasien.Alamat);
                    }

                    // Handling null value untuk umur
                    if (pasien.Umur.HasValue)
                    {
                        command.Parameters.AddWithValue("@umur", pasien.Umur.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@umur", DBNull.Value);
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Pasien tidak ditemukan atau tidak ada perubahan data.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handling error duplicate nomor kartu
                if (ex.Number == 1062)
                {
                    throw new Exception("Nomor kartu sudah digunakan oleh pasien lain.");
                }
                throw new Exception("Error updating pasien: " + ex.Message);
            }
            finally
            {
                // Tutup koneksi database
                dbConnection.CloseConnection();
            }
        }

        // Method untuk menghapus data pasien
        public void DeletePasien(int id)
        {
            try
            {
                // Buka koneksi database
                dbConnection.OpenConnection();

                // Cek apakah pasien memiliki data pendaftaran
                string checkQuery = "SELECT COUNT(*) FROM pendaftaran WHERE id_pasien = @id";
                using (var checkCommand = new MySqlCommand(checkQuery, dbConnection.GetConnection()))
                {
                    checkCommand.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    // Jika ada data pendaftaran, throw exception
                    if (count > 0)
                    {
                        throw new Exception("Tidak dapat menghapus pasien karena masih memiliki data pendaftaran.");
                    }
                }

                // Jika tidak ada data pendaftaran, lakukan delete
                string deleteQuery = "DELETE FROM pasien WHERE id_pasien = @id";
                using (var command = new MySqlCommand(deleteQuery, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Pasien tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting pasien: " + ex.Message);
            }
            finally
            {
                // Tutup koneksi database
                dbConnection.CloseConnection();
            }
        }
    }
}