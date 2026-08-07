using Microsoft.Extensions.Configuration;
using UCP.Common.Plugin.Configuration;
using UCP.Common.Plugin.Services;

namespace TestPlugin.Test;

public class ControllerPluginConfigProviderMock : IControllerPluginConfigProvider
{
    public T? Get<T>() where T : class
    {
        return new ConfigurationBuilder() // Mock plugin setting from platform
            .AddJsonString(@"{ ""myurl"": ""https://example.domain"" }")
            .Build() as T;
    }

    public Task<T?> GetAsync<T>() where T : class
    {
        return Task.FromResult(Get<T>());
    }
}