﻿using AutoMapper;
using Contoso.Forms.Parameters;
using Contoso.Forms.View.Input;
using LogicBuilder.Forms.Parameters;

namespace Contoso.AutoMapperProfiles
{
    public class InputFormMappingProfile : Profile
    {
        public InputFormMappingProfile()
        {
            CreateMap<InputFormView, InputFormParameters>()
                .ConstructUsing((src, context) => new InputFormParameters
                {
                    FormData = context.Mapper.Map<FormDataParameters>(src)
                })
                .ForMember(dest => dest.FormData, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => ((FormDataParameters)src.FormData).Title))
                .ForMember(dest => dest.ValidationMessages, opts => opts.MapFrom(src => ((FormDataParameters)src.FormData).ValidationMessages))
                .ForMember(dest => dest.ConditionalDirectives, opts => opts.MapFrom(src => ((FormDataParameters)src.FormData).ConditionalDirectives));
            CreateMap<FormDataParameters, InputFormView>()
                .ForMember(dest => dest.Rows, opt => opt.Ignore())
                //.ForMember(dest => dest.Errors, opt => opt.Ignore())
                //.ForMember(dest => dest.ValidationErrors, opt => opt.Ignore())
                //.ForMember(dest => dest.Warnings, opt => opt.Ignore())
                //.ForMember(dest => dest.InformationItems, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<InputRowView, InputRowParameters>()
                .ConstructUsing((src, context) => new InputRowParameters
                {
                    RowData = context.Mapper.Map<RowDataParameters>(src)
                })
                .ForMember(dest => dest.RowData, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ShowTitle, opts => opts.MapFrom(src => ((RowDataParameters)src.RowData).ShowTitle))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => ((RowDataParameters)src.RowData).Title))
                .ForMember(dest => dest.ToolTipText, opts => opts.MapFrom(src => ((RowDataParameters)src.RowData).ToolTipText));
            CreateMap<RowDataParameters, InputRowView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Columns, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<InputColumnView, InputColumnParameters>()
                .ConstructUsing((src, context) => new InputColumnParameters
                {
                    ColumnData = context.Mapper.Map<ColumnDataParameters>(src)
                })
                .ForMember(dest => dest.ColumnData, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ColumnShare, opts => opts.MapFrom(src => ((ColumnDataParameters)src.ColumnData).ColumnShare))
                .ForMember(dest => dest.ShowTitle, opts => opts.MapFrom(src => ((ColumnDataParameters)src.ColumnData).ShowTitle))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => ((ColumnDataParameters)src.ColumnData).Title))
                .ForMember(dest => dest.ToolTipText, opts => opts.MapFrom(src => ((ColumnDataParameters)src.ColumnData).ToolTipText));
            CreateMap<ColumnDataParameters, InputColumnView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore())
                .ForMember(dest => dest.Rows, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
