using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data
{
    public class BrumWithMeDbContext : DbContext
    {
        public static BrumWithMeDbContext Create()
        {
            return new BrumWithMeDbContext();
        }

        public BrumWithMeDbContext() : base("BrumWithMe")
        {
        }
    }
}
