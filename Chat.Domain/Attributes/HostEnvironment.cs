using Chat.Domain.Enums;
using System;

namespace Chat.Domain.Attributes
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class HostEnvironmentAttribute : Attribute
    {

        private readonly HostEnvironment _environment;

        public HostEnvironmentAttribute(HostEnvironment environment)
        {
            _environment = environment;
        }
        public HostEnvironment GetHostEnvironment()
        {
            return _environment;
        }
    }
}
