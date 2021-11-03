// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application.Tests
{
    /// <summary>
    /// Give Overview of All Contacts with possible filtering.
    /// </summary>
    public class UseCase1Test
    {
        class DSAddressBookMock : IAddressBookFile
        {
            public DSAddressBookMock(IConfigurationRoot config)
            {
                FullPath = config.GetSection("ContactsFile").Value;
            }

            public string FullPath { get; private set; }

            public void Load(IAddressBookDTO book)
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

        private readonly IGetOverviewQuery _GetOverviewQueryPort;
        private List<IContactLineDTO> _ResultList;
        private string _Filter;

        public UseCase1Test()
        {
            string FullPath = Environment.CurrentDirectory + "\\AddressBookUseCase1.xml";

            Mock<IConfigurationRoot> MockConfig = new();
            MockConfig.SetupGet(p => p.GetSection("ContactsFile").Value).Returns(FullPath);

            IAddressBookFile MockDALAddressBook = new DSAddressBookMock(MockConfig.Object);
            _Filter = "";
        }

        [Theory]
        [InlineData("", 4)]
        [InlineData("a", 1)]
        [InlineData("*de*", 2)]
        [InlineData("*phi*", 1)]
        public void UseCase1Execute_WithFilter_ShouldGiveResultCount(string filter, int count)
        {
            //Arrange

            //Actions
            this.Step1(filter);
            this.Step2();
            this.Step3();

            //Assert
            Assert.Equal(count, _ResultList.Count);
        }

        /// <summary>
        /// START:TRIGGER: USER asks for an overview of the adress Book Contacts with possible filtering.
        /// </summary>
        private void Step1(string filter)
        {
            _Filter = filter;
        }

        /// <summary>
        /// The SYSTEM gets all Contacts from the DB passing the filter.
        /// </summary>
        private void Step2()
        {
            this._ResultList = _GetOverviewQueryPort.GetOverview(_Filter);
        }

        /// <summary>
        /// END: The SYSTEM shows the collected Contacts.
        /// POSTCONDITION: We should have a list of the Contacts filtered by the given filter string.
        /// </summary>
        private void Step3()
        {
            //We can not show anything because there is no user interface
        }
    }
}