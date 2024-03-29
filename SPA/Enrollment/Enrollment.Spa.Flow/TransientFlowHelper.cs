﻿using AutoMapper;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Domain;
using Enrollment.Parameters.Expressions;
using Enrollment.Spa.Flow.Properties;
using Enrollment.Spa.Flow.Requests.TransientFlows;
using Enrollment.Spa.Flow.Responses.TransientFlows;
using System;

namespace Enrollment.Spa.Flow
{
    public class TransientFlowHelper : ITransientFlowHelper
    {
        private readonly IFlowManager _flowManager;
        private readonly IMapper _mapper;

        public TransientFlowHelper(IFlowManager flowManager, IMapper mapper)
        {
            _flowManager = flowManager;
            _mapper = mapper;
        }

        public BaseFlowResponse RunSelectorFlow(SelectorFlowRequest selectorFlowRequest)
        {
            EntityModelBase entity = selectorFlowRequest.Entity ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{EF1F21C1-15A8-4AF2-B80D-466EA531ECEE}}");
            string flowName = selectorFlowRequest.ReloadItemsFlowName ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{86E8D24C-2E1E-4DE3-A73D-15C022520251}}");
            this._flowManager.FlowDataCache.Items[entity.GetType().FullName ?? throw new ArgumentException($"{nameof(selectorFlowRequest)}: {{AAF2C79F-A47B-405B-8BC7-2A6C206E9CE0}}")] = entity;

            this._flowManager.RunFlow(flowName);

            if (!_flowManager.FlowDataCache.Items.TryGetValue(typeof(SelectorLambdaOperatorParameters).FullName!, out object? selector))
                throw new InvalidOperationException(Resources.selectorIsNotSet);

            return new SelectorFlowResponse
            {
                Success = true,
                Selector = _mapper.Map<SelectorLambdaOperatorDescriptor>
                (
                    selector
                )
            };
        }
    }
}
