# Test Consuming the AddressBook WEB API


import requests

api_url = "http://localhost:5000/api/addressbook/"
urlOverview = api_url + "contact/overview"
urlCreateCmd = api_url + "createcontactcommand"


# Build up the CreateContactCommandBuilder
jsonStartData = {"Name": "", "Phone":"", "Email":"", "Street":"", "PostalCode":"", "Town":""}
url1 = "http://localhost:5000/api/addressbook/createcontactcommand/addname/Loan Thi Hong Phan"
response1 = requests.get(url1 , json=jsonStartData)
url2 = "http://localhost:5000/api/addressbook/createcontactcommand/addemail/jessicaphan97@gmail.com"
response2 = requests.get(url2 , json=response1.json())
url3 = "http://localhost:5000/api/addressbook/createcontactcommand/addstreet/Remparden 12 bus 43"
response3 = requests.get(url3 , json=response2.json())
url4 = "http://localhost:5000/api/addressbook/createcontactcommand/addpostalcode/9700"
response4 = requests.get(url4 , json=response3.json())
url5 = "http://localhost:5000/api/addressbook/createcontactcommand/addtown/Oudenaarde"
response5 = requests.get(url5 , json=response4.json())

# Build the CreateContactCommand
url6 = "http://localhost:5000/api/addressbook/createcontactcommand/build"
response6 = requests.get(url6 , json=response5.json())
jsonCommand = response6.json()

# Create the Contact
url7 = "http://localhost:5000/api/addressbook/contact/create"
response7 = requests.post(url7 , json=jsonCommand)

response = requests.get(urlOverview)
OverviewResult = response.json()
exit()
