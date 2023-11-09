using System.Composition;
using UCP.Common.Plugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCP.Common.Plugin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestPlugin;

/// <summary>
///     Export Name Todo description
/// </summary>
[Export("restresource", typeof(IControllerPlugin))]
public class RestResourcePlugin : IControllerPlugin
{
    private HttpRequest _request;
    private HttpResponse _response;
    private IApiContext _apiContext;

    public HttpRequest Request
    {
        get => _request;
        set => _request = value;
    }
    public HttpResponse Response
    {
        get => _response;
        set => _response = value;
    }
    public IApiContext ApiContext
    {
        get => _apiContext;
        set => _apiContext = value;
    }

    /// <summary>
    ///     Create Todo item short description
    /// </summary>
    /// <remarks>
    ///     Create Todo item long description
    /// </remarks>
    /// <param name='model' example='{
    ///     "name": "walk dog"
    /// }'>
    ///     DataRequestModel model for Create item
    /// </param>
    /// <response code='200'
    ///     example='{
    ///         "id": 1,
    ///         "name": "walk dog",
    ///         "isComplete": true
    ///     }'
    /// >
    ///     Success
    /// </response>
    /// <response code='500' 
    ///     examples='{
    ///         "UNKNOWN_CONTEXT": {
    ///             "summary": "Unknown Service Context",
    ///             "description": "The request failed completely due to an unknown service context value",
    ///             "value": {
    ///                "cause": "CHARGING_FAILED",
    ///                "title": "Incomplete or erroneous session or subscriber information",
    ///                "invalidParams": [
    ///                     {
    ///                         "param": "/serviceRating/0/serviceContextId",
    ///                         "reason": "unknown context"
    ///                     }
    ///                 ]
    ///             }
    ///         },
    ///         "UNKNOWN_RESPONSE_CODE": {
    ///             "summary": "Unknown Response Code",
    ///             "description": "Internal Error",
    ///             "value": "405"
    ///         }
    ///     }'
    ///     headers='{
    ///         "Last-Modified":{
    ///             "description": "",
    ///             "schema": {
    ///                 "type": "string",
    ///                 "example": "2019-06-09T15:56:13.8498013Z"
    ///             }
    ///         }
    ///     }'
    /// >
    ///     Error
    /// </response>
    [ProducesResponseType(typeof(DataResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DataResponseModel), StatusCodes.Status500InternalServerError)]
    // [Consumes("application/json")] // http REQUEST content-type, application/json by default
    // [Produces("application/json")] // http RESPONSE content-type, application/json by default
    [HttpPost, Route("create-item")]
    public DataResponseModel CreateItem([FromBody] DataRequestModel model)
    {
        return new DataResponseModel
        {
            Id = 1,
            Name = "walk dog",
            IsComplete = true
        };
    }

    [HttpGet, Route("{id}")]
    public DataResponseModel GetItem([FromRoute] long id)
    {
        // How to get header
        var apiKeyFromHeader = _request.Headers["Last-Modified"];

        // How to get query
        var apiKeyFromQuery = _request.Query["Last-Modified"];

        // How to get service configuration
        var configProvider = _apiContext.ServiceProvider().GetService<IControllerPluginConfigProvider>();
        var configuration = configProvider!.Get<IConfigurationRoot>();

        return new DataResponseModel
        {
            Id = id,
            Name = configuration?.GetSection("myurl").Value,
            IsComplete = true
        };
    }
}
