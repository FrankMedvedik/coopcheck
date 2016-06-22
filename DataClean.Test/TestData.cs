using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Contracts.Models;

namespace DataClean.Test
{
    static class TestData
    {

        public static  InputStreetAddress GoodAddresstoClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };
        public static  InputStreetAddress BadStreetAddressToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };

        public static InputStreetAddress MissingCityToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par",
            //City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };
        public static InputStreetAddress BadPostalCodetoClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            //PostalCode = "",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };
        public static  InputStreetAddress BadCityAndPostalCodetoClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            //City = "Doylestown",
            State = "PA",
            //PostalCode = "",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };

        public static  InputStreetAddress BadEmailToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            //EmailAddress = "fmedvedik.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };

        public static  InputStreetAddress BadPhoneToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "01234569",
            FirstName = "frank",
            LastName = "medvedik"
        };


        public static  InputStreetAddress BadFirstNameToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "",
            LastName = "medvedik"
        };


        public static  InputStreetAddress BadLastNameToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "987"
        };

        public static InputStreetAddress MissingStateToClean = new InputStreetAddress()
        {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            //State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "987"
        };

        public static InputStreetAddress OKCompleteAddresstoClean = new InputStreetAddress()
            {
            AddressLine1 = "2005 par drive",
            City = "Doylestown",
            State = "PA",
            PostalCode = "18901",
            EmailAddress = "fmedvedik@gmail.com",
            PhoneNumber = "2159181557",
            FirstName = "frank",
            LastName = "medvedik"
        };
    }
}
