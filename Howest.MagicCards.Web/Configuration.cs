namespace Howest.MagicCards.Web;

public static class Configuration
{
    const string _defaultSettingsFile = "appsettings.json";

    private static IConfigurationRoot GetConfiguration(string? settingsFile = null)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(settingsFile ?? _defaultSettingsFile, optional: false, reloadOnChange: true);

        return configurationBuilder.Build();
    }

    public static string GetAppSetting(string key)
    {
        IConfigurationRoot appSettings = GetConfiguration();
        return appSettings.GetValue<string>(key);
    }
}
