using BrumWithMe.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Tag> GetTagsByIds(IEnumerable<int> ids);
    }
}
