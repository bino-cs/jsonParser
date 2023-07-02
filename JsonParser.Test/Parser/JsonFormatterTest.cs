using JsonParser.Parser;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
namespace JsonParser.Test
{
    public class JsonFormatterTest
    {
        #region Initialize Variables
        JsonFormatter _jsonFormatter;
        JObject _inputData;
        #endregion
        [SetUp]
        public void SetUp()
        {
            _jsonFormatter = new JsonFormatter();
            string inputString = @"{
                'FirstName': 'Arthur',
                'LastName': 'Bertrand',
                'Address': {
                    'StreetName': 'Gedempte Zalmhaven',
	                'Number': '4K',
	                'City': {
                        'Name': 'Rotterdam',
			            'CODE': 'Netherlands'
                            },
	                'ZipCode': '3011 BT'
                            },
                'Age': 35,
                'Hobbies': ['Fishing', 'Rowing']
                }";
            _inputData = JObject.Parse(inputString);
        }
        [Test]
        public void JsonFormatterReturnsBytesStreamTest_ToCheckFirstNameIsAvaialbleinReturnObject()
        {
            var result = _jsonFormatter.FormatJson(_inputData, new JObject());
            Assert.AreEqual(result.SelectToken("FirstName"), _inputData.SelectToken("FirstName"));

        }
        [Test]
        public void JsonFormatterReturnsBytesStreamTest_ToCheck_StreetName_FromAddres_IsAvaialble_in_ReturnObject()
        {
            var result = _jsonFormatter.FormatJson(_inputData, new JObject());
            Assert.AreEqual(result.SelectToken("Address.StreetName"), _inputData.SelectToken("Address.StreetName"));
           
        }

        [Test]
        public void JsonFormatterReturnsBytesStreamTest_ToCheck_CityObject_From_Addres_IsAvaialble_in_ReturnObject()
        {
            var result = _jsonFormatter.FormatJson(_inputData, new JObject());
            Assert.AreEqual(result.SelectToken("Address.City.CODE"), _inputData.SelectToken("Address.City.CODE"));           
        }
        [Test]
        public void JsonFormatterReturnsBytesStreamTest_ToCheck_ArrayType_IsAvailable_in_ReturnObject()
        {
            var result = _jsonFormatter.FormatJson(_inputData, new JObject());
            Assert.AreEqual(result.SelectToken("Address.Hobbies"), _inputData.SelectToken("Address.Hobbies"));
        }
    }
}
