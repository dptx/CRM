using AutoMapper;
using CRM.DataAccess.Models;
using CRM.DataAccess.Repos;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class NoteController : Controller
    {
        private static IMapper mapper = AutoMapperConfig.Instance.Mapper;

        // GET: Note
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(NoteModel model)
        {
            var result = false;


            if (ModelState.IsValid)
            { 
                IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;
                model.AccountID = Convert.ToInt32(claims.Where(c => c.Type == "ID")
                       .Select(c => c.Value).SingleOrDefault());

                Note note = NoteRepo.Save(mapper.Map<Note>(model));

                if (note.Account != null)
                {
                    return Json(new { Message = "", Result = true, Author = note.Account.FirstName + " " + note.Account.LastName, Created = note.Created.ToString("G") });
                }
            }

            return Json(new { Message = "", Result = result });
        }
    }
}