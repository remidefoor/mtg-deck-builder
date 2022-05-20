using System.Reflection;
using System.Text;

namespace Howest.MagicCards.Shared.Formatters;

public class PropertiesFormatter
{
    public static string GetQueryString(Object o)
    {
        StringBuilder queryString = new StringBuilder();
        PropertyInfo[] properties = o.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            string? propertyValue = (string?) property.GetValue(o, null);
            if (!(propertyValue == null) && propertyValue.Length > 0)
            {
                queryString.Append($"{property.Name}={propertyValue}&");
            }
        }

        if (queryString.Length > 0)
        {
            queryString.Insert(0, "?"); // prepend with ?
            queryString.Length--; // remove trailing &
        }

        return queryString.ToString();
    }
}
