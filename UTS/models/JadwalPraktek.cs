using System;

namespace UTS.models
{
    public class JadwalPraktek
    {
        // Primary key
        public int IdJadwal { get; set; }

        // Foreign key to dokter
        public int IdDokter { get; set; }

        // Data jadwal
        public DateTime HariTanggal { get; set; }
        public TimeSpan JamMulai { get; set; }
        public TimeSpan JamSelesai { get; set; }
        public string StatusJadwal { get; set; } = "Aktif";
        public int KuotaPasien { get; set; } = 20;
        public string Keterangan { get; set; }

        // Display properties
        public string NamaDokter { get; set; }

        // Constructor
        public JadwalPraktek()
        {
            HariTanggal = DateTime.Today;
            JamMulai = new TimeSpan(8, 0, 0);    // 08:00
            JamSelesai = new TimeSpan(17, 0, 0); // 17:00
        }
    }
}