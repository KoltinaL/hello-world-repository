using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Company
    {
        public Company()
        {
            ZakaziCompanies = new HashSet<ZakaziCompany>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<ZakaziCompany> ZakaziCompanies { get; set; }
    }
}
