using System.Collections.Generic;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITagService
    {
        IEnumerable<TagInfo> GetAllTags();

        IEnumerable<Tag> GetTagsByIds(IEnumerable<int> ids);
    }
}
