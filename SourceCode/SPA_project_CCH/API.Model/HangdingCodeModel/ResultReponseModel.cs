using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class ResultResponseModel<T>
    {
        /// <summary>
        /// Gets or sets the data
        /// </summary>
        [JsonProperty("data", Required = Required.AllowNull)]
        public T Data { get; set; }

        public PagingInfo Paging { get; set; }
    }
}
