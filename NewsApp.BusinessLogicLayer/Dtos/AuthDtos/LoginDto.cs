using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.ApplicationLayer.Dtos.AuthDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "user email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="user Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
