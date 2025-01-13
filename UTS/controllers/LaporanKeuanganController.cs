using System;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.config;

namespace UTS.controllers
{
    public class LaporanKeuanganController
    {
        private readonly Connection _connection;

        public LaporanKeuanganController()
        {
            _connection = new Connection();
        }

        public DataTable GetLaporanKeuangan(DateTime startDate, DateTime endDate, string status)
        {
            try
            {
                string query = @"
                    SELECT 
                        DATE_FORMAT(p.tanggal_pembayaran, '%d/%m/%Y') as tanggal_pembayaran,
                        p.no_pembayaran,
                        pas.nama_pasien,
                        p.biaya_konsultasi,
                        p.biaya_tindakan,
                        p.total_biaya,
                        p.status_pembayaran
                    FROM pembayaran p
                    JOIN pasien pas ON p.id_pasien = pas.id_pasien
                    WHERE p.tanggal_pembayaran BETWEEN @startDate AND @endDate";

                if (status != "Semua")
                {
                    query += " AND p.status_pembayaran = @status";
                }

                query += " ORDER BY p.tanggal_pembayaran DESC";

                using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    if (status != "Semua")
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }

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
    }
}