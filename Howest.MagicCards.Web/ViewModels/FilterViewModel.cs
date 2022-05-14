using System.Reflection;
using System.Text;

namespace Howest.MagicCards.Web.ViewModels;

public class FilterViewModel
{
    public string Name { get; set; } = "All"; // TODO move to appsettings
    public string Text { get; set; } = "All"; // TODO move to appsettings
    public string Set { get; set; } = "All"; // TODO move to appsettings
    public string Rarity { get; set; } = "All"; // TODO move to appsettings
    public string Artist { get; set; } = "All"; // TODO move to appsettings
    public string Sort { get; set; } = "None"; // TODO move to appsettings

    public string GetQueryString()
    {
        StringBuilder queryString = new StringBuilder();
        PropertyInfo[] properties = GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            string? propertyValue = (string?) property.GetValue(this, null);
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
