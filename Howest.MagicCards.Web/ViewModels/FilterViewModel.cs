using System.Reflection;
using System.Text;

namespace Howest.MagicCards.Web.ViewModels;

public class FilterViewModel
{
    const string defaultFilter = "All"; // TODO move to appsettings
    const string defaultSort = "None"; // TODO move to appsettings

    public string Name { get; set; } = defaultFilter;
    public string Text { get; set; } = defaultFilter;
    public string Set { get; set; } = defaultFilter;
    public string RarityCode { get; set; } = defaultFilter;
    public string Artist { get; set; } = defaultFilter;
    public string Order { get; set; } = defaultSort;

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
