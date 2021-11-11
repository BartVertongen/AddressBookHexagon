# Test Consuming the AddressBook WEB API


import requests

api_url = "http://localhost:5000/api/addressbook/"
urlOverview = api_url + "contact/overview"


# Build up the CreateContactCommandBuilder
jsonStartData = {"Name": "", "Phone":"", "Email":"", "Street":"", "PostalCode":"", "Town":""}
url1 = "http://localhost:5000/api/addressbook/updatecontactcommand/addname/Loan Thi Hong Phan"
response1 = requests.get(url1 , json=jsonStartData)
url2 = "http://localhost:5000/api/addressbook/updatecontactcommand/addemail/jessicaphan97@gmail.com"
response2 = requests.get(url2 , json=response1.json())
url3 = "http://localhost:5000/api/addressbook/updatecontactcommand/addphone/+32 473%2F327026"
response3 = requests.get(url3 , json=response2.json())

url4 = "http://localhost:5000/api/addressbook/updatecontactcommand/addstreet/Remparden 12 bus 43"
response4 = requests.get(url4 , json=response3.json())
url5 = "http://localhost:5000/api/addressbook/updatecontactcommand/addpostalcode/9700"
response5 = requests.get(url5 , json=response4.json())
url6 = "http://localhost:5000/api/addressbook/updatecontactcommand/addtown/Oudenaarde"
response6 = requests.get(url6 , json=response5.json())

# Build the UpdateContactCommand
url7 = "http://localhost:5000/api/addressbook/updatecontactcommand/build"
response7 = requests.get(url7 , json=response6.json())
jsonCommand = response7.json()

# Update the Contact
url8 = "http://localhost:5000/api/addressbook/contact/update"
response8 = requests.put(url8 , json=jsonCommand)

response = requests.get(urlOverview)
OverviewResult = response.json()
exit()
