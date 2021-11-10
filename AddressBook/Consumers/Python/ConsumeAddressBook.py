"""
Consume the AddressBook WEB API
""" 

import requests

api_url = "http://localhost:5000/api/addressbook/"
urlOverview = api_url + "contact/overview"
urlCreateCmd = api_url + "createcontactcommand"

#response = requests.get(urlOverview)
#OverviewResult = response.json()

response = requests.get(urlCreateCmd +"/addname/Loan20%Thi20%Hong20%Phan" )
CreateCmdAddNameResult = response.json()


#response = requests.post(api_url, json=todo)
exit()
