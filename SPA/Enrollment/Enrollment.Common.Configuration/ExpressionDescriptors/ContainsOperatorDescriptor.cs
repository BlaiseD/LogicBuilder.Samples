﻿namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class ContainsOperatorDescriptor : OperatorDescriptorBase
    {
        public OperatorDescriptorBase Left { get; set; }
        public OperatorDescriptorBase Right { get; set; }
    }
}