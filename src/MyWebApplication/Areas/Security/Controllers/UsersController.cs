using MyWebApplication.Areas.Security.Models;
using MyWebApplication.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApplication.Areas.Security.Controllers
{
    public class UsersController : Controller
    {

        private IList<UserModelView> Users
        {
            get
            {
                if (Session["data"] == null)
                {
                   Session["data"] = new List<UserModelView>(){
                   new UserModelView {
                Id = Guid.NewGuid(),
                Name = "Ralp Yosores",
                Hobby ="Dancing",
                Gender = "Male",
                Age = 21
                },
                 new UserModelView
                {
                Id = Guid.NewGuid(),  
                Name = "Ralppy",
                Hobby ="Singin",
                Gender = "Female",
                Age = 21
                }
            };
 
                       
               }
                return Session["data"] as List<UserModelView>;
            }
        }
        // GET: Security/User
        public ActionResult Index()
        {   
            using (var db= new DatabaseContext())
            {
                var users = (from user in db.Users
                             select new UserModelView
                             {

                                 Id = user.Id,
                                 Name = user.Name,
                                 Hobby = user.Hobby,
                                 Age = user.Age
                             }).ToList();
                return View(users);
            }
       
        
        }

        // GET: Security/User/Details/5
        public ActionResult Details(Guid id)
        {
            var ur = Users.FirstOrDefault(user => user.Id == id);
            return View(ur);
        }

        // GET: Security/User/Create
        public ActionResult Create()
        {
            ViewBag.Gender = new List<SelectListItem>{
                new SelectListItem
                {
                  Value= "Male",
                  Text ="Male"
                },
                   new SelectListItem
                {
                  Value= "Female",
                  Text ="Female"
                }
            };
            return View();
        }

        // POST: Security/User/Create
        [HttpPost]
        public ActionResult Create(UserModelView viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();
                     
                using (var db= new DatabaseContext())
                {
                    db.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Name = viewModel.Name,
                        Hobby = viewModel.Hobby,
                        Gender = viewModel.Gender,
                        Age = viewModel.Age

                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


          

        // GET: Security/User/Edit/5
        public ActionResult Edit(Guid id)
        {
            var edit = Users.FirstOrDefault(user => user.Id == id);
            return View(edit);
        }

        // POST: Security/User/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id,UserModelView viewModel)
        {
            try
            {
                var edit = Users.FirstOrDefault(user => user.Id == id);
                edit.Name = viewModel.Name;
                edit.Hobby = viewModel.Hobby;
                edit.Age = viewModel.Age;
                edit.Gender = viewModel.Gender;

               


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/User/Delete/5
        public ActionResult Delete(Guid id)
        {
            var usr = Users.FirstOrDefault(user => user.Id == id);
            return View(usr);
        }

        // POST: Security/User/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id,FormCollection collection)
        {
            try
            {
                var usr = Users.FirstOrDefault(user => user.Id == id);
                Users.Remove(usr);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
