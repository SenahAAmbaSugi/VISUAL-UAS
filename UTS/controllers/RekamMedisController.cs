using System;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.config;

namespace UTS.controllers
{
    public class RekamMedisController
    {
        private readonly Connection _connection;

        public RekamMedisController()
        {
            _connection = new Connection();
        }

        public DataTable GetAllRekamMedis()
        {
            try
            {
                string query = @"
                    SELECT 
                        rm.id_rekam_medis,
                        rm.id_pendaftaran,
                        rm.id_dokter,
                        p.nama_pasien,
                        d.nama_dokter,
                        pd.nomor_antrian,
                        jp.hari_tanggal as jadwal_periksa,
                        rm.keluhan,
                        rm.diagnosa,
                        rm.tindakan,
                        rm.resep,
                        rm.tanggal_rekam
                    FROM rekam_medis rm
                    JOIN pendaftaran pd ON rm.id_pendaftaran = pd.id_pendaftaran
                    JOIN pasien p ON pd.id_pasien = p.id_pasien
                    JOIN dokter d ON rm.id_dokter = d.id_dokter
                    JOIN jadwal_praktek jp ON pd.id_jadwal = jp.id_jadwal
                    ORDER BY rm.tanggal_rekam DESC";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    var dt = new DataTable();
                    _connection.OpenConnection();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public DataTable GetPendaftaranForComboBox()
        {
            try
            {
                string query = @"
            SELECT 
                p.id_pendaftaran,
                jp.id_dokter,
                CONCAT(pas.nama_pasien, ' - ', p.nomor_antrian, ' - Dr. ', d.nama_dokter, ' - ', 
                      DATE_FORMAT(jp.hari_tanggal, '%d/%m/%Y')) as info_pendaftaran
            FROM pendaftaran p
            JOIN pasien pas ON p.id_pasien = pas.id_pasien
            JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal
            JOIN dokter d ON jp.id_dokter = d.id_dokter
            WHERE NOT EXISTS (
                SELECT 1 
                FROM rekam_medis rm 
                WHERE rm.id_pendaftaran = p.id_pendaftaran
            )
            ORDER BY jp.hari_tanggal DESC, p.nomor_antrian";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    var dt = new DataTable();
                    _connection.OpenConnection();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting pendaftaran data: {ex.Message}");
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void AddRekamMedis(RekamMedis rekamMedis)
        {
            try
            {
                _connection.OpenConnection();

                // Check if rekam medis already exists for this pendaftaran
                string checkQuery = @"
            SELECT COUNT(*) 
            FROM rekam_medis 
            WHERE id_pendaftaran = @idPendaftaran";

                using (var checkCmd = new MySqlCommand(checkQuery, _connection.GetConnection()))
                {
                    checkCmd.Parameters.AddWithValue("@idPendaftaran", rekamMedis.IdPendaftaran);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (exists > 0)
                    {
                        throw new Exception("Rekam medis untuk pendaftaran ini sudah ada!");
                    }
                }

                // If no existing rekam medis, proceed with insertion
                string insertQuery = @"
            INSERT INTO rekam_medis (
                id_pendaftaran, 
                id_dokter,
                keluhan,
                diagnosa,
                tindakan,
                resep,
                tanggal_rekam
            ) VALUES (
                @idPendaftaran,
                @idDokter,
                @keluhan,
                @diagnosa,
                @tindakan,
                @resep,
                @tanggalRekam
            )";

                using (var cmd = new MySqlCommand(insertQuery, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@idPendaftaran", rekamMedis.IdPendaftaran);
                    cmd.Parameters.AddWithValue("@idDokter", rekamMedis.IdDokter);
                    cmd.Parameters.AddWithValue("@keluhan", rekamMedis.Keluhan);
                    cmd.Parameters.AddWithValue("@diagnosa", rekamMedis.Diagnosa);
                    cmd.Parameters.AddWithValue("@tindakan",
                        rekamMedis.Tindakan ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@resep",
                        rekamMedis.Resep ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tanggalRekam", rekamMedis.TanggalRekam);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void UpdateRekamMedis(RekamMedis rekamMedis)
        {
            try
            {
                string query = @"
                    UPDATE rekam_medis 
                    SET keluhan = @keluhan,
                        diagnosa = @diagnosa,
                        resep = @resep,
                        tindakan = @tindakan
                    WHERE id_rekam_medis = @id_rekam_medis";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    _connection.OpenConnection();
                    cmd.Parameters.AddWithValue("@id_rekam_medis", rekamMedis.IdRekamMedis);
                    cmd.Parameters.AddWithValue("@keluhan", rekamMedis.Keluhan);
                    cmd.Parameters.AddWithValue("@diagnosa", rekamMedis.Diagnosa);
                    cmd.Parameters.AddWithValue("@resep", rekamMedis.Resep ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tindakan", rekamMedis.Tindakan ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void DeleteRekamMedis(int idRekamMedis)
        {
            try
            {
                // First check if there are any related records in pembayaran table
                string checkQuery = "SELECT COUNT(*) FROM pembayaran WHERE id_rekam_medis = @id_rekam_medis";
                using (var checkCmd = new MySqlCommand(checkQuery, _connection.GetConnection()))
                {
                    _connection.OpenConnection();
                    checkCmd.Parameters.AddWithValue("@id_rekam_medis", idRekamMedis);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new Exception("Tidak dapat menghapus rekam medis karena masih terkait dengan data pembayaran");
                    }
                }

                string deleteQuery = "DELETE FROM rekam_medis WHERE id_rekam_medis = @id_rekam_medis";
                using (var deleteCmd = new MySqlCommand(deleteQuery, _connection.GetConnection()))
                {
                    deleteCmd.Parameters.AddWithValue("@id_rekam_medis", idRekamMedis);
                    deleteCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }
    }
}