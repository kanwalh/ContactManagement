using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagement.Services.Implementation;
using ContactManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.UI.Controllers
{
    public class ContactController : Controller
    {
        private ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            this._contactService = contactService;
        }

        public IActionResult Index()
        {
            var contacts = _contactService.GetAll().Select(contact =>
            new ContactIndexViewModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                MiddleName = contact.MiddleName,
                LastName = contact.LastName
            }).ToList();
            ;
            return View(contacts);
        }
    }
}