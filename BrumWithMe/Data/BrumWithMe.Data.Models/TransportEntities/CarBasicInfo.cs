﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.TransportEntities
{
    public class CarBasicInfo
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public override string ToString()
        {
            return $"{this.Make} {this.Model} ({this.Year})";
        }
    }
}
