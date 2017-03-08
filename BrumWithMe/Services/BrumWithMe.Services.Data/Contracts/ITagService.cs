using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITagService
    {
        IEnumerable<TagInfo> GetAllTags();
        IEnumerable<Tag> GetTagsByIds(IEnumerable<int> ids);
    }
}
