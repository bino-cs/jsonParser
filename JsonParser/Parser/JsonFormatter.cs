using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParser.Parser
{
   public class JsonFormatter: IJsonFormatter
    {
        /// <summary>
        /// format json will re position the premitievev properties first and other types at the end.
        /// </summary>
        /// <param name="inputItem"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        public JObject FormatJson(JObject inputItem, JObject resultObject)
        {
            try
            {
                if (resultObject is null)
                    resultObject = new JObject();
                resultObject = GetSingleKeyValues(inputItem, resultObject);
                GetObject(inputItem, resultObject);
                GetArrayObjects(inputItem, resultObject);
                return resultObject;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get all array types
        /// </summary>
        /// <param name="inputItem"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
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
        /// <summary>
        ///  Get all Object. It will call recursievly formatjson to cover all the inner objects
        /// </summary>
        /// <param name="inputItem"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get all the Premitieve properties from all the objects.
        /// </summary>
        /// <param name="inputItem"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
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
