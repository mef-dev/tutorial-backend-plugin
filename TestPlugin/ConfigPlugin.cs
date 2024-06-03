using System;
using System.Collections.Generic;
using System.Composition;
using UCP.Common.Plugin.Models.Config;

namespace TestPlugin;
[Export("config", typeof(IPluginConfig))]
public class ConfigPlugin : IPluginConfig
{
    public static class Keys
    {
        internal static string Connection = "Connection";
        internal static string UiParameters = "UiParameters";
        internal static string Logic = "Logic";
        internal static string Report = "Report";
    }

    private readonly Dictionary<string, IEnumerable<PluginConfigSetting>> _configDictionary = new()
    {
        { Keys.Connection, GetConnectionSection() }
    };

    public IDictionary<string, IEnumerable<PluginConfigSetting>> Get()
    {
        return _configDictionary;
    }

    public IDictionary<string, IEnumerable<PluginConfigSetting>> Set(IDictionary<string, IEnumerable<PluginConfigSetting>> config)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<PluginConfigSetting> GetConnectionSection()
    {
        yield return new PluginConfigSetting()
        {
            SettingType = PluginConfigSettingType.LongText,
            Name = "ExampleName",
            Value = @"{
    ""ConnectionStrings"": {
        ""ConnectionString"": ""Server=sqlserver;Database=database;User ID=userid;Password=password;Trusted_Connection=No"",
    },
    ""DebugLevel"" : ""Trace""}"
        };
        yield break;
    }
}
