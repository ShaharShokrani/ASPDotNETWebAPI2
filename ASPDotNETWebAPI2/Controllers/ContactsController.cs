using ASPDotNETWebAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPDotNETWebAPI2.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {
        Contact[] contacts = new Contact[]
        {
            new Contact() {Id=0,FirstName="Peter",LastName="Parker"},
            new Contact() {Id=1,FirstName="Bruce",LastName="Wayne"},
            new Contact() {Id=2,FirstName="Bruce",LastName="Banne"}
        };

        // GET: api/Contacts
        [Route("")]
        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        // GET: api/Contacts/5
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Contact contact = contacts.FirstOrDefault<Contact>(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpGet]
        [Route("{firstName}")]
        //[ActionName("ContactName")]
        //public IEnumerable<Contact> GetContactByName(string firstName)
        public IEnumerable<Contact> FindContactByName(string firstName)
        {
            Contact[] contactArray = contacts.Where<Contact>(c => c.FirstName == firstName).ToArray<Contact>();

            return contactArray;
        }

        // POST: api/Contacts
        [Route("")]
        public IEnumerable<Contact> Post([FromBody]Contact newContact)
        {
            List<Contact> contactsList = contacts.ToList<Contact>();
            newContact.Id = contactsList.Count;
            contactsList.Add(newContact);
            contacts = contactsList.ToArray();

            return contacts;
        }

        // PUT: api/Contacts/5
        [Route("{id:int}")]
        public IEnumerable<Contact> Put(int id, [FromBody]Contact changedContact)
        {
            Contact contact = contacts.FirstOrDefault<Contact>(c => c.Id == id);

            if (contact != null)
            {
                contact.FirstName = changedContact.FirstName;
                contact.LastName = changedContact.LastName;
            }

            return contacts;
        }

        // DELETE: api/Contacts/5
        [Route("{id:int}")]
        public IEnumerable<Contact> Delete(int id)
        {
            contacts = contacts.Where<Contact>(c => c.Id != id).ToArray<Contact>();
            return contacts;
        }
    }
}
