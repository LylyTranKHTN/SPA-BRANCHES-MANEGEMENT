using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class ErrorResponseModel
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        [JsonProperty("statusDescription", Required = Required.Always)]
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("errorMessage", Required = Required.Always)]
        public string Message { get; set; }

        /// Gets or sets the data
        /// </summary>
        [JsonProperty("validation", Required = Required.AllowNull)]
        public ValidationResult Validation { get; set; }

    }
}
