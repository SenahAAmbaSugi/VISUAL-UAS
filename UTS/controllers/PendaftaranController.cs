using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.config;
using UTS.models;

namespace UTS.controllers
{
    public class PendaftaranController
    {
        private readonly Connection connection;

        public PendaftaranController()
        {
            connection = new Connection();
        }

        public DataTable GetAllPendaftaran()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.OpenConnection();
                string query = @"SELECT p.*, ps.nama_pasien, jp.hari_tanggal, d.nama_dokter 
                               FROM pendaftaran p 
                               JOIN pasien ps ON p.id_pasien = ps.id_pasien 
                               JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal 
                               JOIN dokter d ON jp.id_dokter = d.id_dokter 
                               ORDER BY p.tanggal_pendaftaran DESC, p.nomor_antrian";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                // Format tanggal untuk tampilan
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["tanggal_pendaftaran"] != DBNull.Value)
                    {
                        DateTime date = Convert.ToDateTime(row["tanggal_pendaftaran"]);
                        row["tanggal_pendaftaran"] = date.ToString("dd/MM/yyyy");
                    }
                    if (row["hari_tanggal"] != DBNull.Value)
                    {
                        DateTime date = Convert.ToDateTime(row["hari_tanggal"]);
                        row["hari_tanggal"] = date.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving pendaftaran data: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
            return dataTable;
        }

        public DataTable GetPasienForComboBox()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.OpenConnection();
                string query = "SELECT id_pasien, nama_pasien FROM pasien ORDER BY nama_pasien";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving pasien data: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
            return dataTable;
        }

        public DataTable GetJadwalForComboBox()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.OpenConnection();
                string query = @"SELECT 
                                    jp.id_jadwal,
                                    CONCAT(
                                        d.nama_dokter, 
                                        ' - Tanggal: ',
                                        DATE_FORMAT(jp.hari_tanggal, '%d/%m/%Y'),
                                        ' Jam: ',
                                        TIME_FORMAT(jp.jam_mulai, '%H:%i'),
                                        ' - ',
                                        TIME_FORMAT(jp.jam_selesai, '%H:%i'),
                                        ' (Sisa Kuota: ',
                                        jp.kuota_pasien,
                                        ')'
                                    ) as jadwal_info,
                                    jp.kuota_pasien
                                FROM jadwal_praktek jp 
                                JOIN dokter d ON jp.id_dokter = d.id_dokter 
                                WHERE jp.status_jadwal = 'Aktif' 
                                AND jp.hari_tanggal >= CURDATE()
                                AND (
                                    jp.kuota_pasien - COALESCE((
                                        SELECT COUNT(*) 
                                        FROM pendaftaran p 
                                        WHERE p.id_jadwal = jp.id_jadwal 
                                        AND p.tanggal_pendaftaran = CURDATE()
                                    ), 0)
                                ) > 0
                                ORDER BY jp.hari_tanggal, jp.jam_mulai";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving jadwal data: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
            return dataTable;
        }

        public int GetNextNomorAntrian(int idJadwal, DateTime tanggalPendaftaran)
        {
            try
            {
                connection.OpenConnection();
                string query = @"SELECT COALESCE(MAX(nomor_antrian), 0) + 1 
                               FROM pendaftaran 
                               WHERE id_jadwal = @idJadwal 
                               AND tanggal_pendaftaran = @tanggalPendaftaran";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idJadwal", idJadwal);
                    command.Parameters.AddWithValue("@tanggalPendaftaran", tanggalPendaftaran.Date);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting next nomor antrian: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public bool IsKuotaAvailable(int idJadwal, DateTime tanggalPendaftaran)
        {
            try
            {
                connection.OpenConnection();
                string query = @"SELECT 
                                (SELECT kuota_pasien FROM jadwal_praktek WHERE id_jadwal = @idJadwal) >
                                (SELECT COUNT(*) FROM pendaftaran 
                                 WHERE id_jadwal = @idJadwal 
                                 AND tanggal_pendaftaran = @tanggalPendaftaran)";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idJadwal", idJadwal);
                    command.Parameters.AddWithValue("@tanggalPendaftaran", tanggalPendaftaran.Date);
                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking kuota: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public bool IsPasienAlreadyRegistered(int idPasien, int idJadwal, DateTime tanggalPendaftaran)
        {
            try
            {
                connection.OpenConnection();
                string query = @"SELECT COUNT(*) FROM pendaftaran 
                               WHERE id_pasien = @idPasien 
                               AND id_jadwal = @idJadwal 
                               AND tanggal_pendaftaran = @tanggalPendaftaran";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idPasien", idPasien);
                    command.Parameters.AddWithValue("@idJadwal", idJadwal);
                    command.Parameters.AddWithValue("@tanggalPendaftaran", tanggalPendaftaran.Date);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public void AddPendaftaran(Pendaftaran pendaftaran)
        {
            try
            {
                // Validasi pendaftaran ganda
                if (IsPasienAlreadyRegistered(pendaftaran.IdPasien, pendaftaran.IdJadwal, pendaftaran.TanggalPendaftaran))
                {
                    throw new Exception("Pasien sudah terdaftar pada jadwal ini.");
                }

                if (!IsKuotaAvailable(pendaftaran.IdJadwal, pendaftaran.TanggalPendaftaran))
                {
                    throw new Exception("Kuota pendaftaran untuk jadwal ini sudah penuh.");
                }

                connection.OpenConnection();
                MySqlTransaction transaction = connection.GetConnection().BeginTransaction();

                try
                {
                    // Insert pendaftaran
                    string insertQuery = @"INSERT INTO pendaftaran 
                                       (id_pasien, id_jadwal, tanggal_pendaftaran, nomor_antrian) 
                                       VALUES 
                                       (@idPasien, @idJadwal, @tanggalPendaftaran, @nomorAntrian)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection.GetConnection(), transaction))
                    {
                        command.Parameters.AddWithValue("@idPasien", pendaftaran.IdPasien);
                        command.Parameters.AddWithValue("@idJadwal", pendaftaran.IdJadwal);
                        command.Parameters.AddWithValue("@tanggalPendaftaran", pendaftaran.TanggalPendaftaran.Date);
                        command.Parameters.AddWithValue("@nomorAntrian", pendaftaran.NomorAntrian);

                        command.ExecuteNonQuery();
                    }

                    // Update kuota jadwal
                    string updateKuotaQuery = @"UPDATE jadwal_praktek 
                                              SET kuota_pasien = GREATEST(0, kuota_pasien - 1)
                                              WHERE id_jadwal = @idJadwal";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateKuotaQuery, connection.GetConnection(), transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@idJadwal", pendaftaran.IdJadwal);
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Gagal mengupdate kuota jadwal.");
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding pendaftaran: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public void DeletePendaftaran(int idPendaftaran)
        {
            try
            {
                // Periksa apakah sudah ada rekam medis
                connection.OpenConnection();
                string checkQuery = "SELECT COUNT(*) FROM rekam_medis WHERE id_pendaftaran = @id";
                using (var checkCommand = new MySqlCommand(checkQuery, connection.GetConnection()))
                {
                    checkCommand.Parameters.AddWithValue("@id", idPendaftaran);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Tidak dapat menghapus pendaftaran karena sudah memiliki rekam medis.");
                    }
                }

                string deleteQuery = "DELETE FROM pendaftaran WHERE id_pendaftaran = @id";
                using (var command = new MySqlCommand(deleteQuery, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", idPendaftaran);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Pendaftaran tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting pendaftaran: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}