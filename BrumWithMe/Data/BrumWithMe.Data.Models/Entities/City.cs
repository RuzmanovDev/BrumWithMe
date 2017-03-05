using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
