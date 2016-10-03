using AutoMapper;
using CRM.DataAccess.Models;
using CRM.DataAccess.Repos;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private static IMapper mapper = AutoMapperConfig.Instance.Mapper;

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CustomerList()
        {
            var customers = mapper.Map<List<CustomerModel>>(CustomerRepo.GetCustomers());
            return PartialView("_CustomerList", customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save(CustomerModel model)
        {
            var result = 0;

            if(ModelState.IsValid)
            {
                result = CustomerRepo.Save(mapper.Map<Customer>(model));
            }

            return Json(new { Message = "", Result = result });
        }

        public ActionResult Notes(int id)
        {
            CustomerNotesViewModel model = new CustomerNotesViewModel();

            var notes = NoteRepo.GetNotesForCustomer(id);
            model.Notes = notes.Select(note => new NoteModel
                                {
                                    Body = note.Body,
                                    Created = note.Created,
                                    UserName = string.Format("{0} {1}", note.Account.FirstName, note.Account.LastName)
                                })
                .ToList();

            model.Customer = mapper.Map<CustomerModel>(CustomerRepo.Get(id));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(CustomerModel model)
        {
            var result = false;

            if (ModelState.IsValid)
            {
                result = CustomerRepo.Update(mapper.Map<Customer>(model));
            }

            return Json(new { Message = "", Result = result });
        }
    }
}