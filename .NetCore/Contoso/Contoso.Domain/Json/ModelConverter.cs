﻿using Contoso.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<BaseModelClass>
    {
        

        public override string TypePropertyName => "TypeFullName";


        protected override Type GetDerivedType(string typeName)
        {
            return typeof(BaseModelClass).Assembly.GetType(typeName, true, false);
        }
    }
}
