using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UCP.Common.Plugin;

namespace TestPluginPackage
{
    public class ResponseRowBaseEntity : BaseEntity
    {
        public string Description { get; set; } = "Package configuration not found!";

        public IConfigurationRoot Configuration = null;
    }
}
