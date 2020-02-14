using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManagement.Entity;
using ContactManagement.Services.Implementation;
using ContactManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.UI.Controllers
{
    public class ContactController : Controller
    {
        private IMapper _mapper;
        private ContactService _contactService;

        public ContactController(ContactService contactService, IMapper mapper)
        {
            this._mapper = mapper;
            this._contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var contacts = _contactService.GetAll().Select(contact => _mapper.Map<ContactIndexViewModel>(contact)
            //new ContactIndexViewModel
            //{
            //    Id = contact.Id,
            //    FirstName = contact.FirstName,
            //    MiddleName = contact.MiddleName,
            //    LastName = contact.LastName
            //}
            ).ToList();

            return View(contacts);
        }

        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> Update(ContactUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = _mapper.Map<Contact>(model);
                //new Contact
                //{
                //    Id = model.Id,
                //    FirstName = model.FirstName,
                //    MiddleName = model.MiddleName,
                //    LastName = model.LastName
                //};

                await _contactService.UpdateAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Prevents cross-site Request Forgery Attacks
        public async Task<IActionResult> Create(ContactUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = _mapper.Map<Contact>(model);
                //new Contact
                //{
                //    Id = model.Id,
                //    FirstName = model.FirstName,
                //    MiddleName = model.MiddleName,
                //    LastName = model.LastName
                //};

                await _contactService.CreateAsync(contact);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}