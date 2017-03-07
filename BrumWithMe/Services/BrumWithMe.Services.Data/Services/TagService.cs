using BrumWithMe.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrumWithMe.Data.Models.Entities;

namespace BrumWithMe.Services.Data.Services
{
    public class TagService : ITagService
    {
        public IEnumerable<Tag> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetTagsByIds(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
