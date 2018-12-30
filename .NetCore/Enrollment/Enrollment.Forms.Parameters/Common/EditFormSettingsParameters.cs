﻿using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class EditFormSettingsParameters
    {
        public EditFormSettingsParameters()
        {
        }

        public EditFormSettingsParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header field on the form")]
            string title,

            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [Comments("Update modelType first. This field is displayed next to the title - empty on Create.")]
            string displayField,

            [Comments("Includes the URL's for create, read, update and delete.")]
            RequestDetailsParameters requestDetails,

            [Comments("Input validation messages for each field.")]
            List<ValidationMessageParameters> validationMessages,

            [Comments("List of fields and form groups for this form.")]
            List<FormItemSettingParameters> fieldSettings,

            [Comments("Defines the filter for the single object being edited - does not apply on Create.")]
            FilterGroupParameters filterGroup = null,

            [Comments("Conditional directtives for each field.")]
            List<VariableDirectivesParameters> conditionalDirectives = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = "Enrollment.Domain.Entities"
        )
        {
            Title = title;
            DisplayField = displayField;
            RequestDetails = requestDetails;
            ValidationMessages = validationMessages.ToDictionary(kvp => kvp.Field, kvp => kvp.Methods);
            FieldSettings = fieldSettings;
            FilterGroup = filterGroup;
            ConditionalDirectives = conditionalDirectives == null
                                            ? new Dictionary<string, List<DirectiveParameters>>()
                                            : conditionalDirectives
                                                .Select(cd => new VariableDirectivesParameters
                                                {
                                                    Field = cd.Field.Replace('.', '_'),
                                                    ConditionalDirectives = cd.ConditionalDirectives
                                                })
                                                .ToDictionary(kvp => kvp.Field, kvp => kvp.ConditionalDirectives);
            ModelType = modelType;
        }

        public string Title { get; set; }
        public string DisplayField { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public List<FormItemSettingParameters> FieldSettings { get; set; }
        public FilterGroupParameters FilterGroup { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
        public string ModelType { get; set; }//Helps Json.Net on create
    }
}
