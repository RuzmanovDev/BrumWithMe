using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface IReviewService
    {
        double GetAverageUserRating(string userId);
    }
}
