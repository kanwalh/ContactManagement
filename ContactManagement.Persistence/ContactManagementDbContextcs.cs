using ContactManagement.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Persistence
{
    public class ContactManagementDbContext : IdentityDbContext
    {
        public ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options)
            :base (options)
        { }

        #region "Properties - DB Entities"

        public DbSet<Contact> Contacts { get; set; }

        #endregion
    }
}
