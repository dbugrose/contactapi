using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using contactapi.Services.Context;
using contactapi.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using PhoneNumbers;

namespace contactapi.Services
{
    public class ContactServices : ControllerBase
    {
        private readonly DataContext _context;

        public ContactServices(DataContext context)
        {
            _context = context;
        }


        public async Task<ContactModel> AddContact(ContactModel contact)
        {
            if (!MailAddress.TryCreate(contact.Email, out MailAddress address))
                return null;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<List<ContactModel>> GetContacts () => await _context.Contacts.ToListAsync();


        public async Task<ContactModel> GetContactById(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<ContactModel?> UpdateContact(int id, string name, string email, string phone)
        {

            if (!MailAddress.TryCreate(email, out MailAddress address))
                return null;
            var contact = await GetContactById(id);
            contact.Name = name;
            contact.Email = address.Address;
            contact.Phone = phone;
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = await GetContactById(id);
            _context.Contacts.Remove(contact);
            return await _context.SaveChangesAsync() != null;
        }
    }
}