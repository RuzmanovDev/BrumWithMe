using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.Entities
{
    public class UsersTrips
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int TripId { get; set; }

        public bool IsDriver { get; set; }

        public virtual User User { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
