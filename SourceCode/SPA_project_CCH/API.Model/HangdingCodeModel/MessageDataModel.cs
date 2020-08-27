using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class MessageDataModel
    {
        /// <summary>
        /// Gets or sets the api.
        /// </summary>
        [JsonProperty("api", Required = Required.Always)]
        public string Api { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        [JsonProperty("parameters", Required = Required.AllowNull)]
        public ICollection<KeyValuePair<string, string>> Parameters { get; set; }
    }
}
