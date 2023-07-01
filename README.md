# jsonParser
Format the stream of JSON string to reorder the valued types and string to start of the object
Since the primitive properties are reorder to show at first place of the json master object, I have loop through and find the premitieve properties from the given json.
I build a json like below which will have a bit complex structure of json 
{
  "FirstName": "Arthur",
  "LastName": "Bertrand",
  "Adrress": {
    "StreetName": "Gedempte Zalmhaven",
    "Number": "4K",
    "City": {
      "Name": "Rotterdam",
      "Country": "Netherlands"
    },
    "ZipCode": "3011 BT"
  },
  "Age": 35,
  "Hobbies": [
    "Fishing",
    "Rowing"
  ]
}
Here is this Address object has again an object inside it. My soluction will apply the same filter mechanism to the inner objects also.
result will lookes like 
{
  "FirstName": "Arthur",
  "LastName": "Bertrand",
  "Age": 35,
  "Adrress": {
    "StreetName": "Gedempte Zalmhaven",
    "Number": "4K",
    "ZipCode": "3011 BT",
    "City": {
      "Name": "Rotterdam",
      "Country": "Netherlands"
    }
  },
  "Hobbies": [
    "Fishing",
    "Rowing"
  ]
}
I have used newtonsoft libraries for the json processes.
