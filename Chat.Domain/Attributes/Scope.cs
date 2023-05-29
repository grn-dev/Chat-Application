using Chat.Domain.Enums;
using System;

namespace Chat.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ScopeAttribute : Attribute
    {
        private readonly ServiceLifetime _serviceLifetime;

        public ScopeAttribute(ServiceLifetime serviceLifetime)
        {
            _serviceLifetime = serviceLifetime;
        }


        public ServiceLifetime GetServiceLifetime()
        {
            return _serviceLifetime;
        }
    }
}
