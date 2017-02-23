﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.Dal
{
    public class DatabaseContext :DbContext 
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {


        }
        public DbSet<User> Users { get; set; }
             public DbSet<Education> Educations { get; set; }
    }
}
