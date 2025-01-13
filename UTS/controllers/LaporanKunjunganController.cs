using MySql.Data.MySqlClient;
using System.Data;
using System;
using UTS.config;

public class LaporanKunjunganController
{
    private readonly Connection _connection;

    public LaporanKunjunganController()
    {
        _connection = new Connection();
    }

    public DataTable GetLaporanKunjungan(DateTime startDate, DateTime endDate, int? dokterID = null)
    {
        try
        {
            string query = @"
            SELECT 
                DATE_FORMAT(p.tanggal_pendaftaran, '%d/%m/%Y') as tanggal,
                p.nomor_antrian,
                pas.nama_pasien,
                TIME_FORMAT(jp.jam_mulai, '%H:%i') as jam_mulai,
                TIME_FORMAT(jp.jam_selesai, '%H:%i') as jam_selesai,
                d.nama_dokter,
                jp.status_jadwal
            FROM pendaftaran p
            JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal
            JOIN pasien pas ON p.id_pasien = pas.id_pasien
            JOIN dokter d ON jp.id_dokter = d.id_dokter
            WHERE p.tanggal_pendaftaran BETWEEN @startDate AND @endDate";

            if (dokterID.HasValue)
                query += " AND jp.id_dokter = @dokterID";

            query += " ORDER BY p.tanggal_pendaftaran DESC, p.nomor_antrian ASC";

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

    // In LaporanKunjunganController.cs
    public double GetAverageVisitsPerDay(DateTime startDate, DateTime endDate, int? dokterID = null)
    {
        try
        {
            string query = @"
            SELECT 
                COUNT(DISTINCT p.id_pendaftaran) as total_visits,
                COUNT(DISTINCT p.tanggal_pendaftaran) as unique_days
            FROM pendaftaran p
            JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal
            WHERE p.tanggal_pendaftaran BETWEEN @startDate AND @endDate";

            if (dokterID.HasValue)
                query += " AND jp.id_dokter = @dokterID";

            using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                cmd.Parameters.AddWithValue("@endDate", endDate.Date);
                if (dokterID.HasValue)
                    cmd.Parameters.AddWithValue("@dokterID", dokterID.Value);

                _connection.OpenConnection();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int totalVisits = reader.GetInt32("total_visits");
                        int uniqueDays = reader.GetInt32("unique_days");
                        return uniqueDays > 0 ? Math.Round((double)totalVisits / uniqueDays, 1) : 0;
                    }
                    return 0;
                }
            }
        }
        finally
        {
            _connection.CloseConnection();
        }
    }

    public int GetTotalKunjungan(DateTime startDate, DateTime endDate, int? dokterID = null)
    {
        try
        {
            string query = @"
            SELECT COUNT(DISTINCT p.id_pendaftaran) as total_visits
            FROM pendaftaran p
            JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal
            WHERE p.tanggal_pendaftaran BETWEEN @startDate AND @endDate";

            if (dokterID.HasValue)
                query += " AND jp.id_dokter = @dokterID";

            using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                cmd.Parameters.AddWithValue("@endDate", endDate.Date);
                if (dokterID.HasValue)
                    cmd.Parameters.AddWithValue("@dokterID", dokterID.Value);

                _connection.OpenConnection();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32("total_visits");
                    }
                    return 0;
                }
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
}