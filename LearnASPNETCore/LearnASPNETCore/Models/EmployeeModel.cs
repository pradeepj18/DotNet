using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnASPNETCore.Models
{
    public class EmployeeModel
    {
        public int Empid { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name is required.")]
        [DisplayName("Employee First Name")]
        public string EmpFname { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Employee Last Name")]
        public string EmpLname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Address is required.")]
        [DisplayName("Employee Email Address")]
        public string EmpEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Conatct No is required.")]
        [DisplayName("Employee Conatct No")]
        public string EmpContactno { get; set; }

    }
}
