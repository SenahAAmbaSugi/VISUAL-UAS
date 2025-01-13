
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UTS.models;
using UTS.config;

namespace UTS.controllers
{
    public class DokterController
    {
        private readonly Connection dbConnection;

        public DokterController()
        {
            dbConnection = new Connection();
        }

        public bool IsNomorTeleponExists(string nomorTelepon, int? excludeId = null)
        {
            try
            {
                dbConnection.OpenConnection();
                string query = "SELECT COUNT(*) FROM dokter WHERE nomor_telepon = @nomor";

                if (excludeId.HasValue)
                {
                    query += " AND id_dokter != @id";
                }

                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@nomor", nomorTelepon);
                    if (excludeId.HasValue)
                    {
                        command.Parameters.AddWithValue("@id", excludeId.Value);
                    }

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking nomor telepon: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public List<Dokter> GetAllDokter()
        {
            List<Dokter> dokterList = new List<Dokter>();

            try
            {
                dbConnection.OpenConnection();
                string query = "SELECT * FROM dokter ORDER BY id_dokter DESC";

                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dokterList.Add(new Dokter
                        {
                            IdDokter = reader.GetInt32("id_dokter"),
                            NamaDokter = reader.GetString("nama_dokter"),
                            StatusDokter = reader.GetString("status_dokter"),
                            NomorTelepon = reader.GetString("nomor_telepon")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving dokter data: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return dokterList;
        }

        public void AddDokter(Dokter dokter)
        {
            if (dokter == null)
            {
                throw new ArgumentNullException(nameof(dokter));
            }

            if (IsNomorTeleponExists(dokter.NomorTelepon))
            {
                throw new Exception("Nomor telepon sudah digunakan oleh dokter lain.");
            }

            try
            {
                dbConnection.OpenConnection();
                string query = @"INSERT INTO dokter (nama_dokter, status_dokter, nomor_telepon) 
                              VALUES (@nama, @status, @nomor)";

                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@nama", dokter.NamaDokter);
                    command.Parameters.AddWithValue("@status", dokter.StatusDokter);
                    command.Parameters.AddWithValue("@nomor", dokter.NomorTelepon);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding dokter: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public void UpdateDokter(Dokter dokter)
        {
            if (dokter == null)
            {
                throw new ArgumentNullException(nameof(dokter));
            }

            if (IsNomorTeleponExists(dokter.NomorTelepon, dokter.IdDokter))
            {
                throw new Exception("Nomor telepon sudah digunakan oleh dokter lain.");
            }

            try
            {
                dbConnection.OpenConnection();
                string query = @"UPDATE dokter 
                              SET nama_dokter = @nama, 
                                  status_dokter = @status, 
                                  nomor_telepon = @nomor 
                              WHERE id_dokter = @id";

                using (var command = new MySqlCommand(query, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", dokter.IdDokter);
                    command.Parameters.AddWithValue("@nama", dokter.NamaDokter);
                    command.Parameters.AddWithValue("@status", dokter.StatusDokter);
                    command.Parameters.AddWithValue("@nomor", dokter.NomorTelepon);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Data dokter tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating dokter: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public void DeleteDokter(int id)
        {
            try
            {
                dbConnection.OpenConnection();

                string checkQuery = "SELECT COUNT(*) FROM jadwal_praktek WHERE id_dokter = @id";
                using (var checkCommand = new MySqlCommand(checkQuery, dbConnection.GetConnection()))
                {
                    checkCommand.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Tidak dapat menghapus dokter karena masih memiliki jadwal praktek.");
                    }
                }

                string deleteQuery = "DELETE FROM dokter WHERE id_dokter = @id";
                using (var command = new MySqlCommand(deleteQuery, dbConnection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Dokter tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting dokter: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
    }
}