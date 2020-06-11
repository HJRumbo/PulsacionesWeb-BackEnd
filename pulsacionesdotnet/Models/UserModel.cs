using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entity;

namespace pulsacionesdotnet.Models
{
    public class UserInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string MobilePhone { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class UserViewModel : UserInputModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(User user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Password = user.Password;
            MobilePhone = user.MobilePhone;
            Email = user.Email;
            Estado = user.Estado;
        }
        public string Estado { get; set; }
    }
}