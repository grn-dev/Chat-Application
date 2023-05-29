using System;

namespace Chat.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class AutowiredAttribute : Attribute
    {
    }
}
