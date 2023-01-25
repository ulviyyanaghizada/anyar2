using System.ComponentModel.DataAnnotations;

namespace Exam2.Models.ViewModels.User
{
    public class UserLoginVM
    {
        public string UserNameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set;}
    }
}
