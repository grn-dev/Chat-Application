using System;
namespace Chat.Domain.Attributes
{

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class TransactionalAttribute : Attribute
    {
        public TransactionalAttribute()
        {

        }
    }
}
