# AddressBook Project

## User Stories

### Epic: Implement a digital Adress Book.

#### Feature A: The user should be able to consult the AdressBook
##### UserStory A1: Complete Overview
    A USER wants    
        An overview of all contact,      
        So he gets an idea how much contacts he has.    

##### UserStory A2: Filtered Overview
    A USER wants     
        a Filtered Overview of the Contacts,    
        So it becomes easier to find the right Contact. 

#### UserStory A3: Select a Contact
    A USER wants        
        To Select a Name in the list containing a certain string,
        So decide on which Contact the next operation will work.    
						
#### Feature B: The User should be able to Maintain the Contacts (update, delete or change Contacts--CRUD)
#### UserStory B1:
    A USER wants        
        To add a New Contact,      
        So he can use that data later when he needs it.     

#### UserStory B2:
    A USER wants        
        To change or extend the Data about a Contact,       
        So it stays updated.        

#### UserStory B3:
    A User wants        
        To delete an contact that is not used anymore,      
        So it does not find it again in the overview.

---
## Use Cases

### UseCase 1: Give a short Overview of Contacts with possible filtering.
#### Related UserStories:
- UserStory A1: Complete Overview
- UserStory A2: Filtered Overview

#### Trigger
User asks for an overview of the address Book Contacts with possible filtering.

#### UseCase1 Main Steps: Get Overview
1) SYSTEM asks the filter
2) USER gives in the Filter
3) SYSTEM gets all Contacts from the DB passing the filter.
4) END: The System shows the collected Contacts.

PostCondition: Add A code like 
			APE meaning Has adress, Email and Phone 
			AP* means has Adress and Phone.

#### UseCase1.2A Sunny Extension Steps: No Filter
2) USER gives an empty string as Filter
3) SYSTEM gets all Contacts from the DB.
4) END: The System shows the collected Contacts.

#### UseCase1.2B Sunny Extension Steps: Filter
2) USER gives an non empty string as Filter
3) SYSTEM gets all Contacts from the DB starting with the given string.
4) END: The System shows the collected Contacts.

#### UseCase1.2C Sunny Extension Steps: Filter with format '*string*'
PRECONDITION: The filter is not empty and contains char '*' at the start and end.
		The filter is just a *string* and the Contacts with a Name 
			containing the filter will be shown.        
2) USER gives an non empty string as Filter  which contains char '*' at the start and end.         
3) SYSTEM gets all Contacts from the DB passing the Contains filter string without the '*' chars.       
4) END: SYSTEM shows all Contacts passing the filter.


### UseCase2: Creation of a new Contact.

#### Related UserStories:
- UserStory B1: Create a New Contact

#### Trigger
USER asks for Creation of a new Contact.

#### UseCase2 Main Steps: Creation of a new Contact.
1) SYSTEM asks for a Name Input
2) USER supplies a Not Null Name
3) SYSTEM checks if the Name is new and valid.
4) UseCase2.4: USER Gives in the New Address
5) SYSTEM will ask for the Phone Number.
6) USER Supplies a Valid Phone Number.
7) SYSTEM asks for a Valid Email adress.
8) USER Supplies a Valid Email Adress.
9) END: SYSTEM will add the new Contact to the AdressBook and Xml.

#### UseCase 2.3A Rainy Extension Steps: The Contact Name exists already.
PRECONDITION: The Name given by the User for New Contact exists already
1) SYSTEM: gives a message that a Contact with this name exists already.
2) SYSTEM gives a message that you can change this Contact.
3) END: SYSTEM ends the creation process of a New Contact

#### UseCase 2.3B Rainy Extension Steps : The Contact Name is Empty.
PRECONDITION: The User gave an Empty Name.
1) START: SYSTEM gives a message that the given New Name can not be Empty.
2) END: SYSTEM ends the creation process of a New Contact.

#### UseCase2.4 Main Summer Steps: Give in the New Address
PRECONDITIONS:      
    - We are in the Process of creating a new Contact.      
    - The Unique valid Name is already given.
1) START: SYSTEM asks for the Street.
2) USER gives a valid Street.
3) SYSTEM asks for the Postal Code.
4) USER gives in a valid Postal Code.
5) SYSTEM asks for The Town
6) USER gives in a valid Town.
7) END: SYSTEM Creates A Valid Address with the Input.      

