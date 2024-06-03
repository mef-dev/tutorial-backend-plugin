using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UCP.Common.Plugin;

namespace TestPlugin.Test;

public class ApiContextMock : IApiContext
{
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