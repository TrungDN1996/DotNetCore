using System.ComponentModel.DataAnnotations;

namespace Web_App.ViewModels
{
    public class UserCreateViewModel
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }
        
        public int RoleId { get; set; }
    }
}