﻿using AutoMapper;
using Enrollment.Forms.View.Expansions;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;

namespace Enrollment.AutoMapperProfiles
{
    public class ExpansionViewToOperatorMappingProfile : Profile
    {
        public ExpansionViewToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionView, SelectExpandDefinition>();
            CreateMap<SelectExpandItemView, SelectExpandItem>()
                .ForMember(dest => dest.Filter, opts => opts.Ignore());
            CreateMap<SelectExpandItemQueryFunctionView, SelectExpandItemQueryFunction>();
            CreateMap<SortCollectionView, SortCollection>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => src.Skip.HasValue ? src.Skip.Value : 0))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => src.Take.HasValue ? src.Take.Value : int.MaxValue));
            CreateMap<SortDescriptionView, SortDescription>();
        }
    }
}
