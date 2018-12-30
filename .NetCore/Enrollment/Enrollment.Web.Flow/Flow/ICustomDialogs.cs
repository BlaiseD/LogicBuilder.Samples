﻿using Enrollment.Forms.Parameters.Common;
using Enrollment.Web.Flow.ScreenSettings.Views;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow
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
    }
}
