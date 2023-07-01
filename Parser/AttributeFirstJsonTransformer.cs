using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace JsonParser.Parser
{
    public static class AttributeFirstJsonTransformer
    {
        public static Stream Transform(Stream source)
        {
            StreamReader reader = new StreamReader(source);
            string inputForFormatting = reader.ReadToEnd();
            var result= new JsonFormatter().FormatJson(JObject.Parse(inputForFormatting),null);
            source= new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result)));
            return source;
        }
    }
}