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
}
