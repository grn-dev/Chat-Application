using System.Linq;

namespace Chat.Domain.Attributes
{
    static class AttributesUtils
    {



        public static T GetAttribute<T>(object type)
        {
            return type.GetType().GetCustomAttributes(typeof(T), false).Select(attr => (T)attr).FirstOrDefault();
        }


        public static bool CheckHasAttribute<T>(object type)
        {
            return type.GetType().GetCustomAttributes(typeof(T), false).Length > 0;
        }
    }
}
