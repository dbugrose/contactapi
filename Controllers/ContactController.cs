using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using contactapi.Models;
using contactapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace contactapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactServices _services;
        public ContactController(ContactServices services)
        {
            _services = services;
        }

        [HttpPost("AddContact")]
        public async Task<ActionResult<ContactModel>> AddContact(ContactModel contact)
        {
            var newContact = await _services.AddContact(contact);
            return newContact;
        }

        [HttpGet("GetContacts")]
        public async Task<List<ContactModel>> GetContacts()
        {
            var contacts = await _services.GetContacts();
            return contacts;
        }

        [HttpGet("GetContactsByUserId/{id}")]
        public async Task<List<ContactModel>> GetContactsByUserId(int id)
        {
            var contacts = await _services.GetContactsByUserId(id);
            return contacts;
        }

        [HttpGet("GetContactById/{id}")]
        public async Task<ActionResult<ContactModel>> GetContactsById(int id)
        {
            var contact = await _services.GetContactById(id);
            return contact;
        }

        [HttpGet("GetContactBySearch/{contact}")]
        public async Task<List<ContactModel>> GetContactsBySearch(string contact)
        {
            var search = await _services.GetContactBySearch(contact);
            return search;
        }

        [HttpPut("UpdateContact")]
        public async Task<ActionResult<ContactModel>> UpdateContact(ContactModel contact)
        {
            var contactToEdit = await _services.UpdateContact(contact);
            return Ok(contactToEdit);
        }
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _services.DeleteContact(id);
            return Ok(contact);
        }

    }
}