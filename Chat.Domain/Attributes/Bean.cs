using System;

namespace Chat.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BeanAttribute : Attribute
    {
        public BeanAttribute()
        {

        }
    }
}
