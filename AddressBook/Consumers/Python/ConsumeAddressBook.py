"""
Consume the AddressBook WEB API
""" 

import requests

api_url = "http://localhost:5000/api/addressbook/"
urlOverview = api_url + "contact/overview"
urlCreateCmd = api_url + "createcontactcommand"

#response = requests.get(urlOverview)
#OverviewResult = response.json()

jsonData = {"Name": "", "Phone":"", "Email":"", "Street":"", "PostalCode":"", "Town":""}
response = requests.get(urlCreateCmd +"/addname/Loan20%Thi20%Hong20%Phan" , json=jsonData)
jsonData = response.json()
url2 = "http://localhost:5000/api/addressbook/createcontactcommand/addemail/jessicaphan97@gmail.com"
response = requests.get(url2 , json=jsonData)
jsonData3 = response.json()
response = requests.get(urlCreateCmd +"/addstreet/Remparden20%1220%bus20%43" , json=jsonData3)
jsonData4 = response.json()

#response = requests.post(api_url, json=todo)
exit()
