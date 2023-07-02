using System.IO;
using System.Text;
using JsonParser.Parser;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonParser.Test
{
    public class AttributeFirstJsonTransformerTests
    {
        string _inputData;
        object[] _inputPremitieves=new  object[5];
        Stream _inputStream;
        [SetUp]
        public void VariableInitialization()
        {
            _inputData = @"{
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
            _inputStream = new MemoryStream(Encoding.ASCII.GetBytes(_inputData)); 
            _inputPremitieves[0] = (string)JObject.Parse(_inputData)["FirstName"];
            _inputPremitieves[1] = (string)JObject.Parse(_inputData)["LastName"];
            _inputPremitieves[2] = (string)JObject.Parse(_inputData)["Age"];
            _inputPremitieves[3] = JObject.Parse(_inputData)["Address"];
            _inputPremitieves[4] = JObject.Parse(_inputData)["Hobbies"];

        }

        [Test]
        public void WhenEmptyObjectThenEmptyObject()
        {
            // Arrange
            _inputStream = new MemoryStream(Encoding.ASCII.GetBytes("{}"));

            // Act
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();

            // Assert
            Assert.AreEqual("{}", actual);
        }
        [Test]
        public void WhenPremitievePropertiesComesFirst()
        {
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();
            Assert.AreEqual(JObject.Parse(actual).SelectToken("FirstName").ToString(),_inputPremitieves[0]);
            Assert.AreEqual(JObject.Parse(actual).SelectToken("LastName").ToString(), _inputPremitieves[1]);
            // Should be  a  different object since premity value age is re order to 3 position
            Assert.AreNotEqual (JObject.Parse(actual).SelectToken("Address").ToString(), _inputPremitieves[2]);
        }
        [Test]
        public void GetFirstStringPremitieveProperties()
        {
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();
            Assert.AreEqual(JObject.Parse(actual).SelectToken("FirstName").ToString(), _inputPremitieves[0]);
            Assert.AreEqual(JObject.Parse(actual).SelectToken("LastName").ToString(), _inputPremitieves[1]);
            Assert.AreEqual(JObject.Parse(actual).SelectToken("Age").ToString(), _inputPremitieves[2]);
        }
        [Test]
        public void GetFirstObjectWhchIsThirdInOrderAsItISNonPremitieve()
        {
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();
            //Address object will re order and cannot compare
            Assert.AreNotEqual(JObject.Parse(actual).SelectToken("Address"), _inputPremitieves[3]);
            Assert.AreEqual(JObject.Parse(actual).SelectToken("Address")["StreetName"].ToString(), (string)JObject.Parse(_inputPremitieves[3].ToString()).SelectToken("StreetName"));
        }
        [Test]
        public void GetWhenObjectsArethereinsideAnObject_Like_Address_City_CODE()
        {
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();
            var cityCode= (string)JObject.Parse(_inputPremitieves[3].ToString()).SelectToken("City")["CODE"];
            Assert.AreEqual(JObject.Parse(actual).SelectToken("Address")["City"]["CODE"].ToString(), cityCode);
         
        }
        [Test]
        public void GetWhenArraysInInput()
        {
            var output = AttributeFirstJsonTransformer.Transform(_inputStream);
            var actual = new StreamReader(output).ReadToEnd();
            var cityCode = (JArray)_inputPremitieves[4];
            Assert.AreEqual(JObject.Parse(actual).SelectToken("Hobbies").First, cityCode.First);

        }
    }
}