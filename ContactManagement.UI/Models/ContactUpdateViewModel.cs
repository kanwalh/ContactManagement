using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.UI.Models
{
    public class ContactUpdateViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required."), StringLength(50,MinimumLength =3)]
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Z][a-zA-Z'/s-]{3,50}$")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 0), Display(Name = "Middle Name")]
        [RegularExpression("^[A-Z][a-zA-Z'/s-]*$")]
        public string MiddleName { get; set; }

        [StringLength(50, MinimumLength = 0), Display(Name = "Last Name")]
        [RegularExpression("^[A-Z][a-zA-Z'/s-]*$")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return (FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)(MiddleName[0]) + ".")).ToUpper()
                    + LastName);
            }
        }
    }
}
