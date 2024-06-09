using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class UsersZakazi
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int ZakaziId { get; set; }

        public virtual User Users { get; set; } = null!;
        public virtual Zakazi Zakazi { get; set; } = null!;
    }
}
