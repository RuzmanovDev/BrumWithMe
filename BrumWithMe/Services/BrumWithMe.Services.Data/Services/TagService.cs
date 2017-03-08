using BrumWithMe.Services.Data.Contracts;
using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> tagRepo;

        public TagService(IRepository<Tag> tagRepo)
        {
            Guard.WhenArgument(tagRepo, nameof(tagRepo)).IsNull().Throw();

            this.tagRepo = tagRepo;
        }

        public IEnumerable<TagInfo> GetAllTags()
        {
            return this.tagRepo.GetAll(w => !w.IsDeleted, s => new TagInfo()
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public IEnumerable<Tag> GetTagsByIds(IEnumerable<int> ids)
        {
            return this.tagRepo.GetAll(w => !w.IsDeleted && ids.Contains(w.Id), s => s);
        }
    }
}
