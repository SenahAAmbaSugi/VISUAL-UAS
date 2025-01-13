using System;

public class RekamMedis
{
    public int IdRekamMedis { get; set; }
    public int IdPendaftaran { get; set; }
    public int IdDokter { get; set; }
    public string Keluhan { get; set; }
    public string Diagnosa { get; set; }
    public string Resep { get; set; } 
    public string Tindakan { get; set; }
    public DateTime TanggalRekam { get; set; }

    // Properties tambahan untuk display
    public string NamaPasien { get; set; }
    public string NamaDokter { get; set; }
    public string InfoPendaftaran { get; set; }
}