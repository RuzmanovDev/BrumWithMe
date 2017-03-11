using BrumWithMe.Data;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string cityName = "pesho";
            cityName =
                cityName?.ToLower();

            System.Console.WriteLine(cityName);
        }
    }
}
