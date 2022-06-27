using FluentSiren.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Composition;
using UCP.Common.Plugin;
using UCP.Common.Plugin.Services;

namespace TestPluginPackage
{
    [Export("entityName", typeof(IBackendPlugin))] // The unique Name entire the Alias (logical group of Entities) for the interface implementation below
    [ExportMetadata("Actions", new[] { "testAction" })] // The Action is a Name-customized implementation of some CRUD operations
    public class TestEntity : IBackendPlugin // The entity is a particular implementation of the interface IBackendPlugin
    {
        private IApiContext mefDevPlatformContext;

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public JSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public Entity HateousSchema(BaseEntity baseEntity)
        {
            throw new NotImplementedException();
        }

        public BaseEntity Post(string lang, BaseEntity entity, string parent = null)
        {
            throw new NotImplementedException();
        }

        public BaseEntity PostEntityAction(string lang, BaseEntity entity, string action, string parent = null)
        {
            if ("testaction".Equals(action))
            {
                var request = entity as TestEntityRequest_TestAction;
                if (request == null)
                    throw new CommonPlatformException($"Invalid request");

                return new ResponseRowBaseEntity()
                {
                    Description = $"Echo '{request.Message}'"
                };
            }
            else
                throw new CommonPlatformException($"Unknown action '{action}'");


        }

        public BaseEntity Put(string id, string lang, BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public void SetApiContext(IApiContext context)
        {
            this.mefDevPlatformContext = context;
        }

        /// <summary>
        /// Simple plugin GET method
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="view"></param>
        /// <returns> package configuration from Mef.Dev platform. Block `Backend`, `SERVICE CONFIGURATION` field. </returns>
        IPagedList<IModifiedTag, BaseEntity> IBackendPlugin.Get(Filter filter, string view)
        {
            var result = new PagedList<IModifiedTag, ResponseRowBaseEntity>();
            result.Data = new List<ResponseRowBaseEntity>()
            {
                new ResponseRowBaseEntity(){
                    Description = this.mefDevPlatformContext.
                        ServiceProvider().ConfigProvider()
                        .Get<string>(this.mefDevPlatformContext, this), // get package configuration
                    Configuration = this.mefDevPlatformContext.
                        ServiceProvider().ConfigProvider()
                        .Get<IConfigurationRoot>(this.mefDevPlatformContext, this) // get package configuration
                }
            };

            result.Result = new ModifiedTagEntity()
            {
                LastModified = DateTime.Now,
            };

            return result;
        }
    }
}