using FluentSiren.Models;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Composition;
using UCP.Common.Plugin;
using UCP.Common.Plugin.Services;

namespace TestPluginPackage
{
    [Export("testplugin", typeof(IBackendPlugin))]
    [ExportMetadata("Actions", new[] { "testaction" } )]
    [ExportMetadata("EntityName", "testplugin")]
    public class TestEntity : IBackendPlugin
    {
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
                if(request == null)
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
            throw new NotImplementedException();
        }

        IPagedList<IModifiedTag, BaseEntity> IBackendPlugin.Get(Filter filter, string view)
        {
            var result = new PagedList<IModifiedTag, ResponseRowBaseEntity>();
            result.Data = new List<ResponseRowBaseEntity>()
            {
                new ResponseRowBaseEntity()
            };

            result.Result = new ModifiedTagEntity()
            {
                LastModified = DateTime.Now,
            };

            return result;
        }
    }
}