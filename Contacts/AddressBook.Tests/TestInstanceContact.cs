using Microsoft.Communications.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Tests
{
    internal class TestInstanceContact
    {
        public string FormattedName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Notes { get; }


        public TestInstanceContact(Contact contact)
        {
            this.FormattedName = contact.Names[0].FormattedName;
            this.Email = contact.EmailAddresses.Select(e => e.Address)
                .Where(a => a != string.Empty).First();
            this.PhoneNumber = contact.PhoneNumbers.Select(pn => pn.Number)
                .Where(n => n != string.Empty).First();
            this.Notes = contact.Notes;
        }
    }
}
