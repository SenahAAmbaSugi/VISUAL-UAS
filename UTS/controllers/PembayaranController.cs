using System;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.config;
using UTS.models;

namespace UTS.controllers
{
    public class PembayaranController
    {
        private readonly Connection _connection;

        public PembayaranController()
        {
            _connection = new Connection();
        }

        public DataTable GetAllPembayaran()
        {
            var dt = new DataTable();
            try
            {
                _connection.OpenConnection();
                string query = @"
            SELECT 
                p.id_pembayaran,
                p.no_pembayaran,
                p.id_pasien,
                pas.nama_pasien,
                p.id_rekam_medis,
                CONCAT('RM', p.id_rekam_medis, '-', 
                      DATE_FORMAT(rm.tanggal_rekam, '%d/%m/%Y'), ' (',
                      d.nama_dokter, ')') as rekam_medis,
                DATE_FORMAT(p.tanggal_pembayaran, '%d/%m/%Y') as tanggal_pembayaran,
                p.biaya_konsultasi,
                p.biaya_tindakan,
                p.total_biaya,
                p.status_pembayaran,
                p.metode_pembayaran
            FROM pembayaran p
            JOIN pasien pas ON p.id_pasien = pas.id_pasien
            JOIN rekam_medis rm ON p.id_rekam_medis = rm.id_rekam_medis
            JOIN dokter d ON rm.id_dokter = d.id_dokter
            ORDER BY p.tanggal_pembayaran DESC";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving pembayaran data: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public DataTable GetPasienForComboBox()
        {
            var dt = new DataTable();
            try
            {
                _connection.OpenConnection();
                string query = @"
                    SELECT id_pasien, nama_pasien 
                    FROM pasien 
                    ORDER BY nama_pasien";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving pasien data: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public DataTable GetRekamMedisByPasien(int idPasien, int? excludeRekamMedisId = null)
        {
            var dt = new DataTable();
            try
            {
                _connection.OpenConnection();
                string query = @"
            SELECT 
                rm.id_rekam_medis,
                CONCAT('RM', rm.id_rekam_medis, '-', 
                      DATE_FORMAT(rm.tanggal_rekam, '%d/%m/%Y'), ' (',
                      d.nama_dokter, ')') as info_rekam_medis
            FROM rekam_medis rm
            JOIN pendaftaran p ON rm.id_pendaftaran = p.id_pendaftaran
            JOIN dokter d ON rm.id_dokter = d.id_dokter
            WHERE p.id_pasien = @id_pasien
            AND (NOT EXISTS (
                    SELECT 1 FROM pembayaran pb 
                    WHERE pb.id_rekam_medis = rm.id_rekam_medis
                ) 
                OR rm.id_rekam_medis = @exclude_id
            )
            ORDER BY rm.tanggal_rekam DESC";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@id_pasien", idPasien);
                    cmd.Parameters.AddWithValue("@exclude_id", excludeRekamMedisId ?? -1);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving rekam medis data: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public string GenerateNoPembayaran()
        {
            try
            {
                _connection.OpenConnection();
                string prefix = "PYM";
                string date = DateTime.Now.ToString("yyyyMMdd");
                string query = "SELECT COUNT(*) FROM pembayaran WHERE DATE(tanggal_pembayaran) = CURDATE()";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    return $"{prefix}{date}{count:D4}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating payment number: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void AddPembayaran(Pembayaran pembayaran)
        {
            try
            {
                _connection.OpenConnection();
                string query = @"
                    INSERT INTO pembayaran (
                        no_pembayaran,
                        id_rekam_medis,
                        id_pasien,
                        tanggal_pembayaran,
                        biaya_konsultasi,
                        biaya_tindakan,
                        total_biaya,
                        status_pembayaran,
                        metode_pembayaran
                    ) VALUES (
                        @no_pembayaran,
                        @id_rekam_medis,
                        @id_pasien,
                        @tanggal_pembayaran,
                        @biaya_konsultasi,
                        @biaya_tindakan,
                        @total_biaya,
                        @status_pembayaran,
                        'Cash'
                    )";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@no_pembayaran", pembayaran.NoPembayaran);
                    cmd.Parameters.AddWithValue("@id_rekam_medis", pembayaran.IdRekamMedis);
                    cmd.Parameters.AddWithValue("@id_pasien", pembayaran.IdPasien);
                    cmd.Parameters.AddWithValue("@tanggal_pembayaran", pembayaran.TanggalPembayaran);
                    cmd.Parameters.AddWithValue("@biaya_konsultasi", pembayaran.BiayaKonsultasi);
                    cmd.Parameters.AddWithValue("@biaya_tindakan", pembayaran.BiayaTindakan);
                    cmd.Parameters.AddWithValue("@total_biaya", pembayaran.TotalBiaya);
                    cmd.Parameters.AddWithValue("@status_pembayaran", pembayaran.StatusPembayaran);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                        throw new Exception("Pembayaran gagal ditambahkan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding payment: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void UpdatePembayaran(Pembayaran pembayaran)
        {
            try
            {
                _connection.OpenConnection();
                string query = @"
                    UPDATE pembayaran 
                    SET biaya_konsultasi = @biaya_konsultasi,
                        biaya_tindakan = @biaya_tindakan,
                        total_biaya = @total_biaya,
                        status_pembayaran = @status_pembayaran
                    WHERE id_pembayaran = @id_pembayaran";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@id_pembayaran", pembayaran.IdPembayaran);
                    cmd.Parameters.AddWithValue("@biaya_konsultasi", pembayaran.BiayaKonsultasi);
                    cmd.Parameters.AddWithValue("@biaya_tindakan", pembayaran.BiayaTindakan);
                    cmd.Parameters.AddWithValue("@total_biaya", pembayaran.TotalBiaya);
                    cmd.Parameters.AddWithValue("@status_pembayaran", pembayaran.StatusPembayaran);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                        throw new Exception("Pembayaran tidak ditemukan atau tidak ada perubahan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating payment: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void DeletePembayaran(int idPembayaran)
        {
            try
            {
                _connection.OpenConnection();
                string query = "DELETE FROM pembayaran WHERE id_pembayaran = @id_pembayaran";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@id_pembayaran", idPembayaran);
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                        throw new Exception("Pembayaran tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting payment: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }
    }
}