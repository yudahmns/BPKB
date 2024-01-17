using System;
using System.Collections.Generic;

namespace BPKB.MVC.Tables;

public partial class ms_storage_location
{
    public string location_id { get; set; } = null!;

    public string? location_name { get; set; }

    public virtual ICollection<tr_bpkb> tr_bpkb { get; set; } = new List<tr_bpkb>();
}
