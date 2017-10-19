using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
/*
 * 参考地址：http://www.cnblogs.com/wanliwang01/p/ASP_NET_WebAPI.html
 * **/
namespace WebApi.Controllers
{

    public class ContactsController : ApiController
    {
        private static IList<Contact> contacts = new List<Contact>
        {
            new Contact{
            Id="001",
            Name="Xixi",
            PhoneNo="12132432",
            EmailAddress="xixi@gmail.com"
            },
            new Contact{
            Id="002",
            Name="XiongEr",
            PhoneNo="312",
            EmailAddress="XiongEr@gmail.com"
            }
        };

        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        public Contact Get(string id)
        {
            return contacts.FirstOrDefault(p => p.Id == id);
        }

        public void Put(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            contacts.Add(contact);
        }

        public void Post(Contact contact)
        {
            Delete(contact.Id);
            contacts.Add(contact);
        }

        public void Delete(string id)
        {
            Contact tempContact = contacts.FirstOrDefault(p => p.Id == id);
            contacts.Remove(tempContact);
        }
    }

    public class Contact
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string EmailAddress { get; set; }
    }



}

