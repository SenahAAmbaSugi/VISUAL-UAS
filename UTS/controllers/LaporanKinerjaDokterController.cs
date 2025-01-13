using System;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.config;
using UTS.models;

namespace UTS.controllers
{
    public class LaporanKinerjaDokterController
    {
        private readonly Connection _connection;

        public LaporanKinerjaDokterController()
        {
            _connection = new Connection();
        }

        public DataTable GetLaporanKinerjaDokter(DateTime startDate, DateTime endDate, int? dokterID = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        DATE_FORMAT(jp.hari_tanggal, '%d/%m/%Y') as tanggal,
                        COUNT(DISTINCT p.id_pendaftaran) as jumlah_pasien,
                        TIME_FORMAT(jp.jam_mulai, '%H:%i') as jam_mulai,
                        TIME_FORMAT(jp.jam_selesai, '%H:%i') as jam_selesai,
                        TIMEDIFF(jp.jam_selesai, jp.jam_mulai) as jam_praktek,
                        COALESCE(SUM(pb.total_biaya), 0) as total_biaya,
                        jp.status_jadwal
                    FROM jadwal_praktek jp
                    LEFT JOIN pendaftaran p ON jp.id_jadwal = p.id_jadwal
                    LEFT JOIN rekam_medis rm ON p.id_pendaftaran = rm.id_pendaftaran
                    LEFT JOIN pembayaran pb ON rm.id_rekam_medis = pb.id_rekam_medis
                    WHERE jp.hari_tanggal BETWEEN @startDate AND @endDate";

                if (dokterID.HasValue)
                    query += " AND jp.id_dokter = @dokterID";

                query += @" GROUP BY jp.id_jadwal, jp.hari_tanggal, jp.jam_mulai, jp.jam_selesai, jp.status_jadwal
                          ORDER BY jp.hari_tanggal DESC";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    if (dokterID.HasValue)
                        cmd.Parameters.AddWithValue("@dokterID", dokterID.Value);

                    var dt = new DataTable();
                    _connection.OpenConnection();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public DataTable GetDokterForComboBox()
        {
            try
            {
                string query = "SELECT id_dokter, nama_dokter FROM dokter ORDER BY nama_dokter";
                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    var dt = new DataTable();
                    _connection.OpenConnection();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    DataRow row = dt.NewRow();
                    row["id_dokter"] = DBNull.Value;
                    row["nama_dokter"] = "-- Pilih Dokter --";
                    dt.Rows.InsertAt(row, 0);
                    return dt;
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public (int totalPasien, int totalJamPraktek, decimal totalPendapatan, double rataRataPasienPerHari)
            GetStatistik(DateTime startDate, DateTime endDate, int? dokterID = null)
        {
            try
            {
                string query = @"
                    SELECT 
                        COUNT(DISTINCT p.id_pendaftaran) as total_pasien,
                        SUM(TIME_TO_SEC(TIMEDIFF(jp.jam_selesai, jp.jam_mulai))) as total_detik_praktek,
                        COALESCE(SUM(pb.total_biaya), 0) as total_pendapatan,
                        COUNT(DISTINCT jp.hari_tanggal) as total_hari
                    FROM jadwal_praktek jp
                    LEFT JOIN pendaftaran p ON jp.id_jadwal = p.id_jadwal
                    LEFT JOIN rekam_medis rm ON p.id_pendaftaran = rm.id_pendaftaran
                    LEFT JOIN pembayaran pb ON rm.id_rekam_medis = pb.id_rekam_medis
                    WHERE jp.hari_tanggal BETWEEN @startDate AND @endDate";

                if (dokterID.HasValue)
                    query += " AND jp.id_dokter = @dokterID";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    if (dokterID.HasValue)
                        cmd.Parameters.AddWithValue("@dokterID", dokterID.Value);

                    _connection.OpenConnection();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalPasien = reader.GetInt32("total_pasien");
                            int totalDetikPraktek = reader.GetInt32("total_detik_praktek");
                            decimal totalPendapatan = reader.GetDecimal("total_pendapatan");
                            int totalHari = reader.GetInt32("total_hari");

                            int totalJamPraktek = totalDetikPraktek / 3600; // Convert detik ke jam
                            double rataRataPasien = totalHari > 0 ? (double)totalPasien / totalHari : 0;

                            return (totalPasien, totalJamPraktek, totalPendapatan, Math.Round(rataRataPasien, 1));
                        }
                        return (0, 0, 0, 0);
                    }
                }
            }
            finally
            {
                _connection.CloseConnection();
            }
        }
    }
}