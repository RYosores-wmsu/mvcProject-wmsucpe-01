using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApplication.Areas.Security.Models
{
    public class UserModelView
    {
        public int Id { get; set; }
        [Required (ErrorMessage="This is a required field")]
        [MinLength(8, ErrorMessage="Minimum of 8 characters")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characters")]
     
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public IList<EducationViewModel> Educations { get; set; }
    }
}