# myRetail
myRetail Restful service
TargetCaseStudy
Author :Jeya M

1.	myRetail RESTful service

myRetail is a rapidly growing company with HQ in Richmond, VA and over 200 stores across the east coast. myRetail wants to make its internal data available to any number of client devices, from myRetail.com to native mobile apps. 
The goal for this exercise is to create an end-to-end Proof-of-Concept for a products API, which will aggregate product data from multiple sources and return it as JSON to the caller. 
Your goal is to create a RESTful service that can retrieve product and price details by ID. The URL structure is up to you to define, but try to follow some sort of logical convention.
Build an application that performs the following actions: 
•	Responds to an HTTP GET request at /products/{id} and delivers product data as JSON (where {id} will be a number. 
Example product IDs: 13860428, 54456119, 13264003, 12954218) 
•	Example response: {"id":13860428,"name":"The Big Lebowski (Blu-ray) (Widescreen)","current_price":{"value": 13.49,"currency_code":"USD"}}
•	Performs an HTTP GET to retrieve the product name from an external API. (For this exercise the data will come from redsky.target.com, but let’s just pretend this is an internal resource hosted by myRetail) 

•	Example: 
https://redsky-uat.perf.target.com/redsky_aggregations/v1/redsky/case_study_v1?key=3yUxt7WltYG7MFKPp7uyELi1K40ad2ys&tcin=13860428
•	Reads pricing information from a NoSQL data store and combines it with the product id and name from the HTTP request into a single response. 
•	BONUS: Accepts an HTTP PUT request at the same path (/products/{id}), containing a JSON request body similar to the GET response, and updates the product’s price in the data store. 

----------------------------------------------------------------------------------------------------------------------------------------------

##Technology Stack##

1.C#/ASP.NET
 
 Frameworks:  
       • Microsoft.AspNetCore.App
       • MIcrosoft.NETCORE.App  

2.MONGO DB 4.4.11
3.Swagger 
4.Visual Studio 2022


----------------------------------------------------------------------------------------------------------------

##List of Packages installed for this Project##

1. Microsoft.AspNet.WebApi. 5.2.7
2.MongoDB.Driver 2.14.1
3.Swashbuckle.AspNetCore 6.2.3

------------------------------------------------------------------------------------------------

## Mongo DB database and collection##


"ConnectionString": "mongodb+srv://myretailrestful:Target123@productprice.ktgcd.mongodb.net/myFirstDatabase?retryWrites=true&w=majority",
"DatabaseName": "ProductDetails",
"ProductsCollectionName": "ProductPrices"

Queries used to add items into DB
Step 1: Log into/Open cmd for your machine
Step 2: Open Mongo DB folder where it is installed (in my case its C drive.). 
        Type the following command in cmd prompt
        cd C:\mongosh-1.1.9-win32-x64\bin
Step 3: Type mongosh "mongodb+srv://productprice.ktgcd.mongodb.net/myFirstDatabase" --username myretailrestful
        Enter Password : **your password for mongo DB** 

Step 4: Execute the following query one by one to add the details into DB.


use ProductDetails
db.createCollection("ProductPrices")
db.ProductPrices.insertMany([{ "Id": 13860428, "Price": 54.93, "Currency": "USD" }, { "Id": 54456119, "Price": 50.93, "Currency": "USD" }, { "Id": 13264003, "Price": 40.93, "Currency": "USD" },{ "Id": 12954218, "Price": 40.93, "Currency": "USD" }])
db.ProductPrices.find().pretty()

Step 5 : Items will be added into the DB

--------------------------------------------------------------------------------------------------------------

##Steps to launch the application##
# Calling myretail api services#

1. Select MyRetail.sln from the visual studio and Run
2. Swagger will be launched @ https://localhost:7073/swagger/index.html

Performing GET request on https://localhost:7073/Products/13264003 will return json object with product information and pricing information.

GET https://localhost:7073/Products/13264003

Response:-
{
   "id":13264003,
   "name":"Jif Natural Creamy Peanut Butter - 40oz",
   "currentPrice":{
      "value":40.93,
      "currency":"USD"
   }
}


To perform PUT operation, send JSON object with updated price in request body, it will return JSON object with updated pricing information.

PUT https://localhost:7073/Products/13264003

Response

{
  "id": 13264003,
  "name": "Jif Natural Creamy Peanut Butter - 40oz",
  "currentPrice": {
    "value": 200,
    "currency": "USD"
  }
}

***** Price is updated from 40.93 to 200********
----------------------------------------------------------------------------------------------------------------



UNIT test results 
1. Validated across different scenarios. Screen shots will be shared 
------------------------------------------------------------------------------------------------------------------
