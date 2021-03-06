﻿using Enrollment.Domain;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.View;
using Enrollment.Utils;
using Enrollment.Web.Flow.ScreenSettings.Navigation;
using Enrollment.Web.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Cache
{
    public class FlowDataCache
    {
        #region Properties
        public int UserId { get; set; }
        public RequestedFlowStage RequestedFlowStage { get; set; } = new RequestedFlowStage();
        public NavigationBar NavigationBar { get; set; } = new NavigationBar();
        public ScreenSettingsBase ScreenSettings { get; set; }
        //public SafeUpdateDictionary<string, object> FlowItems { get; set; } = new SafeUpdateDictionary<string, object>();
        public ParametersDictionary ParametersItems { get; set; } = new ParametersDictionary();
        public ModelDictionary ModelItems { get; set; } = new ModelDictionary();
        public ViewDictionary ViewItems { get; set; } = new ViewDictionary();
        public Variables Variables { get; set; } = new Variables();
        public Dictionary<int, object> InputQuestionsAnswers { get; } = new Dictionary<int, object>();
        #endregion Properties
    }
}
