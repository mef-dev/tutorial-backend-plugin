// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TestPlugin;
using TestPlugin.Test;
using UCP.Common.Plugin.Services;

var plugin = new RestResourcePlugin();

var context = new DefaultHttpContext();
plugin.Request = context.Request;
plugin.Response = context.Response;

var services = new ServiceCollection();
services.AddScoped<IControllerPluginConfigProvider, ControllerPluginConfigProviderMock>();

plugin.ApiContext = new ApiContextMock
{
    ServiceCollection = services
};

var result = plugin.GetItem(1);
Console.WriteLine($"Result: {result.Name}");