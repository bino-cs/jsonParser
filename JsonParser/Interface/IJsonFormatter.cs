using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParser
{
    interface IJsonFormatter
    {
        public JObject FormatJson(JObject inputItem, JObject resultObject);
    }
}
