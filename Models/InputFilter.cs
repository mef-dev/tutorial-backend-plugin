using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UCP.Common.Plugin;

namespace CustomerAccount.Models
{
    public class InputFilter
    {
        [JsonPropertyName("filterFilds")]
        public FilteredColum[] FilterFilds { get; set; }

        [JsonPropertyName("sortFieldName")]
        public string SortFieldName { get; set; }

        [JsonPropertyName("sortOrder")]
        public string SortOrder { get; set; }
    }
}
