﻿using Enrollment.Forms.Parameters.Common;
using Enrollment.Web.Flow.ScreenSettings.Views;

namespace Enrollment.Web.Flow.Requests
{
    public class GridRequest : RequestBase
    {
        public FilterGroupParameters FilterGroup { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
