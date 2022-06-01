using System;
using System.Collections.Generic;

namespace CarRent.Models
{
    public partial class Renter
    {
        public Renter()
        {
            Rents = new HashSet<Rent>();
        }

        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
