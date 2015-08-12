using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopCheck.Library.data
{
    public class PersonatorErrorCodes
    {
        public class ErrorItem
        {
            public string code { get; set; }
            public string shortMsg { get; set; }
            public string longMsg { get; set; }
        }
        private List<ErrorItem> list = new List<ErrorItem>();
        public PersonatorErrorCodes()
        {
            list.Add(new ErrorItem() { code = "AS01", shortMsg = "Address Fully Verified", longMsg = "The address is valid and deliverable according to official postal agencies." });
            list.Add(new ErrorItem() { code = "AS02", shortMsg = "Street Only Match", longMsg = "The street address was verified but the suite number is missing or invalid." });
            list.Add(new ErrorItem() { code = "AS03", shortMsg = "Non USPS Address Match US Only", longMsg = "This US address is not serviced by the USPS but does exist and may receive mail through third party carriers like UPS." });
            list.Add(new ErrorItem() { code = "AS09", shortMsg = "Foreign Address", longMsg = "The address is in a non-supported country." });
            list.Add(new ErrorItem() { code = "AS10", shortMsg = "CMRA Address US Only", longMsg = "The address is a Commercial Mail Receiving Agency (CMRA) like a Mailboxes Ect. These addresses include a Private Mail Box (PMB or #) number." });
            list.Add(new ErrorItem() { code = "AS12", shortMsg = "Record Move", longMsg = "The record moved to a new address." });
            list.Add(new ErrorItem() { code = "AS13", shortMsg = "Address Updated By LACS US Only", longMsg = "The address has been converted by LACSLink? from a rural-style address to a city-style address." });
            list.Add(new ErrorItem() { code = "AS14", shortMsg = "Suite Appended US Only", longMsg = "A suite was appended by SuiteLink? using the address and company name." });
            list.Add(new ErrorItem() { code = "AS15", shortMsg = "Apartment Appended", longMsg = "An apartment number was appended by AddressPlus using the address and last name." });
            list.Add(new ErrorItem() { code = "AS16", shortMsg = "Vacant Address US Only", longMsg = "The address has been unoccupied for more than 90 days." });
            list.Add(new ErrorItem() { code = "AS17", shortMsg = "No Mail Delivery US Only", longMsg = "The address does not currently receive mail but will likely in the near future." });
            list.Add(new ErrorItem() { code = "AS20", shortMsg = "Deliverable only by USPS US Only", longMsg = "This address can only receive mail delivered through the USPS (ie. PO Box or a military address)." });
            list.Add(new ErrorItem() { code = "AS23", shortMsg = "Extraneous Information", longMsg = "Extraneous information not used in verifying the address was found. This has been placed in the ParsedGarbage field." });
            list.Add(new ErrorItem() { code = "AE01", shortMsg = "Postal Code Error", longMsg = "The Postal Code does not exist and could not be determined by the city/municipality and state/province." });
            list.Add(new ErrorItem() { code = "AE02", shortMsg = "Unknown Street", longMsg = "Could not match the input street to a unique street name. Either no matches or too many matches found." });
            list.Add(new ErrorItem() { code = "AE03", shortMsg = "Component Mismatch Error", longMsg = "The combination of directionals (N, E, SW, etc) and the suffix (AVE, ST, BLVD) is not correct and produced multiple possible matches." });
            list.Add(new ErrorItem() { code = "AE04", shortMsg = "Non-Deliverable Address US Only", longMsg = "A physical plot exists but is not a deliverable addresses. One example might be a railroad track or river running alongside this street, as they would prevent construction of homes in that location." });
            list.Add(new ErrorItem() { code = "AE05", shortMsg = "Multiple Match", longMsg = "The address was matched to multiple records. There is not enough information available in the address to break the tie between multiple records." });
            list.Add(new ErrorItem() { code = "AE06", shortMsg = "Early Warning System US Only", longMsg = "This address currently cannot be verified but was identified by the Early Warning System (EWS) as containing new streets that might be confused with other existing streets." });
            list.Add(new ErrorItem() { code = "AE07", shortMsg = "Missing Minimum Address Minimum requirements for the address to be verified is not met", longMsg = "Address must have at least one address line and also the postal code or the locality/administrative area." });
            list.Add(new ErrorItem() { code = "AE08", shortMsg = "Sub Premise Number Invalid", longMsg = "The thoroughfare (street address) was found but the sub premise (suite) was not valid." });
            list.Add(new ErrorItem() { code = "AE09", shortMsg = "Sub Premise Number Missing", longMsg = "The thoroughfare (street address) was found but the sub premise (suite) was missing." });
            list.Add(new ErrorItem() { code = "AE10", shortMsg = "Premise Number Invalid", longMsg = "The premise (house or building) number for the address is not valid." });
            list.Add(new ErrorItem() { code = "AE11", shortMsg = "Premise Number Missing", longMsg = "The premise (house or building) number for the address is missing." });
            list.Add(new ErrorItem() { code = "AE12", shortMsg = "Box Number Invalid", longMsg = "The PO (Post Office Box), RR (Rural Route), or HC (Highway Contract) Box numer is invalid." });
            list.Add(new ErrorItem() { code = "AE13", shortMsg = "Box Number Missing", longMsg = "The PO (Post Office Box), RR (Rural Route), or HC (Highway Contract) Box number is missing." });
            list.Add(new ErrorItem() { code = "AE14", shortMsg = "PMB Number Missing", longMsg = "US Only. The address is a Commercial Mail Receiving Agency (CMRA) and the Private Mail Box (PMB or #) number is missing." });
            list.Add(new ErrorItem() { code = "AE17", shortMsg = "Sub Premise Not Required", longMsg = "A sub premise (suite) number was entered but the address does not have secondaries." });
            list.Add(new ErrorItem() { code = "AC01", shortMsg = "Postal Code Change", longMsg = "The postal code was changed or added." });
            list.Add(new ErrorItem() { code = "AC02", shortMsg = "Administrative Area Change", longMsg = "The administrative area (state, province) was added or changed." });
            list.Add(new ErrorItem() { code = "AC03", shortMsg = "Locality Change The locality", longMsg = "(city, municipality) name was added or changed." });
            list.Add(new ErrorItem() { code = "AC04", shortMsg = "Alternate to Base Change US Only", longMsg = "The address was found to be an alternate record and changed to the base (preferred) version." });
            list.Add(new ErrorItem() { code = "AC05", shortMsg = "Alias Name Change US Only", longMsg = "An alias is a common abbreviation for a long street name, such as MLK Blvd for Martin Luther King Blvd. This change code indicates that the full street name (preferred) has been substituted for the alias." });
            list.Add(new ErrorItem() { code = "AC06", shortMsg = "Address1/Address2 Swap", longMsg = "Address1 was swapped with Address2 because Address1 could not be verified and Address2 could be verified." });
            list.Add(new ErrorItem() { code = "AC07", shortMsg = "Address1 & Company Swapped", longMsg = "Address1 was swapped with Company because only Company had a valid address." });
            list.Add(new ErrorItem() { code = "AC08", shortMsg = "Plus4 Change US Only", longMsg = "A non-empty plus4 was changed." });
            list.Add(new ErrorItem() { code = "AC09", shortMsg = "Dependent Locality Change US Only", longMsg = "The dependent locality (urbanization) was changed." });
            list.Add(new ErrorItem() { code = "AC10", shortMsg = "Thoroughfare Name Change", longMsg = "The thoroughfare (street) name was changed due to a spelling correction." });
            list.Add(new ErrorItem() { code = "AC11", shortMsg = "Thoroughfare Suffix Change", longMsg = "The thoroughfare (street) suffix was added or changed, such as from St to Rd." });
            list.Add(new ErrorItem() { code = "AC12", shortMsg = "Thouroughfare Directional Change", longMsg = "" });
            list.Add(new ErrorItem() { code = "AC13", shortMsg = "Sub Premise Type Change", longMsg = "The sub premise (suite) type was added or changed, such as from STE to APT." });
            list.Add(new ErrorItem() { code = "AC14", shortMsg = "Sub Premise Number Change", longMsg = "The sub premise (suite) unit number was added or changed." });
            list.Add(new ErrorItem() { code = "AC20", shortMsg = "House Number Change", longMsg = "The house number was changed." });
            list.Add(new ErrorItem() { code = "SE01", shortMsg = "Web Service Internal Erro", longMsg = "The web service experienced an internal error." });
            list.Add(new ErrorItem() { code = "GE01", shortMsg = "Empty Request Structure", longMsg = "The SOAP, JSON, or XML request structure is empty." });
            list.Add(new ErrorItem() { code = "GE02", shortMsg = "Empty Request Record Structure", longMsg = "The SOAP, JSON, or XML request record structure is empty." });
            list.Add(new ErrorItem() { code = "GE03", shortMsg = "Records Per Request Exceeded", longMsg = "The counted records sent more than the number of records allowed per request." });
            list.Add(new ErrorItem() { code = "GE04", shortMsg = "Empty CustomerID", longMsg = "The CustomerID is empty." });
            list.Add(new ErrorItem() { code = "GE05", shortMsg = "Invalid CustomerID", longMsg = "The CustomerID is invalid." });
            list.Add(new ErrorItem() { code = "GE06", shortMsg = "Disabled CustomerID", longMsg = "The CustomerID is disabled." });
            list.Add(new ErrorItem() { code = "GE07", shortMsg = "Invalid Request", longMsg = "The SOAP, JSON, or XML request is invalid." });
            list.Add(new ErrorItem() { code = "GE08", shortMsg = "Invalid CustomerID for Product", longMsg = "The CustomerID is invalid for this product." });
            list.Add(new ErrorItem() { code = "GE20", shortMsg = "Verify Not Activated", longMsg = "The Verify package was requested but is not active for the Customer ID." });
            list.Add(new ErrorItem() { code = "GE21", shortMsg = "Append Not Activated", longMsg = "The Append package was requested but is not active for the Customer ID." });
            list.Add(new ErrorItem() { code = "GE22", shortMsg = "Move Not Activated", longMsg = "The Move package was requested but is not active for the Customer ID." });

        }
        public ErrorItem Find(string code)
        {
            var retVal = new ErrorItem();
            foreach (ErrorItem item in list)
            {
                if (item.code == code)
                {
                    retVal = item;
                    break;
                }
            }
            return retVal;
        }
    }
}
