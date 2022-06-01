using System;
using System.Collections.Generic;

namespace CarRent.Models
{
    public partial class CarBrand
    {
        public CarBrand()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
