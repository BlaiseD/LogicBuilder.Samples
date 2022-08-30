﻿using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.Services;
using System;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    //From DirectiveDescriptor where DirectiveDescriptor.DirectiveDefinitionDescriptor.Classname == HideIf/RelaodIf/ClearIf etc
    //Evaluator is DirectiveDescriptor.FilterLambdaOperatorDescriptor
    //Field is the field to evaluate
    //The manager clases ReloadIffManager, CheckIffManager, ValidateIffManager etc. listen for PropertyChanged and calls ManagerClass.Check()
    // or the specified method from DirectiveDefinition.FunctionName
    abstract public class ConditionBase<T>
    {
        protected ConditionBase()
        {
            /*Properties will be created through inline initialization*/
            Evaluator = null!;
            DirectiveDefinition = null!;
            Field = null!;
        }

        public Expression<Func<T, bool>> Evaluator { get; set; }
        public DirectiveDefinitionDescriptor DirectiveDefinition { get; set; }
        public string Field { get; set; }
        public string? ParentField { get; set; }
    }
}
