using AutoMapper;
using ContactManagement.Entity;
using ContactManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.UI.MappingProfiles
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact, ContactIndexViewModel>();
        }
    }
}
