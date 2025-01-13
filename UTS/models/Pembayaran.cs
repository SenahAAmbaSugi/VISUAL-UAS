using System;

namespace UTS.models
{
    public class Pembayaran
    {
        // Primary Key
        public int IdPembayaran { get; set; }

        // Database Fields
        public string NoPembayaran { get; set; }
        public int IdPasien { get; set; }
        public int IdRekamMedis { get; set; }
        public DateTime TanggalPembayaran { get; set; }
        public decimal BiayaKonsultasi { get; set; }
        public decimal BiayaTindakan { get; set; }
        public decimal TotalBiaya { get; set; }
        public string StatusPembayaran { get; set; }
        public string MetodePembayaran { get; set; }

        // Display Properties
        public string NamaPasien { get; set; }
        public string InfoRekamMedis { get; set; }
        public string Keluhan { get; set; }
        public string Diagnosa { get; set; }
        public string Tindakan { get; set; }
    }
}