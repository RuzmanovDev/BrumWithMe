using BrumWithMe.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.TransportEntities
{
    public class TripDetails
    {
        //var trip = db.Trips.Where(x => x.Id == id)
        //       .Select(x => new TripDetailsViewModel()
        //       {
        //           OriginName = x.Origin.Name,
        //           DestinationName = x.Destination.Name,
        //           TimeOfDeparture = x.TimeOfDeparture,
        //           TakenSeats = x.TakenSeats,
        //           TotalSeats = x.TotalSeats,
        //           Price = x.Price,
        //           Description = x.Description,
        //           Tags = x.Tags.Select(z => z.Name),
        //           Car = new CarViewModel()
        //           {
        //               ImageUrl = x.Car.ImageUrl,
        //               Make = x.Car.Make,
        //               Model = x.Car.Model,
        //               Year = x.Car.Year,
        //               Color = x.Car.Color
        //           }
        //       })
        //       .FirstOrDefault(); ;

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public int TakenSeats { get; set; }

        public int TotalSeats { get; set; }

        public decimal Price { get; set; }

        public User Driver { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagInfo> Tags { get; set; }

        public CarBasicInfo Car { get; set; }

    }
}
