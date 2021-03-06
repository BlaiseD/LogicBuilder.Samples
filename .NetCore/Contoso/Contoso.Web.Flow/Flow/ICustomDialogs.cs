﻿using Contoso.Forms.Parameters.Common;
using Contoso.Web.Flow.ScreenSettings.Views;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace Contoso.Web.Flow
{
    public interface ICustomDialogs
    {
        [AlsoKnownAs("DisplayGrid")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayGrid
        (
            GridSettingsParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayEditForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayEditForm
        (
            [Comments("Configuration details for the form.")]
            EditFormSettingsParameters setting,

            [Comments("Create or Edit")]
            ViewType viewType,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayDetail")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayDetailForm
        (
            [Comments("Configuration details for the form.")]
            DetailFormSettingsParameters setting,

            [Comments("Detail or Delete")]
            ViewType viewType,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayHtmlContent")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayHtmlForm
        (
            [Comments("Configuration details for the form.")]
            HtmlPageSettingsParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayAbout")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayAbout
        (
            [Comments("Configuration details for the form.")]
            AboutFormSettingsParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );
    }
}
