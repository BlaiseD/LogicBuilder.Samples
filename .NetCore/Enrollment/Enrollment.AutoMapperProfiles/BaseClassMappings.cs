﻿using AutoMapper;
using Enrollment.Forms.Parameters.Common;
using Enrollment.Forms.View.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.AutoMapperProfiles
{
    public class BaseClassMappings : Profile
    {
        public BaseClassMappings()
        {
            CreateMap<FormItemSettingView, FormItemSettingParameters>()
                .Include<FormGroupArraySettingsView, FormGroupArraySettingsParameters>()
                .Include<FormGroupSettingsView, FormGroupSettingsParameters>()
                .Include<FormControlSettingsView, FormControlSettingsParameters>()
                .Include<MultiSelectFormControlSettingsView, MultiSelectFormControlSettingsParameters>()
                .ReverseMap()
                .Include<FormGroupArraySettingsParameters, FormGroupArraySettingsView>()
                .Include<FormGroupSettingsParameters, FormGroupSettingsView>()
                .Include<FormControlSettingsParameters, FormControlSettingsView>()
                .Include<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsView>();

            CreateMap<DetailItemView, DetailItemParameters>()
                .Include<DetailFieldSettingView, DetailFieldSettingParameters>()
                .Include<DetailGroupSettingsView, DetailGroupSettingsParameters>()
                .Include<DetailListSettingsView, DetailListSettingsParameters>()
                .ReverseMap()
                .Include<DetailFieldSettingParameters, DetailFieldSettingView>()
                .Include<DetailGroupSettingsParameters, DetailGroupSettingsView>()
                .Include<DetailListSettingsParameters, DetailListSettingsView>();
        }
    }
}
