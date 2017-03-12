using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrumWithMe.Web.Models.Trip
{
    public class CreateTripViewModel
    {
        [Required(ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        [MinLength(3, ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        [MaxLength(30, ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        public string DestinationName { get; set; }

        [Required(ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        [MinLength(3, ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        [MaxLength(30, ErrorMessage = "Името на града трябва да е между 3 и 30 символа!")]
        public string OriginName { get; set; }

        [Required(ErrorMessage = "Описанието трябва да е между 3 и 300 символа!")]
        [MinLength(3, ErrorMessage = "Описанието трябва да е между 3 и 300 символа!")]
        [MaxLength(300, ErrorMessage = "Описанието трябва да е между 3 и 300 символа!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public DateTime DateOfDeparture { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string HourOfDeparture { get; set; }

        [Range(1, 20, ErrorMessage = "Моля, въведете число между 1 и 20!")]
        public int TotalSeats { get; set; } = 1;

        [Range(0, 1000)]
        public decimal Price { get; set; }

        public IList<TagViewModel> Tags { get; set; }

        [Required]
        public int CarId { get; set; }

        public IEnumerable<CarViewModel> UserCars { get; set; }
    }
}
