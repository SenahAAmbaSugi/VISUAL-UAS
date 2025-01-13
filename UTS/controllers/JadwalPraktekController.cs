using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UTS.models;
using UTS.config;

namespace UTS.controllers
{
    public class JadwalPraktekController
    {
        private readonly Connection connection;

        public JadwalPraktekController()
        {
            connection = new Connection();
        }

        public DataTable GetAllJadwalPraktek()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.OpenConnection();
                string query = @"SELECT jp.*, d.nama_dokter 
                               FROM jadwal_praktek jp 
                               INNER JOIN dokter d ON jp.id_dokter = d.id_dokter 
                               ORDER BY jp.hari_tanggal, jp.jam_mulai";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                // Konversi format data untuk tampilan grid
                foreach (DataRow row in dataTable.Rows)
                {
                    // Format tanggal
                    if (row["hari_tanggal"] != DBNull.Value)
                    {
                        DateTime date = Convert.ToDateTime(row["hari_tanggal"]);
                        row["hari_tanggal"] = date.ToString("dd/MM/yyyy");
                    }

                    // Format jam
                    if (row["jam_mulai"] != DBNull.Value)
                    {
                        TimeSpan timeStart = (TimeSpan)row["jam_mulai"];
                        row["jam_mulai"] = timeStart.ToString(@"hh\:mm");
                    }
                    if (row["jam_selesai"] != DBNull.Value)
                    {
                        TimeSpan timeEnd = (TimeSpan)row["jam_selesai"];
                        row["jam_selesai"] = timeEnd.ToString(@"hh\:mm");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving jadwal praktek: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
            return dataTable;
        }

        public DataTable GetDokterForComboBox()
        {
            DataTable dataTable = new DataTable();
            try
            {
                connection.OpenConnection();
                string query = "SELECT id_dokter, nama_dokter FROM dokter WHERE status_dokter = 'Aktif' ORDER BY nama_dokter";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving dokter data: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
            return dataTable;
        }

        public void AddJadwalPraktek(JadwalPraktek jadwal)
        {
            try
            {
                connection.OpenConnection();
                string query = @"INSERT INTO jadwal_praktek 
                               (id_dokter, hari_tanggal, jam_mulai, jam_selesai, 
                                status_jadwal, kuota_pasien, keterangan) 
                               VALUES 
                               (@idDokter, @hariTanggal, @jamMulai, @jamSelesai, 
                                @statusJadwal, @kuotaPasien, @keterangan)";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idDokter", jadwal.IdDokter);
                    command.Parameters.AddWithValue("@hariTanggal", jadwal.HariTanggal);
                    command.Parameters.AddWithValue("@jamMulai", jadwal.JamMulai);
                    command.Parameters.AddWithValue("@jamSelesai", jadwal.JamSelesai);
                    command.Parameters.AddWithValue("@statusJadwal", jadwal.StatusJadwal);
                    command.Parameters.AddWithValue("@kuotaPasien", jadwal.KuotaPasien);
                    command.Parameters.AddWithValue("@keterangan", jadwal.Keterangan ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding jadwal praktek: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public void UpdateJadwalPraktek(JadwalPraktek jadwal)
        {
            try
            {
                connection.OpenConnection();
                string query = @"UPDATE jadwal_praktek 
                               SET id_dokter = @idDokter,
                                   hari_tanggal = @hariTanggal,
                                   jam_mulai = @jamMulai,
                                   jam_selesai = @jamSelesai,
                                   status_jadwal = @statusJadwal,
                                   kuota_pasien = @kuotaPasien,
                                   keterangan = @keterangan
                               WHERE id_jadwal = @idJadwal";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idJadwal", jadwal.IdJadwal);
                    command.Parameters.AddWithValue("@idDokter", jadwal.IdDokter);
                    command.Parameters.AddWithValue("@hariTanggal", jadwal.HariTanggal);
                    command.Parameters.AddWithValue("@jamMulai", jadwal.JamMulai);
                    command.Parameters.AddWithValue("@jamSelesai", jadwal.JamSelesai);
                    command.Parameters.AddWithValue("@statusJadwal", jadwal.StatusJadwal);
                    command.Parameters.AddWithValue("@kuotaPasien", jadwal.KuotaPasien);
                    command.Parameters.AddWithValue("@keterangan", jadwal.Keterangan ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating jadwal praktek: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public void DeleteJadwalPraktek(int idJadwal)
        {
            try
            {
                connection.OpenConnection();
                string query = "DELETE FROM jadwal_praktek WHERE id_jadwal = @idJadwal";

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idJadwal", idJadwal);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting jadwal praktek: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public List<string> GetAvailableTimeSlots()
        {
            List<string> timeSlots = new List<string>();
            DateTime start = DateTime.Today.AddHours(8); // Mulai dari jam 8 pagi
            DateTime end = DateTime.Today.AddHours(17);  // Sampai jam 5 sore

            while (start <= end)
            {
                timeSlots.Add(start.ToString("HH:mm"));
                start = start.AddMinutes(30); // Interval 30 menit
            }

            return timeSlots;
        }

        public bool IsJadwalExists(int idDokter, DateTime hariTanggal, TimeSpan jamMulai,
            TimeSpan jamSelesai, int? excludeJadwalId = null)
        {
            try
            {
                connection.OpenConnection();
                string query = @"SELECT COUNT(*) FROM jadwal_praktek 
                               WHERE id_dokter = @idDokter 
                               AND hari_tanggal = @hariTanggal 
                               AND ((jam_mulai <= @jamMulai AND jam_selesai > @jamMulai) 
                               OR (jam_mulai < @jamSelesai AND jam_selesai >= @jamSelesai)
                               OR (jam_mulai >= @jamMulai AND jam_selesai <= @jamSelesai))";

                if (excludeJadwalId.HasValue)
                {
                    query += " AND id_jadwal != @excludeJadwalId";
                }

                using (MySqlCommand command = new MySqlCommand(query, connection.GetConnection()))
                {
                    command.Parameters.AddWithValue("@idDokter", idDokter);
                    command.Parameters.AddWithValue("@hariTanggal", hariTanggal.Date);
                    command.Parameters.AddWithValue("@jamMulai", jamMulai);
                    command.Parameters.AddWithValue("@jamSelesai", jamSelesai);

                    if (excludeJadwalId.HasValue)
                    {
                        command.Parameters.AddWithValue("@excludeJadwalId", excludeJadwalId.Value);
                    }

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking jadwal existence: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}