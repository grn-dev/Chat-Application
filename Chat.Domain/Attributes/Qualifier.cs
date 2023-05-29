using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QualifierAttribute : Attribute
    {
        private string Name;
        public QualifierAttribute(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