#### UseCase 2.4.4A Extended Rainy Steps: The User gives an Invalid (Empty) Postal Code.
PRECONDITION:        
    - We are in the Process of creating a new Address UseCase 2.4.      
    - The Street was not Empty.
1) START: USER gives in an Empty Postal Code.
2) End: SYSTEM creates an Empty Address.        
	Comment: An Address with Empty Postal Code and Non-Empty Street , can never be a valid Address.     
POSTCONDITION: Empty Address.

#### UseCase2.4_6A Extended Rainy Steps: The User gives an Invalid (Empty) Town.
PRECONDITION:             
    - We are in the Process of creating a new Address.      
    - The Street or PostalCode was not Empty.
1) START: USER gives in an Empty Town.
2) End: SYSTEM creates an Empty Address.        
	Comment: An Address with Empty Town but Valid Street, can never be a valid Address.     
POSTCONDITION: Empty Address.


### UseCase3: Updating a Contact.

#### Related UserStories:
- UserStory B2: To change or extend the Data about a Contact.

#### Trigger
USER asks for Update of a new Contact.

#### UseCase3 Main Steps: Update existing Contact
1) START: USER requests a Contact Update
2) SYSTEM asks for a filter to show possible Contacts.
3) USER gives the filter pattern.
4) SYSTEM shows all possible Contacts. 
5) USER selects the Contact he wants to Update.
6) SYSTEM Retrieves the Contact with that Name.
7) USECASE 3.7: Update the Address.	 
8) USECASE 3.8: to Update The PhoneNumber
9) USECASE 3.9 to update the EmailAddress
10) END: Make the changes persistent and update the AddressBook.


#### UseCase3.7 Main Steps Sunny: Update the Address.
PRECONDITION:       
    - We are in the process of updating a Contact.      
1) START: SYSTEM shows the old Street value.
2) SYSTEM asks for the New Value.
3) USE gives in the new valid Street.
4) SYSTEM shows the old Postal Code.
5) SYSTEM asks for the new value of the Postal Code.
6) USE gives in the new valid value for the Postal Code.
7) SYSTEM shows the old Town.
8) SYSTEM asks for the new value of the Town.
9) USE gives in the new value for the Town.
10) END: SYSTEM creates a valid Address and assigns it to the Current Contact.      
POSTCONDITION: We have a Valid new Address.


#### UseCase3.8 Main Sunny Steps : Update The PhoneNumber
PRECONDITION:       
    - We are in the process of updating a Contact.      
	- We have a valid Contact Name and Address.     
1) START: SYSTEM show the old PhoneNumber.
2) SYSTEM asks for the new value of the PhoneNumber.
3) USER gives in the new valid value for the PhoneNumber.
4) END: SYSTEM sets the PhoneNumber for the Current Contact.

POSTCONDITION:      
    - We have a valid PhoneNumber in the Contact.      


#### UseCase3.9 Main Sunny Steps : Update The EmailAddress.
PRECONDITION:       
    - We are in the process of updating a Contact.      
    - We have a valid Contact Name and Address.         
1) START: SYSTEM show s the old EmailAddress.
2) SYSTEM asks for a new value of the Email.
3) USER gives in the New valid value for the Email.
4) END: SYSTEM sets the Email for the Current Contact.

POSTCONDITION:      
    - We have a valid Email Address in the Contact.


#### UseCase 3.7.3A Extended Rainy Steps : The User gives in an empty Street.
PRECONDITION:       
    - Updating a Contact, Updating the Address.        
1) START: USE gives in an Empty Street or nothing      
2) SYSTEM creates an Empty Address      

#### UseCase 3.7.6A Extended Rainy Steps: The User gives in an Empty Postal Code.
PRECONDITION:
    - Updating a Contact, Updating the Address.          
1) START: USER gives in an Empty Postal Code or nothing
2) END: SYSTEM creates an Empty Address


#### UseCase 3.7.8A Extended Rainy Steps: The User gives in an Empty Town.
PRECONDITION:       
    - Updating a Contact, Updating the Address.
1) START: USER gives in an Empty Town or nothing
2) END: SYSTEM creates an Empty Address     

