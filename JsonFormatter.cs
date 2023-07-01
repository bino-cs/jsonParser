using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParser
{
    class JsonFormatter
    {
        public JObject FormatJson(JObject inputItem, JObject resultObject)
        {
            
            if (resultObject is null)
                resultObject = new JObject();
            resultObject = GetSingleKeyValues(inputItem, resultObject);
            GetObject(inputItem, resultObject);
            GetArrayObjects(inputItem, resultObject);
            return resultObject;
        }
        private JObject GetArrayObjects(JObject inputItem, JObject resultObject)
        {
            foreach (var item in inputItem)
            {
                if (typeof(JArray) == item.Value.GetType())
                {
                    resultObject.Add(item.Key, item.Value);
                }
            }
            return resultObject;

        }

        private JObject GetObject(JObject inputItem, JObject resultObject)
        {
            foreach (var item in inputItem)
            {
                if (typeof(JObject) == item.Value.GetType())
                {
                    JObject child = (JObject)item.Value;
                    var res = FormatJson(child, resultObject);
                    resultObject.Add(item.Key, res);
                }

            }
            return resultObject;
        }

        private JObject GetSingleKeyValues(JObject inputItem, JObject resultObject)
        {
            string result = string.Empty;
            resultObject = new JObject();
            foreach (var item in inputItem)
            {

                if (!(item.Value != null && item.Value.ToString().Trim().Equals(""))
                        && !(typeof(JObject) == item.Value.GetType())
                        && !(typeof(JArray) == item.Value.GetType()))
                    resultObject.Add(item.Key, item.Value);


            }
            return resultObject;
        }
    }
}
