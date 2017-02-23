using MyWebApplication.Areas.Security.Models;
using MyWebApplication.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApplication.Areas.Security.Controllers
{
    public class UsersController : Controller
    {

        public UserModelView GetUser(int id)
        {

            using (var db = new DatabaseContext())
            {

                return (from user in db.Users
                        where user.Id == id
                        select new UserModelView
                            {

                                Id = user.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Gender = user.Gender,
                                Age = user.Age,
                                EmploymentDate = user.EmploymentDate
                            


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
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Gender = user.Gender,
                                 Age = user.Age,
                                 EmploymentDate = user.EmploymentDate,
                                 Educations = user.Educations.Select(s => 
                                  new EducationViewModel 
                             { 
                                 School = s.School, 
                                 YearAttended = s.YearAttended
                             }).ToList()
                             }).ToList();
                return View(users);
            }


        }

        // GET: Security/User/Details/5
        public ActionResult Details(int id)
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
                    var sql = @"exec uspCreateUser @guid,
	                                @fname,
                                @lname,	                                @age,
	                                @gender,
	                                @empDate,
	                                @school,
	                                @yrAttended";

                    var result = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@guid", Guid.NewGuid()),
                        new SqlParameter("@fname", viewModel.FirstName),
                        new SqlParameter("@lname", viewModel.LastName),
                       new SqlParameter("@age", viewModel.Age),
                       new SqlParameter("@gender", viewModel.Gender),
                       new SqlParameter("@empDate", DateTime.UtcNow),
                        new SqlParameter("@school", "WMSU"),
                        new SqlParameter("@yrAttended", "2002"));

                    if (result > 1)
                        return RedirectToAction("Index");
                    else
                        return View();

                    //    db.Users.Add(new User
                    //    {
                    //       // Id = Guid.NewGuid(),
                    //       FirstName = viewModel.FirstName,
                    //       LastName = viewModel.LastName,
                    //       Age = viewModel.Age,
                    //      Gender = viewModel.Gender,
                    //      EmploymentDate = viewModel.EmploymentDate
                    //    });
                    //    db.SaveChanges();
                    //}



                   // return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }




        // GET: Security/User/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetUser(id));
        }

        // POST: Security/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserModelView modelView)
        {
            try
            {

                using (var db = new DatabaseContext())
                {

                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    user.FirstName = modelView.FirstName;
                    user.LastName = modelView.LastName;
                    user.Age = modelView.Age;
                    user.Gender = modelView.Gender;
                    user.EmploymentDate = modelView.EmploymentDate;
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
        public ActionResult Delete(int id)
        {

            return View(GetUser(id));
        }

        // POST: Security/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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