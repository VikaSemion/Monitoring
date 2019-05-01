using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NLayerApp.DAL.Entities
{
    class Register
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Worker { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords did not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
