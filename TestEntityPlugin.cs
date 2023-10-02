using System.Composition;
using UCP.Common.Plugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestPlugin;

[Export("testentity", typeof(IControllerPlugin))]
public class TestEntityPlugin : IControllerPlugin
{
    private HttpRequest _request;
    private HttpResponse _response;
    private IApiContext _apiContext;

    HttpRequest IControllerPlugin.Request
    {
        set => _request = value;
    }
    HttpResponse IControllerPlugin.Response
    {
        set => _response = value;
    }
    IApiContext IControllerPlugin.ApiContext
    {
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
}
