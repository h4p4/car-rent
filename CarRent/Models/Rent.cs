using System;
using System.Collections.Generic;

namespace CarRent.Models
{
    public partial class Rent
    {
        public int Id { get; set; }
        public int RenterId { get; set; }
        public int CarId { get; set; }
        public int DurationDays { get; set; }
        public string? Review { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual Renter Renter { get; set; } = null!;
    }
}
