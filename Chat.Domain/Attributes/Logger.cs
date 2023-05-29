
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Attributes
{


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class LoggerAttribute : Attribute
    {

        public LoggerAttribute()
        {

        }


    }
}
