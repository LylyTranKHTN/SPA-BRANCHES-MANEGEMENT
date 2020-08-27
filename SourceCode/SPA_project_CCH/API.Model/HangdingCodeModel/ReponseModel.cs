using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        [JsonProperty("error")]
        public ErrorResponseModel Error { get; set; }

        /// <summary>
        /// Gets or sets the Message Details.
        /// </summary>
        [JsonProperty("messageDetails", Required = Required.Always)]
        public MessageDataModel MessageDetails { get; set; }

        /// <summary>
        /// Gets or sets the Result.
        /// </summary>
        [JsonProperty("result", Required = Required.Default)]
        public ResultResponseModel<T> Result { get; set; }
    }
}
