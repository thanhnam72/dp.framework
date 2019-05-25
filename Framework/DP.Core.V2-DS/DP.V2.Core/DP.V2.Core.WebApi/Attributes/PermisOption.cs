using System;
using System.Collections.Generic;
using System.Linq;

namespace DP.V2.Core.WebApi.Attributes
{
    public class PermisOption : Attribute
    {
        public List<string> PermisTypes { get; private set; }

        public PermisOption(string type)
        {
            PermisTypes = new List<string>();
            PermisTypes.Add(type);
        }

        public PermisOption(string[] types)
        {
            PermisTypes = new List<string>();
            PermisTypes.AddRange(types.ToList());
        }
    }
}
