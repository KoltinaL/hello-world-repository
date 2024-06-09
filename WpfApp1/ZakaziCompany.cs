using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class ZakaziCompany
    {
        public int Id { get; set; }
        public int ZakazId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Zakazi Zakaz { get; set; } = null!;
    }
}
