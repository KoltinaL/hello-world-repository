using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Zakazi
    {
        public Zakazi()
        {
            UsersZakazis = new HashSet<UsersZakazi>();
            ZakaziCompanies = new HashSet<ZakaziCompany>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly Createddate { get; set; }
        public TimeOnly Createdtime { get; set; }

        public virtual ICollection<UsersZakazi> UsersZakazis { get; set; }
        public virtual ICollection<ZakaziCompany> ZakaziCompanies { get; set; }
    }
}
