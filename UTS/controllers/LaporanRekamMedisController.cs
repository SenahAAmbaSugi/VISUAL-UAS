using MySql.Data.MySqlClient;
using System.Data;
using System;
using UTS.config;

public class LaporanRekamMedisController
{
    private readonly Connection _connection;

    public LaporanRekamMedisController()
    {
        _connection = new Connection();
    }

    public DataTable GetLaporanRekamMedis(DateTime startDate, DateTime endDate,
        int? dokterID = null, int? pasienID = null, string status = null)
    {
        try
        {
            string query = @"
                SELECT 
                    DATE_FORMAT(rm.tanggal_rekam, '%d/%m/%Y') as tanggal_rekam,
                    pas.nama_pasien,
                    d.nama_dokter,
                    rm.keluhan,
                    rm.diagnosa,
                    rm.tindakan,
                    rm.resep,
                    jp.status_jadwal
                FROM rekam_medis rm
                JOIN pendaftaran p ON rm.id_pendaftaran = p.id_pendaftaran
                JOIN pasien pas ON p.id_pasien = pas.id_pasien
                JOIN dokter d ON rm.id_dokter = d.id_dokter
                JOIN jadwal_praktek jp ON p.id_jadwal = jp.id_jadwal
                WHERE rm.tanggal_rekam BETWEEN @startDate AND @endDate";

            if (dokterID.HasValue)
                query += " AND rm.id_dokter = @dokterID";
            if (pasienID.HasValue)
                query += " AND p.id_pasien = @pasienID";
            if (!string.IsNullOrEmpty(status))
                query += " AND jp.status_jadwal = @status";

            query += " ORDER BY rm.tanggal_rekam DESC";

            using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                if (dokterID.HasValue)
                    cmd.Parameters.AddWithValue("@dokterID", dokterID.Value);
                if (pasienID.HasValue)
                    cmd.Parameters.AddWithValue("@pasienID", pasienID.Value);
                if (!string.IsNullOrEmpty(status))
                    cmd.Parameters.AddWithValue("@status", status);

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

    public DataTable GetPasienForComboBox()
    {
        try
        {
            string query = "SELECT id_pasien, nama_pasien FROM pasien ORDER BY nama_pasien";
            using (var cmd = new MySqlCommand(query, _connection.GetConnection()))
            {
                var dt = new DataTable();
                _connection.OpenConnection();
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                DataRow row = dt.NewRow();
                row["id_pasien"] = DBNull.Value;
                row["nama_pasien"] = "-- Pilih Pasien --";
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