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
    {   private readonly ContactServices _services;
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
            var contacts= await _services.GetContacts();
            return contacts;
        }

        [HttpGet("GetContactById/{id}")]
        public async Task<ActionResult<ContactModel>> GetContactsById(int id)
        {   
            var contact= await _services.GetContactById(id);
            return contact;
        }

        [HttpPut("UpdateContact")]
        public async Task<ActionResult<ContactModel>> UpdateContact(int id, string name, string email, string phone)
        {  
            var contact = await _services.UpdateContact(id, name, email, phone);
            return Ok(contact);
        }
        [HttpDelete("DeleteContact")]
        public async Task<IActionResult> DeleteContact(int id)
        { 
           var contact = await _services.DeleteContact(id);
            return Ok(contact);
        }

    }
}