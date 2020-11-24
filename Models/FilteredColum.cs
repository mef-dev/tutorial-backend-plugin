using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerAccount.Models
{
    public class FilteredColum
    {
        [JsonPropertyName("columName")]
        public string ColumName { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
