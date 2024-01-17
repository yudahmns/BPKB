using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BPKB.MVC.Tables;

public partial class tr_bpkb
{
    [DisplayName("Agreement Number")]
    public string agreement_number { get; set; } = null!;
    [DisplayName("Branch Id")]
    public string? branch_id { get; set; }
    [DisplayName("No. BPKB")]
    public string? bpkb_no { get; set; }
    [DisplayName("Tanggal BPKB in")]
    [DataType(DataType.Date)]
    public DateTime? bpkb_date_in { get; set; }
    [DisplayName("Tanggal BPKB")]
    [DataType(DataType.Date)]
    public DateTime? bpkb_date { get; set; }
    [DisplayName("No. Faktur")]
    public string? faktur_no { get; set; }
    [DisplayName("Tanggal Faktur")]
    [DataType(DataType.Date)]
    public DateTime? faktur_date { get; set; }
    [DisplayName("Nomor Polisi")]
    public string? police_no { get; set; }
    [DisplayName("Lokasi Penyimpanan")]
    public string? location_id { get; set; }

    public string? created_by { get; set; }

    public DateTime? created_on { get; set; }

    public string? last_updated_by { get; set; }

    public DateTime? last_updated_on { get; set; }

    public virtual ms_storage_location? location { get; set; }
}
