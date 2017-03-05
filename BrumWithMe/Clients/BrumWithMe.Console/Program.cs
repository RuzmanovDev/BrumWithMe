using BrumWithMe.Data;
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
            BrumWithMeDbContext.Create().Database.CreateIfNotExists();
        }
    }
}
