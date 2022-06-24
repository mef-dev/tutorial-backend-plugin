using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using UCP.Common.Plugin;

namespace TestPluginPackage
{
    [Export("testplugin", typeof(BaseEntity))]
    [JsonObject(Title = "testplugin", IsReference = true, ItemIsReference = true, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class TestEntityRequest : BaseEntity
    {
    }

    [Export("testplugin/testaction", typeof(BaseEntity))]
    [JsonObject(Title = "testplugin/testaction", IsReference = true, ItemIsReference = true, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class TestEntityRequest_TestAction : BaseEntity
    {
        public string Message { get; set; }
    }
}