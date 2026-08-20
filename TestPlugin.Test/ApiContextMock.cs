using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UCP.Common.Interfaces;
using UCP.Common.Plugin;

namespace TestPlugin.Test;

public class ApiContextMock : IApiContext, IApiContext2
{
    private IServiceProvider? _serviceProvider;

    // Since MEF.DEV.Common.Plugin 1.5.x the platform passes a ready service provider
    // through IApiContext2; the mock builds one from the registered collection.
    public IServiceProvider ServiceProvider =>
        _serviceProvider ??= ServiceCollection.BuildServiceProvider();

    public ClaimsPrincipal User { get; }
    public ILogger Log { get; }
    public string Name { get; }
    public string Alias { get; }
    public string Entity { get; }
    public string Version { get; }
    public string AssemblyLocation { get; }
    public string BaseUrl { get; }
    public string Lang { get; }
    public IReadOnlyDictionary<string, object> HeadersInfo { get; }
    public long? UserId { get; }
    public dynamic DataBag { get; }
    public IServiceCollection ServiceCollection { get; set; }
}