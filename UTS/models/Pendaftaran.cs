using System;

namespace UTS.models
{
    public class Pendaftaran
    {
        // Database columns
        public int IdPendaftaran { get; set; }
        public int IdPasien { get; set; }
        public int IdJadwal { get; set; }
        public DateTime TanggalPendaftaran { get; set; }
        public int NomorAntrian { get; set; }

        // Additional properties for display
        public string NamaPasien { get; set; }
        public string NamaDokter { get; set; }
        public DateTime HariTanggal { get; set; }
        public TimeSpan JamMulai { get; set; }
        public TimeSpan JamSelesai { get; set; }
    }
}