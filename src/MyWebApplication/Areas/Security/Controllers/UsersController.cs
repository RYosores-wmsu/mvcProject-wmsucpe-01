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

        public UserModelView GetUser(Guid id)
        {

            using (var db = new DatabaseContext())
            {

                return (from user in db.Users
                        where user.Id == id
                        select new UserModelView
                            {

                                Id = user.Id,
                                Name = user.Name,
                                Hobby = user.Hobby,
                                Gender = user.Gender,
                                Age = user.Age


                            }).FirstOrDefault();

            }

        }
        // GET: Security/User
        public ActionResult Index()
        {
            using (var db = new DatabaseContext())
            {
                var users = (from user in db.Users
                             select new UserModelView
                             {

                                 Id = user.Id,
                                 Name = user.Name,
                                 Hobby = user.Hobby,
                                 Gender = user.Gender,
                                 Age = user.Age
                             }).ToList();
                return View(users);
            }


        }

        // GET: Security/User/Details/5
        public ActionResult Details(Guid id)
        {

            return View(GetUser(id));
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

                using (var db = new DatabaseContext())
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
            return View(GetUser(id));
        }

        // POST: Security/User/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, UserModelView modelView)
        {
            try
            {

                using (var db = new DatabaseContext())
                {

                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    user.Name = modelView.Name;
                    user.Hobby = modelView.Hobby;
                    user.Age = modelView.Age;
                    user.Gender = modelView.Gender;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/User/Delete/5
        public ActionResult Delete(Guid id)
        {

            return View(GetUser(id));
        }

        // POST: Security/User/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }


                }

            }

            catch
            {
                return View();
            }


        }
    }
}