# Test Consuming the AddressBook WEB API


import requests

api_url = "http://localhost:5000/api/addressbook/"
urlOverview = api_url + "contact/overview"


# Build up the DeleteContactCommandBuilder
jsonStartData = {"Name": ""}
url1 = "http://localhost:5000/api/addressbook/deletecontactcommand/addname/Loan Thi Hong Phan"
response1 = requests.get(url1 , json=jsonStartData)

# Build the DeleteContactCommand
url2 = "http://localhost:5000/api/addressbook/deletecontactcommand/build"
response2 = requests.get(url2 , json=response1.json())

# Delete the Contact
url3 = "http://localhost:5000/api/addressbook/contact/delete"
response3 = requests.delete(url3 , json=response2.json())

response = requests.get(urlOverview)
OverviewResult = response.json()
exit()
