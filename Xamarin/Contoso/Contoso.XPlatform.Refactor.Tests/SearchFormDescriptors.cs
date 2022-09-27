﻿using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ItemFilter;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.SearchForm;
using LogicBuilder.Expressions.Utils.Strutures;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Maui.Tests
{
    internal static class SearchFormDescriptors
    {
        internal static SearchFormSettingsDescriptor SudentsForm = new()
        {
            Title = "About",
            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            ItemTemplateName = "TextDetailTemplate",
            Bindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "DateTimeValue",
                    Title = "DateTimeValue",
                    StringFormat = "Enrollment Date: {0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor
                    {
                        TemplateName = "TextTemplate"
                    }
                }
            }.ToDictionary(i => i.Name),
            ItemFilterGroup = new ItemFilterGroupDescriptor
            {
                Logic = "and",
                Filters = new List<ItemFilterDescriptorBase>
                {
                    new MemberSourceFilterDescriptor
                    {
                        Field = "ID",
                        Operator = "eq",
                        MemberSource = "ID"
                    }
                }
            },
            SearchFilterGroup = new SearchFilterGroupDescriptor
            {
                Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor
                    {
                        Field = "FirstName"
                    }
                }
            },
            SortCollection = new SortCollectionDescriptor
            {
                SortDescriptions = new List<SortDescriptionDescriptor>
                { 
                    new SortDescriptionDescriptor { PropertyName = "FullName", SortDirection = ListSortDirection.Descending },
                }
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                DataReturnType = typeof(IQueryable<LookUps>).AssemblyQualifiedName,
            }
        };
    }
}