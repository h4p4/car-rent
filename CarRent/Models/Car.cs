using System;
using System.Collections.Generic;

namespace CarRent.Models
{
    public partial class Car
    {
        public Car()
        {
            Rents = new HashSet<Rent>();
        }

        public int Id { get; set; }
        public int CarStatusId { get; set; }
        public int CarBrandId { get; set; }
        public int TransmissionTypeId { get; set; }
        public int SteeringWheelSideId { get; set; }
        public decimal EngineVolume { get; set; }
        public int ReleaseYear { get; set; }
        public string? Image { get; set; }
        public decimal CostPerDay { get; set; }

        public virtual CarBrand CarBrand { get; set; } = null!;
        public virtual CarStatus CarStatus { get; set; } = null!;
        public virtual SteeringWheelSide SteeringWheelSide { get; set; } = null!;
        public virtual TransmissionType TransmissionType { get; set; } = null!;
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
