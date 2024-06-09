using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class User
    {
        public User()
        {
            UsersZakazis = new HashSet<UsersZakazi>();
        }

        public int Id { get; set; }
        public byte[] Image { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int RoleId { get; set; }
        public string Telephone { get; set; } = null!;

        public string ZakaziList => String.Join(", ", UsersZakazis.Select(p => p.Zakazi.Title));

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<UsersZakazi> UsersZakazis { get; set; }
    }
}
