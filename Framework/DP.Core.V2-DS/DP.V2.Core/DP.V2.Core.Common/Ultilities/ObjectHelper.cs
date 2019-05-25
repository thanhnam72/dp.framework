using System.Reflection;

namespace DP.V2.Core.Common.Ultilities
{
    public class ObjectHelper
    {
        public static object GetValue(string propertyName, object model)
        {
            PropertyInfo prop = model.GetType().GetProperty(propertyName);
            if (prop == null)
                return null;
            else
                return prop.GetValue(model);
        }
    }
}
