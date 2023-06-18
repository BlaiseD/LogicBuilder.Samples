﻿using Enrollment.Spa.Flow.Requests;
using System;

namespace Enrollment.Spa.Flow.Dialogs
{
    public class GridDialogHandler : BaseDialogHandler
    {
        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            if (request.CommandButtonRequest == null)
                throw new ArgumentException($"{nameof(request.CommandButtonRequest)}: {{C40A885A-06F9-4DB4-B87F-F6027F4B8601}}");

            if (request is not GridRequest gridRequest)
                throw new ArgumentException($"{nameof(request)}: {{B73D824B-F4D7-43EC-91CF-4A2F71D26795}}");

            if (!request.CommandButtonRequest.Cancel
                && gridRequest.Entity != null)
            {
                flowManager.FlowDataCache.ModelItems[gridRequest.Entity.GetType().FullName] = gridRequest.Entity;
            }

            base.Complete(flowManager, request);
        }
    }
}
