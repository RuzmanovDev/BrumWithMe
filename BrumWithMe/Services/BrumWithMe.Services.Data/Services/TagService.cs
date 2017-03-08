using BrumWithMe.Services.Data.Contracts;
using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;
using System;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public class TagService : BaseDataService, ITagService
    {
        private readonly IRepository<Tag> tagRepo;

        public TagService(IRepository<Tag> tagRepo, Func<IUnitOfWork> unitOfWork, IMappingProvider mappingProvider)
            : base(unitOfWork, mappingProvider)
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

        public IEnumerable<Tag> GetTagsByIds(IEnumerable<int> tagIds)
        {
            if (tagIds.Count() == 0)
            {
                return new List<Tag>();
            }

            return this.tagRepo.GetAll(w => !w.IsDeleted && tagIds.Contains(w.Id), s => s);
        }
    }
}
