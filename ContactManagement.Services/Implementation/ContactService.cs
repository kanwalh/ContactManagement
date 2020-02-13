using ContactManagement.Entity;
using ContactManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ContactManagement.Services.Implementation
{
    public class ContactService : IRepo<Contact>
    {
        private readonly ContactManagementDbContext _context;

        public ContactService(ContactManagementDbContext context)
        {
            _context = context;
        }

        public Contact Create(Contact entity)
        {
            _context.Contacts.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<Contact> CreateAsync(Contact entity)
        {
            await _context.Contacts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public void Delete(Contact entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            var contact = GetById(id);
            _context.Remove(contact);
            _context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var contact = GetById(id);
            _context.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Contact> GetAll() => _context.Contacts;

        public Contact GetById(int id) => _context.Contacts.Where(con => con.Id == id).SingleOrDefault();

        public Contact Update(Contact entity)
        {
            _context.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<Contact> UpdateAsync(Contact entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
