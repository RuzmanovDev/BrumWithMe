using AutoMapper;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Providers.Mapping
{
    public class MappingProvider : IMappingProvider
    {
        private readonly IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();

            this.mapper = mapper;
        }
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TDestination>(source);
        }
    }
}
