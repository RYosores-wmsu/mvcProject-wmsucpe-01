using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApplication.Areas.Security.Models
{
    public class EducationViewModel
    {
        public string School { get; set; }
        public string YearAttended { get; set; }

        public UserModelView User { get; set; }
    }
}