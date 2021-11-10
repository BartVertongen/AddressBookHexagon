//By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Services;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application;


namespace PS.AddressBook.Business.Tests
{
    /// <summary>
    /// UseCase4: Delete a Contact.
    /// </summary>
    public class UseCaseDeleteContactTest
    {
        class DALAddressBookMock : IAddressBookFile
        {
            public DALAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(ref IAddressBookDTO book)
            {
                IContactDTO NewContact1, NewContact2, NewContact3;
                IContactDTO NewContact4;

                Mock<IAddressDTO> MockAddress1 = new();
                MockAddress1.SetupGet(p => p.Street).Returns("");
                MockAddress1.SetupGet(p => p.PostalCode).Returns("");
                MockAddress1.SetupGet(p => p.Town).Returns("");
                Mock<IContactDTO> MockContact1 = new();
                MockContact1.SetupGet(p => p.Name).Returns("Elizabeth De Prinses");
                MockContact1.SetupGet(p => p.Phone).Returns("02/581.14.78");
                MockContact1.SetupGet(p => p.Email).Returns("");
                MockContact1.SetupGet(p => p.Address).Returns(MockAddress1.Object);
                NewContact1 = MockContact1.Object;
                (book as IList<IContactDTO>).Add(NewContact1);

                Mock<IAddressDTO> MockAddress2 = new();
                MockAddress2.SetupGet(p => p.Street).Returns("");
                MockAddress2.SetupGet(p => p.PostalCode).Returns("");
                MockAddress2.SetupGet(p => p.Town).Returns("");
                Mock<IContactDTO> MockContact2 = new();
                MockContact2.SetupGet(p => p.Name).Returns("André Hazes");
                MockContact2.SetupGet(p => p.Phone).Returns("");
                MockContact2.SetupGet(p => p.Email).Returns("andre@heaven.com");
                MockContact2.SetupGet(p => p.Address).Returns(MockAddress2.Object);
                NewContact2 = MockContact2.Object;
                (book as IList<IContactDTO>).Add(NewContact2);

                Mock<IAddressDTO> MockAddress3 = new();
                MockAddress3.SetupGet(p => p.Street).Returns("");
                MockAddress3.SetupGet(p => p.PostalCode).Returns("");
                MockAddress3.SetupGet(p => p.Town).Returns("");
                Mock<IContactDTO> MockContact3 = new();
                MockContact3.SetupGet(p => p.Name).Returns("Jan Franchipan");
                MockContact3.SetupGet(p => p.Phone).Returns("");
                MockContact3.SetupGet(p => p.Email).Returns("jan@eigenbelang.be");
                MockContact3.SetupGet(p => p.Address).Returns(MockAddress3.Object);
                NewContact3 = MockContact3.Object;
                (book as IList<IContactDTO>).Add(NewContact3);

                Mock<IAddressDTO> MockAddress4 = new();
                MockAddress4.SetupGet(p => p.Street).Returns("Weverijstraat 12");
                MockAddress4.SetupGet(p => p.PostalCode).Returns("9500");
                MockAddress4.SetupGet(p => p.Town).Returns("Geraardsbergen");
                Mock<IContactDTO> MockContact4 = new();
                MockContact4.SetupGet(p => p.Name).Returns("Josephine DePin");
                MockContact4.SetupGet(p => p.Phone).Returns("054/44.87.26");
                MockContact4.SetupGet(p => p.Email).Returns("jospin@proximus.be");
                MockContact4.SetupGet(p => p.Address).Returns(MockAddress4.Object);
                NewContact4 = MockContact4.Object;
                (book as IList<IContactDTO>).Add(NewContact4);
            }

            public void Save(IAddressBookDTO book)
            {
                throw new NotImplementedException();
            }
        }

        private IAddressBookFile _DALAddressBookFilePort;
        private readonly IGetOverviewQuery _GetOverviewPort;
        private readonly IDeleteContactUseCase _DeleteContactPort;

 
        private List<IContactLineDTO> _ResultList;
        private string _Filter, _SelectedName;

        public UseCaseDeleteContactTest()
        {
            this.PreCondition();
            _DeleteContactPort = new DeleteContactService(_DALAddressBookFilePort);
        }

        [Theory]
        [InlineData("a", 1)]
        [InlineData("*de*", 1)]
        [InlineData("*phi*", 1)]
        public void UseCase4Execute_DeleteOneContact_ShouldHaveCountOneLess(string filter, int selection)
        {
            //Arrange

            //Action
            this.Step1_2_SYS_AsksForFilter_USER_givesTheFilter(filter);
            this.Step3_SYS_ShowsAllContactsPassingFilterToChooseFrom();
            this.Step4_USER_SelectsContactToDelete(selection);
            this.Step5_6_SYS_DeletesChosenContactAndMakePersistant();

            //Assert
            Assert.Equal(3, _GetOverviewPort.GetOverview().Count);
        }

        /// <summary>
        /// You need an AddressBook File with Contacts.
        /// </summary>
        private void PreCondition()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase4.xml";
            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns("AddressBookUseCase4.xml");

            _DALAddressBookFilePort = new DALAddressBookMock(MockConfig.Object);
            _Filter = "";
        }

        /// <summary>
        /// START:TRIGGER: USER asks for a delete.
        /// STEP1: The SYSTEM asks for a filter to show possible Contacts to delete.
        /// </summary>
        private void Step1_2_SYS_AsksForFilter_USER_givesTheFilter(string filter)
        {
            //Console.WriteLine($"UseCase4: Delete a Contact.");
            //Console.Write($"Give in the filter you want to select the Contact: ");
            _Filter = filter;
        }

        /// <summary>
        /// STEP3: The System shows all possible Contacts.
        /// </summary>
        private void Step3_SYS_ShowsAllContactsPassingFilterToChooseFrom()
        {
            this._ResultList = _GetOverviewPort.GetOverview(_Filter);
        }

        /// <summary>
        /// STEP4: The USER selects the Contact he wants to Delete.
        /// </summary>
        private void Step4_USER_SelectsContactToDelete(int id)
        {
            this._SelectedName = this._ResultList[id-1].Name;               
        }

        /// <summary>
        /// STEP 5:  The SYSTEM adapts the AddressBook, does the Delete.
        /// </summary>
        private void Step5_6_SYS_DeletesChosenContactAndMakePersistant()
        {
            DeleteContactCommandBuilder oBuilder = new();

            oBuilder.AddName(_SelectedName);
            DeleteContactCommand oCommand = (DeleteContactCommand)oBuilder.Build();
            _DeleteContactPort.DeleteContact(oCommand);
        }
    }
}